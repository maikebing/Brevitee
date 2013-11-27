using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data.Schema
{
    public class NamespaceNotSpecifiedException: Exception
    {
        public NamespaceNotSpecifiedException() : base("Namespace was not specified") { }
    }
}
