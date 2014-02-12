using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Logging
{
    public class CategoryNameColumns: QueryFilter<CategoryNameColumns>, IFilterToken
    {
        public CategoryNameColumns() { }
        public CategoryNameColumns(string columnName)
            : base(columnName)
        { }
		
		public CategoryNameColumns KeyColumn
		{
			get
			{
				return new CategoryNameColumns("Id");
			}
		}	
				
        public CategoryNameColumns Id
        {
            get
            {
                return new CategoryNameColumns("Id");
            }
        }
        public CategoryNameColumns Value
        {
            get
            {
                return new CategoryNameColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(CategoryName);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}