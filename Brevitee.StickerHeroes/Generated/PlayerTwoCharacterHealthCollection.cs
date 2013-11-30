using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerTwoCharacterHealthCollection: DaoCollection<PlayerTwoCharacterHealthColumns, PlayerTwoCharacterHealth>
    { 
		public PlayerTwoCharacterHealthCollection(){}
		public PlayerTwoCharacterHealthCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerTwoCharacterHealthCollection(Query<PlayerTwoCharacterHealthColumns, PlayerTwoCharacterHealth> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerTwoCharacterHealthCollection(Query<PlayerTwoCharacterHealthColumns, PlayerTwoCharacterHealth> q, bool load) : base(q, load) { }
    }
}