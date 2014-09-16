using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class ImageColumns: QueryFilter<ImageColumns>, IFilterToken
    {
        public ImageColumns() { }
        public ImageColumns(string columnName)
            : base(columnName)
        { }
		
		public ImageColumns KeyColumn
		{
			get
			{
				return new ImageColumns("Id");
			}
		}	
				
﻿        public ImageColumns Id
        {
            get
            {
                return new ImageColumns("Id");
            }
        }
﻿        public ImageColumns Uuid
        {
            get
            {
                return new ImageColumns("Uuid");
            }
        }
﻿        public ImageColumns Url
        {
            get
            {
                return new ImageColumns("Url");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Image);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}