using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class ProtocolCollection: DaoCollection<ProtocolColumns, Protocol>
    { 
		public ProtocolCollection(){}
		public ProtocolCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ProtocolCollection(Query<ProtocolColumns, Protocol> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ProtocolCollection(Query<ProtocolColumns, Protocol> q, bool load) : base(q, load) { }
    }
}