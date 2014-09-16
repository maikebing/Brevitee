using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class StickerizerStickerizeeColumns: QueryFilter<StickerizerStickerizeeColumns>, IFilterToken
    {
        public StickerizerStickerizeeColumns() { }
        public StickerizerStickerizeeColumns(string columnName)
            : base(columnName)
        { }
		
		public StickerizerStickerizeeColumns KeyColumn
		{
			get
			{
				return new StickerizerStickerizeeColumns("Id");
			}
		}	
				
﻿        public StickerizerStickerizeeColumns Id
        {
            get
            {
                return new StickerizerStickerizeeColumns("Id");
            }
        }

﻿        public StickerizerStickerizeeColumns StickerizerId
        {
            get
            {
                return new StickerizerStickerizeeColumns("StickerizerId");
            }
        }
﻿        public StickerizerStickerizeeColumns StickerizeeId
        {
            get
            {
                return new StickerizerStickerizeeColumns("StickerizeeId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(StickerizerStickerizee);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}