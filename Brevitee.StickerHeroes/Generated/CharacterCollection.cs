using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class CharacterCollection: DaoCollection<CharacterColumns, Character>
    { 
		public CharacterCollection(){}
		public CharacterCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CharacterCollection(Query<CharacterColumns, Character> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CharacterCollection(Query<CharacterColumns, Character> q, bool load) : base(q, load) { }
    }
}