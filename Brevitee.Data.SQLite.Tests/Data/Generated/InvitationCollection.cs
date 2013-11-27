using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class InvitationCollection: DaoCollection<InvitationColumns, Invitation>
    { 
		public InvitationCollection(){}
		public InvitationCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public InvitationCollection(Query<InvitationColumns, Invitation> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public InvitationCollection(Query<InvitationColumns, Invitation> q, bool load) : base(q, load) { }
    }
}