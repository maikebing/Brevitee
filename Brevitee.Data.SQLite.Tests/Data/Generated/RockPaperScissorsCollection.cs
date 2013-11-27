using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class RockPaperScissorsCollection: DaoCollection<RockPaperScissorsColumns, RockPaperScissors>
    { 
		public RockPaperScissorsCollection(){}
		public RockPaperScissorsCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RockPaperScissorsCollection(Query<RockPaperScissorsColumns, RockPaperScissors> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RockPaperScissorsCollection(Query<RockPaperScissorsColumns, RockPaperScissors> q, bool load) : base(q, load) { }
    }
}