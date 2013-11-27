using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class UserIdentifierColumns: QueryFilter<UserIdentifierColumns>, IFilterToken
    {
        public UserIdentifierColumns() { }
        public UserIdentifierColumns(string columnName)
            : base(columnName)
        { }
		
		public UserIdentifierColumns KeyColumn
		{
			get
			{
				return new UserIdentifierColumns("Id");
			}
		}	
				
        public UserIdentifierColumns Id
        {
            get
            {
                return new UserIdentifierColumns("Id");
            }
        }
        public UserIdentifierColumns UserName
        {
            get
            {
                return new UserIdentifierColumns("UserName");
            }
        }
        public UserIdentifierColumns Source
        {
            get
            {
                return new UserIdentifierColumns("Source");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(UserIdentifier);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}