using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLFactory
    {
        public static IBL instance;

        protected BLFactory() { instance = null; }
        public static IBL getBL()
        {
            if (null == instance)
                instance = new MyBL();
            return instance;
        }
    }
}
