using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class GiveStatusColumns: QueryFilter<GiveStatusColumns>, IFilterToken
    {
        public GiveStatusColumns() { }
        public GiveStatusColumns(string columnName)
            : base(columnName)
        { }

        public GiveStatusColumns Id
        {
            get
            {
                return new GiveStatusColumns("Id");
            }
        }
        public GiveStatusColumns Value
        {
            get
            {
                return new GiveStatusColumns("Value");
            }
        }
        public GiveStatusColumns Status
        {
            get
            {
                return new GiveStatusColumns("Status");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}