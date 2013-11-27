using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class ActivityColumns: QueryFilter<ActivityColumns>, IFilterToken
    {
        public ActivityColumns() { }
        public ActivityColumns(string columnName)
            : base(columnName)
        { }

        public ActivityColumns Id
        {
            get
            {
                return new ActivityColumns("Id");
            }
        }
        public ActivityColumns Action
        {
            get
            {
                return new ActivityColumns("Action");
            }
        }
        public ActivityColumns DateTime
        {
            get
            {
                return new ActivityColumns("DateTime");
            }
        }

        public ActivityColumns UserId
        {
            get
            {
                return new ActivityColumns("UserId");
            }
        }
        public ActivityColumns ItemId
        {
            get
            {
                return new ActivityColumns("ItemId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}