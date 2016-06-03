using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.Processor
{
    abstract class AbstractChangeProcessor
    {
        protected long EvaluateChangeOperation(List<long> coinCollection, long changeAmountInCents, long[] availableCoinCollection)
        {
            if (changeAmountInCents == 0)
            {
                return changeAmountInCents;
            }

            foreach (long coin in availableCoinCollection.OrderByDescending(coin => coin))
            {
                long coinCount = changeAmountInCents / coin;
                for (int i = 0; i < coinCount; i++)
                {
                    coinCollection.Add(coin);
                }
                changeAmountInCents = changeAmountInCents % coin;
            }
            return changeAmountInCents;
        }
    }
}
