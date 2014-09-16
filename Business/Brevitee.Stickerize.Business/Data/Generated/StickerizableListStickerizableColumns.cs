using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerizableListStickerizableColumns: QueryFilter<StickerizableListStickerizableColumns>, IFilterToken
    {
        public StickerizableListStickerizableColumns() { }
        public StickerizableListStickerizableColumns(string columnName)
            : base(columnName)
        { }
		
		public StickerizableListStickerizableColumns KeyColumn
		{
			get
			{
				return new StickerizableListStickerizableColumns("Id");
			}
		}	
				
﻿        public StickerizableListStickerizableColumns Id
        {
            get
            {
                return new StickerizableListStickerizableColumns("Id");
            }
        }

﻿        public StickerizableListStickerizableColumns StickerizableListId
        {
            get
            {
                return new StickerizableListStickerizableColumns("StickerizableListId");
            }
        }
﻿        public StickerizableListStickerizableColumns StickerizableId
        {
            get
            {
                return new StickerizableListStickerizableColumns("StickerizableId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(StickerizableListStickerizable);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}