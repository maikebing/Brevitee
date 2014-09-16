using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Incubation;

namespace Brevitee.Data
{
    public class SqlClientRegistrarCaller: IRegistrarCaller
    {
        public void Register(Database database)
        {
            SqlClientRegistrar.Register(database);
        }

        public void Register(string connectionName)
        {
            SqlClientRegistrar.Register(connectionName);
        }

        public void Register(Type daoType)
        {
            SqlClientRegistrar.Register(daoType);
        }

        public void Register<T>() where T : Dao
        {
            SqlClientRegistrar.Register<T>();
        }

        public void Register(Incubator incubator)
        {
            SqlClientRegistrar.Register(incubator);
        }
    }
}
