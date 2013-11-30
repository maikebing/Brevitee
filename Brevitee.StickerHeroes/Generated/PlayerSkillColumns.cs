using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerSkillColumns: QueryFilter<PlayerSkillColumns>, IFilterToken
    {
        public PlayerSkillColumns() { }
        public PlayerSkillColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerSkillColumns KeyColumn
		{
			get
			{
				return new PlayerSkillColumns("Id");
			}
		}	
				
        public PlayerSkillColumns Id
        {
            get
            {
                return new PlayerSkillColumns("Id");
            }
        }

        public PlayerSkillColumns PlayerId
        {
            get
            {
                return new PlayerSkillColumns("PlayerId");
            }
        }
        public PlayerSkillColumns SkillId
        {
            get
            {
                return new PlayerSkillColumns("SkillId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerSkill);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}