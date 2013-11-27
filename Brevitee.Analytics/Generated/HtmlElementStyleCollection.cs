using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class HtmlElementStyleCollection: DaoCollection<HtmlElementStyleColumns, HtmlElementStyle>
    { 
		public HtmlElementStyleCollection(){}
		public HtmlElementStyleCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HtmlElementStyleCollection(Query<HtmlElementStyleColumns, HtmlElementStyle> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HtmlElementStyleCollection(Query<HtmlElementStyleColumns, HtmlElementStyle> q, bool load) : base(q, load) { }
    }
}