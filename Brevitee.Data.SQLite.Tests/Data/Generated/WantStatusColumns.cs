using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class WantStatusColumns: QueryFilter<WantStatusColumns>, IFilterToken
    {
        public WantStatusColumns() { }
        public WantStatusColumns(string columnName)
            : base(columnName)
        { }

        public WantStatusColumns Id
        {
            get
            {
                return new WantStatusColumns("Id");
            }
        }
        public WantStatusColumns Value
        {
            get
            {
                return new WantStatusColumns("Value");
            }
        }
        public WantStatusColumns Status
        {
            get
            {
                return new WantStatusColumns("Status");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}