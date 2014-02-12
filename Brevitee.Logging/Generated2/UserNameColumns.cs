using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Logging
{
    public class UserNameColumns: QueryFilter<UserNameColumns>, IFilterToken
    {
        public UserNameColumns() { }
        public UserNameColumns(string columnName)
            : base(columnName)
        { }
		
		public UserNameColumns KeyColumn
		{
			get
			{
				return new UserNameColumns("Id");
			}
		}	
				
        public UserNameColumns Id
        {
            get
            {
                return new UserNameColumns("Id");
            }
        }
        public UserNameColumns Value
        {
            get
            {
                return new UserNameColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(UserName);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}