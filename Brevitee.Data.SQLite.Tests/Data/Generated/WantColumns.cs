using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class WantColumns: QueryFilter<WantColumns>, IFilterToken
    {
        public WantColumns() { }
        public WantColumns(string columnName)
            : base(columnName)
        { }

        public WantColumns Id
        {
            get
            {
                return new WantColumns("Id");
            }
        }
        public WantColumns LastModified
        {
            get
            {
                return new WantColumns("LastModified");
            }
        }

        public WantColumns UserId
        {
            get
            {
                return new WantColumns("UserId");
            }
        }
        public WantColumns ItemId
        {
            get
            {
                return new WantColumns("ItemId");
            }
        }
        public WantColumns WantStatusId
        {
            get
            {
                return new WantColumns("WantStatusId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}