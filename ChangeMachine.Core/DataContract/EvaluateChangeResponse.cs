using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.DataContract
{
    public class EvaluateChangeResponse
    {
        public long TotalAmountInCents { get; set; }

        public List<long> CoinCollection { get; set; }

        public Boolean HasError
        {
            get
            {
                return ErrorList.Count > 0;
            }
            
        }

        public List<Error> ErrorList { get; set; }

        public EvaluateChangeResponse()
        {
            this.ErrorList = new List<Error>();
        }
    }
}
