using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerizeeColumns: QueryFilter<StickerizeeColumns>, IFilterToken
    {
        public StickerizeeColumns() { }
        public StickerizeeColumns(string columnName)
            : base(columnName)
        { }
		
		public StickerizeeColumns KeyColumn
		{
			get
			{
				return new StickerizeeColumns("Id");
			}
		}	
				
﻿        public StickerizeeColumns Id
        {
            get
            {
                return new StickerizeeColumns("Id");
            }
        }
﻿        public StickerizeeColumns Uuid
        {
            get
            {
                return new StickerizeeColumns("Uuid");
            }
        }
﻿        public StickerizeeColumns Created
        {
            get
            {
                return new StickerizeeColumns("Created");
            }
        }
﻿        public StickerizeeColumns Name
        {
            get
            {
                return new StickerizeeColumns("Name");
            }
        }
﻿        public StickerizeeColumns DisplayName
        {
            get
            {
                return new StickerizeeColumns("DisplayName");
            }
        }
﻿        public StickerizeeColumns Gender
        {
            get
            {
                return new StickerizeeColumns("Gender");
            }
        }
﻿        public StickerizeeColumns UserName
        {
            get
            {
                return new StickerizeeColumns("UserName");
            }
        }

﻿        public StickerizeeColumns ImageId
        {
            get
            {
                return new StickerizeeColumns("ImageId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Stickerizee);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}