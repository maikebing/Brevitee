using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class HtmlElementStyleColumns: QueryFilter<HtmlElementStyleColumns>, IFilterToken
    {
        public HtmlElementStyleColumns() { }
        public HtmlElementStyleColumns(string columnName)
            : base(columnName)
        { }
		
		public HtmlElementStyleColumns KeyColumn
		{
			get
			{
				return new HtmlElementStyleColumns("Id");
			}
		}	
				
        public HtmlElementStyleColumns Id
        {
            get
            {
                return new HtmlElementStyleColumns("Id");
            }
        }

        public HtmlElementStyleColumns HtmlElementId
        {
            get
            {
                return new HtmlElementStyleColumns("HtmlElementId");
            }
        }
        public HtmlElementStyleColumns StyleId
        {
            get
            {
                return new HtmlElementStyleColumns("StyleId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(HtmlElementStyle);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}