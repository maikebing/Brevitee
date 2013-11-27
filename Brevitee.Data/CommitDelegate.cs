using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public delegate SqlStringBuilder CommitDelegate(string tableName, params AssignValue[] values);
}
