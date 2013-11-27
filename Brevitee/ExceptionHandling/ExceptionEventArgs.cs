using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.ExceptionHandling
{
    public class ExceptionEventArgs: EventArgs
    {
        public ExceptionEventArgs(Exception ex)
        {
            this.Exception = ex;
        }

        public Exception Exception { get; private set; }
    }
}
