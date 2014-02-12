using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Brevitee;
using Brevitee.Configuration;

namespace Brevitee.Logging
{
    public abstract class Loggable: ILoggable
    {
        public Loggable()
        {
            this._subscribers = new List<ILogger>();
            this.LogVerbosity = LogEventType.Custom;
        }

        /// <summary>
        /// A value from 0 - 5, represented by the LogEventType enum.
        /// The higher the value the more log entries will 
        /// be logged.
        /// </summary>
        public LogEventType LogVerbosity { get; set; }

        List<ILogger> _subscribers;
        /// <summary>
        /// An array of all the ILoggers that have
        /// been subscribed to this Loggable
        /// </summary>
        public ILogger[] Subscribers
        {
            get { return _subscribers.ToArray(); }
        }

        object _subscriberLock = new object();
        /// <summary>
        /// Subscribe the specified logger to
        /// all the events of the current type
        /// where the event delegate is defined
        /// as an EventHandler.  This method 
        /// will also take into account the 
        /// current value of LogVerbosity if
        /// the events found are addorned with the 
        /// Verbosity attribute
        /// </summary>
        /// <param name="logger"></param>
        public void Subscribe(ILogger logger)
        {
            lock (_subscriberLock)
            {
                if (!IsSubscribed(logger))
                {
                    _subscribers.Add(logger);
                    Type currentType = this.GetType();
                    EventInfo[] eventInfos = currentType.GetEvents();
                    eventInfos.Each(eventInfo =>
                    {
                        VerbosityAttribute verbosity;
                        bool shouldSubscribe = true;
                        LogEventType logEventType = LogEventType.Information;
                        if (eventInfo.HasCustomAttributeOfType<VerbosityAttribute>(out verbosity))
                        {
                            shouldSubscribe = (int)verbosity.Value <= (int)LogVerbosity;
                            logEventType = verbosity.Value;
                        }

                        if (shouldSubscribe)
                        {
                            if (eventInfo.EventHandlerType.Equals(typeof(EventHandler)))
                            {
                                eventInfo.AddEventHandler(this, (EventHandler)((s, a) =>
                                {
                                    string message = "";
                                    if(verbosity != null)
                                    {
                                        if (!verbosity.TryGetMessage(this, out message))
                                        {
                                            verbosity.TryGetMessage(a, out message);
                                        }
                                    }
                                    logger.AddEntry("Event {0} raised on type {1}::{2}", logEventType, eventInfo.Name, currentType.Name, message);
                                }));
                            }
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Returns true if the specified logger is 
        /// subscribed to the current Loggable
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public bool IsSubscribed(ILogger logger)
        {
            return _subscribers.Contains(logger);
        }
    }
}
