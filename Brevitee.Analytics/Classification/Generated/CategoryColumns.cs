using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Classification
{
    public class CategoryColumns: QueryFilter<CategoryColumns>, IFilterToken
    {
        public CategoryColumns() { }
        public CategoryColumns(string columnName)
            : base(columnName)
        { }
		
		public CategoryColumns KeyColumn
		{
			get
			{
				return new CategoryColumns("Id");
			}
		}	
				
        public CategoryColumns Id
        {
            get
            {
                return new CategoryColumns("Id");
            }
        }
        public CategoryColumns Value
        {
            get
            {
                return new CategoryColumns("Value");
            }
        }
        public CategoryColumns DocumentCount
        {
            get
            {
                return new CategoryColumns("DocumentCount");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}