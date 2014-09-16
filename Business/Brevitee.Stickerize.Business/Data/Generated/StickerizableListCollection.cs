using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerizableListCollection: DaoCollection<StickerizableListColumns, StickerizableList>
    { 
		public StickerizableListCollection(){}
		public StickerizableListCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public StickerizableListCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public StickerizableListCollection(Query<StickerizableListColumns, StickerizableList> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public StickerizableListCollection(Database db, Query<StickerizableListColumns, StickerizableList> q, bool load) : base(db, q, load) { }
		public StickerizableListCollection(Query<StickerizableListColumns, StickerizableList> q, bool load) : base(q, load) { }
    }
}