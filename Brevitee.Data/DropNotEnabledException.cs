using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public class DropNotEnabledException: Exception
    {
        public DropNotEnabledException()
            : base("Drop is not enabled on the SchemaWriter.  Set EnableDrop = true if you really want to")
        { }
    }
}
