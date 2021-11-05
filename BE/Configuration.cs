using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        public int expireRequest{ get; set; }
        public double fee = 0.01;//{ get; set; }
        public static long guestRequestKeySeq = 10000000;
        public static long hostingUnitKeySeq = 10000000;
        public static long hostKeySeq = 10000000;
        public static long orderKeySeq = 10000000;
        public static int commission = 10;
    }
}
