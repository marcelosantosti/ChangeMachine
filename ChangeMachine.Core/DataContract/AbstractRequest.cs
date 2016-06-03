using ChangeMachine.Core.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.DataContract
{
    public abstract class AbstractRequest
    {
        internal List<Report> ErrorList { get; private set; }

        internal bool IsValid
        {
            get
            {
                ErrorList.Clear();
                ValidateRequest();
                return ErrorList.Any() == false;
            }
        }

        public AbstractRequest()
        {
            ErrorList = new List<Report>();
        }

        internal abstract void ValidateRequest();
    }
}
