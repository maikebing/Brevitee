using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class UrlTagCollection: DaoCollection<UrlTagColumns, UrlTag>
    { 
		public UrlTagCollection(){}
		public UrlTagCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UrlTagCollection(Query<UrlTagColumns, UrlTag> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UrlTagCollection(Query<UrlTagColumns, UrlTag> q, bool load) : base(q, load) { }
    }
}