using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Server
{
    public class ExecutionResult
    {
        public ExecutionResult()
        {
            success = true;
        }

        public ExecutionResult(object data)
            : this()
        {
            this.data = data;
        }

        public ExecutionResult(Exception ex)
        {
            this.success = false;
            this.message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
        }

        public bool success
        {
            get;
            set;
        }

        public string message
        {
            get;
            set;
        }

        public object data
        {
            get;
            set;
        }
    }
}
