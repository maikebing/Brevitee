using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Logging
{
    public class ParamCollection: DaoCollection<ParamColumns, Param>
    { 
		public ParamCollection(){}
		public ParamCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ParamCollection(Query<ParamColumns, Param> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ParamCollection(Query<ParamColumns, Param> q, bool load) : base(q, load) { }
    }
}