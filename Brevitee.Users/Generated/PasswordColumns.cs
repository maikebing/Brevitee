using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Users.Data
{
    public class PasswordColumns: QueryFilter<PasswordColumns>, IFilterToken
    {
        public PasswordColumns() { }
        public PasswordColumns(string columnName)
            : base(columnName)
        { }
		
		public PasswordColumns KeyColumn
		{
			get
			{
				return new PasswordColumns("Id");
			}
		}	
				
        public PasswordColumns Id
        {
            get
            {
                return new PasswordColumns("Id");
            }
        }
        public PasswordColumns Value
        {
            get
            {
                return new PasswordColumns("Value");
            }
        }
        public PasswordColumns Salt
        {
            get
            {
                return new PasswordColumns("Salt");
            }
        }
        public PasswordColumns Sha1
        {
            get
            {
                return new PasswordColumns("Sha1");
            }
        }

        public PasswordColumns UserId
        {
            get
            {
                return new PasswordColumns("UserId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Password);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}