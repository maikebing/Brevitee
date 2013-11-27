using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Schema.Org
{
    public class Text: DataType
    {
        public Text()
        {
            this.Name = "Text";
        }

        public Text(string value)
        {
            this.Value = value;
        }

        public string Value
        {
            get;
            set;
        }
    }
}
