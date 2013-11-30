using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerCollection: DaoCollection<PlayerColumns, Player>
    { 
		public PlayerCollection(){}
		public PlayerCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerCollection(Query<PlayerColumns, Player> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerCollection(Query<PlayerColumns, Player> q, bool load) : base(q, load) { }
    }
}