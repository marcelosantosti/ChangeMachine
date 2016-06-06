using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMachine.Core.DataContract
{
    public sealed class Change
    {
        public string TypeName { get; set; }

        public List<long> ChangeCollection { get; set; }

        public Change()
        {
            ChangeCollection = new List<long>();
        }

        public Change(string typeName, List<long> changeCollection)
        {
            this.TypeName = typeName;
            this.ChangeCollection = changeCollection;
        }
    }
}
