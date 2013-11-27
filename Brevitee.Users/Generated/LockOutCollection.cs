using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Users.Data
{
	public class LockOutCollection: DaoCollection<LockOutColumns, LockOut>
	{ 
		public LockOutCollection(){}
		public LockOutCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LockOutCollection(Query<LockOutColumns, LockOut> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LockOutCollection(Query<LockOutColumns, LockOut> q, bool load) : base(q, load) { }
	}
}