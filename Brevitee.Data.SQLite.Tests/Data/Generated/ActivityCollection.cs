using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class ActivityCollection: DaoCollection<ActivityColumns, Activity>
    { 
		public ActivityCollection(){}
		public ActivityCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ActivityCollection(Query<ActivityColumns, Activity> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ActivityCollection(Query<ActivityColumns, Activity> q, bool load) : base(q, load) { }
    }
}