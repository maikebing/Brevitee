using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.UserAccounts.Data
{
    public class LoginColumns: QueryFilter<LoginColumns>, IFilterToken
    {
        public LoginColumns() { }
        public LoginColumns(string columnName)
            : base(columnName)
        { }
		
		public LoginColumns KeyColumn
		{
			get
			{
				return new LoginColumns("Id");
			}
		}	
				
        public LoginColumns Id
        {
            get
            {
                return new LoginColumns("Id");
            }
        }
        public LoginColumns Uuid
        {
            get
            {
                return new LoginColumns("Uuid");
            }
        }
        public LoginColumns DateTime
        {
            get
            {
                return new LoginColumns("DateTime");
            }
        }

        public LoginColumns UserId
        {
            get
            {
                return new LoginColumns("UserId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Login);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}