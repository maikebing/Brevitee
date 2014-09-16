using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerColumns: QueryFilter<StickerColumns>, IFilterToken
    {
        public StickerColumns() { }
        public StickerColumns(string columnName)
            : base(columnName)
        { }
		
		public StickerColumns KeyColumn
		{
			get
			{
				return new StickerColumns("Id");
			}
		}	
				
﻿        public StickerColumns Id
        {
            get
            {
                return new StickerColumns("Id");
            }
        }
﻿        public StickerColumns Uuid
        {
            get
            {
                return new StickerColumns("Uuid");
            }
        }
﻿        public StickerColumns Created
        {
            get
            {
                return new StickerColumns("Created");
            }
        }
﻿        public StickerColumns Name
        {
            get
            {
                return new StickerColumns("Name");
            }
        }
﻿        public StickerColumns Description
        {
            get
            {
                return new StickerColumns("Description");
            }
        }

﻿        public StickerColumns ImageId
        {
            get
            {
                return new StickerColumns("ImageId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Sticker);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}