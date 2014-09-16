using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerizeeCollection: DaoCollection<StickerizeeColumns, Stickerizee>
    { 
		public StickerizeeCollection(){}
		public StickerizeeCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public StickerizeeCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public StickerizeeCollection(Query<StickerizeeColumns, Stickerizee> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public StickerizeeCollection(Database db, Query<StickerizeeColumns, Stickerizee> q, bool load) : base(db, q, load) { }
		public StickerizeeCollection(Query<StickerizeeColumns, Stickerizee> q, bool load) : base(q, load) { }
    }
}