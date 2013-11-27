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
        public static T Property<T>(this object instance, string propertyName)
        {
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty(propertyName);
            return (T)property.GetValue(instance, null);
        }

        public static object Property(this object instance, string propertyName, object value)
        {
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty(propertyName);
            property.SetValue(instance, value, null);
            return instance;
        }

        public static void EachProperty(this object instance, Action<PropertyInfo> action)
        {
            Type type = instance.GetType();
            PropertyInfo[] properties = type.GetProperties();
            properties.Each(action);
        }

        public static void EachProperty(this object instance, Action<PropertyInfo, int> action)
        {
            Type type = instance.GetType();
            PropertyInfo[] properties = type.GetProperties();
            properties.Each(action);
        }
    }
}
