using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Metrics
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
				
﻿        public UserIdentifierColumns Id
        {
            get
            {
                return new UserIdentifierColumns("Id");
            }
        }
﻿        public UserIdentifierColumns Uuid
        {
            get
            {
                return new UserIdentifierColumns("Uuid");
            }
        }
﻿        public UserIdentifierColumns Name
        {
            get
            {
                return new UserIdentifierColumns("Name");
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