using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class LastLoginColumns: QueryFilter<LastLoginColumns>, IFilterToken
    {
        public LastLoginColumns() { }
        public LastLoginColumns(string columnName)
            : base(columnName)
        { }

        public LastLoginColumns Id
        {
            get
            {
                return new LastLoginColumns("Id");
            }
        }
        public LastLoginColumns DateTime
        {
            get
            {
                return new LastLoginColumns("DateTime");
            }
        }

        public LastLoginColumns UserId
        {
            get
            {
                return new LastLoginColumns("UserId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}