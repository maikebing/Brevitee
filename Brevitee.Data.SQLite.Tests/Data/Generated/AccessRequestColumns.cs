using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class AccessRequestColumns: QueryFilter<AccessRequestColumns>, IFilterToken
    {
        public AccessRequestColumns() { }
        public AccessRequestColumns(string columnName)
            : base(columnName)
        { }

        public AccessRequestColumns Id
        {
            get
            {
                return new AccessRequestColumns("Id");
            }
        }
        public AccessRequestColumns Email
        {
            get
            {
                return new AccessRequestColumns("Email");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}