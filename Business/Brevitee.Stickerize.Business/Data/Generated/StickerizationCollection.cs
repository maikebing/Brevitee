using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerizationCollection: DaoCollection<StickerizationColumns, Stickerization>
    { 
		public StickerizationCollection(){}
		public StickerizationCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public StickerizationCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public StickerizationCollection(Query<StickerizationColumns, Stickerization> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public StickerizationCollection(Database db, Query<StickerizationColumns, Stickerization> q, bool load) : base(db, q, load) { }
		public StickerizationCollection(Query<StickerizationColumns, Stickerization> q, bool load) : base(q, load) { }
    }
}