using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Data.Repositories
{
    public interface IRepository
    {
        T Create<T>(T toCreate);
        T CreateOrUpdate<T>(T toCreateOrUpdate);
        T Retrieve<T>(int id);
        T Retrieve<T>(long id);
        T Retrieve<T>(Func<T, bool> predicate);
        T Retrieve<C, T>(WhereDelegate<C> where) where C : IQueryFilter, IFilterToken, new();
        T Update<T>(T toUpdate);
        bool Delete<T>(T toDelete);
    }
}
