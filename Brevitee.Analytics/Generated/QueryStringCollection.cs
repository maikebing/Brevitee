using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class QueryStringCollection: DaoCollection<QueryStringColumns, QueryString>
    { 
		public QueryStringCollection(){}
		public QueryStringCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public QueryStringCollection(Query<QueryStringColumns, QueryString> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public QueryStringCollection(Query<QueryStringColumns, QueryString> q, bool load) : base(q, load) { }
    }
}