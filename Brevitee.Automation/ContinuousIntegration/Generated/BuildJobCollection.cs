using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Automation.ContinuousIntegration.Data
{
    public class BuildJobCollection: DaoCollection<BuildJobColumns, BuildJob>
    { 
		public BuildJobCollection(){}
		public BuildJobCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public BuildJobCollection(Query<BuildJobColumns, BuildJob> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public BuildJobCollection(Query<BuildJobColumns, BuildJob> q, bool load) : base(q, load) { }
    }
}