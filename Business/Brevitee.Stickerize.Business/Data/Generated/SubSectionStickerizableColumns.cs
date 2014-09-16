using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class SubSectionStickerizableColumns: QueryFilter<SubSectionStickerizableColumns>, IFilterToken
    {
        public SubSectionStickerizableColumns() { }
        public SubSectionStickerizableColumns(string columnName)
            : base(columnName)
        { }
		
		public SubSectionStickerizableColumns KeyColumn
		{
			get
			{
				return new SubSectionStickerizableColumns("Id");
			}
		}	
				
﻿        public SubSectionStickerizableColumns Id
        {
            get
            {
                return new SubSectionStickerizableColumns("Id");
            }
        }

﻿        public SubSectionStickerizableColumns SubSectionId
        {
            get
            {
                return new SubSectionStickerizableColumns("SubSectionId");
            }
        }
﻿        public SubSectionStickerizableColumns StickerizableId
        {
            get
            {
                return new SubSectionStickerizableColumns("StickerizableId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(SubSectionStickerizable);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}