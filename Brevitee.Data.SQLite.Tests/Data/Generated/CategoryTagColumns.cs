using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class CategoryTagColumns: QueryFilter<CategoryTagColumns>, IFilterToken
    {
        public CategoryTagColumns() { }
        public CategoryTagColumns(string columnName)
            : base(columnName)
        { }

        public CategoryTagColumns Id
        {
            get
            {
                return new CategoryTagColumns("Id");
            }
        }

        public CategoryTagColumns CategoryId
        {
            get
            {
                return new CategoryTagColumns("CategoryId");
            }
        }
        public CategoryTagColumns TagId
        {
            get
            {
                return new CategoryTagColumns("TagId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}