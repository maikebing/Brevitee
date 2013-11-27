using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Users.Data
{
    public class SessionStateCollection: DaoCollection<SessionStateColumns, SessionState>
    { 
		public SessionStateCollection(){}
		public SessionStateCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SessionStateCollection(Query<SessionStateColumns, SessionState> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SessionStateCollection(Query<SessionStateColumns, SessionState> q, bool load) : base(q, load) { }
    }
}