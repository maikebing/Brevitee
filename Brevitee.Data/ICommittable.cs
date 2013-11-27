using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public interface ICommittable: IDeleteable
    {
        void Commit();
        void WriteCommit(SqlStringBuilder sql);
    }
}
