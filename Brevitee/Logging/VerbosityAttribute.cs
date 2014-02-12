using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Logging
{
    [AttributeUsage(AttributeTargets.Event)]
    public class VerbosityAttribute: Attribute
    {
        public VerbosityAttribute(LogEventType eventType)
        {
            this.Value = eventType;
        }

        public LogEventType Value { get; set; }

        public string MessageFormat { get; set; }

        public bool TryGetMessage(object value, out string message)
        {
            try
            {
                message = value.PropertiesToString();
                if (!string.IsNullOrEmpty(MessageFormat))
                {
                    message = MessageFormat.NamedFormat(value);
                }

                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}
