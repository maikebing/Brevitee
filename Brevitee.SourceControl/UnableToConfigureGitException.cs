using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.SourceControl
{
    public class UnableToConfigureGitException: Exception
    {
        public UnableToConfigureGitException(string message) : base(message) { }
    }
}
