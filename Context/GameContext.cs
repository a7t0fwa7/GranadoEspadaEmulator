using GranadoEspadaHeadless.Game;
using GranadoEspadaHeadless.Network;
using GranadoEspadaHeadless.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace GranadoEspadaHeadless.Context
{
    public sealed class GameContext : CBase
    {
        public GameContext(Client client, Session session)
            : base(client, session, true)
        {
            Console.WriteLine("Created GameContext.");
        }

        public override void HandleConnected()
        {
            Console.WriteLine("Connected to game server with account {0}, sending handshake..", this.Client.account.Username);

            if (Client.AcceptedAccountIndex != 0)
            {
                Send(Factory.EnterGameServer(0x1AD4, Client.account.AccountId, Client.account.Username, 0xB38B, Client.account.SelectedTeamHash));
            }
            else
            {
                Send(Factory.EnterGameServer(0x1AD4, Client.account.AccountId, Client.account.Username, 0xB38B, Client.account.SelectedTeamHash));
            }
        }

        public override void HandleDisconnected(bool byChoice)
        {
            Factory.packetCount = 0;

            Client.ReadyInGame = false;

            if (!byChoice)
            {
                Console.WriteLine("Remote server disconnected the client. Please reconnect.");
            }
            else
            {
                Console.WriteLine("Disconnected from game server.");
            }

            if (Client.AutoReconnecting)
            {
                Thread.Sleep(5000);
                Client.Migrate(Client.LoginServerIP, Client.LoginPort, true);
            }
        }
    }
}
