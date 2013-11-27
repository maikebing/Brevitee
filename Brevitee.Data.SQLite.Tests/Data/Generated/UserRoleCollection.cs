using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class UserRoleCollection: DaoCollection<UserRoleColumns, UserRole>
    { 
		public UserRoleCollection(){}
		public UserRoleCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserRoleCollection(Query<UserRoleColumns, UserRole> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserRoleCollection(Query<UserRoleColumns, UserRole> q, bool load) : base(q, load) { }
    }
}