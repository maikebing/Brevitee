using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerizerStickerizeeCollection: DaoCollection<StickerizerStickerizeeColumns, StickerizerStickerizee>
    { 
		public StickerizerStickerizeeCollection(){}
		public StickerizerStickerizeeCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public StickerizerStickerizeeCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public StickerizerStickerizeeCollection(Query<StickerizerStickerizeeColumns, StickerizerStickerizee> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public StickerizerStickerizeeCollection(Database db, Query<StickerizerStickerizeeColumns, StickerizerStickerizee> q, bool load) : base(db, q, load) { }
		public StickerizerStickerizeeCollection(Query<StickerizerStickerizeeColumns, StickerizerStickerizee> q, bool load) : base(q, load) { }
    }
}