using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Logging
{
    public class EventParamCollection: DaoCollection<EventParamColumns, EventParam>
    { 
		public EventParamCollection(){}
		public EventParamCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public EventParamCollection(Query<EventParamColumns, EventParam> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public EventParamCollection(Query<EventParamColumns, EventParam> q, bool load) : base(q, load) { }
    }
}