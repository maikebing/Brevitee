using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerizableListColumns: QueryFilter<StickerizableListColumns>, IFilterToken
    {
        public StickerizableListColumns() { }
        public StickerizableListColumns(string columnName)
            : base(columnName)
        { }
		
		public StickerizableListColumns KeyColumn
		{
			get
			{
				return new StickerizableListColumns("Id");
			}
		}	
				
﻿        public StickerizableListColumns Id
        {
            get
            {
                return new StickerizableListColumns("Id");
            }
        }
﻿        public StickerizableListColumns Uuid
        {
            get
            {
                return new StickerizableListColumns("Uuid");
            }
        }
﻿        public StickerizableListColumns Created
        {
            get
            {
                return new StickerizableListColumns("Created");
            }
        }
﻿        public StickerizableListColumns Name
        {
            get
            {
                return new StickerizableListColumns("Name");
            }
        }
﻿        public StickerizableListColumns Description
        {
            get
            {
                return new StickerizableListColumns("Description");
            }
        }
﻿        public StickerizableListColumns Public
        {
            get
            {
                return new StickerizableListColumns("Public");
            }
        }

﻿        public StickerizableListColumns CreatorId
        {
            get
            {
                return new StickerizableListColumns("CreatorId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(StickerizableList);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}