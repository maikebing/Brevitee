using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Metrics
{
    public class LoginCounterCollection: DaoCollection<LoginCounterColumns, LoginCounter>
    { 
		public LoginCounterCollection(){}
		public LoginCounterCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LoginCounterCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LoginCounterCollection(Query<LoginCounterColumns, LoginCounter> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LoginCounterCollection(Database db, Query<LoginCounterColumns, LoginCounter> q, bool load) : base(db, q, load) { }
		public LoginCounterCollection(Query<LoginCounterColumns, LoginCounter> q, bool load) : base(q, load) { }
    }
}