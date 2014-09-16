using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class TagCollection: DaoCollection<TagColumns, Tag>
    { 
		public TagCollection(){}
		public TagCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TagCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TagCollection(Query<TagColumns, Tag> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TagCollection(Database db, Query<TagColumns, Tag> q, bool load) : base(db, q, load) { }
		public TagCollection(Query<TagColumns, Tag> q, bool load) : base(q, load) { }
    }
}