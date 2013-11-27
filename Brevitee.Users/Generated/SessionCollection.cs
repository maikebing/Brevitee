using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Users.Data
{
    public class SessionCollection: DaoCollection<SessionColumns, Session>
    { 
		public SessionCollection(){}
		public SessionCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SessionCollection(Query<SessionColumns, Session> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SessionCollection(Query<SessionColumns, Session> q, bool load) : base(q, load) { }
    }
}