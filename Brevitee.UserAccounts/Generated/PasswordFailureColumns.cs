using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.UserAccounts.Data
{
    public class PasswordFailureColumns: QueryFilter<PasswordFailureColumns>, IFilterToken
    {
        public PasswordFailureColumns() { }
        public PasswordFailureColumns(string columnName)
            : base(columnName)
        { }
		
		public PasswordFailureColumns KeyColumn
		{
			get
			{
				return new PasswordFailureColumns("Id");
			}
		}	
				
        public PasswordFailureColumns Id
        {
            get
            {
                return new PasswordFailureColumns("Id");
            }
        }
        public PasswordFailureColumns Uuid
        {
            get
            {
                return new PasswordFailureColumns("Uuid");
            }
        }
        public PasswordFailureColumns DateTime
        {
            get
            {
                return new PasswordFailureColumns("DateTime");
            }
        }

        public PasswordFailureColumns UserId
        {
            get
            {
                return new PasswordFailureColumns("UserId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PasswordFailure);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}