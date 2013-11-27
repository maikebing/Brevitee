using System;
using System.Collections.Generic;
using System.Text;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Logging.Data.Qi
{
	[Proxy("logEvent")]
    public class LogEvent
    {	
		public object OneWhere(QiQuery query)
		{
			return Data.LogEvent.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return Data.LogEvent.Where(query).ToJsonSafe();
		}
	}
}