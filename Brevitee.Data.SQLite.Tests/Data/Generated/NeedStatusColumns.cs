using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class NeedStatusColumns: QueryFilter<NeedStatusColumns>, IFilterToken
    {
        public NeedStatusColumns() { }
        public NeedStatusColumns(string columnName)
            : base(columnName)
        { }

        public NeedStatusColumns Id
        {
            get
            {
                return new NeedStatusColumns("Id");
            }
        }
        public NeedStatusColumns Value
        {
            get
            {
                return new NeedStatusColumns("Value");
            }
        }
        public NeedStatusColumns Status
        {
            get
            {
                return new NeedStatusColumns("Status");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}