using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class ImageRatingColumns: QueryFilter<ImageRatingColumns>, IFilterToken
    {
        public ImageRatingColumns() { }
        public ImageRatingColumns(string columnName)
            : base(columnName)
        { }
		
		public ImageRatingColumns KeyColumn
		{
			get
			{
				return new ImageRatingColumns("Id");
			}
		}	
				
        public ImageRatingColumns Id
        {
            get
            {
                return new ImageRatingColumns("Id");
            }
        }
        public ImageRatingColumns Value
        {
            get
            {
                return new ImageRatingColumns("Value");
            }
        }

        public ImageRatingColumns ImageId
        {
            get
            {
                return new ImageRatingColumns("ImageId");
            }
        }
        public ImageRatingColumns UserIdentifierId
        {
            get
            {
                return new ImageRatingColumns("UserIdentifierId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ImageRating);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}