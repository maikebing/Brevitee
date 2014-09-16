using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerizerCollection: DaoCollection<StickerizerColumns, Stickerizer>
    { 
		public StickerizerCollection(){}
		public StickerizerCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public StickerizerCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public StickerizerCollection(Query<StickerizerColumns, Stickerizer> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public StickerizerCollection(Database db, Query<StickerizerColumns, Stickerizer> q, bool load) : base(db, q, load) { }
		public StickerizerCollection(Query<StickerizerColumns, Stickerizer> q, bool load) : base(q, load) { }
    }
}