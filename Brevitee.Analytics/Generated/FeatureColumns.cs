using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class FeatureColumns: QueryFilter<FeatureColumns>, IFilterToken
    {
        public FeatureColumns() { }
        public FeatureColumns(string columnName)
            : base(columnName)
        { }
		
		public FeatureColumns KeyColumn
		{
			get
			{
				return new FeatureColumns("Id");
			}
		}	
				
        public FeatureColumns Id
        {
            get
            {
                return new FeatureColumns("Id");
            }
        }
        public FeatureColumns Value
        {
            get
            {
                return new FeatureColumns("Value");
            }
        }
        public FeatureColumns FeatureToCategoryCount
        {
            get
            {
                return new FeatureColumns("FeatureToCategoryCount");
            }
        }

        public FeatureColumns CategoryId
        {
            get
            {
                return new FeatureColumns("CategoryId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Feature);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}