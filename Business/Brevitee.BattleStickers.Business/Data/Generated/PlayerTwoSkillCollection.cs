using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerTwoSkillCollection: DaoCollection<PlayerTwoSkillColumns, PlayerTwoSkill>
    { 
		public PlayerTwoSkillCollection(){}
		public PlayerTwoSkillCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PlayerTwoSkillCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerTwoSkillCollection(Query<PlayerTwoSkillColumns, PlayerTwoSkill> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerTwoSkillCollection(Database db, Query<PlayerTwoSkillColumns, PlayerTwoSkill> q, bool load) : base(db, q, load) { }
		public PlayerTwoSkillCollection(Query<PlayerTwoSkillColumns, PlayerTwoSkill> q, bool load) : base(q, load) { }
    }
}