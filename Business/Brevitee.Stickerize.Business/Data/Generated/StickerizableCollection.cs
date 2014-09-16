using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerizableCollection: DaoCollection<StickerizableColumns, Stickerizable>
    { 
		public StickerizableCollection(){}
		public StickerizableCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public StickerizableCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public StickerizableCollection(Query<StickerizableColumns, Stickerizable> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public StickerizableCollection(Database db, Query<StickerizableColumns, Stickerizable> q, bool load) : base(db, q, load) { }
		public StickerizableCollection(Query<StickerizableColumns, Stickerizable> q, bool load) : base(q, load) { }
    }
}