using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class UserColumns: QueryFilter<UserColumns>, IFilterToken
    {
        public UserColumns() { }
        public UserColumns(string columnName)
            : base(columnName)
        { }

        public UserColumns Id
        {
            get
            {
                return new UserColumns("Id");
            }
        }
        public UserColumns FirstName
        {
            get
            {
                return new UserColumns("FirstName");
            }
        }
        public UserColumns LastName
        {
            get
            {
                return new UserColumns("LastName");
            }
        }
        public UserColumns UserName
        {
            get
            {
                return new UserColumns("UserName");
            }
        }
        public UserColumns Email
        {
            get
            {
                return new UserColumns("Email");
            }
        }
        public UserColumns AuthSource
        {
            get
            {
                return new UserColumns("AuthSource");
            }
        }
        public UserColumns SourceId
        {
            get
            {
                return new UserColumns("SourceId");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}