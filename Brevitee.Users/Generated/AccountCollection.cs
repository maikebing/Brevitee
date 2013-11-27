using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Users.Data
{
    public class AccountCollection: DaoCollection<AccountColumns, Account>
    { 
		public AccountCollection(){}
		public AccountCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AccountCollection(Query<AccountColumns, Account> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AccountCollection(Query<AccountColumns, Account> q, bool load) : base(q, load) { }
    }
}