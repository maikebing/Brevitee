using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.UserAccounts.Data
{
    public class RoleCollection: DaoCollection<RoleColumns, Role>
    { 
		public RoleCollection(){}
		public RoleCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public RoleCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RoleCollection(Query<RoleColumns, Role> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RoleCollection(Database db, Query<RoleColumns, Role> q, bool load) : base(db, q, load) { }
		public RoleCollection(Query<RoleColumns, Role> q, bool load) : base(q, load) { }
    }
}