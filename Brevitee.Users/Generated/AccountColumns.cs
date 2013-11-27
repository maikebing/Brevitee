using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Users.Data
{
    public class AccountColumns: QueryFilter<AccountColumns>, IFilterToken
    {
        public AccountColumns() { }
        public AccountColumns(string columnName)
            : base(columnName)
        { }
		
		public AccountColumns KeyColumn
		{
			get
			{
				return new AccountColumns("Id");
			}
		}	
				
        public AccountColumns Id
        {
            get
            {
                return new AccountColumns("Id");
            }
        }
        public AccountColumns Token
        {
            get
            {
                return new AccountColumns("Token");
            }
        }
        public AccountColumns Provider
        {
            get
            {
                return new AccountColumns("Provider");
            }
        }
        public AccountColumns ProviderUserId
        {
            get
            {
                return new AccountColumns("ProviderUserId");
            }
        }
        public AccountColumns Comment
        {
            get
            {
                return new AccountColumns("Comment");
            }
        }
        public AccountColumns CreationDate
        {
            get
            {
                return new AccountColumns("CreationDate");
            }
        }
        public AccountColumns IsConfirmed
        {
            get
            {
                return new AccountColumns("IsConfirmed");
            }
        }
        public AccountColumns ConfirmationDate
        {
            get
            {
                return new AccountColumns("ConfirmationDate");
            }
        }

        public AccountColumns UserId
        {
            get
            {
                return new AccountColumns("UserId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Account);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}