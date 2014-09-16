using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Metrics
{
    public class MethodCounterCollection: DaoCollection<MethodCounterColumns, MethodCounter>
    { 
		public MethodCounterCollection(){}
		public MethodCounterCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public MethodCounterCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public MethodCounterCollection(Query<MethodCounterColumns, MethodCounter> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public MethodCounterCollection(Database db, Query<MethodCounterColumns, MethodCounter> q, bool load) : base(db, q, load) { }
		public MethodCounterCollection(Query<MethodCounterColumns, MethodCounter> q, bool load) : base(q, load) { }
    }
}