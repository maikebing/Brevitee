using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleData
{
    public class NullUser: User
    {
        public NullUser()
        {
            this.Id = -1;
        }

        public override bool IsNull
        {
            get
            {
                return true;
            }
        }

        public override void Commit()
        {
            
        }

        protected override void Delete<C>(Func<C, Brevitee.Data.IQueryFilter<C>> where)
        {
            
        }

        public override void Delete(Brevitee.Data.Database database = null)
        {
            
        }

        protected override void Delete(Brevitee.Data.IQueryFilter filter)
        {
            
        }
     
        public override void WriteCommit(Brevitee.Data.SqlStringBuilder sqlStringBuilder)
        {
            
        }

        public override void WriteCommit(Brevitee.Data.SqlStringBuilder sqlStringBuilder, Brevitee.Data.Database db)
        {
            
        }

        public override void WriteDelete(Brevitee.Data.SqlStringBuilder sql)
        {
            
        }

        protected override void WriteDelete(Brevitee.Data.SqlStringBuilder sqlStringBuilder, Brevitee.Data.IQueryFilter filter)
        {
            
        }
    }
}
