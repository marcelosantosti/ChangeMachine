using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.DataContract
{
    public abstract class AbstractResponse
    {
        public List<Report> OperationReport { get; set; }

        public Boolean IsSuccess
        {
            get
            {
                return OperationReport.Select(x => x.Type == ReportType.ERROR).Any() == false;
            }
        }

        public AbstractResponse()
        {
            this.OperationReport = new List<Report>();
        }
    }
}
