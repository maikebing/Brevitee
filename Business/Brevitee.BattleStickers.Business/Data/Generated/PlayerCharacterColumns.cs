using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerCharacterColumns: QueryFilter<PlayerCharacterColumns>, IFilterToken
    {
        public PlayerCharacterColumns() { }
        public PlayerCharacterColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerCharacterColumns KeyColumn
		{
			get
			{
				return new PlayerCharacterColumns("Id");
			}
		}	
				
        public PlayerCharacterColumns Id
        {
            get
            {
                return new PlayerCharacterColumns("Id");
            }
        }

        public PlayerCharacterColumns PlayerId
        {
            get
            {
                return new PlayerCharacterColumns("PlayerId");
            }
        }
        public PlayerCharacterColumns CharacterId
        {
            get
            {
                return new PlayerCharacterColumns("CharacterId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerCharacter);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}