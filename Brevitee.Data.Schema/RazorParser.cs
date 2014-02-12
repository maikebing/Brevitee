using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Razor;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Schema;
using Microsoft.CSharp;
using System.Reflection;
using System.IO;
using System.CodeDom.Compiler;
using Brevitee.ServiceProxy;
using Brevitee.ServiceProxy.Js;

namespace Brevitee.Data.Schema
{
    public class RazorParser<TBaseTemplate> where TBaseTemplate: RazorBaseTemplate
    {
        RazorTemplateEngine _engine;
        public RazorParser()
            : this(typeof(TBaseTemplate).Namespace, string.Format("{0}Template", typeof(TBaseTemplate).Name))
        {
        }

        public RazorParser(object options = null)
            : this(typeof(TBaseTemplate).Namespace, string.Format("{0}Template", typeof(TBaseTemplate).Name), options)
        {
        }

        public RazorParser(Action<string> resultInspector)
            : this(typeof(TBaseTemplate).Namespace, string.Format("{0}Template", typeof(TBaseTemplate).Name))
        {
            this.ResultInspector = resultInspector;
        }

        static RazorParser()
        {
            DefaultRazorInspector = RazorBaseTemplate.DefaultInspector;//(s) => { Console.WriteLine(s); };
        }

        /// <summary>
        /// The default inspector used by any RazorParser that hasn't been assigned one
        /// </summary>
        public static Action<string> DefaultRazorInspector
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultNamespace"></param>
        /// <param name="defaultClassName"></param>
        /// <param name="options">Applied to the GeneratedClassContext</param>
        public RazorParser(string defaultNamespace = "RazorOutput", string defaultClassName = "Template", object options = null)
        {
            if (this.ResultInspector == null)
            {
                this.ResultInspector = DefaultRazorInspector;
            }
            // Set up the hosting environment

            // a. Use the C# language (you could detect this based on the file extension if you want to)
            RazorEngineHost host = new RazorEngineHost(new CSharpRazorCodeLanguage());

            // b. Set the base class
            host.DefaultBaseClass = typeof(TBaseTemplate).FullName;

            // c. Set the output namespace and type name
            host.DefaultNamespace = "RazorOutput";
            host.DefaultClassName = "Template";

            // d. Add default imports
            host.NamespaceImports.Add("System");
            host.NamespaceImports.Add("Brevitee");
            host.NamespaceImports.Add("Brevitee.ServiceProxy");
            host.NamespaceImports.Add(defaultNamespace);
            
            if(options != null)
            {
                Type contextType = host.GeneratedClassContext.GetType();
                foreach (PropertyInfo prop in options.GetType().GetProperties())
                {
                    PropertyInfo contextProp = contextType.GetProperty(prop.Name);
                    if (contextProp != null)
                    {
                        object valueToSet = prop.GetValue(options, null);
                        contextProp.SetValue(host.GeneratedClassContext, valueToSet, null);
                    }
                }
            }
            // Create the template engine using this host
            _engine = new RazorTemplateEngine(host);
        }
        
        public Action<string> ResultInspector
        {
            get;
            set;
        }

        public string ExecuteResource(string templateName, object options = null)
        {
            Type currentType = this.GetType();
            string namespacePath = string.Format("{0}.Templates.", currentType.Namespace);
            Assembly assembly = currentType.Assembly;
            string resourcePath = string.Empty;
            foreach (string fullPath in assembly.GetManifestResourceNames())
            {
                string resourceTemplateName = fullPath.Substring(namespacePath.Length, fullPath.Length - namespacePath.Length);
                if (resourceTemplateName.Equals(templateName))
                {
                    resourcePath = fullPath;
                }
            }

            if (string.IsNullOrEmpty(resourcePath))
            {
                throw new InvalidOperationException(string.Format("The specified resource template was not found: {0}", templateName));
            }

            using (StreamReader resourceTemplate = new StreamReader(assembly.GetManifestResourceStream(resourcePath)))
            {
                return Execute(resourceTemplate, options, ResultInspector);
            }
        }

        /// <summary>
        /// Execute the 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string Execute(TextReader input, object options = null, Action<string> inspector = null)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            GeneratorResults results = _engine.GenerateCode(input);
            CompilerResults compilerResults = codeProvider.CompileAssemblyFromDom(
                new CompilerParameters(new string[] {
                    typeof(TBaseTemplate).Assembly.CodeBase.Replace("file:///", "").Replace("/", "\\"),
                    typeof(ServiceProxyController).Assembly.CodeBase.Replace("file:///", "").Replace("/", "\\"),
                    typeof(Providers).Assembly.CodeBase.Replace("file:///", "").Replace("/", "\\")
                }),
                results.GeneratedCode);

            using (StringWriter sw = new StringWriter())
            {
                codeProvider.GenerateCodeFromCompileUnit(results.GeneratedCode, sw, new CodeGeneratorOptions());
                if (inspector != null)
                {
                    inspector(sw.GetStringBuilder().ToString());
                }
            }

            if (compilerResults.Errors.HasErrors)
            {
                throw new Exception(compilerResults.Errors[0].ErrorText);
            }
            else
            {
                Assembly asm = compilerResults.CompiledAssembly;//Assembly.LoadFrom(outputAssemblyName);

                Type templateType = asm.GetType(string.Format("{0}.{1}", _engine.Host.DefaultNamespace, _engine.Host.DefaultClassName));
                ConstructorInfo ctor = templateType.GetConstructor(Type.EmptyTypes);
                object instance = ctor.Invoke(null);

                if (options != null)
                {
                    Type setType = options.GetType();
                    foreach (PropertyInfo prop in setType.GetProperties())
                    {
                        templateType.GetProperty(prop.Name).SetValue(instance, prop.GetValue(options, null), null);
                    }
                }

                templateType.GetMethod("Execute").Invoke(instance, null);

                return ((StringBuilder)templateType.GetProperty("Generated").GetValue(instance, null)).ToString();
            }
        }
    }
}
