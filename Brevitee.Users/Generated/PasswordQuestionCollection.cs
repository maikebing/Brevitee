using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Users.Data
{
    public class PasswordQuestionCollection: DaoCollection<PasswordQuestionColumns, PasswordQuestion>
    { 
		public PasswordQuestionCollection(){}
		public PasswordQuestionCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PasswordQuestionCollection(Query<PasswordQuestionColumns, PasswordQuestion> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PasswordQuestionCollection(Query<PasswordQuestionColumns, PasswordQuestion> q, bool load) : base(q, load) { }
    }
}