using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class BattleCollection: DaoCollection<BattleColumns, Battle>
    { 
		public BattleCollection(){}
		public BattleCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public BattleCollection(Query<BattleColumns, Battle> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public BattleCollection(Query<BattleColumns, Battle> q, bool load) : base(q, load) { }
    }
}