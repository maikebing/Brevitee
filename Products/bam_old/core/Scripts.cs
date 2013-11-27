using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Brevitee;
using Brevitee.Data;
using Brevitee.Dust;
using Brevitee.Configuration;
using Brevitee.Logging;
using Brevitee.Incubation;
using Brevitee.Javascript;
using Yahoo.Yui.Compressor;

using System.Reflection;

namespace Bam.core
{
    public class Scripts: ResponderBase
    {
        private const string scriptNameFormat = "~/scripts/{0}";
        private const string contentScriptFormat = "~/content/scripts/{0}";
        Dictionary<string, Func<string>> _dynamicResponders;

        Incubator _serviceProvider;

        static Scripts()
        {
            ResourceScripts.LoadScripts(typeof(scripts.scripts));
            ResourceScripts.LoadScripts(typeof(resources.js.scripts));
        }

        public Scripts(Fs fs)
            : base(fs)
        {
            this._serviceProvider = Incubator.Default;
            Init();
        }

        public Scripts(Fs fs, ILogger logger)
            : base(fs, logger)
        {
            this._serviceProvider = Incubator.Default;
            Init();
        }

        public Scripts(Fs fs, ILogger logger, Incubator serviceProvider)
            : base(fs, logger)
        {
            this._serviceProvider = serviceProvider;
            Init();
        }

        private void Init()
        {
            this._dynamicResponders = new Dictionary<string, Func<string>>();
            this._dynamicResponders.Add("proxies", Proxies);
            this._dynamicResponders.Add("ctors", Ctors);
            this._dynamicResponders.Add("templates", Templates);
        }

        public Incubator ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
            set
            {
                _serviceProvider = value;
            }
        }

        /// <summary>
        /// Combines and compresses the specified scripts into the specified newScriptname
        /// </summary>
        /// <param name="newScriptName"></param>
        /// <param name="scriptNames"></param>
        public void CompressToFile(string newScriptName, params string[] scriptNames)
        {
            ThrowIfContentExists(newScriptName);
            WriteContentScript(newScriptName, Compress(scriptNames));
        }

        public void CompressToContentFile(string newScriptName, bool overwrite, params string[] scriptNames)
        {
            if (ContentScriptExists(newScriptName) && !overwrite)
            {
                ThrowIfContentExists(newScriptName);
            }
            else
            {
                WriteContentScript(newScriptName, Compress(scriptNames));
            }
        }

        /// <summary>
        /// Compress and combines the specified scriptNames in the ~/scripts folder
        /// </summary>
        /// <param name="scriptNames"></param>
        /// <returns></returns>
        public string Compress(params string[] scriptNames)
        {
            JavaScriptCompressor jsc = new JavaScriptCompressor();            
            return jsc.Compress(Combine(scriptNames));
        }

        public string Combine(params string[] scriptNames)
        {
            StringBuilder result = new StringBuilder();
            foreach (string script in scriptNames)
            {
                string scriptName = script;
                if (!scriptName.EndsWith(".js"))
                {
                    scriptName = string.Format("{0}.js", script);
                }

                if (Fs.FileExists(script))
                {
                    result.Append(Fs.ReadAllText(script));
                }
                else if (ScriptExists(scriptName))
                {
                    result.Append(ReadScriptText(scriptName));
                }
                else if (ContentScriptExists(scriptName))
                {
                    result.Append(ReadContentScriptText(scriptName));
                }
            }

            return result.ToString();
        }

        public void CombineToFile(string newScriptName, params string[] scriptNames)
        {
            ThrowIfExists(newScriptName);
            WriteScript(newScriptName, Combine(scriptNames));
        }

        private void ThrowIfExists(string newScriptName)
        {
            Args.ThrowIf<InvalidOperationException>(ScriptExists(newScriptName), "A script named {0} already exists", newScriptName);
        }

        private void ThrowIfContentExists(string newScriptName)
        {
            Args.ThrowIf<InvalidOperationException>(ContentScriptExists(newScriptName), "A script named {0} already exists", newScriptName);
        }
        
