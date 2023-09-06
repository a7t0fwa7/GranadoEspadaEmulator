using System;
using System.Threading;
using GranadoEspadaHeadless.Network;
using GranadoEspadaHeadless.Network.Packets;


namespace GranadoEspadaHeadless.Context
{
    public sealed class LoginContext : CBase
    {
        public LoginContext(Client client, Session session)
            : base(client, session, true)
        {    
        }

        public override void HandleConnected()
        {
            Console.WriteLine("Connected to login!!");

            Send(Factory.Login(Client.account.Username));
        }

        public override void HandleDisconnected(bool byChoice)
        {
            Factory.packetCount = 0;

            if (!byChoice)
            {
                Console.WriteLine("Disconnected from login by the remote host.");

                if (Client.AutoReconnecting)
                {
                    Thread.Sleep(5000);
                    Client.Migrate(Client.LoginServerIP, Client.LoginPort, true);
                }
            }
            else
            {
                Console.WriteLine("Client was disconnected, please re-connect!");
            }
        }
    }
}
