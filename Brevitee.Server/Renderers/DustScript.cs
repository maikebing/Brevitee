using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.ServiceProxy;
using Brevitee.ServiceProxy.Js;
using J = Brevitee.Javascript;
using System.Text.RegularExpressions;
using System.IO;

namespace Brevitee.Server.Renderers
{
    public class DustScript
    {
        static DustScript()
        {
            J.ResourceScripts.LoadScripts(typeof(DustScript));
        }

        public static string Get()
        {
            StringBuilder script = new StringBuilder();
            script.Append(";\r\n");
            script.Append(J.ResourceScripts.Get("json2.js", typeof(DustScript)));
            script.Append(J.ResourceScripts.Get("dust.custom.js", typeof(DustScript)));
            script.Append(";\r\n");
            return script.ToString();
        }

        public static string CompileDirectory(string directoryPath, string fileSearchPattern = "*.*")
        {
            return CompileDirectory(new DirectoryInfo(directoryPath), fileSearchPattern);
        }

        public static string CompileDirectory(DirectoryInfo directory, string fileSearchPattern = "*.*")
        {
            StringBuilder compiled = new StringBuilder();
            if (directory.Exists)
            {
                FileInfo[] files = directory.GetFiles(fileSearchPattern);
                foreach (FileInfo file in files)
                {
                    compiled.Append(";\r\n");
                    string templateSource = File.ReadAllText(file.FullName, Encoding.UTF8);
                    compiled.Append(Compile(templateSource, Path.GetFileNameWithoutExtension(file.Name)));
                    compiled.Append(";\r\n");
                }
            }

            return compiled.ToString();
        }

        public static string Compile(string templateSource, string templateName)
        {
            J.JsContext scriptContext = new J.JsContext();
            scriptContext.Load(Get());
            scriptContext.SetCliValue("templateSource", templateSource.Replace("\r", "").Replace("\n", ""));
            scriptContext.SetCliValue("templateName", templateName);
            scriptContext.Run("var compiled = dust.compile(templateSource, templateName);");
            return scriptContext.GetValue<string>("compiled");
        }

        public static string Render(string compiled, string templateName, object data)
        {
            J.JsContext scriptContext = new J.JsContext();
            scriptContext.Load(Get());
            scriptContext.SetCliValue("compiled", Regex.Unescape(compiled));
            scriptContext.SetCliValue("templateName", templateName);
            scriptContext.SetCliValue("jsonData", data.ToJson());
            scriptContext.Run(@"
var output;
var error;
dust.loadSource(compiled);
dust.render(templateName, JSON.parse(jsonData), function(err, out){
    error = err;
    output = out;
});");
            object error = scriptContext.GetValue<object>("error");
            if (error != null)
            {
                throw new Exception("An error occurred rendering dust template: {0}"._Format(error.ToString()));
            }

            return scriptContext.GetValue<string>("output");
        }

        public static string Render(string templateDirectory, string templateName, object data, string searchPattern = "*.*")
        {
            return Render(new DirectoryInfo(templateDirectory), templateName, data, searchPattern);
        }

        public static string Render(DirectoryInfo templateDirectory, string templateName, object data, string searchPattern = "*.*")
        {
            string compiled = CompileDirectory(templateDirectory, searchPattern);
            return Render(compiled, templateName, data);
        }
    }
}
