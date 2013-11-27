using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Incubation
{
    public class ConstructFailedException: Exception
    {
        public ConstructFailedException(Type type, Type[] ctorTypes)
            : base(string.Format("{0}({1}):: The constructor couldn't be found for the specified type and parameter combination",
                    type.Name, ctorTypes.ToDelimited<Type>(t => t.Name)))
        {

        }
    }
}
