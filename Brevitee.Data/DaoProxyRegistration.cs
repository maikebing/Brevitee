using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.Routing;
using System.Web.Mvc;
using Brevitee;
using Brevitee.Data;
using Brevitee.Incubation;
using Brevitee.Logging;
using Yahoo.Yui.Compressor;
using System.Threading;

namespace Brevitee.Data
{
    public class DaoProxyRegistration
    {
        static IDictionary<string, DaoProxyRegistration> _registrations;

        /// <summary>
        /// Initialize the inner registration container and 
        /// registers the mvc route for query interface (qi.js; pronounced "chi") 
        /// calls.
        /// </summary>
        internal static void Initialize()
        {
            _registrations = new Dictionary<string, DaoProxyRegistration>();
            RouteTable.Routes.MapRoute(
                name: "Qi",
                url: "Qi/{controller}/{action}",
                defaults: new { controller = "Dao", action = "Default" },
                namespaces: new string[] { "Qi" }
            );
        }

        public DaoProxyRegistration(Type daoType)
        {
            Args.ThrowIfNull(daoType, "daoType");
            Args.ThrowIf<InvalidOperationException>(
                !daoType.IsSubclassOf(typeof(Dao)), "The specified type ({0}) must be a Dao implementation.",
                daoType.Name);

            this.ServiceProvider = new Incubator();
            this.Assembly = daoType.Assembly;
            this.ContextName = Dao.ConnectionName(daoType);
            Dao.RegisterDaoTypes(daoType, this.ServiceProvider);
        }

        public static void RegisterConnection(string connectionName, int retryCount = 5)
        {
            try
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

                for (int i = 0; i < assemblies.Length; i++)
                {
                    Assembly current = assemblies[i];
                    Type[] types = current.GetTypes();
                    for (int ii = 0; ii < types.Length; ii++)
                    {
                        Type type = types[ii];
                        string conn = Dao.ConnectionName(type);
                        if (conn.Equals(connectionName))
                        {
                            Register(type);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry("Exception occurred registering connection ({0}): retryCount=({1}): {2}", LogEventType.Warning, connectionName, retryCount.ToString(), ex.Message);
                if (retryCount > 0)
                {
                    Thread.Sleep(300);
                    RegisterConnection(connectionName, --retryCount);
                }
            }
        }

        /// <summary>
        /// Register siblings of the specified Dao type T along with
        /// T itself
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DaoProxyRegistration Register<T>() where T : Dao
        {
            return Register(typeof(T));
        }

        static object _registerLock = new object();
        public static DaoProxyRegistration Register(Type daoType)
        {
            string connectionName = Dao.ConnectionName(daoType);
            if (!_registrations.ContainsKey(connectionName))
            {
                lock (_registerLock)
                {
                    if (!_registrations.ContainsKey(connectionName))
                    {
                        DaoProxyRegistration registration = new DaoProxyRegistration(daoType);
                        _registrations.Add(connectionName, registration);
                    }
                }
            }

            return _registrations[connectionName];
        }

        public static StringBuilder GetScript(bool min = false)
        {
            StringBuilder result = new StringBuilder();
            foreach (string contextName in _registrations.Keys)
            {
                result.AppendLine(GetScript(contextName, min).ToString());
            }
            return result;
        }

        public static StringBuilder GetScript(string contextName, bool min = false)
        {
            Args.ThrowIf<InvalidOperationException>(
                !_registrations.ContainsKey(contextName),
                "The specified contextName ({0}) was not registered for proxying", contextName);

            StringBuilder result;
            if (min)
            {
                result = _registrations[contextName].MinScript;
            }
            else
            {
                result = _registrations[contextName].Script;
            }

            return result;
        }

        public string ContextName { get; set; }
        public Assembly Assembly { get; set; }
        public Incubator ServiceProvider { get; set; }

        StringBuilder _script;
        object _scriptLock = new object();
        public StringBuilder Script
        {
            get
            {
                return _scriptLock.DoubleCheckLock<StringBuilder>(ref _script, () => BuildProxyScript());
            }
        }

        StringBuilder _minScript;
        object _minScriptLock = new object();
        public StringBuilder MinScript
        {
            get
            {
                return _minScriptLock.DoubleCheckLock<StringBuilder>(
                    ref _minScript,
                    () =>
                    {
                        StringBuilder temp = new StringBuilder();
                        JavaScriptCompressor jsc = new JavaScriptCompressor();
                        string script = Script.ToString();
                        string minScript = jsc.Compress(script);
                        temp.Append(minScript);
                        return temp;
                    }
                );
            }
        }

        private StringBuilder BuildProxyScript()
        {
            return BuildProxyScript(ServiceProvider, ContextName);
        }

        #region assemble script
        private StringBuilder BuildProxyScript(Incubator incubator, string connectionName = "")
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("$(document).ready(function(){");
            if (!string.IsNullOrEmpty(connectionName))
            {
                stringBuilder.AppendFormat("\tdao.{0} = {{}};\r\n", connectionName);
            }
            stringBuilder.AppendLine("\t(function(d, $, win){");
            stringBuilder.AppendLine("\t\t\"use strict\";");
            stringBuilder.AppendLine("\t\td.ctors = d.ctors || {};");
            stringBuilder.AppendLine("\t\td.fks = d.fks || [];");
            stringBuilder.AppendLine(GetBodyAndMeta(incubator).ToString());

            stringBuilder.AppendFormat("\t}})(dao.{0} || dao, jQuery, window || {{}});\r\n", connectionName);
            stringBuilder.AppendLine("});");
            return stringBuilder;
        }

        private StringBuilder GetBodyAndMeta(Incubator incubator)
        {
            StringBuilder script = new StringBuilder();
            StringBuilder meta = new StringBuilder();

            foreach (string className in incubator.ClassNames)
            {
                Type modelType = incubator[className];
                if (modelType.IsSubclassOf(typeof(Dao)))
                {
                    StringBuilder parameters;
                    StringBuilder body;

                    meta.AppendLine("\t\td.tables = d.tables || {};");
                    meta.AppendFormat("\t\td.tables.{0} = {{}};\r\n", className);
                    meta.AppendFormat("\t\td.tables.{0}.keyColumn = '{1}';\r\n", className, Dao.GetKeyColumnName(modelType));
                    meta.AppendFormat("\t\td.tables.{0}.cols = [];\r\n", className);
                    meta.AppendFormat("\t\td.tables.{0}.ctx = '{1}';\r\n\r\n", className, Dao.ConnectionName(modelType));

                    GetCtorParamsAndBody(modelType, meta, out parameters, out body);
                    script.AppendFormat("\t\td.ctors.{0} = function {0}(", className);
                    // -- params 
                    script.Append(parameters.ToString());
                    // -- end params
                    script.AppendLine("){");

                    // writing meta data
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

                            meta.AppendFormat("\t\td.tables.{0}.cols.push({{name: '{1}', type: '{2}', nullable: {3} }});\r\n", className, col.Name, typeName, col.AllowNull ? "true" : "false");
                        }

                        ForeignKeyAttribute fk;
                        if (prop.HasCustomAttributeOfType<ForeignKeyAttribute>(out fk))
                        {
                            meta.AppendFormat("\t\td.fks.push({{ pk: '{0}', pt: '{1}', fk: '{2}', ft: '{3}', nullable: {4} }});\r\n", fk.ReferencedKey, fk.ReferencedTable, fk.Name, fk.Table, fk.AllowNull ? "true" : "false");
                        }
                    }

                    // -- body
                    script.Append(body.ToString());
                    // -- end body
                    script.AppendLine("\t\t}");



                    // -- end writing meta data
                }
            }

