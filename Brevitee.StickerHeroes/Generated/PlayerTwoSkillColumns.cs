using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerTwoSkillColumns: QueryFilter<PlayerTwoSkillColumns>, IFilterToken
    {
        public PlayerTwoSkillColumns() { }
        public PlayerTwoSkillColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerTwoSkillColumns KeyColumn
		{
			get
			{
				return new PlayerTwoSkillColumns("Id");
			}
		}	
				
        public PlayerTwoSkillColumns Id
        {
            get
            {
                return new PlayerTwoSkillColumns("Id");
            }
        }

        public PlayerTwoSkillColumns PlayerTwoId
        {
            get
            {
                return new PlayerTwoSkillColumns("PlayerTwoId");
            }
        }
        public PlayerTwoSkillColumns SkillId
        {
            get
            {
                return new PlayerTwoSkillColumns("SkillId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerTwoSkill);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}