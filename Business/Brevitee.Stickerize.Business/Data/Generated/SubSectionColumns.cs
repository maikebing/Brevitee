using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class SubSectionColumns: QueryFilter<SubSectionColumns>, IFilterToken
    {
        public SubSectionColumns() { }
        public SubSectionColumns(string columnName)
            : base(columnName)
        { }
		
		public SubSectionColumns KeyColumn
		{
			get
			{
				return new SubSectionColumns("Id");
			}
		}	
				
﻿        public SubSectionColumns Id
        {
            get
            {
                return new SubSectionColumns("Id");
            }
        }
﻿        public SubSectionColumns Uuid
        {
            get
            {
                return new SubSectionColumns("Uuid");
            }
        }
﻿        public SubSectionColumns Created
        {
            get
            {
                return new SubSectionColumns("Created");
            }
        }
﻿        public SubSectionColumns Name
        {
            get
            {
                return new SubSectionColumns("Name");
            }
        }

﻿        public SubSectionColumns StickerizableListId
        {
            get
            {
                return new SubSectionColumns("StickerizableListId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(SubSection);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}