using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class TagColumns: QueryFilter<TagColumns>, IFilterToken
    {
        public TagColumns() { }
        public TagColumns(string columnName)
            : base(columnName)
        { }

        public TagColumns Id
        {
            get
            {
                return new TagColumns("Id");
            }
        }
        public TagColumns Name
        {
            get
            {
                return new TagColumns("Name");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}