using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Razor
{
    public abstract class RazorBaseTemplate
    {
        public StringBuilder Generated { get; protected set; }

        public abstract void Execute();

        public virtual void Write(object value)
        {
            WriteLiteral(value);
        }

        public virtual void WriteLiteral(object value)
        {
            Generated.Append(value);
        }
    }
}
