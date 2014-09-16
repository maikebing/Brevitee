using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.UserAccounts.Data
{
    public class UserBehaviorCollection: DaoCollection<UserBehaviorColumns, UserBehavior>
    { 
		public UserBehaviorCollection(){}
		public UserBehaviorCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UserBehaviorCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserBehaviorCollection(Query<UserBehaviorColumns, UserBehavior> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserBehaviorCollection(Database db, Query<UserBehaviorColumns, UserBehavior> q, bool load) : base(db, q, load) { }
		public UserBehaviorCollection(Query<UserBehaviorColumns, UserBehavior> q, bool load) : base(q, load) { }
    }
}