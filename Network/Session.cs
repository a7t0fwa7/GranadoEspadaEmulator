using System;
using System.Net.Sockets;
using GranadoEspadaHeadless.Network.Packets;
using GranadoEspadaHeadless.Context;

namespace GranadoEspadaHeadless.Network
{
    public sealed class Session : IDisposable
    {
        public const int HeaderLen = 2;
        public const short ReceiveSize = 2048 * 10;

        public int packetCount = 0; //+1 for each packet sent

        private readonly Socket m_socket;

        private bool m_connected;
        private bool m_userClose;

        private byte[] m_recvBuffer;
        private byte[] m_packetBuffer;

        private object m_sendLock;

        public event EventHandler<bool> OnDisconnected;

        public LoginContext m_LoginContext;
        public GameContext m_GameContext;

        public bool Connected
        {
            get
            {
                return m_connected;
            }
        }

        public Session(Socket socket)
        {
            m_socket = socket;

            m_connected = true;
            m_userClose = false;

            m_packetBuffer = new byte[ReceiveSize];
            m_recvBuffer = new byte[ReceiveSize];

            m_sendLock = new object();
        }

        public void Receive()
        {
            while (m_connected)
            {
                var error = SocketError.Success;

                int length = 0;

                try
                {
                    length = m_socket.Receive(m_recvBuffer);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Receive error: " + e.Message);
                    break;
                }
               
                if (length > 0)
                {
                    if (length == 0 || error != SocketError.Success)
                    {
                        Dispose();
                    }

                    PacketReader packet = new PacketReader(m_recvBuffer, length);
            
                    short opcode = packet.ReadShort();

                    if(m_LoginContext != null)
                    {
                        m_LoginContext.Client.RecvPacketQueue.Enqueue(new PacketReader(m_recvBuffer, length));

                        switch (opcode)
                        {
                            case Opcodes.Inbound.LoginResponse:
                                LoginServerHandler.HandleLoginResponse(this.m_LoginContext, packet);
                                break;

                            case Opcodes.Inbound.PacketFailed:
                                LoginServerHandler.HandleFailedPacket(this.m_LoginContext, packet);
                                break;

                            case Opcodes.Inbound.CharacterList:
                                LoginServerHandler.HandleCharacterList(this.m_LoginContext, packet);
                                break;

                            case Opcodes.Inbound.CharacterList2:
                                LoginServerHandler.HandleCharacterList2(this.m_LoginContext, packet);
                                break;

                            case Opcodes.Inbound.ServerIP:
                                LoginServerHandler.HandleServerIP(this.m_LoginContext, packet);
                                break;

                            case Opcodes.Inbound.StringInfo:
                                LoginServerHandler.HandleClientData(this.m_LoginContext, packet);
                                break;

                            default:
                                LoginServerHandler.HandleUnknown(this.m_LoginContext, packet);
                                break;

                        };
                    }
                    else if(m_GameContext != null)
                    {
                        if(opcode != Opcodes.Inbound.JunkData)
                            m_GameContext.Client.RecvPacketQueue.Enqueue(new PacketReader(m_recvBuffer, length));

                        switch (opcode)
                        {
                            case Opcodes.Inbound.ForceDisconnected:
                                Console.WriteLine("Server forcefully disconnected client due to some wrong packet (Check header/checksum).");
                                m_GameContext.Disconnect();
                                this.Dispose();
                                break;

                            case Opcodes.Inbound.ZoneCharactersData:
                                GameServerHandler.HandleCharacterData(this.m_GameContext, packet);
                                break;

                            case Opcodes.Inbound.PlayerChat:
                                GameServerHandler.HandleChat(this.m_GameContext, packet);
                                break;

                            case Opcodes.Inbound.Whisper:
                                GameServerHandler.HandleWhisper(this.m_GameContext, packet);
                                break;

                            case Opcodes.Inbound.AdminWhisper:
                                GameServerHandler.HandleAdminWhisper(this.m_GameContext, packet);
                                break;

                            case Opcodes.Inbound.Movement:
                                GameServerHandler.HandlePlayerMovement(this.m_GameContext, packet);
                                break;

                            case Opcodes.Inbound.BroadcastMessage:
                                GameServerHandler.HandleBroadcastMessage(this.m_GameContext, packet);
                                break;

                            default:
                                GameServerHandler.HandleUnknown(this.m_GameContext, packet);
                                break;

                        };
                    }
                    
                    for (int i = 0; i < length; i++)
                    {
                        m_recvBuffer[i] = 0;
                    }
                }         
            }
        }

        public bool SendPacket(PacketWriter packet)
        {
            if (m_connected)
            {
                byte[] data = packet.ToArray_Raw();
                byte[] outData = new byte[data.Length + 8];

                if(m_LoginContext != null)
                {
                    m_LoginContext.Client.SendPacketQueue.Enqueue(new PacketReader(data, data.Length));
                }
                else if(m_GameContext != null)
                {
                    m_GameContext.Client.SendPacketQueue.Enqueue(new PacketReader(data, data.Length));
                }

                ushort checksum = Crypto.MakeChecksum(data, data.Length);

                //for (int i = 0; i < data.Length; i++)
                //{
                //    Console.Write("{0:X} ", data[i]);
                //}

                int encodedLength = Crypto.Encrypt(data, outData, data.Length);

                byte[] final = new byte[encodedLength + 2];

                final[0] = (byte)((final.Length - 2) & 0xFF);
                final[1] = (byte)(((final.Length - 2) >> 8) & 0xFF);

                Buffer.BlockCopy(outData, 0, final, 2, encodedLength);

                lock (m_sendLock)
                {
                    int offset = 0;

                    while (offset < final.Length)
                    {
                        SocketError errorCode = SocketError.Success;
                        
                        int sent = m_socket.Send(final, offset, final.Length, SocketFlags.None, out errorCode);

                        Console.WriteLine("Sending {0} bytes.., sent {1} bytes", final.Length, sent);

                        if (sent == 0 || errorCode != SocketError.Success)
                        {
                            Dispose();
                            return false;
                        }

                        offset += sent;
                    }
                }

                packet.Dispose();
            }
            else
            {
                return false;
            }

            return true;
        }

        public void Disconnect()
        {
            m_userClose = true;
            Dispose();
        }
        public void Dispose()
        {
            if (m_connected)
            {
                m_connected = false;

                try
                {
                    m_socket.Shutdown(SocketShutdown.Both);
                    m_socket.Close();
                }
                finally
                {
                    if (OnDisconnected != null)
                        OnDisconnected(this, m_userClose);
                }
            }
        } 
    }
}
