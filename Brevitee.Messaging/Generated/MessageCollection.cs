using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Messaging.Data
{
    public class MessageCollection: DaoCollection<MessageColumns, Message>
    { 
		public MessageCollection(){}
		public MessageCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public MessageCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public MessageCollection(Query<MessageColumns, Message> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public MessageCollection(Database db, Query<MessageColumns, Message> q, bool load) : base(db, q, load) { }
		public MessageCollection(Query<MessageColumns, Message> q, bool load) : base(q, load) { }
    }
}