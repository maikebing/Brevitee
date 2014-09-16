using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Metrics
{
    public class LoadCounterColumns: QueryFilter<LoadCounterColumns>, IFilterToken
    {
        public LoadCounterColumns() { }
        public LoadCounterColumns(string columnName)
            : base(columnName)
        { }
		
		public LoadCounterColumns KeyColumn
		{
			get
			{
				return new LoadCounterColumns("Id");
			}
		}	
				
﻿        public LoadCounterColumns Id
        {
            get
            {
                return new LoadCounterColumns("Id");
            }
        }
﻿        public LoadCounterColumns Uuid
        {
            get
            {
                return new LoadCounterColumns("Uuid");
            }
        }
﻿        public LoadCounterColumns UrlId
        {
            get
            {
                return new LoadCounterColumns("UrlId");
            }
        }

﻿        public LoadCounterColumns CounterId
        {
            get
            {
                return new LoadCounterColumns("CounterId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(LoadCounter);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}