using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data;
using Brevitee.Incubation;
using Brevitee.Logging;
using Brevitee.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using System.IO;

namespace Brevitee.ServiceProxy
{
    public class ServiceProxySystem
    {
        public const string ServiceProxyPartialFormat = "~/Views/ServiceProxy/{0}/{1}";

        static bool initialized;
        static object initLock = new object();
        /// <summary>
        /// Initialize the underlying ServiceProxySystem.
        /// </summary>
        public static void Initialize()
        {
            if (!initialized)
            {
                lock (initLock)
                {
                    initialized = true;
                    RegisterRoutes();
                    ServiceProxyController.Init();
                }
            }
        }


        /// <summary>
        /// Maps the ServiceProxy routes in the default Mvc RouteTable.
        /// This should be called from Global before setting the default
        /// action route.
        /// </summary>
        public static void RegisterRoutes()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// Maps the ServiceProxy routes in the specified RouteCollection.
        /// This should be called from Global before setting the default
        /// action route.
        /// </summary>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "ServiceProxy",
                "{action}/{className}/{methodName}.{ext}",
                new { controller = "ServiceProxy", action = "Get", ext = "json" },
                new string[] { "Brevitee.ServiceProxy" }
            );
        }

        static ServiceProxySystem current;
        static object currentLock = new object();
        /// <summary>
        /// Provides an extension point to add functionality to the ServiceProxySystem
        /// </summary>
        public static ServiceProxySystem Current
        {
            get
            {
                if (current == null)
                {
                    lock (currentLock)
                    {
                        current = new ServiceProxySystem();
                    }
                }

                return current;
            }
        }

        static string _proxySearchPattern;
        static object _proxySearchPatternLock = new object();
        /// <summary>
        /// The search pattern used to find assemblies that host
        /// service proxies (classes addorned with the ProxyAttribute custom attribute).
        /// This value is retrieved from the config file, the default is "*.dll" if none
        /// is provided.
        /// </summary>
        public static string ProxySearchPattern
        {
            get
            {
                return _proxySearchPatternLock.DoubleCheckLock(ref _proxySearchPattern, () => DefaultConfiguration.GetAppSetting("ProxySearchPattern", "*.dll"));
            }
        }

        /// <summary>
        /// Analyzes all the files in the bin directory of the current app that match the
        /// ProxySearchPattern and registers each matching class as services
        /// </summary>
        /// <see cref="ServiceProxySystem.ProxySearchPattern" />
        public static void RegisterBinProviders()
        {
            HttpServerUtility server = HttpContext.Current.Server;
            DirectoryInfo bin = new DirectoryInfo(server.MapPath("~/bin"));
            RegisterTypesWithAttributeFrom<ProxyAttribute>(bin);
        }

        /// <summary>
        /// Searches the specified folder for assemblies that contain types 
        /// addorned with the specified attribute and registers each as
        /// services
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="folderPath"></param>
        public static void RegisterTypesWithAttributeFrom<T>(string folderPath) where T : Attribute
        {
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            if (dir.Exists)
            {
                RegisterTypesWithAttributeFrom<T>(dir);
            }
        }

        /// <summary>
        /// Searches the specified folder for assemblies that contain types 
        /// addorned with the specified attribute and registers each as
        /// services
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dir"></param>
        public static void RegisterTypesWithAttributeFrom<T>(DirectoryInfo dir) where T : Attribute
        {
            FileInfo[] files = dir.GetFiles(ProxySearchPattern);
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];
                try
                {
                    Assembly container = Assembly.LoadFrom(file.FullName);
                    RegisterTypesWithAttribute<T>(container);
                }
                catch (Exception ex)
                {
                    Log.AddEntry("Non fatal exception occurred registering proxy types from {0}", ex, file.Name);
                }
            }
        }

        /// <summary>
        /// Registers the types from the specified assemblyToLoadFrom that are addorned with
        /// the specified generic type attribute T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyToLoadFrom"></param>
        public static void RegisterTypesWithAttribute<T>(Assembly assemblyToLoadFrom) where T : Attribute
        {
            Type[] types = assemblyToLoadFrom.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                Type current = types[i];
                if (current.HasCustomAttributeOfType<T>())
                {
                    Register(current);
                }
            }
        }

        /// <summary>
        /// Register the specified type as a ServiceProxy responder.
        /// </summary>
        /// <param name="type"></param>
        public static void Register(Type type)
        {
            Initialize();
            Incubator.Construct(type);
        }

        /// <summary>
        /// Register the speicified generic type T as a ServiceProxy responder.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
        {
            Initialize();
            Incubator.Construct<T>();
        }

        /// <summary>
        /// Register the instance of T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public static void Register<T>(T instance)
        {
            Initialize();
            Incubator.Set<T>(instance);
        }

        public static void Unregister<T>()
        {
            Incubator.Remove<T>();
        }

        /// <summary>
        /// Register the specified handler to handle the specified file extension.
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="handler"></param>
        /// <param name="reset"></param>
        public static void RegisterServiceProxyRequestDelegate(string extension, ExecutionResultDelegate handler, bool reset = false)
        {
            ServiceProxyController.RegisterServiceProxyRequestDelegate(extension, handler, reset);
        }

        public static ServiceProxyClient<T> CreateClient<T>(string baseAddress)
        {
            return new ServiceProxyClient<T>(baseAddress);
        }

        public static MethodInfo[] GetProxiedMethods(Type type)
        {
            List<MethodInfo> results = new List<MethodInfo>();
            foreach (MethodInfo method in type.GetMethods())
            {
                if (WillProxyMethod(method))
                {
                    results.Add(method);
                }
            }

            return results.ToArray();
        }

        public static bool WillProxyMethod(MethodInfo method)
        {
            return !method.Name.StartsWith("remove_") &&
                                    !method.Name.StartsWith("add_") &&
                                    method.MemberType == MemberTypes.Method &&
                                    !method.IsProperty() &&
                                    !method.HasCustomAttributeOfType<ExcludeAttribute>() &&
                                    method.DeclaringType != typeof(object);
        }

        static Incubator incubator;
        static object incubatorLock = new object();
        /// <summary>
        /// Gets or sets the default Incubator instance used by the ServiceProxy system.
        /// </summary>
        public static Incubator Incubator
        {
            get
            {
                if (incubator == null)
                {
                    lock (incubatorLock)
                    {
                        incubator = new Incubator();
                    }
                }
                return incubator;
            }
            set
            {
                incubator = value;
            }
        }


        public static StringBuilder GenerateCSharpProxyCode(string defaultBaseAddress, string[] classNames, string nameSpace, string contractNamespace)
        {
            string headerFormat = @"/**
This file was generated from {0}ServiceProxy/CSharpProxies.  This file should not be modified directly
**/

";
            string usingFormat = "  using {0};\r\n";

            string nameSpaceFormat = @"

namespace {0}
{{
{1}
    {2}
}}";
            string classFormat = @"
    public class {0}Client: ServiceProxyClient<{1}.I{2}>, {1}.I{2}
    {{
        public {0}Client(): base(DefaultConfiguration.GetAppSetting(""{2}Url"", ""{3}""))
        {{            
        }}

        public {0}Client(string baseAddress): base(baseAddress)
        {{
        }}
        
        {4}
    }}
";
            string interfaceFormat = @"
    public interface I{0}
    {{
        {1}
    }}

";
            string interfaceMethodFormat = "{0} {1}({2});\r\n";

            string methodFormat = @"
        public {0} {1}({2})
        {{
            object[] parameters = new object[] {{ {3} }};
            {4}(""{1}"", parameters);
        }}
";
            StringBuilder code = new StringBuilder();
            StringBuilder classes = new StringBuilder();
            StringBuilder interfaces = new StringBuilder();
            List<string> usingNamespaces = new List<string>();
            usingNamespaces.Add("System");
            usingNamespaces.Add("System.Data");
            usingNamespaces.Add("Brevitee.Configuration");
            usingNamespaces.Add("Brevitee.ServiceProxy");
            usingNamespaces.Add(contractNamespace);

            foreach (string className in classNames)
            {
                Type type = ServiceProxySystem.Incubator[className];

                StringBuilder methods = new StringBuilder();
                StringBuilder interfaceMethods = new StringBuilder();
                foreach (MethodInfo method in ServiceProxySystem.GetProxiedMethods(type))
                {
                    ParameterInfo[] parameters = method.GetParameters();
                    parameters.Each(p =>
                    {
                        AddIfNotAdded(usingNamespaces, p.ParameterType.Namespace);
                    });

                    string methodParams = parameters.ToDelimited(p => string.Format("{0} {1}", p.ParameterType.Name, p.Name.CamelCase())); // method signature
                    string wrapped = parameters.ToDelimited(p => p.Name.CamelCase()); // wrapped as object array

                    bool isVoidReturn = method.ReturnType == typeof(void);
                    if (!isVoidReturn)
                    {
                        string ns = method.ReturnType.Namespace;
                        AddIfNotAdded(usingNamespaces, ns);
                    }

                    string returnType = isVoidReturn ? "void" : method.ReturnType.Name;

                    Type[] genericTypesOfReturn = method.ReturnType.GetGenericArguments();
                    if (genericTypesOfReturn.Length > 0)
                    {
                        returnType = string.Format("{0}<{1}>", method.ReturnType.Name.DropTrailingNonLetters(), genericTypesOfReturn.ToDelimited(t => t.Name));
                        genericTypesOfReturn.Each(t =>
                        {
                            AddIfNotAdded(usingNamespaces, t.Namespace);
                        });
                    }

                    string returnOrBlank = isVoidReturn ? "" : "return ";
                    string genericTypeOrBlank = isVoidReturn ? "" : string.Format("<{0}>", returnType);
                    string invoke = string.Format("{0}Invoke{1}", returnOrBlank, genericTypeOrBlank);

                    methods.AppendFormat(methodFormat, returnType, method.Name, methodParams, wrapped, invoke);
                    interfaceMethods.AppendFormat(interfaceMethodFormat, returnType, method.Name, methodParams);
                }

                string serverName = type.Name;
                string clientName = serverName;
                if (clientName.EndsWith("Server"))
                {
                    clientName = clientName.Truncate(6);
                }
                classes.AppendFormat(classFormat, clientName, contractNamespace, serverName, defaultBaseAddress, methods.ToString());
                interfaces.AppendFormat(interfaceFormat, serverName, interfaceMethods.ToString());
            }

            StringBuilder usings = new StringBuilder();
            usingNamespaces.Each(ns =>
            {
                usings.AppendFormat(usingFormat, ns);
            });

            string usingStatements = usings.ToString();
            code.AppendFormat(headerFormat, defaultBaseAddress);
            code.AppendFormat(nameSpaceFormat, nameSpace, usingStatements, classes.ToString());
            code.AppendFormat(nameSpaceFormat, contractNamespace, usingStatements, interfaces.ToString());
            return code;
        }

        private static void AddIfNotAdded(List<string> usingNamespaces, string nameSpace)
        {
            if (!usingNamespaces.Contains(nameSpace))
            {
                usingNamespaces.Add(nameSpace);
            }
        }

        internal static StringBuilder GenerateJsProxyScript(Incubator incubator, string[] classes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("(function(b, d, $, win){");
            stringBuilder.AppendLine(GetJsCtorScript(incubator, classes).ToString());

            foreach (string className in classes)
            {
                Type type = incubator[className];
                string varName = GetVarName(type);
                string var = string.Format("\tb.{0}", className);
                stringBuilder.Append(var);
                stringBuilder.Append(" = {};\r\n");
                stringBuilder.Append(var);
                stringBuilder.Append(".proxyName = \"{0}\";\r\n"._Format(varName));

                foreach (MethodInfo method in type.GetMethods())
                {
                    if (ServiceProxySystem.WillProxyMethod(method))
                    {
                        stringBuilder.AppendLine(GetMethodCall(type, method));
                    }
                }

                MethodInfo modelTypeMethod = type.GetMethod("GetDaoType");
                if (modelTypeMethod != null && modelTypeMethod.ReturnType == typeof(Type))
                {
                    Type modelType = (Type)modelTypeMethod.Invoke(null, null);
                    stringBuilder.Append("\td.entities = d.entities || {};");
                    stringBuilder.Append("\td.fks = d.fks || [];");
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

                            stringBuilder.AppendFormat("\td.entities.{0}.cols.push({{name: '{1}', type: '{2}', nullable: {3} }});\r\n", className, col.Name, typeName, col.AllowNull ? "true" : "false");
                        }

                        ForeignKeyAttribute fk;
                        if (prop.HasCustomAttributeOfType<ForeignKeyAttribute>(out fk))
                        {
                            stringBuilder.AppendFormat("\td.fks.push({{ pk: '{0}', pt: '{1}', fk: '{2}', ft: '{3}', nullable: {4} }});\r\n", fk.ReferencedKey, fk.ReferencedTable, fk.Name, fk.Table, fk.AllowNull ? "true" : "false");
                        }
                    }
                }

                stringBuilder.AppendFormat("\twin.{0} = {1};\r\n", varName, var.Trim());
                stringBuilder.AppendFormat("\twin.{0}.className = '{1}';\r\n", varName, className);
                stringBuilder.AppendFormat("\td.{0} = b.{1};\r\n", varName, className);
            }

            stringBuilder.AppendLine("})(bam, dao, jQuery, window || {});");

            return stringBuilder;
        }

        internal static string GetMethodCall(Type type, MethodInfo method)
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
                builder.AppendFormat("\tb.{0}.{1} = b.{0}.{2};", type.Name, otherMethodName, defaultMethodName);
                builder.AppendLine();
            }
            return builder.ToString();
        }

        internal static void GetMethodDetails(MethodInfo method, out string defaultMethodName, out string otherMethodName)
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

        internal static MethodCase GetMethodCase(ProxyAttribute proxyAttr)
        {
            MethodCase methodCase = MethodCase.Both;

            if (proxyAttr != null)
            {
                methodCase = proxyAttr.MethodCase;
            }
            return methodCase;
        }

        internal static string GetVarName(Type type)
        {
            string varName = type.Name;
            ProxyAttribute proxyAttr = null;
            if (type.HasCustomAttributeOfType<ProxyAttribute>(true, out proxyAttr))
            {
                varName = proxyAttr.VarName;
            }

            return varName;
        }

        internal static StringBuilder GetJsCtorScript(Incubator incubator, string[] classes)
        {
            StringBuilder ctorScript = new StringBuilder();
            StringBuilder fkProto = new StringBuilder();

            foreach (string className in classes)
            {
                Type modelType = incubator[className];
                MethodInfo modelTypeMethod = modelType.GetMethod("GetDaoType");
                if (modelTypeMethod != null)
                {
                    modelType = (Type)modelTypeMethod.Invoke(null, null);
                }

                if (modelType.HasCustomAttributeOfType<TableAttribute>())
                {
                    StringBuilder parameters;
                    StringBuilder body;
                    GetJsCtorParamsAndBody(modelType, fkProto, out parameters, out body);
                    ctorScript.AppendFormat("b.ctor.{0} = function {0}(", className);
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

        internal static void GetJsCtorParamsAndBody(Type type, StringBuilder fkProto, out StringBuilder paramList, out StringBuilder body)
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

                    fkProto.AppendFormat("b.ctor.{0}.prototype.{1}Collection = function(){{\r\n", fk.ReferencedTable, fk.Table.CamelCase());
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

        internal static bool ServiceProxyPartialExists(Type type, string viewName)
        {
            return ServiceProxyPartialExists(type.Name, viewName);
        }

        internal static bool ServiceProxyPartialExists(string typeName, string viewName)
        {
            List<string> fileExtensions = new List<string>();
            fileExtensions.Add(".cshtml");
            fileExtensions.Add(".vbhtml");
            fileExtensions.Add(".aspx");
            fileExtensions.Add(".ascx");
            string path = System.Web.HttpContext.Current.Server.MapPath(string.Format(ServiceProxyPartialFormat, typeName, viewName));

            bool exists = false;
            foreach (string ext in fileExtensions)
            {
                if (System.IO.File.Exists(string.Format("{0}{1}", path, ext)))
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }


        internal static void WriteServiceProxyPartial(Type type, string viewName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(string.Format(ServiceProxyPartialFormat, type.Name, viewName));
            path = string.Format("{0}.cshtml", path);
            StringBuilder source = BuildPartialView(type);

            WriteServiceProxyPartial(path, source);
        }

        internal static void WriteVoidServiceProxyPartial(string viewName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(string.Format(ServiceProxyPartialFormat, "Void", viewName));
            path = string.Format("{0}.cshtml", path);
            StringBuilder builder = new StringBuilder();
            builder.Append("@* place holder *@\r\n\r\n<h1>Void place holder</h1>");
            WriteServiceProxyPartial(path, builder);
        }

        private static void WriteServiceProxyPartial(string path, StringBuilder source)
        {
            FileInfo file = new FileInfo(path);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(source.ToString());
            }
        }

        private static StringBuilder BuildPartialView(Type type)
        {
            StringBuilder source = new StringBuilder();
            string typeName = type.Name.DropTrailingNonLetters();
            Type[] genericTypes = type.GetGenericArguments();
            if (genericTypes.Length > 0)
            {
                typeName = "{0}<{1}>"._Format(typeName, genericTypes.ToDelimited(t => t.Name));
            }
            source.AppendFormat("@model {0}.{1}", type.Namespace, typeName);
            source.AppendLine();
            source.Append("<div type=\"object\" itemscope itemtype=\"http://schema.org/Thing\">\r\n");

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (!property.HasCustomAttributeOfType<ExcludeAttribute>())
                {
                    source.AppendFormat("\t{0}: <span itemprop=\"{0}\">@Model.{0}</span>", property.Name);
                    source.AppendLine("<br />");
                }
            }
            source.AppendLine("</div>");
            return source;
        }


    }
}
