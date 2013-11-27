using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class HtmlElementColumns: QueryFilter<HtmlElementColumns>, IFilterToken
    {
        public HtmlElementColumns() { }
        public HtmlElementColumns(string columnName)
            : base(columnName)
        { }
		
		public HtmlElementColumns KeyColumn
		{
			get
			{
				return new HtmlElementColumns("Id");
			}
		}	
				
        public HtmlElementColumns Id
        {
            get
            {
                return new HtmlElementColumns("Id");
            }
        }
        public HtmlElementColumns DomId
        {
            get
            {
                return new HtmlElementColumns("DomId");
            }
        }
        public HtmlElementColumns TagName
        {
            get
            {
                return new HtmlElementColumns("TagName");
            }
        }

        public HtmlElementColumns UrlId
        {
            get
            {
                return new HtmlElementColumns("UrlId");
            }
        }
        public HtmlElementColumns ContentId
        {
            get
            {
                return new HtmlElementColumns("ContentId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(HtmlElement);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}