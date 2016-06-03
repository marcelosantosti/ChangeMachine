using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.DataContract
{
    public sealed class EvaluateChangeResponse : AbstractResponse
    {
        public long TotalAmountInCents { get; set; }

        public List<long> BillCollection { get; set; }
        public List<long> CoinCollection { get; set; }

        public EvaluateChangeResponse() : base()
        {

        }
    }
}
