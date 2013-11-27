using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            this.Success = true;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
