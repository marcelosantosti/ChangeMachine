using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.Processor
{
    internal sealed class CoinChangeProcessor : AbstractChangeProcessor
    {
        protected override long[] ValuableCollection
        {
            get
            {
                return new long[] { 100, 50, 25, 5, 10 };
            }
        }

        public override string ChangeType
        {
            get { return "Coin"; }
        }

        public CoinChangeProcessor()
            : base()
        {
        }
    }
}
