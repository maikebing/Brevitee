using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Web;
using Brevitee.Html;

namespace Brevitee.Javascript
{
    public static class Extensions
    {
        

        public static JsContext RunJavascript(this string javascriptSource, params CliProvider[] cliProviders)
        {
            JsContext ctx = new JsContext();
            foreach (CliProvider o in cliProviders)
            {
                ctx.SetCliValue(o.VarName, o.Provider);
            }

            ctx.Run(javascriptSource);
            return ctx;
        }

        public static JsContext RunJavascriptFile(this string javascriptFilePath, params CliProvider[] cliProviders)
        {
            using (TextReader r = new StreamReader(javascriptFilePath))
            {
                return RunJavascript(r.ReadToEnd());
            }
        }

        public static string JsonFromJsLiteralFile(this string javascriptFilePath, string objName)
        {
            return JsonFromJsLiteralFile(new FileInfo(javascriptFilePath), objName);
        }

        public static string JsonFromJsLiteralFile(this FileInfo jsLiteralFile, string objName)
        {
            string json = Brevitee.Javascript.ResourceScripts.Get("json2.js");
            string database = File.ReadAllText(jsLiteralFile.FullName);
            string command = string.Format(";var objJson = JSON.stringify({0});", objName);

            string script = "{0}{1}{2}"._Format(json, database, command);
            JsContext context = script.RunJavascript();
            string result = context.GetValue<string>("objJson");
            return result;
        }

        public static dynamic JsonToDynamic(this string json)
        {
            return JsonConvert.DeserializeObject<dynamic>(json);
        }
    }
}
