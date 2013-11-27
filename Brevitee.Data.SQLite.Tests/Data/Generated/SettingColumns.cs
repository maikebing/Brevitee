using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class SettingColumns: QueryFilter<SettingColumns>, IFilterToken
    {
        public SettingColumns() { }
        public SettingColumns(string columnName)
            : base(columnName)
        { }

        public SettingColumns Id
        {
            get
            {
                return new SettingColumns("Id");
            }
        }
        public SettingColumns Name
        {
            get
            {
                return new SettingColumns("Name");
            }
        }
        public SettingColumns Value
        {
            get
            {
                return new SettingColumns("Value");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}