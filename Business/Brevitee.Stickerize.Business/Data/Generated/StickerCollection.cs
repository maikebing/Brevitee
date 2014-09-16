using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerCollection: DaoCollection<StickerColumns, Sticker>
    { 
		public StickerCollection(){}
		public StickerCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public StickerCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public StickerCollection(Query<StickerColumns, Sticker> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public StickerCollection(Database db, Query<StickerColumns, Sticker> q, bool load) : base(db, q, load) { }
		public StickerCollection(Query<StickerColumns, Sticker> q, bool load) : base(q, load) { }
    }
}