using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GranadoEspadaHeadless.Context;

namespace GranadoEspadaHeadless.Network.Packets
{
    public static class LoginServerHandler
    {
        public static void HandleFailedPacket(LoginContext context, PacketReader packet)
        {
            Console.WriteLine("Server returned: invalid packet: Please try re-connecting!");
            context.Disconnect();
        }

        public static void HandleUnknown(LoginContext context, PacketReader packet)
        {
            int len = packet.Length;

            Console.WriteLine("Parsing unknown packet:");

            //for(int i = 0; i < len; i++)
            //{
            //    Console.Write("{0:X} ", packet.ToArray()[i]);
            //}

            //Console.WriteLine();
        }

        public static void HandleLoginResponse(LoginContext context,PacketReader packet)
        {
            uint AccountId = packet.ReadUInt();

            if(AccountId > 0)
            {
                context.Client.account.AccountId = AccountId;
                Console.WriteLine("Got Account ID: {0}", AccountId);
            }

            Console.WriteLine("Got Login Response...");
            Thread.Sleep(1000);
            context.Send(Factory.AfterLogin());
            Thread.Sleep(1000);
            context.Send(Factory.RequestCharacterList());
        }

        public static void HandleClientData(LoginContext context, PacketReader packet)
        {
            Console.WriteLine("Got some client data...");
        }

        public static void HandleCharacterList(LoginContext context, PacketReader packet)
        {
            Console.WriteLine("Got character list!");
            packet.Skip(11);

            byte charNumber_1 = packet.ReadByte();
            string charType_1 = packet.ReadNullTerminatedString();

            Console.WriteLine("Found a {0} at slot {1}!", charType_1, charNumber_1);

            packet.Skip(335);
            byte charNumber_2 = packet.ReadByte();
            string charType_2 = packet.ReadNullTerminatedString();

            Console.WriteLine("Found a {0} at slot {1}!", charType_2, charNumber_2);

            packet.Skip(334);
            byte charNumber_3 = packet.ReadByte();
            string charType_3 = packet.ReadNullTerminatedString();

            Console.WriteLine("Found a {0} at slot {1}!", charType_3, charNumber_3);
        }

        public static void HandleCharacterList2(LoginContext context, PacketReader packet)
        {
            packet.Skip(88);
            byte bUnk1 = packet.ReadByte();

            context.Send(Factory.MigrateLastPoint(context.Client.account.TeamId));
        }

        public static void HandleServerIP(LoginContext context, PacketReader packet)
        {
            Console.WriteLine();   

            packet.Skip(4);
            string ip = packet.ReadNullTerminatedString();

            packet.Skip(2);
 
            ushort port = packet.ReadUShort();

            string mapName = packet.ReadNullTerminatedString();

            Console.WriteLine("Got server IP + port ({0}, {1}, {2}), ready to migrate to game.", ip, port, mapName);

            context.Client.ConnectedIp = ip;

            context.Client.GameServerIP = ip;
            context.Client.GamePort = port;

            context.Client.Migrate(ip, port, false);
        }
    }
}
