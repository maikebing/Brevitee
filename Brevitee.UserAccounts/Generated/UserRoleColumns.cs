using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.UserAccounts.Data
{
    public class UserRoleColumns: QueryFilter<UserRoleColumns>, IFilterToken
    {
        public UserRoleColumns() { }
        public UserRoleColumns(string columnName)
            : base(columnName)
        { }
		
		public UserRoleColumns KeyColumn
		{
			get
			{
				return new UserRoleColumns("Id");
			}
		}	
				
        public UserRoleColumns Id
        {
            get
            {
                return new UserRoleColumns("Id");
            }
        }

        public UserRoleColumns UserId
        {
            get
            {
                return new UserRoleColumns("UserId");
            }
        }
        public UserRoleColumns RoleId
        {
            get
            {
                return new UserRoleColumns("RoleId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(UserRole);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}