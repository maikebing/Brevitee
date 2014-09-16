using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Metrics
{
    public class MethodCounterColumns: QueryFilter<MethodCounterColumns>, IFilterToken
    {
        public MethodCounterColumns() { }
        public MethodCounterColumns(string columnName)
            : base(columnName)
        { }
		
		public MethodCounterColumns KeyColumn
		{
			get
			{
				return new MethodCounterColumns("Id");
			}
		}	
				
﻿        public MethodCounterColumns Id
        {
            get
            {
                return new MethodCounterColumns("Id");
            }
        }
﻿        public MethodCounterColumns Uuid
        {
            get
            {
                return new MethodCounterColumns("Uuid");
            }
        }
﻿        public MethodCounterColumns MethodName
        {
            get
            {
                return new MethodCounterColumns("MethodName");
            }
        }

﻿        public MethodCounterColumns CounterId
        {
            get
            {
                return new MethodCounterColumns("CounterId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(MethodCounter);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}