            script.AppendLine(meta.ToString());
            return script;
        }

        private void GetCtorParamsAndBody(Type type, StringBuilder meta, out StringBuilder paramList, out StringBuilder body)
        {
            string ctorName = type.Name;//GetVarName(type);

            paramList = new StringBuilder();
            body = new StringBuilder();
            body.AppendFormat("\t\t\tthis.tableName = '{0}';\r\n", ctorName);
            body.AppendLine("\t\t\tthis.Dao = {};");
            body.AppendLine("\t\t\tthis.collections = {};");
            body.AppendFormat("\t\t\tthis.Dao.{0} = undefined;\r\n", Dao.GetKeyColumnName(type));

            PropertyInfo[] properties = (from prop in type.GetPropertiesWithAttributeOfType<ColumnAttribute>()
                                         where !prop.HasCustomAttributeOfType<KeyColumnAttribute>()
                                         select prop).ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];

                string propertyName = property.Name;
                paramList.Append(propertyName);
                if (i != properties.Length - 1)
                {
                    paramList.Append(", ");
                }

                ForeignKeyAttribute fk;
                if (property.HasCustomAttributeOfType<ForeignKeyAttribute>(out fk))
                {
                    string refProperty = string.Format("{0}Of{1}", fk.ReferencedTable, fk.Name);
                    body.AppendFormat("\t\t\tthis.{0} = new dao.wrapper('{1}', {2});\r\n", refProperty, fk.ReferencedTable, fk.Name);
                    
                    meta.AppendFormat("\t\td.ctors.{0}.prototype.{1}Collection = function(){{\r\n", fk.ReferencedTable, fk.Table);

                    meta.AppendFormat("\t\t\tif(_.isUndefined(this.collections.{0})){{\r\n", fk.Table);                    
                    meta.AppendFormat("\t\t\t\tthis.collections.{2} =  new dao.collection(this, '{0}', '{1}', '{2}', '{3}');\r\n", fk.ReferencedTable, fk.ReferencedKey, fk.Table, fk.Name);
                    meta.Append("\t\t\t}\r\n");
                    meta.AppendFormat("\t\t\treturn this.collections.{0};\r\n", fk.Table);
                    meta.Append("\t\t};\r\n");
                }
                else
                {
                    body.AppendFormat("\t\t\tthis.Dao.{0} = {0};\r\n", propertyName);
                }
            }

            meta.AppendFormat("\t\tfor(var f in dao.wrapper.prototype){{ d.ctors.{0}.prototype[f] = dao.wrapper.prototype[f];}}\r\n", ctorName);

            body.AppendFormat("\t\t\tthis.fks = function(){{ return dao.getFks('{0}');}};\r\n", Dao.TableName(type));
        }
        #endregion
    }
}
