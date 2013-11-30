using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerColumns: QueryFilter<PlayerColumns>, IFilterToken
    {
        public PlayerColumns() { }
        public PlayerColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerColumns KeyColumn
		{
			get
			{
				return new PlayerColumns("Id");
			}
		}	
				
        public PlayerColumns Id
        {
            get
            {
                return new PlayerColumns("Id");
            }
        }
        public PlayerColumns Name
        {
            get
            {
                return new PlayerColumns("Name");
            }
        }
        public PlayerColumns Level
        {
            get
            {
                return new PlayerColumns("Level");
            }
        }
        public PlayerColumns ExperiencePoints
        {
            get
            {
                return new PlayerColumns("ExperiencePoints");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Player);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}