using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class PlayerTwoCharacterCollection: DaoCollection<PlayerTwoCharacterColumns, PlayerTwoCharacter>
    { 
		public PlayerTwoCharacterCollection(){}
		public PlayerTwoCharacterCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerTwoCharacterCollection(Query<PlayerTwoCharacterColumns, PlayerTwoCharacter> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerTwoCharacterCollection(Query<PlayerTwoCharacterColumns, PlayerTwoCharacter> q, bool load) : base(q, load) { }
    }
}