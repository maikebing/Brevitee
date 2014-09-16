using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.UserAccounts.Data
{
    public class RoleColumns: QueryFilter<RoleColumns>, IFilterToken
    {
        public RoleColumns() { }
        public RoleColumns(string columnName)
            : base(columnName)
        { }
		
		public RoleColumns KeyColumn
		{
			get
			{
				return new RoleColumns("Id");
			}
		}	
				
        public RoleColumns Id
        {
            get
            {
                return new RoleColumns("Id");
            }
        }
        public RoleColumns Uuid
        {
            get
            {
                return new RoleColumns("Uuid");
            }
        }
        public RoleColumns Name
        {
            get
            {
                return new RoleColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Role);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}