        /// <summary>
        /// Write a script with the specified scriptName and the specified 
        /// script text to the ~/content/scripts folder
        /// </summary>
        /// <param name="scriptName"></param>
        /// <param name="text"></param>
        public void WriteContentScript(string scriptName, string text)
        {
            Fs.WriteFile(string.Format(contentScriptFormat, scriptName), text);
        }

        public void WriteScript(string scriptName, string text)
        {
            Fs.WriteFile(string.Format(scriptNameFormat, scriptName), text);
        }

        public void AppendToScript(string scriptName, string text)
        {
            Fs.AppendToFile(string.Format(scriptNameFormat, scriptName), text);
        }

        public string ReadContentScriptText(string scriptName)
        {
            return Fs.ReadAllText(string.Format(contentScriptFormat, scriptName));
        }

        public string ReadScriptText(string scriptName)
        {
            return Fs.ReadAllText(string.Format(scriptNameFormat, scriptName));
        }

        public bool ContentScriptExists(string scriptName)
        {
            return Fs.FileExists(string.Format(contentScriptFormat, scriptName));
        }

        public bool ScriptExists(string scriptName)
        {
            return Fs.FileExists(string.Format(scriptNameFormat, scriptName));
        }

        public string Templates()
        {
            Dust.DustRoot = Fs.GetAbsolutePath("~/content/templates");
            Dust.Initialize();
            return Regex.Unescape(Dust.AllCompiledTemplates); //Dust.AllCompiledTemplates.Replace("\\\\\"", "\\\"");
        }

        public string Ctors()
        {
            return GetCtorScript(_serviceProvider, _serviceProvider.ClassNames).ToString();
        }

        private StringBuilder GetCtorScript(Incubator incubator, string[] classes)
        {
            StringBuilder ctorScript = new StringBuilder();
            StringBuilder fkProto = new StringBuilder();
                        
            foreach (string className in classes)
            {
                Type type = incubator[className];
                MethodInfo modelTypeMethod = type.GetMethod("GetModelType");
                if (modelTypeMethod != null)
                {
                    Type modelType = (Type)modelTypeMethod.Invoke(null, null);
                    StringBuilder parameters;
                    StringBuilder body;
                    GetCtorParamsAndBody(modelType, fkProto, out parameters, out body);
                    ctorScript.AppendFormat("function {0}(", className);
                    // -- params 
                    ctorScript.Append(parameters.ToString());
                    // -- end params
                    ctorScript.AppendLine("){");
                    // -- body
                    ctorScript.Append(body.ToString());
                    // -- end body
                    ctorScript.AppendLine("}");
                }
            }

            ctorScript.AppendLine(fkProto.ToString());
            return ctorScript;
        }

        private void GetCtorParamsAndBody(Type type, StringBuilder fkProto, out StringBuilder paramList, out StringBuilder body)
        {
            paramList = new StringBuilder();
            body = new StringBuilder();
            PropertyInfo[] properties = (from prop in type.GetPropertiesWithAttributeOfType<ColumnAttribute>()
                                         where !prop.HasCustomAttributeOfType<KeyColumnAttribute>()
                                         select prop).ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];

                string propertyName = property.Name.CamelCase();
                paramList.Append(propertyName);
                if (i != properties.Length - 1)
                {
                    paramList.Append(", ");
                }

