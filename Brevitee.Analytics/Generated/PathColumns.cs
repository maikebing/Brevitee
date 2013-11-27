using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class PathColumns: QueryFilter<PathColumns>, IFilterToken
    {
        public PathColumns() { }
        public PathColumns(string columnName)
            : base(columnName)
        { }
		
		public PathColumns KeyColumn
		{
			get
			{
				return new PathColumns("Id");
			}
		}	
				
        public PathColumns Id
        {
            get
            {
                return new PathColumns("Id");
            }
        }
        public PathColumns Value
        {
            get
            {
                return new PathColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Path);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}