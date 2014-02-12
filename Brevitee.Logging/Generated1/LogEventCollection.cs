using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Logging.Data
{
    public class LogEventCollection: DaoCollection<LogEventColumns, LogEvent>
    { 
		public LogEventCollection(){}
		public LogEventCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		
    }
}