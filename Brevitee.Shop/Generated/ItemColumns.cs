using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Shop
{
    public class ItemColumns: QueryFilter<ItemColumns>, IFilterToken
    {
        public ItemColumns() { }
        public ItemColumns(string columnName)
            : base(columnName)
        { }
		
		public ItemColumns KeyColumn
		{
			get
			{
				return new ItemColumns("Id");
			}
		}	
				
        public ItemColumns Id
        {
            get
            {
                return new ItemColumns("Id");
            }
        }
        public ItemColumns Uuid
        {
            get
            {
                return new ItemColumns("Uuid");
            }
        }
        public ItemColumns Name
        {
            get
            {
                return new ItemColumns("Name");
            }
        }
        public ItemColumns Source
        {
            get
            {
                return new ItemColumns("Source");
            }
        }
        public ItemColumns SourceId
        {
            get
            {
                return new ItemColumns("SourceId");
            }
        }
        public ItemColumns DetailUrl
        {
            get
            {
                return new ItemColumns("DetailUrl");
            }
        }
        public ItemColumns ImageSrc
        {
            get
            {
                return new ItemColumns("ImageSrc");
            }
        }
        public ItemColumns Price
        {
            get
            {
                return new ItemColumns("Price");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Item);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}