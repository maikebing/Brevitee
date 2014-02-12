using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;
using Brevitee.Logging.Data;

namespace Brevitee.Logging
{
    /// <summary>
    /// A basic database logger.  Logs all entries
    /// to a single table called LogEvent
    /// </summary>
    public class DaoLogger: Logger
    {
        public override void CommitLogEvent(LogEvent logEvent)
        {
            Data.LogEvent logData = new Data.LogEvent();
            logData.Source = logEvent.Source.First(4000);
            logData.Category = logEvent.Category.First(4000);
            logData.EventID = logEvent.EventID;
            logData.User = logEvent.User.First(4000);
            logData.Time = logEvent.Time;
            logData.MessageSignature = logEvent.MessageSignature.First(4000);
            logData.MessageVariableValues = logEvent.MessageVariableValues.ToDelimited(v => v, ",").First(4000);
            logData.Message = logEvent.Message.First(4000);
            logData.Computer = logEvent.Computer.First(4000);
            logData.Severity = logEvent.Severity.ToString().First(4000);

            logData.Commit();
        }
    }
}
