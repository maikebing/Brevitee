using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.DaoRef
{
    public class DaoReferenceObjectCollection : DaoCollection<DaoReferenceObjectColumns, DaoReferenceObject>
    {
        public DaoReferenceObjectCollection() { }
        public DaoReferenceObjectCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
        public DaoReferenceObjectCollection(Query<DaoReferenceObjectColumns, DaoReferenceObject> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
        public DaoReferenceObjectCollection(Query<DaoReferenceObjectColumns, DaoReferenceObject> q, bool load) : base(q, load) { }
    }
}