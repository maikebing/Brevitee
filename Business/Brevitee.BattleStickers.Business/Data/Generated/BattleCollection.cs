using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class BattleCollection: DaoCollection<BattleColumns, Battle>
    { 
		public BattleCollection(){}
		public BattleCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public BattleCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public BattleCollection(Query<BattleColumns, Battle> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public BattleCollection(Database db, Query<BattleColumns, Battle> q, bool load) : base(db, q, load) { }
		public BattleCollection(Query<BattleColumns, Battle> q, bool load) : base(q, load) { }
    }
}