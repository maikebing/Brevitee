using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class StyleColumns: QueryFilter<StyleColumns>, IFilterToken
    {
        public StyleColumns() { }
        public StyleColumns(string columnName)
            : base(columnName)
        { }
		
		public StyleColumns KeyColumn
		{
			get
			{
				return new StyleColumns("Id");
			}
		}	
				
        public StyleColumns Id
        {
            get
            {
                return new StyleColumns("Id");
            }
        }
        public StyleColumns Name
        {
            get
            {
                return new StyleColumns("Name");
            }
        }
        public StyleColumns Value
        {
            get
            {
                return new StyleColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Style);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}