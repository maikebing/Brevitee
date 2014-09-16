using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.DaoReferenceObjects.Data
{
    public class DaoReferenceObjectCollection: DaoCollection<DaoReferenceObjectColumns, DaoReferenceObject>
    { 
		public DaoReferenceObjectCollection(){}
		public DaoReferenceObjectCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		
    }
}