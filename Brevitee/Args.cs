using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Brevitee
{
    public class Args
    {
        public static void ThrowIfNull(object param, string paramName = "param")
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ThrowIfNullOrEmpty(string param, string paramName = "param")
        {
            if (string.IsNullOrEmpty(param))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ThrowIf<E>(bool condition, string msgFormat, params object[] values) where E: Exception
        {
            if(condition)
            {
                throw Exception<E>(msgFormat, values);
            }
        }

        public static void Throw<E>(string msgFormat, params object[] values) where E: Exception
        {
            throw Exception<E>(msgFormat, values);
        }

        public static Exception Exception(string msgFormat, params object[] values)
        {
            return Exception<Exception>(msgFormat, values);
        }

        public static E Exception<E>(string msgFormat, params object[] values) where E : Exception
        {
            return Exception<E>(msgFormat, null, values);
        }

        public static E Exception<E>(string msgFormat, Exception innerException, params object[] values) where E : Exception
        {
            ConstructorInfo ctor  = typeof(E).GetConstructor(new Type[] { typeof(string), typeof(Exception) });
            return (E)ctor.Invoke(new object[] { string.Format(msgFormat, values), innerException });
        }
    }
}
