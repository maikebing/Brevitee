using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data.Schema
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

        static Action<string> _defaultInspector = (s) => { Console.WriteLine(s); };
        public static Action<string> DefaultInspector
        {
            get
            {
                return _defaultInspector;
            }
            set
            {
                _defaultInspector = value;
            }
        }
    }
}
