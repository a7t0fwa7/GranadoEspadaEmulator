using GranadoEspadaHeadless.Context;
using System;
using System.Threading;

namespace GranadoEspadaHeadless.Network.Packets
{
    public static class GameServerHandler
    {
        public static void HandleUnknown(GameContext context, PacketReader packet)
        {
            //Console.WriteLine("[ZONE] Got some unknown packet.. length: {0}", packet.Length);

            //for (int i = 0; i < packet.Length; i++)
            //{
            //    Console.Write("{0:X} ", packet.ToArray()[i]);
            //}
        }

        public static void threadWorker(GameContext context) //test function for sending pcakets ingame
        {
            Thread.Sleep(5000);

            var p = new PacketWriter();
            p.WriteFullPacket(context.Client.PacketAfterLoginBytes);
 
            if(p.ToArray().Length > 0)
            {
                context.Send(p);                          
            }
        }

        public static void HandleCharacterData(GameContext context, PacketReader packet)
        {
            Console.WriteLine("Found some character data... entering game");
            Thread.Sleep(1000);
            context.Send(Factory.EnterGameServer2(0xD8, 0xE91B));
            Thread.Sleep(200);
            context.Send(Factory.EnterGameServer3(0x7A));
            Thread.Sleep(200);
            context.Send(Factory.EnterGameServer4(0x79));
            Thread.Sleep(200);
            context.Send(Factory.EnterGameServer5()); //2e
            Thread.Sleep(200);
            context.Send(Factory.EnterGameServer6(1));
            Thread.Sleep(200);
            context.Send(Factory.EnterGameServer6(2));
            Thread.Sleep(200);
            context.Send(Factory.EnterGameServer8(0x71));
            Thread.Sleep(200);
            context.Send(Factory.EnterGameServer7(0x83));
            Thread.Sleep(200);
            context.Send(Factory.EnterGameServer8(0x7F));
            Thread.Sleep(500);
            context.Send(Factory.EnterGameServer9(0x50)); //all required to get placed into town/field
            Thread.Sleep(200);

            //you can send your own packets here, character should be loaded fine..
            //avoid using Thread.Sleep in this class as much as you can as it will block the next recv() packets!
            //for example, if you call send() then Sleep, the recv packet wont be picked up. make a thread somewhere else for sending packets and looping.
            //Thread n = new Thread(new ParameterizedThreadStart(context, threadWorkerSpeak));
            Thread.Sleep(500);
            context.Client.ReadyInGame = true;

            context.Client.AcceptedAccountIndex = context.Client.AccountIndex;

            if (context.Client.SendingPacketAfterLogin)
            {
                Console.WriteLine("Creating new thread: send data after login");
                Thread myNewThread = new Thread(() => threadWorker(context));
                myNewThread.Start();
            }
        }

        public static void HandleZonePlayers(GameContext context, PacketReader packet)
        {

        }

        public static void HandleChat(GameContext context, PacketReader packet)
        {
            short unk1 = packet.ReadShort();
            uint playerId = packet.ReadUInt();
            packet.Skip(6);
            byte chatType = packet.ReadByte();
            string msg = packet.ReadNullTerminatedString();

            Console.WriteLine("Player #{0} says: {1}", playerId, msg);

            //context.Client.m_DB.InsertWorldChat(Convert.ToString(playerId), msg);
        }

        public static void HandleSquadChat(GameContext context, PacketReader packet)
        {
            short unk1 = packet.ReadShort();
            short chatType = packet.ReadShort();
            
            packet.Skip(11);

            string fullMsg = packet.ReadNullTerminatedString();

            string[] splitMsg = fullMsg.Split(':');

            for(int i = 0; i < splitMsg.Length; i++)
            {
                splitMsg[i] = splitMsg[i].Trim();
            }

            for (int i = 0; i < packet.Length; i++)
            {
                Console.Write("{0:X} ", packet.ToArray()[i]);
            }

            Console.WriteLine("Got some message: " + fullMsg);      
        }

        public static void HandlePlayerMovement(GameContext context, PacketReader packet)
        {
            uint playerID = packet.ReadUInt();
            uint unk1 = packet.ReadUInt();
            int x = packet.ReadInt();
            int y = packet.ReadInt();
            int z = packet.ReadInt();
            float rotation = packet.ReadFloat();
            Console.WriteLine("Player #{0} movement: {1},{2},{3}, rotation: {4}", playerID, x, y, z, rotation);
        }

        public static void HandleWhisper(GameContext context, PacketReader packet)
        {
            short dataLen = packet.ReadShort();

            byte bUnk1 = packet.ReadByte();
            int bUnk2 = packet.ReadInt();

            if(packet.Length > 12)
            {
                string characterName = packet.ReadNullTerminatedString();

                packet.Skip(16 - characterName.Length);

                string content = packet.ReadNullTerminatedString();

                if(characterName.Length > 0 && content.Length > 0)
                {
                    Console.WriteLine("Got WHISPER message: " + characterName + " : " + content);
                    
                    context.Client.m_DB.InsertWhisper(characterName, content);

                    //context.Send(Factory.Speak("\"" + characterName + " " + "Hello! This is an automation bot run by alsch092!"));
                    //Console.WriteLine("Said hello back!");
                }
            }            
        }

        public static void HandleAdminWhisper(GameContext context, PacketReader packet)
        {
            short dataLen = packet.ReadShort();
            int unk1 = packet.ReadInt();

            if (packet.Length > 12)
            {
                string characterName = packet.ReadNullTerminatedString();
                packet.Skip(11);
                string content = packet.ReadNullTerminatedString();

                if (characterName.Length > 0 && content.Length > 0)
                {
                    Console.WriteLine("Got ADMIN WHISPER message: " + characterName + " : " + content);

                    context.Client.m_DB.InsertWhisper(characterName, content);
                }
            }
        }

        public static void HandleBroadcastMessage(GameContext context, PacketReader packet) //megaphones and squad chat
        {
            short dataLen = packet.ReadShort();
            short chatType = packet.ReadShort();

            if(chatType == 8)
            {
                packet.Skip(11);

                string fullMsg = packet.ReadNullTerminatedString();
                string message;
                string characterName;

                string[] splitMsg = fullMsg.Split('-');

                for (int i = 0; i < splitMsg.Length; i++) //Remove excess spaces at back and front
                {
                    splitMsg[i] = splitMsg[i].Trim();
                }

                if (splitMsg.Length > 0)
                {
                    message = splitMsg[0];
                    characterName = splitMsg[1];

                    if (message.Length > 0 && characterName.Length > 0)
                    {                    
                        if (characterName[0] == '[')
                        {
                            characterName = characterName.Remove(0, 1); //remove FIRST character to get rid of [

                            if (characterName[characterName.Length - 1] == ']')
                                characterName = characterName.Remove(characterName.Length - 1, 1);
                        }
                 
                        if (chatType == 8) //Message: from WORLD
                        {
                            Console.WriteLine("Got WORLD message: " + characterName + " : " + message);
                            if(message.Length < 1000)
                                context.Client.m_DB.InsertWorldChat(characterName, message);
                        }
                        else if (chatType == 9) // Message: from Squad
                        {
                            Console.WriteLine("Got SQUAD message: " + fullMsg);
                        }
                    }
                } 
            }  
            else if(chatType == 7)
            {

            }
        } 

    }
}
