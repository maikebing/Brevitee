using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Users.Data
{
    public class UserColumns: QueryFilter<UserColumns>, IFilterToken
    {
        public UserColumns() { }
        public UserColumns(string columnName)
            : base(columnName)
        { }
		
		public UserColumns KeyColumn
		{
			get
			{
				return new UserColumns("Id");
			}
		}	
				
        public UserColumns Id
        {
            get
            {
                return new UserColumns("Id");
            }
        }
        public UserColumns CreationDate
        {
            get
            {
                return new UserColumns("CreationDate");
            }
        }
        public UserColumns IsDeleted
        {
            get
            {
                return new UserColumns("IsDeleted");
            }
        }
        public UserColumns UserName
        {
            get
            {
                return new UserColumns("UserName");
            }
        }
        public UserColumns IsApproved
        {
            get
            {
                return new UserColumns("IsApproved");
            }
        }
        public UserColumns Email
        {
            get
            {
                return new UserColumns("Email");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(User);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}