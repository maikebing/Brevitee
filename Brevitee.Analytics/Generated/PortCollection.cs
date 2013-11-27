using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class PortCollection: DaoCollection<PortColumns, Port>
    { 
		public PortCollection(){}
		public PortCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PortCollection(Query<PortColumns, Port> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PortCollection(Query<PortColumns, Port> q, bool load) : base(q, load) { }
    }
}