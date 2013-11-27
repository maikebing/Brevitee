using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Users.Data
{
    public class PasswordFailureCollection: DaoCollection<PasswordFailureColumns, PasswordFailure>
    { 
		public PasswordFailureCollection(){}
		public PasswordFailureCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PasswordFailureCollection(Query<PasswordFailureColumns, PasswordFailure> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PasswordFailureCollection(Query<PasswordFailureColumns, PasswordFailure> q, bool load) : base(q, load) { }
    }
}