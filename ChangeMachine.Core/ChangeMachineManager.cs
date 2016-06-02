using ChangeMachine.Core.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChangeMachine.Core
{
    public class ChangeMachineManager
    {
        private long[] AvailableCoinCollection = { 100, 50, 25, 5, 1, 10 };

        public EvaluateChangeResponse EvaluateChange(EvaluateChangeRequest changeRequest)
        {
            EvaluateChangeResponse change = new EvaluateChangeResponse();
            List<long> coinCollection = new List<long>();

            if (change.HasError)
            {
                return change;
            }

            try
            {
                long changeAmountInCents = changeRequest.InputAmount - changeRequest.PriceAmount;

                change.TotalAmountInCents = changeAmountInCents;
                change.CoinCollection = coinCollection;

                foreach (long coin in AvailableCoinCollection.OrderByDescending(coin => coin))
                {
                    long coinCount = changeAmountInCents / coin;
                    for (int i = 0; i < coinCount; i++)
                    {
                        coinCollection.Add(coin);
                    }
                    changeAmountInCents = changeAmountInCents % coin;
                }
            }
            catch (Exception ex)
            {
                //change.ErrorList.Add(ex);
            }

            return change;
        }

       


        /*
        public List<long> EvaluateChange(long inputAmount, long priceAmount)
        {
            return EvaluateChange(inputAmount, priceAmount).CoinCollection;
        }
        */
    }
}
