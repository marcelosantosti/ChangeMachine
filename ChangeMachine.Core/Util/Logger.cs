using ChangeMachine.Core.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.Util
{
    internal class Logger
    {
        private Logger logger;

        public Logger Instance
        {
            get
            {
                if(logger == null)
                {
                    logger = new Logger();
                }
                return logger;
            }
        }

        private void Log(string message, string method)
        {

        }

        public void Info(string method, EvaluateChangeResponse response)
        {
            string message = string.Format("Response: IsSuccess:{0}, TotalAmountInCents:{1}",
                response.IsSuccess, response.TotalAmountInCents);

            if(response.OperationReport.Any())
            {
                string opReport = "";
                foreach(Report report in response.OperationReport)
                {
                    opReport += string.Format("{0}/t/t{1}:{2}", Environment.NewLine, report.Type, report.Message);
                }
            }

            if (response.ChangeCollection.Any())
            {
                List<Coin> queryGroupCoins = response.ChangeCollection.GroupBy(coin => coin)
                    .Select(coin => new Coin(coin.Key, coin.Count()))
                    .OrderByDescending(coin => coin.Amount).ToList();

                string changeCollection = "";
                foreach (Change change in response.ChangeCollection)
                {
                    changeCollection += string.Format("{0}/t/t{1}:{2}", Environment.NewLine, change.TypeName,
                        change.ChangeCollection);
                }
            }
        }

        public void Info(string method, AbstractRequest request)
        {

        }

        public void Error(string message, Exception exception = null)
        {

        }
    }
}
