using GranadoEspadaHeadless.Context;
using GranadoEspadaHeadless.Game;
using GranadoEspadaHeadless.Network.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GranadoEspadaHeadless.Network
{
    public sealed class Client
    {
        public Network.Database.Database m_DB { get; set;}
        
        private CBase m_context;

        public Socket m_socket;
        public Session m_session;

        public string LoginServerIP;
        public int LoginPort;

        public string GameServerIP;
        public int GamePort;

        public string ConnectedIp;
        public int Port;

        public int AccountIndex = 4;
        public int AcceptedAccountIndex = 0;

        public Account account { get; set; }

        public Servers.Regions Region { get; set; }

        public Queue<Packets.PacketReader> SendPacketQueue { get; set; }
        public Queue<Packets.PacketReader> RecvPacketQueue { get; set; }

        public bool ReadyInGame {  get;  set; }

        public bool AutoReconnecting { get; set; }

        public bool SendingPacketAfterLogin { get; set; }
        public byte[] PacketAfterLoginBytes { get; set; }

        public bool Migrate(string ip, int port, bool isLoginServer)
        {
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
       
            try
            {
                m_socket.Connect(ip, port);
                m_session = new Session(m_socket);
                Port = port;

                ConnectedIp = ip;
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not connect to server: " + e.Message);
                return false;
            }

            if (m_context != null)
                m_context.Disconnect();

            if (isLoginServer)
            {
                m_context = new LoginContext(this, m_session);
                m_session.m_LoginContext = (LoginContext)m_context;
                m_session.m_GameContext = null;
            }
            else
            {
                m_context = new GameContext(this, m_session);
                m_session.m_GameContext = (GameContext)m_context;
                m_session.m_LoginContext = null;
            }

            return true;
        }
    
    }
}
