using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public delegate void SqlExecuteDelegate(SqlStringBuilder sql, Database db);    
}
