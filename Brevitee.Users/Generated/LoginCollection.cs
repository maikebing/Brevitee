using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Users.Data
{
    public class LoginCollection: DaoCollection<LoginColumns, Login>
    { 
		public LoginCollection(){}
		public LoginCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LoginCollection(Query<LoginColumns, Login> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LoginCollection(Query<LoginColumns, Login> q, bool load) : base(q, load) { }
    }
}