using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Messaging.Data
{
    public class DirectMessageCollection: DaoCollection<DirectMessageColumns, DirectMessage>
    { 
		public DirectMessageCollection(){}
		public DirectMessageCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DirectMessageCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DirectMessageCollection(Query<DirectMessageColumns, DirectMessage> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DirectMessageCollection(Database db, Query<DirectMessageColumns, DirectMessage> q, bool load) : base(db, q, load) { }
		public DirectMessageCollection(Query<DirectMessageColumns, DirectMessage> q, bool load) : base(q, load) { }
    }
}