using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class PlayerOneColumns: QueryFilter<PlayerOneColumns>, IFilterToken
    {
        public PlayerOneColumns() { }
        public PlayerOneColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerOneColumns KeyColumn
		{
			get
			{
				return new PlayerOneColumns("Id");
			}
		}	
				
        public PlayerOneColumns Id
        {
            get
            {
                return new PlayerOneColumns("Id");
            }
        }

        public PlayerOneColumns BattleId
        {
            get
            {
                return new PlayerOneColumns("BattleId");
            }
        }
        public PlayerOneColumns PlayerId
        {
            get
            {
                return new PlayerOneColumns("PlayerId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerOne);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}