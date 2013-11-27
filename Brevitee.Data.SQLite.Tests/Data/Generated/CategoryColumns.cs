using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class CategoryColumns: QueryFilter<CategoryColumns>, IFilterToken
    {
        public CategoryColumns() { }
        public CategoryColumns(string columnName)
            : base(columnName)
        { }

        public CategoryColumns Id
        {
            get
            {
                return new CategoryColumns("Id");
            }
        }
        public CategoryColumns Name
        {
            get
            {
                return new CategoryColumns("Name");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}