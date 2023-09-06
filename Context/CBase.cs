using System.Net.Sockets;
using GranadoEspadaHeadless.Network.Packets;
using GranadoEspadaHeadless.Network;
using System.Threading;

namespace GranadoEspadaHeadless.Context
{
    public abstract class CBase
    {
      public Client Client { get; private set; }

        private Session m_session;

        public CBase(Client client, Session session, bool receiving)
        {
            Client = client;

            m_session = session;
            m_session.OnDisconnected += (s, e) => HandleDisconnected(e);

            HandleConnected();

            Thread trd = new Thread(new ThreadStart(m_session.Receive)); //thread for recving packets
            trd.IsBackground = true;
            trd.Start();
            System.Console.WriteLine("Started receiving thread...");
        }

        public abstract void HandleConnected();
        public abstract void HandleDisconnected(bool byChoice);

        public void Send(PacketWriter packet)
        {
            if (m_session != null)
                m_session.SendPacket(packet);
        }

        public void Disconnect()
        {
            m_session.Disconnect();
        } 
    }
}
