using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace GranadoEspadaHeadless.Network.Packets
{
    public class Crypto
    {
        [DllImport("Crypt.dll", EntryPoint = "Encrypt", CallingConvention = CallingConvention.Cdecl)] //we're using either assembler or C for our encryption routines, and are importing it from a DLL.
        public static extern int Encrypt(byte[] inData, byte[] outData, int length);

        [DllImport("Crypt.dll", EntryPoint = "MakeChecksum", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort MakeChecksum(byte[] outData, int length);
    }
}
