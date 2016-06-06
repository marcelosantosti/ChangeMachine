using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.Processor
{
    internal sealed class GoldChangeProcessor : AbstractChangeProcessor
    {
        protected override long[] ValuableCollection
        {
            get
            {
                return new long[]{ 100000, 50000, 20000 };
            }
        }

        public override string ChangeType
        {
            get { return "Gold"; }
        }

        public GoldChangeProcessor()
            : base()
        {
        }
    }
}
