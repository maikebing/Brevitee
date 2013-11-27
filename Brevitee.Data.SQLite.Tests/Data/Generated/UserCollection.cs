using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class UserCollection: DaoCollection<UserColumns, User>
    { 
		public UserCollection(){}
		public UserCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserCollection(Query<UserColumns, User> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserCollection(Query<UserColumns, User> q, bool load) : base(q, load) { }
    }
}