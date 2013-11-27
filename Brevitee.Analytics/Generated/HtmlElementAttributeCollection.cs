using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class HtmlElementAttributeCollection: DaoCollection<HtmlElementAttributeColumns, HtmlElementAttribute>
    { 
		public HtmlElementAttributeCollection(){}
		public HtmlElementAttributeCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HtmlElementAttributeCollection(Query<HtmlElementAttributeColumns, HtmlElementAttribute> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HtmlElementAttributeCollection(Query<HtmlElementAttributeColumns, HtmlElementAttribute> q, bool load) : base(q, load) { }
    }
}