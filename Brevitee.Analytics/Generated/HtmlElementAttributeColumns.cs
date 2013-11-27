using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class HtmlElementAttributeColumns: QueryFilter<HtmlElementAttributeColumns>, IFilterToken
    {
        public HtmlElementAttributeColumns() { }
        public HtmlElementAttributeColumns(string columnName)
            : base(columnName)
        { }
		
		public HtmlElementAttributeColumns KeyColumn
		{
			get
			{
				return new HtmlElementAttributeColumns("Id");
			}
		}	
				
        public HtmlElementAttributeColumns Id
        {
            get
            {
                return new HtmlElementAttributeColumns("Id");
            }
        }

        public HtmlElementAttributeColumns HtmlElementId
        {
            get
            {
                return new HtmlElementAttributeColumns("HtmlElementId");
            }
        }
        public HtmlElementAttributeColumns AttributeId
        {
            get
            {
                return new HtmlElementAttributeColumns("AttributeId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(HtmlElementAttribute);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}