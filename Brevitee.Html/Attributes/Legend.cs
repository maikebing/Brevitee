using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Html
{
    public class Legend: Attribute
    {
        public Legend()
        {
        }

        public string Value { get; set; }
    }
}
