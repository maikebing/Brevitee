using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using Brevitee.Configuration;

namespace Brevitee.Logging
{
    public class DiagnosticDetails
    {
        public const string DefaultMessageFormat = "Thread=#{ThreadHashCode}({ThreadId})~~App={ApplicationName}~~PID={ProcessId}~~Utc={UtcShortDate}::{UtcShortTime}~~{Message}";
        
        public DiagnosticDetails()
        {
            this.NamedMessageFormat = DefaultMessageFormat;
        }

        public DiagnosticDetails(LogEvent logEvent)
            : this()
        {
            this.Message = logEvent.Message;
            this.Utc = DateTime.UtcNow;
        }

        public override string ToString()
        {
            object names = new {
                ThreadHashCode = ThreadHashCode.ToString(),
                ThreadId = ThreadId.ToString(),
                ApplicationName = ApplicationName,
                ProcessId = ProcessId.ToString(),
                UtcShortDate = Utc.ToShortDateString(),
                UtcShortTime = Utc.ToShortTimeString(),
                Message = Message
            };
            return NamedMessageFormat.NamedFormat(names);
        }

        public string NamedMessageFormat
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public int ProcessId
        {
            get
            {
                return Process.GetCurrentProcess().Id;
            }
        }

        public DateTime Utc
        {
            get;
            set;
        }

        public int ThreadHashCode
        {
            get
            {
                return Thread.CurrentThread.GetHashCode();
            }
        }

        public int ThreadId
        {
            get
            {
                return Thread.CurrentThread.ManagedThreadId;
            }
        }
        
        string appName;
        public string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(appName) || appName.Equals("UNKOWN"))
                {
                    appName = DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN");
                }
                return appName;
            }
            private set
            {
                appName = value;
            }
        }
    }
}
