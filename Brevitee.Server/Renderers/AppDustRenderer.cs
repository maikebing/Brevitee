using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Brevitee;
using Brevitee.Web;
using Brevitee.Incubation;
using Brevitee.Html;
using System.Reflection;

namespace Brevitee.Server.Renderers
{
    public class AppDustRenderer: CommonDustRenderer
    {
        public AppDustRenderer(AppContentResponder appContent)
            : base(appContent.ContentResponder)
        {
            this.AppContentResponder = appContent;
        }

        public AppContentResponder AppContentResponder
        {
            get;
            set;
        }

        string _compiledDustTemplates;
        object _compiledDustTemplatesLock = new object();
        /// <summary>
        /// Represents the compiled javascript result of doing dust.compile
        /// against all the files found in ~a:/dust.
        /// </summary>
        public override string CompiledDustTemplates
        {
            get
            {
                return _compiledDustTemplatesLock.DoubleCheckLock(ref _compiledDustTemplates, () =>
                {
                    StringBuilder templates = new StringBuilder();
                    DirectoryInfo appDust = new DirectoryInfo(Path.Combine(AppContentResponder.Root, "dust"));

                    string appCompiledTemplates = DustScript.CompileDirectory(appDust);

                    templates.Append(appCompiledTemplates);
                    return templates.ToString();
                });
            }
        }

        string _compiledDustTypeTemplates;
        object _compiledDustTypeTemplatesLock = new object();
        /// <summary>
        /// The combination of CompiledDustTemplates and all type templates
        /// </summary>
        public string CompiledDustTypeTemplates
        {
            get
            {
                return _compiledDustTemplatesLock.DoubleCheckLock(ref _compiledDustTypeTemplates, () =>
                {
                    StringBuilder result = new StringBuilder();
                    result.AppendLine(CompiledDustTemplates);

                    DirectoryInfo dustDir = new DirectoryInfo(Path.Combine(AppContentResponder.Root, "dust"));
                    DirectoryInfo[] typeSubDirs = dustDir.GetDirectories();
                    typeSubDirs.Each(subDir =>
                    {
                        result.AppendLine(DustScript.CompileDirectory(subDir, "*.dust"));
                    });

                    return result.ToString();
                });
            }
        }
        
        protected internal void EnsureDefaultTemplate(Type anyType)
        {
            string relativeFilePath = "~/dust/{0}/default.dust"._Format(anyType.Name);
            string fullPath = AppContentResponder.AppRoot.GetAbsolutePath(relativeFilePath);

            if (!File.Exists(fullPath))
            {
                lock(_compiledDustTemplatesLock)
                {
                    object instance = anyType.ValuePropertiesToDynamicInstance();
                    SetTemplateProperties(instance);
                    string htm = InputFor(instance.GetType(), instance).XmlToHumanReadable();

                    File.WriteAllText(fullPath, htm);
                    _compiledDustTemplates = null; // forces reload
                }
            }
        }

        public string InputFor(Type type, object defaults = null, string name = null)
        {
            InputFormBuilder builder = new InputFormBuilder();
            return builder.FieldsetFor(type, defaults, name).ToString();
        }

        private void SetTemplateProperties(object instance)
        {
            Type type = instance.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                if (prop.PropertyType == typeof(string) ||
                    prop.PropertyType == typeof(int) ||
                    prop.PropertyType == typeof(long))
                {
                    prop.SetValue(instance, "{" + prop.Name + "}", null);
                }
            }
        }
    }
}
