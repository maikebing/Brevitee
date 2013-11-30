using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerOneCharacterColumns: QueryFilter<PlayerOneCharacterColumns>, IFilterToken
    {
        public PlayerOneCharacterColumns() { }
        public PlayerOneCharacterColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerOneCharacterColumns KeyColumn
		{
			get
			{
				return new PlayerOneCharacterColumns("Id");
			}
		}	
				
        public PlayerOneCharacterColumns Id
        {
            get
            {
                return new PlayerOneCharacterColumns("Id");
            }
        }

        public PlayerOneCharacterColumns PlayerOneId
        {
            get
            {
                return new PlayerOneCharacterColumns("PlayerOneId");
            }
        }
        public PlayerOneCharacterColumns CharacterId
        {
            get
            {
                return new PlayerOneCharacterColumns("CharacterId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerOneCharacter);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}