using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class LoginTimeColumns: QueryFilter<LoginTimeColumns>, IFilterToken
    {
        public LoginTimeColumns() { }
        public LoginTimeColumns(string columnName)
            : base(columnName)
        { }
		
		public LoginTimeColumns KeyColumn
		{
			get
			{
				return new LoginTimeColumns("Id");
			}
		}	
				
﻿        public LoginTimeColumns Id
        {
            get
            {
                return new LoginTimeColumns("Id");
            }
        }
﻿        public LoginTimeColumns Uuid
        {
            get
            {
                return new LoginTimeColumns("Uuid");
            }
        }
﻿        public LoginTimeColumns DateTime
        {
            get
            {
                return new LoginTimeColumns("DateTime");
            }
        }
﻿        public LoginTimeColumns UserName
        {
            get
            {
                return new LoginTimeColumns("UserName");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(LoginTime);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}