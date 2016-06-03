using ChangeMachine.Core.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChangeMachine.Core
{
    public class ChangeMachineManager
    {
        private long[] AvailableBillCollection = { 10000, 5000, 2000, 1000, 500, 200 };
        private long[] AvailableCoinCollection = { 100, 50, 25, 5, 1, 10 };

        public EvaluateChangeResponse EvaluateChange(EvaluateChangeRequest changeRequest)
        {
            EvaluateChangeResponse response = new EvaluateChangeResponse();
            List<long> coinCollection = new List<long>();
            List<long> billCollection = new List<long>();

            if (changeRequest.IsValid == false)
            {
                response.OperationReport.AddRange(changeRequest.ErrorList);
                return response;
            }

            try
            {
                long changeAmountInCents = changeRequest.InputAmount - changeRequest.PriceAmount;

                response.TotalAmountInCents = changeAmountInCents;

                response.BillCollection = billCollection;
                changeAmountInCents = EvaluateChangeOperation(billCollection, changeAmountInCents, AvailableBillCollection);

                response.CoinCollection = coinCollection;
                changeAmountInCents = EvaluateChangeOperation(coinCollection, changeAmountInCents, AvailableCoinCollection);

                if(changeAmountInCents > 0)
                {
                    response.OperationReport.Add(new Report("", string.Format("Unable to generate change. Missing value: {0}", changeAmountInCents), ReportType.ERROR));
                }
            }
            catch (Exception ex)
            {
                response.OperationReport.Add(new Report("", string.Format("Error processing request: {0}", ex.Message), ReportType.ERROR));
            }

            return response;
        }

        private long EvaluateChangeOperation(List<long> coinCollection, long changeAmountInCents, long[] availableCoinCollection)
        {
            if(changeAmountInCents == 0)
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

       


        /*
        public List<long> EvaluateChange(long inputAmount, long priceAmount)
        {
            return EvaluateChange(inputAmount, priceAmount).CoinCollection;
        }
        */
    }
}
