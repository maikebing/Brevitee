using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.ServiceProxy.Secure
{
    public class ApiKeyCollection: DaoCollection<ApiKeyColumns, ApiKey>
    { 
		public ApiKeyCollection(){}
		public ApiKeyCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ApiKeyCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ApiKeyCollection(Query<ApiKeyColumns, ApiKey> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ApiKeyCollection(Database db, Query<ApiKeyColumns, ApiKey> q, bool load) : base(db, q, load) { }
		public ApiKeyCollection(Query<ApiKeyColumns, ApiKey> q, bool load) : base(q, load) { }
    }
}