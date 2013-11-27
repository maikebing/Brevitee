using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public class NullComparison: Comparison
    {
        public NullComparison(string columnName, string oper)
            : base(columnName, oper, null)
        { }

        public override string ToString()
        {
            return string.Format("{0} {1} NULL", ColumnName, this.Operator);
        }
    }
}
