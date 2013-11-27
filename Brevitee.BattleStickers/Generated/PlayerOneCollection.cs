using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class PlayerOneCollection: DaoCollection<PlayerOneColumns, PlayerOne>
    { 
		public PlayerOneCollection(){}
		public PlayerOneCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerOneCollection(Query<PlayerOneColumns, PlayerOne> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerOneCollection(Query<PlayerOneColumns, PlayerOne> q, bool load) : base(q, load) { }
    }
}