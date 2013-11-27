using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public class StartsWithComparison: Comparison
    {
        public StartsWithComparison(string columnName, object value, int? number = null)
            : base(columnName, "LIKE", "{0}%"._Format(value), number)
        { }
    }
}
