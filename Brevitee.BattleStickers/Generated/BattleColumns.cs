using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class BattleColumns: QueryFilter<BattleColumns>, IFilterToken
    {
        public BattleColumns() { }
        public BattleColumns(string columnName)
            : base(columnName)
        { }
		
		public BattleColumns KeyColumn
		{
			get
			{
				return new BattleColumns("Id");
			}
		}	
				
        public BattleColumns Id
        {
            get
            {
                return new BattleColumns("Id");
            }
        }
        public BattleColumns MaxActiveCharacters
        {
            get
            {
                return new BattleColumns("MaxActiveCharacters");
            }
        }

        public BattleColumns RockPaperScissorsWinnerId
        {
            get
            {
                return new BattleColumns("RockPaperScissorsWinnerId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Battle);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}