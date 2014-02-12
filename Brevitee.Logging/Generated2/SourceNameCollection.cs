using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Logging
{
    public class SourceNameCollection: DaoCollection<SourceNameColumns, SourceName>
    { 
		public SourceNameCollection(){}
		public SourceNameCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SourceNameCollection(Query<SourceNameColumns, SourceName> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SourceNameCollection(Query<SourceNameColumns, SourceName> q, bool load) : base(q, load) { }
    }
}