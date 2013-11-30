using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerCharacterCollection: DaoCollection<PlayerCharacterColumns, PlayerCharacter>
    { 
		public PlayerCharacterCollection(){}
		public PlayerCharacterCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerCharacterCollection(Query<PlayerCharacterColumns, PlayerCharacter> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerCharacterCollection(Query<PlayerCharacterColumns, PlayerCharacter> q, bool load) : base(q, load) { }
    }
}