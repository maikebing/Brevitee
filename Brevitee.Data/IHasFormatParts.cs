using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public interface IHasFormatParts
    {
        List<FormatPart> Parts
        {
            get;
            set;
        }
    }
}
