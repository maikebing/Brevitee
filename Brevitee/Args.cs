using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Brevitee.Configuration;

namespace Brevitee
{
    public class Args
    {
        /// <summary>
        /// Throw an ArgumentNullException if the specified 
        /// param is null
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramName"></param>
        public static void ThrowIfNull(object param, string paramName = "param")
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Throw an ArgumentNullException if the specified
        /// string is null or an empty string
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramName"></param>
        public static void ThrowIfNullOrEmpty(string param, string paramName = "param")
        {
            if (string.IsNullOrEmpty(param))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Throw an exception of generic type E if the specified
        /// condition is true using the specified format and
        /// format values to define the message of the exception
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="condition"></param>
        /// <param name="msgFormat"></param>
        /// <param name="values"></param>
        public static void ThrowIf<E>(bool condition, string msgFormat, params object[] values) where E: Exception
        {
            if(condition)
            {
                throw Exception<E>(msgFormat, values);
            }
        }

        /// <summary>
        /// Throw an exception of generic type E using the specified
        /// format and format values to define the message of the
        /// exception
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="msgFormat"></param>
        /// <param name="values"></param>
        public static void Throw<E>(string msgFormat, params object[] values) where E: Exception
        {
            throw Exception<E>(msgFormat, values);
        }

        /// <summary>
        /// Create an exception using the specified format and
        /// format values to define the message of the exception
        /// </summary>
        /// <param name="msgFormat"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Exception Exception(string msgFormat, params object[] values)
        {
            return Exception<Exception>(msgFormat, values);
        }

        /// <summary>
        /// Create an exception of generic type E using the 
        /// specified format and format values to define
        /// the message of the exception
        /// </summary>
        /// <typeparam name="E">The type of the exception to return</typeparam>
        /// <param name="msgFormat">The string format</param>
        /// <param name="values">The arguments to pass to string.Format</param>
        /// <returns></returns>
        public static E Exception<E>(string msgFormat, params object[] values) where E : Exception
        {
            return Exception<E>(msgFormat, null, values);
        }

        /// <summary>
        /// Create an exception of generic type E using the 
        /// specified format, format values and inner exception
        /// to define the message and inner exception of the 
        /// resultant exception
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="msgFormat"></param>
        /// <param name="innerException"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static E Exception<E>(string msgFormat, Exception innerException, params object[] values) where E : Exception
        {
            ConstructorInfo ctor  = typeof(E).GetConstructor(new Type[] { typeof(string), typeof(Exception) });
            return (E)ctor.Invoke(new object[] { string.Format(msgFormat, values), innerException });
        }
    }
}
