using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerizationColumns: QueryFilter<StickerizationColumns>, IFilterToken
    {
        public StickerizationColumns() { }
        public StickerizationColumns(string columnName)
            : base(columnName)
        { }
		
		public StickerizationColumns KeyColumn
		{
			get
			{
				return new StickerizationColumns("Id");
			}
		}	
				
﻿        public StickerizationColumns Id
        {
            get
            {
                return new StickerizationColumns("Id");
            }
        }
﻿        public StickerizationColumns Uuid
        {
            get
            {
                return new StickerizationColumns("Uuid");
            }
        }
﻿        public StickerizationColumns Created
        {
            get
            {
                return new StickerizationColumns("Created");
            }
        }
﻿        public StickerizationColumns ForDate
        {
            get
            {
                return new StickerizationColumns("ForDate");
            }
        }
﻿        public StickerizationColumns UndoneAt
        {
            get
            {
                return new StickerizationColumns("UndoneAt");
            }
        }
﻿        public StickerizationColumns IsUndone
        {
            get
            {
                return new StickerizationColumns("IsUndone");
            }
        }

﻿        public StickerizationColumns StickerId
        {
            get
            {
                return new StickerizationColumns("StickerId");
            }
        }
﻿        public StickerizationColumns StickerizerId
        {
            get
            {
                return new StickerizationColumns("StickerizerId");
            }
        }
﻿        public StickerizationColumns StickerizableId
        {
            get
            {
                return new StickerizationColumns("StickerizableId");
            }
        }
﻿        public StickerizationColumns StickerizeeId
        {
            get
            {
                return new StickerizationColumns("StickerizeeId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Stickerization);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}