using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class TagCollection: DaoCollection<TagColumns, Tag>
    { 
		public TagCollection(){}
		public TagCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TagCollection(Query<TagColumns, Tag> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TagCollection(Query<TagColumns, Tag> q, bool load) : base(q, load) { }
    }
}