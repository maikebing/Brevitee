using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerOneSkillCollection: DaoCollection<PlayerOneSkillColumns, PlayerOneSkill>
    { 
		public PlayerOneSkillCollection(){}
		public PlayerOneSkillCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerOneSkillCollection(Query<PlayerOneSkillColumns, PlayerOneSkill> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerOneSkillCollection(Query<PlayerOneSkillColumns, PlayerOneSkill> q, bool load) : base(q, load) { }
    }
}