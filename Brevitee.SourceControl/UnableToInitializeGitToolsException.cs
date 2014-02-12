using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.SourceControl
{
    public class UnableToInitializeGitToolsException: Exception
    {
        public UnableToInitializeGitToolsException(Exception innerException = null)
            : base("Unable to initialize git tools", innerException)
        { }

        public UnableToInitializeGitToolsException(string message, Exception innerException = null)
            : base("Unable to initialize git tools: {0}"._Format(message), innerException)
        { }
    }
}
