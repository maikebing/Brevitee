using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.core
{
    public abstract class ResultBase
    {
        public object Value { get; set; }

        public abstract void Execute(IContext context);
    }
}
