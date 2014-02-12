using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Logging
{
    public class ComputerNameCollection: DaoCollection<ComputerNameColumns, ComputerName>
    { 
		public ComputerNameCollection(){}
		public ComputerNameCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ComputerNameCollection(Query<ComputerNameColumns, ComputerName> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ComputerNameCollection(Query<ComputerNameColumns, ComputerName> q, bool load) : base(q, load) { }
    }
}