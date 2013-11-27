using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class RockPaperScissorsColumns: QueryFilter<RockPaperScissorsColumns>, IFilterToken
    {
        public RockPaperScissorsColumns() { }
        public RockPaperScissorsColumns(string columnName)
            : base(columnName)
        { }

        public RockPaperScissorsColumns Id
        {
            get
            {
                return new RockPaperScissorsColumns("Id");
            }
        }
        public RockPaperScissorsColumns UserOneOption
        {
            get
            {
                return new RockPaperScissorsColumns("UserOneOption");
            }
        }
        public RockPaperScissorsColumns UserTwoOption
        {
            get
            {
                return new RockPaperScissorsColumns("UserTwoOption");
            }
        }
        public RockPaperScissorsColumns LastModifiedBy
        {
            get
            {
                return new RockPaperScissorsColumns("LastModifiedBy");
            }
        }
        public RockPaperScissorsColumns LastModifiedDate
        {
            get
            {
                return new RockPaperScissorsColumns("LastModifiedDate");
            }
        }

        public RockPaperScissorsColumns UserIdOne
        {
            get
            {
                return new RockPaperScissorsColumns("UserIdOne");
            }
        }
        public RockPaperScissorsColumns UserIdTwo
        {
            get
            {
                return new RockPaperScissorsColumns("UserIdTwo");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}