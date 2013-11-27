using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data.Schema
{
    public class Result
    {
        public Result(string message)
        {
            this.Message = message;
            this.Success = true;
        }

        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }

        public bool Success { get; set; }
        public string Namespace { get; set; }
        public string SchemaName { get; set; }
    }
}
