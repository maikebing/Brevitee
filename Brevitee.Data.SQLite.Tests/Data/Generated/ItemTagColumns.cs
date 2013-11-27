using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class ItemTagColumns: QueryFilter<ItemTagColumns>, IFilterToken
    {
        public ItemTagColumns() { }
        public ItemTagColumns(string columnName)
            : base(columnName)
        { }

        public ItemTagColumns Id
        {
            get
            {
                return new ItemTagColumns("Id");
            }
        }

        public ItemTagColumns ItemId
        {
            get
            {
                return new ItemTagColumns("ItemId");
            }
        }
        public ItemTagColumns TagId
        {
            get
            {
                return new ItemTagColumns("TagId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}