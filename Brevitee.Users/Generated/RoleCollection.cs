using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Users.Data
{
    public class RoleCollection: DaoCollection<RoleColumns, Role>
    { 
		public RoleCollection(){}
		public RoleCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RoleCollection(Query<RoleColumns, Role> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RoleCollection(Query<RoleColumns, Role> q, bool load) : base(q, load) { }
    }
}