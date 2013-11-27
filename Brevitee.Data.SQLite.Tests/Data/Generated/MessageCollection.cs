using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class MessageCollection: DaoCollection<MessageColumns, Message>
    { 
		public MessageCollection(){}
		public MessageCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public MessageCollection(Query<MessageColumns, Message> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public MessageCollection(Query<MessageColumns, Message> q, bool load) : base(q, load) { }
    }
}