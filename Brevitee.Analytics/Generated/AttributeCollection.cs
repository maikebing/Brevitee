using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class AttributeCollection: DaoCollection<AttributeColumns, Attribute>
    { 
		public AttributeCollection(){}
		public AttributeCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AttributeCollection(Query<AttributeColumns, Attribute> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AttributeCollection(Query<AttributeColumns, Attribute> q, bool load) : base(q, load) { }
    }
}