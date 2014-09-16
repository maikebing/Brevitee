using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerizableListStickerizableCollection: DaoCollection<StickerizableListStickerizableColumns, StickerizableListStickerizable>
    { 
		public StickerizableListStickerizableCollection(){}
		public StickerizableListStickerizableCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public StickerizableListStickerizableCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public StickerizableListStickerizableCollection(Query<StickerizableListStickerizableColumns, StickerizableListStickerizable> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public StickerizableListStickerizableCollection(Database db, Query<StickerizableListStickerizableColumns, StickerizableListStickerizable> q, bool load) : base(db, q, load) { }
		public StickerizableListStickerizableCollection(Query<StickerizableListStickerizableColumns, StickerizableListStickerizable> q, bool load) : base(q, load) { }
    }
}