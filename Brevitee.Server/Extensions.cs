using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Brevitee;
using Newtonsoft.Json;
using Brevitee.Drawing;

namespace Brevitee.Server
{
    public static class Extensions
    {
        public static string ToJson(this object obj, bool pretty = false)
        {
            if (pretty)
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.Formatting = Formatting.Indented;

                return JsonConvert.SerializeObject(obj, settings);
            }
            else
            {
                return Brevitee.ExtensionsClass.ToJson(obj);
            }
        }

        public static void Save(this ColorScheme scheme, Fs fs, bool overwrite = false)
        {
            scheme.Save(fs.GetAbsolutePath("~/ColorScheme.yaml"), overwrite);
        }
    }
}
