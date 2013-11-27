using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.ServiceProxy
{
    public delegate void DocAttributeRenderDelegate(Dictionary<Type, DocInfo[]> docInfosByType, StringBuilder renderInto);
    
}
