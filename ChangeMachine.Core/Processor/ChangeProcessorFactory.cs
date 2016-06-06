using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.Processor
{
    internal static class ChangeProcessorFactory
    {
        /// <summary>
        /// List all change processor used by this ChangeMachine
        /// </summary>
        private static AbstractChangeProcessor[] ChangeProcessorCollection = 
        { 
            new BillChangeProcessor(), 
            new GoldChangeProcessor(), 
            new CoinChangeProcessor(),
            new BalinhaChangeProcessor()
        };

        public static AbstractChangeProcessor Create(long remainingChange)
        {
            return ChangeProcessorCollection.OrderByDescending(p => p.HighestValue)
                        .Where(p => p.IsWithinRange(remainingChange)).First();
        }
    }
}
