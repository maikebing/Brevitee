using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class SkillCollection: DaoCollection<SkillColumns, Skill>
    { 
		public SkillCollection(){}
		public SkillCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SkillCollection(Query<SkillColumns, Skill> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SkillCollection(Query<SkillColumns, Skill> q, bool load) : base(q, load) { }
    }
}