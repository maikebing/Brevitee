using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.ServiceProxy.Secure
{
    public class SecureSessionCollection: DaoCollection<SecureSessionColumns, SecureSession>
    { 
		public SecureSessionCollection(){}
		public SecureSessionCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public SecureSessionCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SecureSessionCollection(Query<SecureSessionColumns, SecureSession> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SecureSessionCollection(Database db, Query<SecureSessionColumns, SecureSession> q, bool load) : base(db, q, load) { }
		public SecureSessionCollection(Query<SecureSessionColumns, SecureSession> q, bool load) : base(q, load) { }
    }
}