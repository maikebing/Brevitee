using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Data.Repositories
{
    public class DaoRepository: IRepository
    {
        #region IRepository Members

        public T Create<T>(T toCreate)
        {
            throw new NotImplementedException();
        }

        public T CreateOrUpdate<T>(T toCreateOrUpdate)
        {
            throw new NotImplementedException();
        }

        public T Retrieve<T>(int id)
        {
            throw new NotImplementedException();
        }

        public T Retrieve<T>(long id)
        {
            throw new NotImplementedException();
        }

        public T Retrieve<T>(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public T Retrieve<C, T>(WhereDelegate<C> where) where C : IQueryFilter, IFilterToken, new()
        {
            throw new NotImplementedException();
        }

        public T Update<T>(T toUpdate)
        {
            throw new NotImplementedException();
        }

        public bool Delete<T>(T toDelete)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
