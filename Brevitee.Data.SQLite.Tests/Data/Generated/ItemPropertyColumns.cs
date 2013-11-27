using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class ItemPropertyColumns: QueryFilter<ItemPropertyColumns>, IFilterToken
    {
        public ItemPropertyColumns() { }
        public ItemPropertyColumns(string columnName)
            : base(columnName)
        { }

        public ItemPropertyColumns Id
        {
            get
            {
                return new ItemPropertyColumns("Id");
            }
        }
        public ItemPropertyColumns Name
        {
            get
            {
                return new ItemPropertyColumns("Name");
            }
        }
        public ItemPropertyColumns Value
        {
            get
            {
                return new ItemPropertyColumns("Value");
            }
        }

        public ItemPropertyColumns ItemId
        {
            get
            {
                return new ItemPropertyColumns("ItemId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}