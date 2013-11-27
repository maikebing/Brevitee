using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Brevitee.Data;
using Brevitee.Incubation;
using System.Data.SqlClient;

namespace Brevitee.Data
{
    public abstract class ParameterBuilder: IParameterBuilder
    {
        public abstract DbParameter BuildParameter(IParameterInfo c);

        public DbParameter[] BuildParamters(InComparison c)
        {
            DbParameter[] results = new DbParameter[c.Parameters.Length];

            for (int i = 0; i < c.Parameters.Length; i++)
            {
                results[i] = BuildParameter(c.Parameters[i]);
            }

            return results;
        }

        #region IParameterBuilder<T> Members

        public DbParameter[] GetParameters(IHasFilters filter)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            foreach (IFilterToken token in filter.Filters)
            {
                IParameterInfo c = token as IParameterInfo;
                if (c != null)
                {
                    InComparison inC = c as InComparison;
                    if (inC != null)
                    {
                        parameters.AddRange(BuildParamters(inC));
                    }
                    else
                    {
                        parameters.Add(this.BuildParameter(c));
                    }
                }
            }

            return parameters.ToArray();
        }

        #endregion
    }
}
