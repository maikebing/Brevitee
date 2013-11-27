using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;
using Brevitee.Incubation;
using Castle.DynamicProxy;
using Brevitee;
using Brevitee.ServiceProxy;
using Brevitee.ServiceProxy.Js;

namespace Brevitee.Interceptors
{
    public static class ServiceProxySystemExtensions
    {
        private static void EnsureInterceptionIncubator()
        {
            InterceptionIncubator incubator = ServiceProxySystem.Incubator as InterceptionIncubator;
            if (incubator == null)
            {
                incubator = new InterceptionIncubator();
                foreach (string className in ServiceProxySystem.Incubator.ClassNames)
                {
                    Type t;
                    object instance = ServiceProxySystem.Incubator.Get(className, out t);
                    incubator.Set(t, instance);
                }

                ServiceProxySystem.Incubator = incubator;
            }
        }

        //public static T CreateClient<T>(this ServiceProxySystem system, string baseAddress) where T: class, new()
        //{
        //    return WebClientInterceptorProxy.Create<T>(baseAddress);
        //}

        /// <summary>
        /// Register the specified type as a ServiceProxy responder.
        /// </summary>
        /// <param name="type"></param>
        public static void Register(this ServiceProxySystem system, Type type, IInterceptor[] interceptors = null, params object[] ctorParams)
        {
            EnsureInterceptionIncubator();
            ServiceProxySystem.Incubator.Construct(type, ctorParams);
            ((InterceptionIncubator)ServiceProxySystem.Incubator).SetInterceptors(type, interceptors);
        }

        /// <summary>
        /// Register the specified type as a Poc responder.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>(IInterceptor[] interceptors = null, params object[] ctorParams) where T : class
        {
            EnsureInterceptionIncubator();
            ServiceProxySystem.Incubator.Construct<T>(ctorParams);
            ((InterceptionIncubator)ServiceProxySystem.Incubator).SetInterceptors<T>(interceptors);
        }
    }
}
