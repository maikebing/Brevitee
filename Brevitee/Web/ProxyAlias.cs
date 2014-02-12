using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Web
{
    public class ProxyAlias
    {
        public ProxyAlias() { }

        public ProxyAlias(string alias, Type typeToAlias)
        {
            this.Alias = alias;
            this.ClassName = typeToAlias.Name;
        }

        public string Alias { get; set; }
        public string ClassName { get; set; }
    }
}
