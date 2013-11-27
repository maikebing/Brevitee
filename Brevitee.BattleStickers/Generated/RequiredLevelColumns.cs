using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class RequiredLevelColumns: QueryFilter<RequiredLevelColumns>, IFilterToken
    {
        public RequiredLevelColumns() { }
        public RequiredLevelColumns(string columnName)
            : base(columnName)
        { }
		
		public RequiredLevelColumns KeyColumn
		{
			get
			{
				return new RequiredLevelColumns("Id");
			}
		}	
				
        public RequiredLevelColumns Id
        {
            get
            {
                return new RequiredLevelColumns("Id");
            }
        }
        public RequiredLevelColumns Value
        {
            get
            {
                return new RequiredLevelColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(RequiredLevel);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}