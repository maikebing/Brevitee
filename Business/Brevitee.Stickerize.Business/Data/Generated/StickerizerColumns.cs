using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerizerColumns: QueryFilter<StickerizerColumns>, IFilterToken
    {
        public StickerizerColumns() { }
        public StickerizerColumns(string columnName)
            : base(columnName)
        { }
		
		public StickerizerColumns KeyColumn
		{
			get
			{
				return new StickerizerColumns("Id");
			}
		}	
				
﻿        public StickerizerColumns Id
        {
            get
            {
                return new StickerizerColumns("Id");
            }
        }
﻿        public StickerizerColumns Uuid
        {
            get
            {
                return new StickerizerColumns("Uuid");
            }
        }
﻿        public StickerizerColumns Created
        {
            get
            {
                return new StickerizerColumns("Created");
            }
        }
﻿        public StickerizerColumns Name
        {
            get
            {
                return new StickerizerColumns("Name");
            }
        }
﻿        public StickerizerColumns DisplayName
        {
            get
            {
                return new StickerizerColumns("DisplayName");
            }
        }
﻿        public StickerizerColumns UserName
        {
            get
            {
                return new StickerizerColumns("UserName");
            }
        }

﻿        public StickerizerColumns ImageId
        {
            get
            {
                return new StickerizerColumns("ImageId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Stickerizer);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}