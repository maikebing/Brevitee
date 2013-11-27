using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Brevitee.Data
{
    public interface IHasDataRow
    {
        DataRow DataRow { get; set; }
    }
}
