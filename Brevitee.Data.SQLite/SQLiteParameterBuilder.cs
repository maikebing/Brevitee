using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data;
using System.Data.Common;
using Brevitee.Incubation;
using System.Data.SQLite;

namespace Brevitee.Data
{
    public class SQLiteParameterBuilder: ParameterBuilder
    {
        public override DbParameter BuildParameter(IParameterInfo c)
        {
            string parameterName = string.Format("@{0}{1}", c.ColumnName, c.Number);
            SQLiteParameter result = new SQLiteParameter(parameterName, c.Value);
            
            return result;
        }

    }
}
