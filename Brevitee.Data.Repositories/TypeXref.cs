using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Data.Repositories
{
    /// <summary>
    /// Used to describe a many to many 
    /// relationship between two types.
    /// This would imply that each type
    /// has an IEnumerable property
    /// of the other type
    /// </summary>
    public class TypeXref
    {
        public Type Left { get; set; }
        public Type Right { get; set; }
    }
}
