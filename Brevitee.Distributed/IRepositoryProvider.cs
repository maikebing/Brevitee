using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Distributed
{
    public interface IRepositoryProvider
    {
        void Create(object value);
        T Retrieve<T>(object keyOwner);
        void Update(object value);
        void Delete(object value);
        T[] Search<T>(object query);
    }
}
