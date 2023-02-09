using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulatedMarket.Humans
{
    class Buyer : Human
    {
        public string need { get; set; }
        
        public bool principle(int inflation)
        {
            if (inflation >= 10)
            {
                return false;

            }
            else
            {
                return true;
            }
        }

    }
}
