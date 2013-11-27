using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class PlayerTwoCharacterHealthColumns: QueryFilter<PlayerTwoCharacterHealthColumns>, IFilterToken
    {
        public PlayerTwoCharacterHealthColumns() { }
        public PlayerTwoCharacterHealthColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerTwoCharacterHealthColumns KeyColumn
		{
			get
			{
				return new PlayerTwoCharacterHealthColumns("Id");
			}
		}	
				
        public PlayerTwoCharacterHealthColumns Id
        {
            get
            {
                return new PlayerTwoCharacterHealthColumns("Id");
            }
        }
        public PlayerTwoCharacterHealthColumns Value
        {
            get
            {
                return new PlayerTwoCharacterHealthColumns("Value");
            }
        }

        public PlayerTwoCharacterHealthColumns PlayerTwoId
        {
            get
            {
                return new PlayerTwoCharacterHealthColumns("PlayerTwoId");
            }
        }
        public PlayerTwoCharacterHealthColumns CharacterId
        {
            get
            {
                return new PlayerTwoCharacterHealthColumns("CharacterId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerTwoCharacterHealth);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}