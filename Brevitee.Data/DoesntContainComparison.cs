using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public class DoesntContainComparison : Comparison
    {
        public DoesntContainComparison(string columnName, object value, int? num = null)
            : base(columnName, "NOT LIKE", "%{0}%"._Format(value), num)
        { }
    }
}
