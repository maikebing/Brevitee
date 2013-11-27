using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class RoleColumns: QueryFilter<RoleColumns>, IFilterToken
    {
        public RoleColumns() { }
        public RoleColumns(string columnName)
            : base(columnName)
        { }

        public RoleColumns Id
        {
            get
            {
                return new RoleColumns("Id");
            }
        }
        public RoleColumns Name
        {
            get
            {
                return new RoleColumns("Name");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}