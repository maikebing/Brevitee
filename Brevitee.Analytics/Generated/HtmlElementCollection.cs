using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class HtmlElementCollection: DaoCollection<HtmlElementColumns, HtmlElement>
    { 
		public HtmlElementCollection(){}
		public HtmlElementCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HtmlElementCollection(Query<HtmlElementColumns, HtmlElement> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HtmlElementCollection(Query<HtmlElementColumns, HtmlElement> q, bool load) : base(q, load) { }
    }
}