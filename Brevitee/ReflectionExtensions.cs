using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Brevitee
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Get the property of the current instance with the specified name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyName">The name of the property value to retrieve</param>
        /// <returns></returns>
        public static T Property<T>(this object instance, string propertyName)
        {
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty(propertyName);
            return (T)property.GetValue(instance, null);
        }

        /// <summary>
        /// Set the property with the specified name
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object Property(this object instance, string propertyName, object value)
        {
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty(propertyName);
            property.SetValue(instance, value, null);
            return instance;
        }

        public static void EachPropertyInfo(this object instance, Action<PropertyInfo> action)
        {
            Type type = instance.GetType();
            PropertyInfo[] properties = type.GetProperties();
            properties.Each(action);
        }

        public static void EachPropertyInfo(this object instance, Action<PropertyInfo, int> action)
        {
            Type type = instance.GetType();
            PropertyInfo[] properties = type.GetProperties();
            properties.Each(action);
        }

        public static void EachPropertyValue(this object instance, Action<PropertyInfo, object> action)
        {
            instance.EachPropertyInfo(pi =>
            {
                object value = pi.GetValue(instance, null);
                action(pi, value);
            });
        }

        public static void EachPropertyValue(this object instance, Action<PropertyInfo, object, int> action)
        {
            instance.EachPropertyInfo((pi, i) =>
            {
                object value = pi.GetValue(instance, null);
                action(pi, value, i);
            });
        }
    }
}
