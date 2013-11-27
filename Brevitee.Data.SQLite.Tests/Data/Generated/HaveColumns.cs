using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class HaveColumns: QueryFilter<HaveColumns>, IFilterToken
    {
        public HaveColumns() { }
        public HaveColumns(string columnName)
            : base(columnName)
        { }

        public HaveColumns Id
        {
            get
            {
                return new HaveColumns("Id");
            }
        }
        public HaveColumns LastModified
        {
            get
            {
                return new HaveColumns("LastModified");
            }
        }
        public HaveColumns Quantity
        {
            get
            {
                return new HaveColumns("Quantity");
            }
        }

        public HaveColumns UserId
        {
            get
            {
                return new HaveColumns("UserId");
            }
        }
        public HaveColumns HaveStatusId
        {
            get
            {
                return new HaveColumns("HaveStatusId");
            }
        }
        public HaveColumns ItemId
        {
            get
            {
                return new HaveColumns("ItemId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}