using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Metrics
{
    public class ClickCounterCollection: DaoCollection<ClickCounterColumns, ClickCounter>
    { 
		public ClickCounterCollection(){}
		public ClickCounterCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ClickCounterCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ClickCounterCollection(Query<ClickCounterColumns, ClickCounter> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ClickCounterCollection(Database db, Query<ClickCounterColumns, ClickCounter> q, bool load) : base(db, q, load) { }
		public ClickCounterCollection(Query<ClickCounterColumns, ClickCounter> q, bool load) : base(q, load) { }
    }
}