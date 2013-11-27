using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc.Html;

namespace Brevitee.Analytics.Data
{
    public partial class Style
    {
        public Style(string name, string value)
            : this()
        {
            this.Name = name;
            this.Value = value;
        }
        
        public override int GetHashCode()
        {
            return string.Format("{0}.{1}", Name, Value).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Style o = obj as Style;
            if (o != null)
            {
                return o.Name.Equals(this.Name) && o.Value.Equals(this.Value);
            }
            else
            {
                return base.Equals(obj);
            }
        }
    }
}
