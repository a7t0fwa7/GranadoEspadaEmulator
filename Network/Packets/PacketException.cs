namespace GranadoEspadaHeadless.Network.Packets
{
    public sealed class PacketException : System.Exception
    {
        public PacketException(string message)
            : base(message)
        {
        }
    }
}
