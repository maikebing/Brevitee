using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Logging;
using Brevitee;

namespace Brevitee.CommandLine
{
    public class ConsoleLogger : Logger
    {
        public ConsoleLogger()
            : base()
        {
            this.AddDetails = true;
        }
        public bool UseColors { get; set; }
        public bool AddDetails { get; set; }

        protected override StringBuilder HandleDetails(LogEvent ev)
        {
            if (AddDetails)
            {
                return base.HandleDetails(ev);
            }
            else
            {
                return new StringBuilder();
            }
        }

        public override void CommitLogEvent(LogEvent logEvent)
        {
            if (AddDetails)
            {
                ShowDetails(logEvent);
            }

            if (UseColors)
            {
                switch (logEvent.Severity)
                {
                    case LogEventType.None:
                        ConsoleExtensions.SetTextColor(ConsoleColor.Cyan);
                        break;
                    case LogEventType.Information:
                        ConsoleExtensions.SetTextColor(ConsoleColor.Gray);
                        break;
                    case LogEventType.Warning:
                        ConsoleExtensions.SetTextColor(ConsoleColor.Yellow);
                        break;
                    case LogEventType.Error:
                        ConsoleExtensions.SetTextColor(ConsoleColor.Magenta);
                        break;
                    case LogEventType.Fatal:
                        ConsoleExtensions.SetTextColor(ConsoleColor.Red);
                        break;
                    default:
                        break;
                }
            }
            
            Console.WriteLine(logEvent.Message);
            ConsoleExtensions.SetTextColor();
        }

        private static void ShowDetails(LogEvent logEvent)
        {
            Console.WriteLine(logEvent.Severity.ToString());
            Console.WriteLine("Computer: {0}", logEvent.Computer);
            string[] split = logEvent.Message.Split(new string[] { "~~" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length - 1; i++)
            {
                Console.WriteLine(split[i]);
            }
        }
    }
}
