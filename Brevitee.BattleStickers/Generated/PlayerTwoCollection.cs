using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class PlayerTwoCollection: DaoCollection<PlayerTwoColumns, PlayerTwo>
    { 
		public PlayerTwoCollection(){}
		public PlayerTwoCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerTwoCollection(Query<PlayerTwoColumns, PlayerTwo> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerTwoCollection(Query<PlayerTwoColumns, PlayerTwo> q, bool load) : base(q, load) { }
    }
}