using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class SkillColumns: QueryFilter<SkillColumns>, IFilterToken
    {
        public SkillColumns() { }
        public SkillColumns(string columnName)
            : base(columnName)
        { }
		
		public SkillColumns KeyColumn
		{
			get
			{
				return new SkillColumns("Id");
			}
		}	
				
        public SkillColumns Id
        {
            get
            {
                return new SkillColumns("Id");
            }
        }
        public SkillColumns Name
        {
            get
            {
                return new SkillColumns("Name");
            }
        }
        public SkillColumns Strength
        {
            get
            {
                return new SkillColumns("Strength");
            }
        }
        public SkillColumns Element
        {
            get
            {
                return new SkillColumns("Element");
            }
        }

        public SkillColumns EffectOverTimeId
        {
            get
            {
                return new SkillColumns("EffectOverTimeId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Skill);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}