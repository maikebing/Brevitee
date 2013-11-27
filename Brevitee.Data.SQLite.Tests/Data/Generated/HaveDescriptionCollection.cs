using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class HaveDescriptionCollection: DaoCollection<HaveDescriptionColumns, HaveDescription>
    { 
		public HaveDescriptionCollection(){}
		public HaveDescriptionCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HaveDescriptionCollection(Query<HaveDescriptionColumns, HaveDescription> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HaveDescriptionCollection(Query<HaveDescriptionColumns, HaveDescription> q, bool load) : base(q, load) { }
    }
}