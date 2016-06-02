using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.UI.Desktop
{
    class Coin
    {
        public long Amount { get; set; }

        public int Count { get; set; }

        public Coin(long amount, int count)
        {
            Amount = amount;
            Count = count;
        }
    }
}
