using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.ServiceProxy;

namespace Brevitee.Server
{
    public abstract class ResultBase
    {
        public object Value { get; set; }

        public abstract void Execute(IHttpContext context);
    }
}
