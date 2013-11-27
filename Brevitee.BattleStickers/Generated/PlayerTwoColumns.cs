using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class PlayerTwoColumns: QueryFilter<PlayerTwoColumns>, IFilterToken
    {
        public PlayerTwoColumns() { }
        public PlayerTwoColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerTwoColumns KeyColumn
		{
			get
			{
				return new PlayerTwoColumns("Id");
			}
		}	
				
        public PlayerTwoColumns Id
        {
            get
            {
                return new PlayerTwoColumns("Id");
            }
        }

        public PlayerTwoColumns BattleId
        {
            get
            {
                return new PlayerTwoColumns("BattleId");
            }
        }
        public PlayerTwoColumns PlayerId
        {
            get
            {
                return new PlayerTwoColumns("PlayerId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerTwo);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}