﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.Processor
{
    internal sealed class BillChangeProcessor : AbstractChangeProcessor
    {
        protected override long[] ValuableCollection
        {
            get
            {
                return new long[] { 10000, 5000, 2000, 1000, 500, 200 };
            }
        }

        public override string ChangeType
        {
            get { return "Bill"; }
        }

        public BillChangeProcessor()
            : base()
        {
        }
    }
}
