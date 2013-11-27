using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class PlayerOneCharacterCollection: DaoCollection<PlayerOneCharacterColumns, PlayerOneCharacter>
    { 
		public PlayerOneCharacterCollection(){}
		public PlayerOneCharacterCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerOneCharacterCollection(Query<PlayerOneCharacterColumns, PlayerOneCharacter> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerOneCharacterCollection(Query<PlayerOneCharacterColumns, PlayerOneCharacter> q, bool load) : base(q, load) { }
    }
}