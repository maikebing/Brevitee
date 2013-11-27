using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class UserIdentifierCollection: DaoCollection<UserIdentifierColumns, UserIdentifier>
    { 
		public UserIdentifierCollection(){}
		public UserIdentifierCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserIdentifierCollection(Query<UserIdentifierColumns, UserIdentifier> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserIdentifierCollection(Query<UserIdentifierColumns, UserIdentifier> q, bool load) : base(q, load) { }
    }
}