using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Yaml.Serialization;
using System.Yaml;
using Brevitee;

namespace Brevitee.Yaml
{
    public static class Extensions
    {
        public static string ToYaml(this object val, YamlConfig conf = null)
        {
            YamlSerializer ser = conf == null ? new YamlSerializer() : new YamlSerializer(conf);
            return ser.Serialize(val);
        }
        
        public static object[] FromYaml(this string yaml, params Type[] expectedTypes)
        {
            YamlSerializer ser = new YamlSerializer();
            return ser.Deserialize(yaml, expectedTypes);
        }

        public static object[] FromYamlFile(this string filePath, params Type[] expectedTypes)
        {
            YamlSerializer ser = new YamlSerializer();
            return ser.DeserializeFromFile(filePath, expectedTypes);
        }
        
        public static T FromYaml<T>(this string yaml)
        {
            return yaml.ArrayFromYaml<T>().FirstOrDefault();
        }

        public static T[] ArrayFromYaml<T>(this string yaml)
        {
            YamlSerializer ser = new YamlSerializer();
            YamlConfig c = new YamlConfig();
            object[] des = ser.Deserialize(yaml, typeof(T));
            return des.Each<T>((o) =>
            {
                return (T)o;
            });
        }
    }
}
