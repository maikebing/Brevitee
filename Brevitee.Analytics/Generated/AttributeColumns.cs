using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class AttributeColumns: QueryFilter<AttributeColumns>, IFilterToken
    {
        public AttributeColumns() { }
        public AttributeColumns(string columnName)
            : base(columnName)
        { }
		
		public AttributeColumns KeyColumn
		{
			get
			{
				return new AttributeColumns("Id");
			}
		}	
				
        public AttributeColumns Id
        {
            get
            {
                return new AttributeColumns("Id");
            }
        }
        public AttributeColumns Name
        {
            get
            {
                return new AttributeColumns("Name");
            }
        }
        public AttributeColumns Value
        {
            get
            {
                return new AttributeColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Attribute);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}