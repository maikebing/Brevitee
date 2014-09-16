using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class RequiredLevelSkillColumns: QueryFilter<RequiredLevelSkillColumns>, IFilterToken
    {
        public RequiredLevelSkillColumns() { }
        public RequiredLevelSkillColumns(string columnName)
            : base(columnName)
        { }
		
		public RequiredLevelSkillColumns KeyColumn
		{
			get
			{
				return new RequiredLevelSkillColumns("Id");
			}
		}	
				
        public RequiredLevelSkillColumns Id
        {
            get
            {
                return new RequiredLevelSkillColumns("Id");
            }
        }

        public RequiredLevelSkillColumns RequiredLevelId
        {
            get
            {
                return new RequiredLevelSkillColumns("RequiredLevelId");
            }
        }
        public RequiredLevelSkillColumns SkillId
        {
            get
            {
                return new RequiredLevelSkillColumns("SkillId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(RequiredLevelSkill);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}