using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.Processor
{
    internal sealed class BalinhaChangeProcessor : AbstractChangeProcessor
    {

        public BalinhaChangeProcessor()
            : base ()
        {

        }

        public override string ChangeType
        {
            get { return "Balinha"; }
        }

        protected override long[] ValuableCollection
        {
            get { return new long[] {3, 1}; }
        }
    }
}
