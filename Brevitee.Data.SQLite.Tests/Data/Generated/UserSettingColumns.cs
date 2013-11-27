using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class UserSettingColumns: QueryFilter<UserSettingColumns>, IFilterToken
    {
        public UserSettingColumns() { }
        public UserSettingColumns(string columnName)
            : base(columnName)
        { }

        public UserSettingColumns Id
        {
            get
            {
                return new UserSettingColumns("Id");
            }
        }

        public UserSettingColumns UserId
        {
            get
            {
                return new UserSettingColumns("UserId");
            }
        }
        public UserSettingColumns SettingId
        {
            get
            {
                return new UserSettingColumns("SettingId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}