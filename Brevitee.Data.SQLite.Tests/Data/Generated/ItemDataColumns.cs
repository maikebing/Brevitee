using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class ItemDataColumns: QueryFilter<ItemDataColumns>, IFilterToken
    {
        public ItemDataColumns() { }
        public ItemDataColumns(string columnName)
            : base(columnName)
        { }

        public ItemDataColumns Id
        {
            get
            {
                return new ItemDataColumns("Id");
            }
        }
        public ItemDataColumns Name
        {
            get
            {
                return new ItemDataColumns("Name");
            }
        }
        public ItemDataColumns Value
        {
            get
            {
                return new ItemDataColumns("Value");
            }
        }
        public ItemDataColumns DataType
        {
            get
            {
                return new ItemDataColumns("DataType");
            }
        }

        public ItemDataColumns ItemId
        {
            get
            {
                return new ItemDataColumns("ItemId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}