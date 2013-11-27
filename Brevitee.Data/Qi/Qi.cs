using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data;
using System.Data;
using System.Data.Common;

namespace Brevitee.Data.Qi
{
    public class Qi
    {      

        public static DataTable Where(QiQuery query)
        {
            Database db = _.Db.For(query.cxName);
            SqlStringBuilder sql = new SqlStringBuilder();
            sql
                .Select(query.table, query.columns)
                .Where(query);

            IParameterBuilder parameterBuilder = db.ServiceProvider.Get<IParameterBuilder>();
            DbParameter[] parameters = parameterBuilder.GetParameters(sql);
            return db.GetDataTableFromSql(sql, System.Data.CommandType.Text, parameters);
        }
    }
}
