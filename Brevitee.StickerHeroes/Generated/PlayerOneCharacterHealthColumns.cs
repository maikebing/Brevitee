using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerOneCharacterHealthColumns: QueryFilter<PlayerOneCharacterHealthColumns>, IFilterToken
    {
        public PlayerOneCharacterHealthColumns() { }
        public PlayerOneCharacterHealthColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerOneCharacterHealthColumns KeyColumn
		{
			get
			{
				return new PlayerOneCharacterHealthColumns("Id");
			}
		}	
				
        public PlayerOneCharacterHealthColumns Id
        {
            get
            {
                return new PlayerOneCharacterHealthColumns("Id");
            }
        }
        public PlayerOneCharacterHealthColumns Value
        {
            get
            {
                return new PlayerOneCharacterHealthColumns("Value");
            }
        }

        public PlayerOneCharacterHealthColumns PlayerOneId
        {
            get
            {
                return new PlayerOneCharacterHealthColumns("PlayerOneId");
            }
        }
        public PlayerOneCharacterHealthColumns CharacterId
        {
            get
            {
                return new PlayerOneCharacterHealthColumns("CharacterId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerOneCharacterHealth);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}