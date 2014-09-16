using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Metrics
{
    public class MethodTimerCollection: DaoCollection<MethodTimerColumns, MethodTimer>
    { 
		public MethodTimerCollection(){}
		public MethodTimerCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public MethodTimerCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public MethodTimerCollection(Query<MethodTimerColumns, MethodTimer> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public MethodTimerCollection(Database db, Query<MethodTimerColumns, MethodTimer> q, bool load) : base(db, q, load) { }
		public MethodTimerCollection(Query<MethodTimerColumns, MethodTimer> q, bool load) : base(q, load) { }
    }
}