                ForeignKeyAttribute fk;
                if (property.HasCustomAttributeOfType<ForeignKeyAttribute>(out fk))
                {
                    string refProperty = string.Format("{0}Of{1}", fk.ReferencedTable, fk.Name).CamelCase();
                    body.AppendFormat("\tthis.{0} = new dao.entity('{1}', {2});\r\n", refProperty, fk.ReferencedTable, fk.Name);

                    fkProto.AppendFormat("{0}.prototype.{1}Collection = function(){{\r\n", fk.ReferencedTable, fk.Table.CamelCase());
                    fkProto.AppendFormat("\treturn new dao.collection(this, '{0}', '{1}', '{2}', '{3}');\r\n", fk.ReferencedTable, fk.ReferencedKey, fk.Table, fk.Name);
                    fkProto.Append("};\r\n");
                }
                else
                {
                    body.AppendFormat("\tthis.{0} = {0};\r\n", propertyName);
                }
            }

            string varName = GetVarName(type);
            body.AppendFormat("\tthis.update = function(opts){{ bam.{0}.update(this, opts); }};\r\n", varName);
            body.AppendFormat("\tthis.save = this.update;\r\n");
            body.AppendFormat("\tthis.delete = function(opts){{ bam.{0}.delete(this, opts); }};\r\n", varName);
            body.AppendFormat("\tthis.fks = function(){{ return dao.getFks('{0}');}};\r\n", Dao.TableName(type));
            body.AppendFormat("\tthis.pk = function(){{ return '{0}'; }};\r\n", Dao.GetKeyColumnName(type).ToLowerInvariant());
        }

        public string Proxies()
        {
            return GetProxyScript(_serviceProvider, _serviceProvider.ClassNames).ToString();
        }
        
        private StringBuilder GetProxyScript(Incubator incubator, string[] classes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("(function(b, d, $, win){");

            foreach (string className in classes)
            {
                Type type = incubator[className];
                string var = string.Format("\tb.{0}", className);
                stringBuilder.Append(var);
                stringBuilder.Append(" = {};\r\n");                

                foreach (MethodInfo method in type.GetMethods())
                {
                    if (!method.Name.StartsWith("remove_") &&
                        !method.Name.StartsWith("add_") &&
                        method.MemberType == MemberTypes.Method &&
                        !method.IsProperty() &&
                        !method.HasCustomAttributeOfType<ExcludeAttribute>() &&
                        method.DeclaringType != typeof(object))
                    {
                        stringBuilder.AppendLine(GetMethodCall(type, method));
                    }
                }

                MethodInfo modelTypeMethod = type.GetMethod("GetModelType");
                if (modelTypeMethod != null && modelTypeMethod.ReturnType == typeof(Type))
                {
                    Type modelType = (Type)modelTypeMethod.Invoke(null, null);
                    stringBuilder.AppendFormat("\td.entities.{0} = b.{0};\r\n", className);
                    stringBuilder.AppendFormat("\td.entities.{0}.ctx = '{1}';\r\n", className, Dao.ConnectionName(modelType));
                    stringBuilder.AppendFormat("\td.entities.{0}.cols = [];\r\n", className);

                    PropertyInfo[] modelProps = modelType.GetProperties();
                    foreach (PropertyInfo prop in modelProps)
                    {
                        ColumnAttribute col;
                        if (prop.HasCustomAttributeOfType<ColumnAttribute>(out col))
                        {
                            string typeName = prop.PropertyType.Name;
                            if (prop.PropertyType.IsGenericType &&
                                prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                typeName = prop.PropertyType.GetGenericArguments()[0].Name;
                            }

                            stringBuilder.AppendFormat("\td.entities.{0}.cols.push({{name: '{1}', type: '{2}', nullable: {3} }});\r\n", className, col.Name, typeName, col.AllowNull ? "true": "false");
                        }

                        ForeignKeyAttribute fk;
                        if (prop.HasCustomAttributeOfType<ForeignKeyAttribute>(out fk))
                        {
                            stringBuilder.AppendFormat("\td.fks.push({{ pk: '{0}', pt: '{1}', fk: '{2}', ft: '{3}', nullable: {4} }});\r\n", fk.ReferencedKey, fk.ReferencedTable, fk.Name, fk.Table, fk.AllowNull ? "true": "false");
                        }
                    }
                }

                string varName = GetVarName(type);
                stringBuilder.AppendFormat("\twin.{0} = {1};\r\n", varName, var.Trim());
                stringBuilder.AppendFormat("\twin.{0}.className = '{1}';\r\n", varName, className);
                stringBuilder.AppendFormat("\td.{0} = b.{1};\r\n", varName, className);
            }

            stringBuilder.AppendLine("})(bam, dao, jQuery, window || {});");

            return stringBuilder;
        }


        private string GetMethodCall(Type type, MethodInfo method)
        {
            StringBuilder builder = new StringBuilder();
            ParameterInfo[] parameterInfos = method.GetParameters();
            string defaultMethodName;
            string otherMethodName;
            GetMethodDetails(method, out defaultMethodName, out otherMethodName);

            string parameters = parameterInfos.ToArray().ToDelimited((pi) => pi.Name);
            string comma = parameterInfos.Length > 0 ? ", " : "";
            builder.AppendFormat("\tb.{0}.{1} = function({2}{3}options)", type.Name, defaultMethodName, parameters, comma);
            builder.AppendLine("{");
            builder.AppendFormat("\t\treturn b.invoke('{0}', '{1}', [{2}], options != null ? (options.format == null ? 'json': options.format) : 'json', $.isFunction(options) ? {3} : options);\r\n", type.Name, method.Name, parameters, "{success: options}");
            builder.AppendLine("\t}");

            MethodCase methodCase = GetMethodCase(type.GetCustomAttributeOfType<ProxyAttribute>());
            if (methodCase == MethodCase.Both)
            {
                builder.AppendFormat("\tb.{0}.{1} = d.{0}.{2};", type.Name, otherMethodName, defaultMethodName);
                builder.AppendLine();
            }
            return builder.ToString();
        }

        private static void GetMethodDetails(MethodInfo method, out string defaultMethodName, out string otherMethodName)
        {
            string camelCasedMethodName = method.Name.CamelCase();
            string pascalCasedMethodName = method.Name.PascalCase();
            defaultMethodName = camelCasedMethodName;
            otherMethodName = pascalCasedMethodName;

            MethodCase methodCase = GetMethodCase(method.DeclaringType.GetCustomAttributeOfType<ProxyAttribute>());

            switch (methodCase)
            {
                case MethodCase.Invalid:
                    throw new InvalidOperationException("Invalid MethodCase specified");
                case MethodCase.CamelCase:
                    defaultMethodName = camelCasedMethodName;
                    otherMethodName = pascalCasedMethodName;
                    break;
                case MethodCase.PascalCase:
                    defaultMethodName = pascalCasedMethodName;
                    otherMethodName = camelCasedMethodName;
                    break;
                case MethodCase.Both:
                    break;
                default:
                    break;
            }
        }

        private static MethodCase GetMethodCase(ProxyAttribute proxyAttr)
        {
            MethodCase methodCase = MethodCase.Both;

            if (proxyAttr != null)
            {
                methodCase = proxyAttr.MethodCase;
            }
            return methodCase;
        }

        private static string GetVarName(Type type)
        {
            string varName = type.Name;
            ProxyAttribute proxyAttr = null;
            if (type.HasCustomAttributeOfType<ProxyAttribute>(true, out proxyAttr))
            {
                varName = proxyAttr.VarName;
            }

            return varName;
        }

        #region IResponder Members

        public override bool TryRespond(IContext context)
        {
            string path = context.Request.Url.AbsolutePath;
            SetContentType(context.Response, path);

            bool handled = false;            
            
            string[] chunks = path.DelimitSplit("/", "\\");

            if (chunks.Length == 2)
            {
                string name = chunks[1];
                string script = ResourceScripts.Get(name);
                if (!string.IsNullOrEmpty(script))
                {
                    SendResponse(context.Response, script);
                    handled = true;
                }
                else
                {
                    SetContentType(context.Response, ".js");
                    foreach (string methodName in _dynamicResponders.Keys)
                    {
                        if (name.ToLowerInvariant().Equals(methodName))
                        {
                            SendResponse(context.Response, _dynamicResponders[methodName]());
                            handled = true;
                            break;
                        }
                    }
                }
            }

            if (!handled)
            {
                path = string.Format("~/content{0}", path);

                if (Fs.FileExists(path))
                {
                    SendResponse(context.Response, Fs.ReadAllText(path));
                    handled = true;
                }
            }
            return handled;
        }

        #endregion
    }
}
