﻿using ChangeMachine.Core.DataContract;
using ChangeMachine.Core.Processor;
using ChangeMachine.Core.Logger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChangeMachine.Core
{
    public class ChangeMachineManager
    {
        public EvaluateChangeResponse EvaluateChange(EvaluateChangeRequest changeRequest)
        {
            LogManager.Info(changeRequest);

            long totalChangeAmountInCents = 0;
            List<Change> changeCollection = new List<Change>();
            EvaluateChangeResponse response = new EvaluateChangeResponse();

            if (changeRequest.IsValid == false)
            {
                response.OperationReport.AddRange(changeRequest.ErrorList);
                LogManager.Error("Invalid request received.", response);
                return response;
            }

            try
            {
                long changeAmountInCents = changeRequest.InputAmount - changeRequest.PriceAmount;

                totalChangeAmountInCents = changeAmountInCents;

                while (changeAmountInCents > 0)
                {
                    AbstractChangeProcessor changeProcessor = ChangeProcessorFactory.Create(changeAmountInCents);
                    if(changeProcessor == null)
                    {
                        LogManager.Error(string.Format("Unable to process operation: Could not find appropriate Change Processor for value {0}.", changeAmountInCents));
                        response.OperationReport.Add(new Report("", "Unable to process operation.", ReportType.ERROR));
                        return response;
                    }

                    List<long> currentChangeCollection = new List<long>();
                    changeAmountInCents = changeProcessor.EvaluateChangeOperation(currentChangeCollection, changeAmountInCents);
                    if (currentChangeCollection.Any())
                    {
                        Change change = new Change(changeProcessor.ChangeType, currentChangeCollection);
                        changeCollection.Add(change);
                    }
                }

                // Sanity check
                if(changeAmountInCents > 0)
                {
                    LogManager.Error(string.Format("Unable to generate change. After all processing remaining change is {0}.", changeAmountInCents));
                    response.OperationReport.Add(new Report("", string.Format("Unable to generate change. Missing value: {0}", changeAmountInCents), ReportType.ERROR));
                }
            }
            catch (Exception ex)
            {
                LogManager.Error("Error processing request.", ex);
                response.OperationReport.Add(new Report("", string.Format("Error processing request: {0}", ex.Message), ReportType.ERROR));
            }

            if(response.IsSuccess)
            {
                response.TotalAmountInCents = totalChangeAmountInCents;
                response.ChangeCollection = changeCollection;
                LogManager.Info("Evaluation succeeded", response);
            }
            else
            {
                LogManager.Error("Evaluation failed", response);
            }


            return response;
        }    
        
        
    }
}
