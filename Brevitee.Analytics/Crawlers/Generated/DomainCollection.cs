using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class DomainCollection: DaoCollection<DomainColumns, Domain>
    { 
		public DomainCollection(){}
		public DomainCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DomainCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DomainCollection(Query<DomainColumns, Domain> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DomainCollection(Database db, Query<DomainColumns, Domain> q, bool load) : base(db, q, load) { }
		public DomainCollection(Query<DomainColumns, Domain> q, bool load) : base(q, load) { }
    }
}