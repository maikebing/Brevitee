using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class RequiredLevelSkillCollection: DaoCollection<RequiredLevelSkillColumns, RequiredLevelSkill>
    { 
		public RequiredLevelSkillCollection(){}
		public RequiredLevelSkillCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public RequiredLevelSkillCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RequiredLevelSkillCollection(Query<RequiredLevelSkillColumns, RequiredLevelSkill> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RequiredLevelSkillCollection(Database db, Query<RequiredLevelSkillColumns, RequiredLevelSkill> q, bool load) : base(db, q, load) { }
		public RequiredLevelSkillCollection(Query<RequiredLevelSkillColumns, RequiredLevelSkill> q, bool load) : base(q, load) { }
    }
}