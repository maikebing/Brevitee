using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Logging
{
    public class SignatureCollection: DaoCollection<SignatureColumns, Signature>
    { 
		public SignatureCollection(){}
		public SignatureCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SignatureCollection(Query<SignatureColumns, Signature> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SignatureCollection(Query<SignatureColumns, Signature> q, bool load) : base(q, load) { }
    }
}