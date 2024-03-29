using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.UserAccounts.Data
{
    public class PasswordQuestionColumns: QueryFilter<PasswordQuestionColumns>, IFilterToken
    {
        public PasswordQuestionColumns() { }
        public PasswordQuestionColumns(string columnName)
            : base(columnName)
        { }
		
		public PasswordQuestionColumns KeyColumn
		{
			get
			{
				return new PasswordQuestionColumns("Id");
			}
		}	
				
        public PasswordQuestionColumns Id
        {
            get
            {
                return new PasswordQuestionColumns("Id");
            }
        }
        public PasswordQuestionColumns Uuid
        {
            get
            {
                return new PasswordQuestionColumns("Uuid");
            }
        }
        public PasswordQuestionColumns Value
        {
            get
            {
                return new PasswordQuestionColumns("Value");
            }
        }
        public PasswordQuestionColumns Answer
        {
            get
            {
                return new PasswordQuestionColumns("Answer");
            }
        }

        public PasswordQuestionColumns UserId
        {
            get
            {
                return new PasswordQuestionColumns("UserId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PasswordQuestion);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}