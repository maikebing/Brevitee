using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleData
{
    public partial class ItemProperty
    {
        public static implicit operator string(ItemProperty p)
        {
            return p.Value;
        }
    }
}
