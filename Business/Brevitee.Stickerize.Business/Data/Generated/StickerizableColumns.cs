using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerizableColumns: QueryFilter<StickerizableColumns>, IFilterToken
    {
        public StickerizableColumns() { }
        public StickerizableColumns(string columnName)
            : base(columnName)
        { }
		
		public StickerizableColumns KeyColumn
		{
			get
			{
				return new StickerizableColumns("Id");
			}
		}	
				
﻿        public StickerizableColumns Id
        {
            get
            {
                return new StickerizableColumns("Id");
            }
        }
﻿        public StickerizableColumns Uuid
        {
            get
            {
                return new StickerizableColumns("Uuid");
            }
        }
﻿        public StickerizableColumns Created
        {
            get
            {
                return new StickerizableColumns("Created");
            }
        }
﻿        public StickerizableColumns Name
        {
            get
            {
                return new StickerizableColumns("Name");
            }
        }
﻿        public StickerizableColumns Description
        {
            get
            {
                return new StickerizableColumns("Description");
            }
        }
﻿        public StickerizableColumns For
        {
            get
            {
                return new StickerizableColumns("For");
            }
        }

﻿        public StickerizableColumns CreatorId
        {
            get
            {
                return new StickerizableColumns("CreatorId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Stickerizable);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}