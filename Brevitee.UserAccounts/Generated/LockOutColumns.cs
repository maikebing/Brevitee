using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.UserAccounts.Data
{
    public class LockOutColumns: QueryFilter<LockOutColumns>, IFilterToken
    {
        public LockOutColumns() { }
        public LockOutColumns(string columnName)
            : base(columnName)
        { }
		
		public LockOutColumns KeyColumn
		{
			get
			{
				return new LockOutColumns("Id");
			}
		}	
				
        public LockOutColumns Id
        {
            get
            {
                return new LockOutColumns("Id");
            }
        }
        public LockOutColumns Uuid
        {
            get
            {
                return new LockOutColumns("Uuid");
            }
        }
        public LockOutColumns DateTime
        {
            get
            {
                return new LockOutColumns("DateTime");
            }
        }
        public LockOutColumns Unlocked
        {
            get
            {
                return new LockOutColumns("Unlocked");
            }
        }
        public LockOutColumns UnlockedDate
        {
            get
            {
                return new LockOutColumns("UnlockedDate");
            }
        }
        public LockOutColumns UnlockedBy
        {
            get
            {
                return new LockOutColumns("UnlockedBy");
            }
        }

        public LockOutColumns UserId
        {
            get
            {
                return new LockOutColumns("UserId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(LockOut);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}