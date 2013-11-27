using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class ItemColumns: QueryFilter<ItemColumns>, IFilterToken
    {
        public ItemColumns() { }
        public ItemColumns(string columnName)
            : base(columnName)
        { }

        public ItemColumns Id
        {
            get
            {
                return new ItemColumns("Id");
            }
        }
        public ItemColumns Name
        {
            get
            {
                return new ItemColumns("Name");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}