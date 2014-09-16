using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Logging.Debug
{
	public class TraceLogger: DebugLogger
	{
		public TraceLogger() : base() { }

		public override void CommitLogEvent(LogEvent logEvent)
		{
			System.Diagnostics.Trace.WriteLine(MessageFormat.NamedFormat(logEvent));
		}
	}
}
