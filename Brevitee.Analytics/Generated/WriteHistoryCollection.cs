using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class WriteHistoryCollection: DaoCollection<WriteHistoryColumns, WriteHistory>
    { 
		public WriteHistoryCollection(){}
		public WriteHistoryCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public WriteHistoryCollection(Query<WriteHistoryColumns, WriteHistory> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public WriteHistoryCollection(Query<WriteHistoryColumns, WriteHistory> q, bool load) : base(q, load) { }
    }
}