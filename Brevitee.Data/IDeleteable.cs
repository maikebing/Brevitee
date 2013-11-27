using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public interface IDeleteable
    {
        void Delete(Database db = null);
        void WriteDelete(SqlStringBuilder sql);
    }
}
