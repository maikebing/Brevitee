using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Logging
{
    public class ParamColumns: QueryFilter<ParamColumns>, IFilterToken
    {
        public ParamColumns() { }
        public ParamColumns(string columnName)
            : base(columnName)
        { }
		
		public ParamColumns KeyColumn
		{
			get
			{
				return new ParamColumns("Id");
			}
		}	
				
        public ParamColumns Id
        {
            get
            {
                return new ParamColumns("Id");
            }
        }
        public ParamColumns Position
        {
            get
            {
                return new ParamColumns("Position");
            }
        }
        public ParamColumns Value
        {
            get
            {
                return new ParamColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Param);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}