using ChangeMachine.Core.DataContract;
using ChangeMachine.Core.Processor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChangeMachine.Core
{
    public class ChangeMachineManager
    {
        public EvaluateChangeResponse EvaluateChange(EvaluateChangeRequest changeRequest)
        {
            EvaluateChangeResponse response = new EvaluateChangeResponse();

            if (changeRequest.IsValid == false)
            {
                response.OperationReport.AddRange(changeRequest.ErrorList);
                return response;
            }

            try
            {
                long changeAmountInCents = changeRequest.InputAmount - changeRequest.PriceAmount;

                response.TotalAmountInCents = changeAmountInCents;

                while (changeAmountInCents > 0)
                {
                    AbstractChangeProcessor changeProcessor = ChangeProcessorFactory.Create(changeAmountInCents);

                    List<long> changeCollection = new List<long>();
                    changeAmountInCents = changeProcessor.EvaluateChangeOperation(changeCollection, changeAmountInCents);
                    if (changeCollection.Any())
                    {
                        Change change = new Change(changeProcessor.ChangeType, changeCollection);
                        response.ChangeCollection.Add(change);
                    }
                }

                // Sanity check
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
        
        
    }
}
