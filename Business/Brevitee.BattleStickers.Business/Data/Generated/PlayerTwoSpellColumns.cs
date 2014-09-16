using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerTwoSpellColumns: QueryFilter<PlayerTwoSpellColumns>, IFilterToken
    {
        public PlayerTwoSpellColumns() { }
        public PlayerTwoSpellColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerTwoSpellColumns KeyColumn
		{
			get
			{
				return new PlayerTwoSpellColumns("Id");
			}
		}	
				
        public PlayerTwoSpellColumns Id
        {
            get
            {
                return new PlayerTwoSpellColumns("Id");
            }
        }

        public PlayerTwoSpellColumns PlayerTwoId
        {
            get
            {
                return new PlayerTwoSpellColumns("PlayerTwoId");
            }
        }
        public PlayerTwoSpellColumns SpellId
        {
            get
            {
                return new PlayerTwoSpellColumns("SpellId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerTwoSpell);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}