using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerOneSpellColumns: QueryFilter<PlayerOneSpellColumns>, IFilterToken
    {
        public PlayerOneSpellColumns() { }
        public PlayerOneSpellColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerOneSpellColumns KeyColumn
		{
			get
			{
				return new PlayerOneSpellColumns("Id");
			}
		}	
				
        public PlayerOneSpellColumns Id
        {
            get
            {
                return new PlayerOneSpellColumns("Id");
            }
        }

        public PlayerOneSpellColumns PlayerOneId
        {
            get
            {
                return new PlayerOneSpellColumns("PlayerOneId");
            }
        }
        public PlayerOneSpellColumns SpellId
        {
            get
            {
                return new PlayerOneSpellColumns("SpellId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerOneSpell);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}