using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Logging
{
    public class SignatureColumns: QueryFilter<SignatureColumns>, IFilterToken
    {
        public SignatureColumns() { }
        public SignatureColumns(string columnName)
            : base(columnName)
        { }
		
		public SignatureColumns KeyColumn
		{
			get
			{
				return new SignatureColumns("Id");
			}
		}	
				
        public SignatureColumns Id
        {
            get
            {
                return new SignatureColumns("Id");
            }
        }
        public SignatureColumns Value
        {
            get
            {
                return new SignatureColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Signature);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}