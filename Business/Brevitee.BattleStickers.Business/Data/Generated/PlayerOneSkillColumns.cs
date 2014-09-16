using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerOneSkillColumns: QueryFilter<PlayerOneSkillColumns>, IFilterToken
    {
        public PlayerOneSkillColumns() { }
        public PlayerOneSkillColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerOneSkillColumns KeyColumn
		{
			get
			{
				return new PlayerOneSkillColumns("Id");
			}
		}	
				
        public PlayerOneSkillColumns Id
        {
            get
            {
                return new PlayerOneSkillColumns("Id");
            }
        }

        public PlayerOneSkillColumns PlayerOneId
        {
            get
            {
                return new PlayerOneSkillColumns("PlayerOneId");
            }
        }
        public PlayerOneSkillColumns SkillId
        {
            get
            {
                return new PlayerOneSkillColumns("SkillId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerOneSkill);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}