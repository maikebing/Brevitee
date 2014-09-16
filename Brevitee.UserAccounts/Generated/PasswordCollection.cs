using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.UserAccounts.Data
{
    public class PasswordCollection: DaoCollection<PasswordColumns, Password>
    { 
		public PasswordCollection(){}
		public PasswordCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PasswordCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PasswordCollection(Query<PasswordColumns, Password> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PasswordCollection(Database db, Query<PasswordColumns, Password> q, bool load) : base(db, q, load) { }
		public PasswordCollection(Query<PasswordColumns, Password> q, bool load) : base(q, load) { }
    }
}