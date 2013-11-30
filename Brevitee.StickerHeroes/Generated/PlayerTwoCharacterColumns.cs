using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerTwoCharacterColumns: QueryFilter<PlayerTwoCharacterColumns>, IFilterToken
    {
        public PlayerTwoCharacterColumns() { }
        public PlayerTwoCharacterColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerTwoCharacterColumns KeyColumn
		{
			get
			{
				return new PlayerTwoCharacterColumns("Id");
			}
		}	
				
        public PlayerTwoCharacterColumns Id
        {
            get
            {
                return new PlayerTwoCharacterColumns("Id");
            }
        }

        public PlayerTwoCharacterColumns PlayerTwoId
        {
            get
            {
                return new PlayerTwoCharacterColumns("PlayerTwoId");
            }
        }
        public PlayerTwoCharacterColumns CharacterId
        {
            get
            {
                return new PlayerTwoCharacterColumns("CharacterId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerTwoCharacter);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}