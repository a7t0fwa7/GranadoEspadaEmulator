using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranadoEspadaHeadless.Game
{
    public static class Servers
    {
        public enum Regions
        {
            STEAM
        }

        public struct STEAM
        {
            public static string Server0_Ip = "00.111.222.333";  //login IP
            public static int Port0 = 7001;

            public static string Server1_Ip = "01.111.222.333"; //Game IP
            public static int Port1 = 7001;
        }  
    }
}
