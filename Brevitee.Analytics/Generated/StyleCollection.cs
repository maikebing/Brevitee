using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class StyleCollection: DaoCollection<StyleColumns, Style>
    { 
		public StyleCollection(){}
		public StyleCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public StyleCollection(Query<StyleColumns, Style> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public StyleCollection(Query<StyleColumns, Style> q, bool load) : base(q, load) { }
    }
}