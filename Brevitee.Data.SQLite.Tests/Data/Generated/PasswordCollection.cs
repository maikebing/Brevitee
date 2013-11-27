using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class PasswordCollection: DaoCollection<PasswordColumns, Password>
    { 
		public PasswordCollection(){}
		public PasswordCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PasswordCollection(Query<PasswordColumns, Password> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PasswordCollection(Query<PasswordColumns, Password> q, bool load) : base(q, load) { }
    }
}