using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M = Microsoft.Build.Framework;
using B = Brevitee.Logging;
using System.Reflection;

namespace Brevitee.Automation.Build
{
    public class BuildLogger<T>: BuildLogger where T: B.ILogger, new()
    {
        T _actualLogger;
        public BuildLogger()
        {
            this._actualLogger = new T();            
        }

        public void SetLoggerProperties(Dictionary<string, object> properties)
        {
            Type loggerType = typeof(T);

            properties.Keys.Each(key =>
            {
                PropertyInfo propInfo = loggerType.GetProperty(key);
                if (propInfo != null)
                {
                    propInfo.SetValue(_actualLogger, properties[key]);
                }
            });
        }

        public override void CommitLogEvent(Logging.LogEvent logEvent)
        {
            this._actualLogger.CommitLogEvent(logEvent);
        }
    }

    public abstract class BuildLogger: B.Logger, M.ILogger
    {
        #region ILogger Members
        
        public void Initialize(M.IEventSource eventSource)
        {
            this.StartLoggingThread();
            //eventSource.AnyEventRaised += (o, ba) =>
            //{
            //    AddEntry("AnyEventRaised", ba, B.LogEventType.Information);
            //};

            eventSource.BuildFinished += (o, ba) =>
            {
                if (ba.Succeeded)
                {
                    AddEntry("BuildFinished", ba, B.LogEventType.Information);
                }
                else
                {
                    AddEntry("BuildFinished", ba, B.LogEventType.Error);
                }
            };
            eventSource.BuildStarted += (o, ba) =>
            {
                AddEntry("BuildStarted", ba, B.LogEventType.Information);
            };

            eventSource.CustomEventRaised += (o, ba) =>
            {
                AddEntry("CustomEventRaised", ba, B.LogEventType.Custom);
            };

            eventSource.ErrorRaised += (o, ba) =>
            {
                AddEntry("ErrorRaised", ba, B.LogEventType.Error);
            };

            eventSource.MessageRaised += (o, ba) =>
            {
                AddEntry("MessageRaised", ba, B.LogEventType.Information);
            };

            eventSource.ProjectFinished += (o, ba) =>
            {
                AddEntry("ProjectFinished", ba, B.LogEventType.Information);
            };

            eventSource.ProjectStarted += (o, ba) =>
            {
                AddEntry("ProjectStarted", ba, B.LogEventType.Information);
            };

            //eventSource.StatusEventRaised += (o, ba) =>
            //{
            //    AddEntry("StatusEventRaised", ba, B.LogEventType.Information);
            //};

            eventSource.TargetFinished += (o, ba) =>
            {
                AddEntry("TargetFinished", ba, B.LogEventType.Information);
            };

            eventSource.TargetStarted += (o, ba) =>
            {
                AddEntry("TargetStarted", ba, B.LogEventType.Information);
            };

            eventSource.TaskFinished += (o, ba) =>
            {
                AddEntry("TaskFinished", ba, B.LogEventType.Information);
            };

            eventSource.TaskStarted += (o, ba) =>
            {
                AddEntry("TaskStarted", ba, B.LogEventType.Information);
            };

            eventSource.WarningRaised += (o, ba) =>
            {
                AddEntry("WarningRaised", ba, B.LogEventType.Warning);
            };
        }

        protected override StringBuilder HandleDetails(B.LogEvent ev)
        {
            return new StringBuilder(ev.Message);
        }

        protected override void HandleStackTrace(Exception ex, StringBuilder message, StringBuilder stack)
        {
            // turn off the stack trace
        }

        protected virtual void AddEntry(string eventName, M.BuildEventArgs ba, B.LogEventType entryType)
        {
            BuildEventInfo info = new BuildEventInfo(ba);
            AddEntry("{0}\t,{1}"._Format(eventName, info.ToCsvLine()), entryType);
        }

        public string Parameters
        {
            get;
            set;
        }

        public void Shutdown()
        {
            BlockUntilEventQueueIsEmpty();
            StopLoggingThread();
        }

        public M.LoggerVerbosity Verbosity
        {
            get;
            set;
        }

        #endregion
    }
}
