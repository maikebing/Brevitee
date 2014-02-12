using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Logging
{
    public class UserNameCollection: DaoCollection<UserNameColumns, UserName>
    { 
		public UserNameCollection(){}
		public UserNameCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserNameCollection(Query<UserNameColumns, UserName> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserNameCollection(Query<UserNameColumns, UserName> q, bool load) : base(q, load) { }
    }
}