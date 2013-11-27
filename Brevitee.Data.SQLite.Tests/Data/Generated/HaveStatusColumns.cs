using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class HaveStatusColumns: QueryFilter<HaveStatusColumns>, IFilterToken
    {
        public HaveStatusColumns() { }
        public HaveStatusColumns(string columnName)
            : base(columnName)
        { }

        public HaveStatusColumns Id
        {
            get
            {
                return new HaveStatusColumns("Id");
            }
        }
        public HaveStatusColumns Value
        {
            get
            {
                return new HaveStatusColumns("Value");
            }
        }
        public HaveStatusColumns Status
        {
            get
            {
                return new HaveStatusColumns("Status");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}