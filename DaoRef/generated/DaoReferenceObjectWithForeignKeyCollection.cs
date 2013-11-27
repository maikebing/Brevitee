using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.DaoRef
{
    public class DaoReferenceObjectWithForeignKeyCollection : DaoCollection<DaoReferenceObjectWithForeignKeyColumns, DaoReferenceObjectWithForeignKey>
    {
        public DaoReferenceObjectWithForeignKeyCollection() { }
        public DaoReferenceObjectWithForeignKeyCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
        public DaoReferenceObjectWithForeignKeyCollection(Query<DaoReferenceObjectWithForeignKeyColumns, DaoReferenceObjectWithForeignKey> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
        public DaoReferenceObjectWithForeignKeyCollection(Query<DaoReferenceObjectWithForeignKeyColumns, DaoReferenceObjectWithForeignKey> q, bool load) : base(q, load) { }
    }
}