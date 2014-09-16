using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class SkillCollection: DaoCollection<SkillColumns, Skill>
    { 
		public SkillCollection(){}
		public SkillCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public SkillCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SkillCollection(Query<SkillColumns, Skill> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SkillCollection(Database db, Query<SkillColumns, Skill> q, bool load) : base(db, q, load) { }
		public SkillCollection(Query<SkillColumns, Skill> q, bool load) : base(q, load) { }
    }
}