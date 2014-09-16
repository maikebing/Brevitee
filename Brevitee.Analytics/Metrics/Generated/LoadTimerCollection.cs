using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Metrics
{
    public class LoadTimerCollection: DaoCollection<LoadTimerColumns, LoadTimer>
    { 
		public LoadTimerCollection(){}
		public LoadTimerCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LoadTimerCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LoadTimerCollection(Query<LoadTimerColumns, LoadTimer> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LoadTimerCollection(Database db, Query<LoadTimerColumns, LoadTimer> q, bool load) : base(db, q, load) { }
		public LoadTimerCollection(Query<LoadTimerColumns, LoadTimer> q, bool load) : base(q, load) { }
    }
}