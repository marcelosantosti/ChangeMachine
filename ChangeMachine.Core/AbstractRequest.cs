using ChangeMachine.Core.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core
{
    public abstract class AbstractRequest
    {
        public List<Error> ErrorList { get; private set; }

        public bool HasError
        {
            get
            {
                return ErrorList.Any();
            }
        }

        public AbstractRequest()
        {
            ErrorList = new List<Error>();
        }

        public abstract void ValidateRequest();
    }
}
