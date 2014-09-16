using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerOneSkillCollection: DaoCollection<PlayerOneSkillColumns, PlayerOneSkill>
    { 
		public PlayerOneSkillCollection(){}
		public PlayerOneSkillCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PlayerOneSkillCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerOneSkillCollection(Query<PlayerOneSkillColumns, PlayerOneSkill> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerOneSkillCollection(Database db, Query<PlayerOneSkillColumns, PlayerOneSkill> q, bool load) : base(db, q, load) { }
		public PlayerOneSkillCollection(Query<PlayerOneSkillColumns, PlayerOneSkill> q, bool load) : base(q, load) { }
    }
}