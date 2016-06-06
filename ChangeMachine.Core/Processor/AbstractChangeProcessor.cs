using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.Processor
{
    internal abstract class AbstractChangeProcessor
    {
        public abstract string ChangeType { get; }

        protected abstract long[] ValuableCollection { get; }

        public long HighestValue
        {
            get { return ValuableCollection.Max(); }
        }

        public AbstractChangeProcessor()
        {

        }

        public bool IsWithinRange(long changeAmount)
        {
            return (changeAmount >= ValuableCollection.Min());
        }

        public virtual long EvaluateChangeOperation(List<long> outputCollection, long changeAmountInCents)
        {
            if (changeAmountInCents == 0)
            {
                return changeAmountInCents;
            }

            foreach (long coin in ValuableCollection.OrderByDescending(coin => coin))
            {
                long coinCount = changeAmountInCents / coin;
                for (int i = 0; i < coinCount; i++)
                {
                    outputCollection.Add(coin);
                }
                changeAmountInCents = changeAmountInCents % coin;
            }
            return changeAmountInCents;
        }
    }
}
