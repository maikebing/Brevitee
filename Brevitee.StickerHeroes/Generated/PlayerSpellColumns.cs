using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerSpellColumns: QueryFilter<PlayerSpellColumns>, IFilterToken
    {
        public PlayerSpellColumns() { }
        public PlayerSpellColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerSpellColumns KeyColumn
		{
			get
			{
				return new PlayerSpellColumns("Id");
			}
		}	
				
        public PlayerSpellColumns Id
        {
            get
            {
                return new PlayerSpellColumns("Id");
            }
        }

        public PlayerSpellColumns PlayerId
        {
            get
            {
                return new PlayerSpellColumns("PlayerId");
            }
        }
        public PlayerSpellColumns SpellId
        {
            get
            {
                return new PlayerSpellColumns("SpellId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerSpell);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}