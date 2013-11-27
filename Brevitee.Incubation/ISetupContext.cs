using System;

namespace Brevitee.Incubation
{
    public interface ISetupContext
    {
        T Construct<T>(params object[] ctorParams);
        T Construct<T>(params Type[] ctorParamTypes);
        T Get<T>();
        T Get<T>(params object[] ctorParams);
        T Get<T>(params Type[] ctorParamTypes);
        void Set<T>(T instance);
        object this[Type type] { get; set; }
    }
}
