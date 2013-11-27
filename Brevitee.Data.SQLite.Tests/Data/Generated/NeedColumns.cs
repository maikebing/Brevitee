using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class NeedColumns: QueryFilter<NeedColumns>, IFilterToken
    {
        public NeedColumns() { }
        public NeedColumns(string columnName)
            : base(columnName)
        { }

        public NeedColumns Id
        {
            get
            {
                return new NeedColumns("Id");
            }
        }
        public NeedColumns LastModified
        {
            get
            {
                return new NeedColumns("LastModified");
            }
        }

        public NeedColumns UserId
        {
            get
            {
                return new NeedColumns("UserId");
            }
        }
        public NeedColumns ItemId
        {
            get
            {
                return new NeedColumns("ItemId");
            }
        }
        public NeedColumns NeedStatusId
        {
            get
            {
                return new NeedColumns("NeedStatusId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}