using System;
using Brevitee.Incubation;

namespace Brevitee.Data
{
    public interface IDatabaseInitializer
    {
        DatabaseInitializationResult Initialize(string connectionName);
        void Ignore(params Type[] types);
        void Ignore(params string[] connectionNames);
    }
}
