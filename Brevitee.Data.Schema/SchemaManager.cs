using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.IO;
using Brevitee;
using Brevitee.Data;
using System.Web;
using Brevitee.ServiceProxy;
using Brevitee.ServiceProxy.Js;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Brevitee.Javascript;
using System.Threading;

namespace Brevitee.Data.Schema
{
    [Proxy("dbm")]
    public class SchemaManager
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        
        public SchemaManager()
        {

        }

        SchemaDefinition _currentSchema;
        object _currentSchemaLock = new object();
        public SchemaDefinition CurrentSchema
        {
            get
            {
                return _currentSchemaLock.DoubleCheckLock<SchemaDefinition>(ref _currentSchema, () => LoadSchema(string.Format("Default_{0}", DateTime.Now.ToJulianDate().ToString())));
            }

            set
            {
                _currentSchema = value;
            }
        }
        
        private static string AppDataFolder
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                }
                else
                {
                    return HttpContext.Current.Server.MapPath("~/App_Data/");
                }
            }
        }

        /// <summary>
        /// Loads the specified schema and sets it as Current
        /// </summary>
        /// <param name="schemaName"></param>
        /// <returns></returns>
        public SchemaDefinition SetSchema(string schemaName, bool useExisting = true)
        {
            lock (_sync)
            {
                string filePath = SchemaNameToFilePath(schemaName);
                if (!useExisting && File.Exists(filePath))
                {                    
                    string backUpPath = SchemaNameToFilePath("{0}_{1}"._Format(schemaName, DateTime.UtcNow.ToJulianDate()));
                    File.Move(filePath, backUpPath);
                }
                SchemaDefinition schema = LoadSchema(schemaName);
                CurrentSchema = schema;
                return schema;
            }
        }

        /// <summary>
        /// Calls SetSchema if the specified schema does not already
        /// exist.
        /// </summary>
        /// <param name="schemaName"></param>
        /// <returns></returns>
        public SchemaDefinition SetNewSchema(string schemaName)
        {
            if (SchemaExists(schemaName))
            {
                throw new InvalidOperationException("The specified schema already exists");
            }

            return SetSchema(schemaName);
        }

        public Table GetTable(string tableName)
        {
            return CurrentSchema.GetTable(tableName);
        }

        public XrefTable GetXref(string tableName)
        {
            return CurrentSchema.GetXref(tableName);
        }

        private static SchemaDefinition LoadSchema(string schemaName)
        {
            string schemaFile = SchemaNameToFilePath(schemaName);
            SchemaDefinition schema = SchemaDefinition.Load(schemaFile);
            schema.Name = schemaName;
            return schema;
        }

        public static bool SchemaExists(string schemaName)
        {
            return File.Exists(SchemaNameToFilePath(schemaName));
        }

        private static string SchemaNameToFilePath(string schemaName)
        {
            string schemaFile = Path.Combine(AppDataFolder, "{0}.json"._Format(schemaName));
            return schemaFile;
        }

        public SchemaDefinition GetCurrentSchema()
        {
            return CurrentSchema;
        }

        public Result AddTable(string tableName)
        {
            lock (_sync)
            {
                try
                {
                    SchemaDefinition schema = CurrentSchema;
                    Table t = new Table();
                    t.Name = tableName;
                    Result result = schema.AddTable(t);
                    schema.Save();
                    return result;
                }
                catch (Exception ex)
                {
                    return GetErrorResult(ex);
                }
            }
        }

        public Result AddXref(string left, string right)
        {
            lock (_sync)
            {
                try
                {
                    SchemaDefinition schema = CurrentSchema;
                    XrefTable x = new XrefTable(left, right);
                    Result result = schema.AddXref(x);
                    schema.Save();
                    return result;
                }
                catch (Exception ex)
                {
                    return GetErrorResult(ex);
                }
            }
        }

        private static Result GetErrorResult(Exception ex)
        {
            Result result = new Result(ex.Message);
            result.ExceptionMessage = ex.Message;
            result.Success = false;
#if DEBUG
            result.StackTrace = ex.StackTrace;
#endif
            return result;
        }

        /// <summary>
        /// Add the specified column to the specified table.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public Result AddColumn(string tableName, Column column)
        {
            lock (_sync)
            {
                try
                {
                    Table table = CurrentSchema.GetTable(tableName);
                    table.AddColumn(column);
                    CurrentSchema.Save();
                    return new Result("column added");
                }
                catch (Exception ex)
                {
                    return GetErrorResult(ex);
                }
            }
        }

        public Result RemoveColumn(string tableName, string columnName)
        {
            lock (_sync)
            {
                try
                {
                    Table table = CurrentSchema.GetTable(tableName);
                    table.RemoveColumn(columnName);
                    CurrentSchema.Save();
                    return new Result("column removed");
                }
                catch (Exception ex)
                {
                    return GetErrorResult(ex);
                }
            }
        }

        public Result SetKeyColumn(string tableName, string columnName)
        {
            lock (_sync)
            {
                try
                {
                    Table table = CurrentSchema.GetTable(tableName);
                    table.SetKeyColumn(columnName);
                    CurrentSchema.Save();
                    return new Result("Key column set");
                }
                catch (Exception ex)
                {
                    return GetErrorResult(ex);
                }
            }
        }

        public Result SetForeignKey(string targetTable, string referencingTable, string referencingColumn)
        {
            lock (_sync)
            {
                try
                {
                    Table table = CurrentSchema.GetTable(referencingTable);
                    Table target = CurrentSchema.GetTable(targetTable);
                    Column col = table[referencingColumn];
                    if (col.Type == DataTypes.Int || col.Type == DataTypes.Long)
                    {
                        ForeignKeyColumn fk = new ForeignKeyColumn(col, targetTable);
                        fk.ReferencedKey = target.Key != null ? target.Key.Name.PascalCase(true, " ", "_").DropLeadingNonLetters() : string.Empty;
                        fk.ReferencedTable = target.Name;
                        return SetForeignKey(table, target, fk);
                    }
                    else
                    {
                        throw new InvalidOperationException("The specified column must be a number type");
                    }
                }
                catch (Exception ex)
                {
                    return GetErrorResult(ex);
                }
            }
        }

        protected virtual Result SetForeignKey(Table table, Table target, ForeignKeyColumn fk)
        {
            CurrentSchema.AddForeignKey(fk);
            table.RemoveColumn(fk.Name);
            table.AddColumn(fk);
            target.ReferencingForeignKeys = GetReferencingForeignKeysForTable(target.Name);
            CurrentSchema.Save();
            return new Result("ForeignKeyColumn set");
        }

        protected void SetXrefs(XrefTable[] xrefs)
        {
            CurrentSchema.Xrefs = xrefs;
        }

        /// <summary>
        /// Get the ForeignKeyColumns where the specified table is the 
        /// referenced table.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected ForeignKeyColumn[] GetReferencingForeignKeysForTable(string tableName)
        {
            string lowered = tableName.ToLowerInvariant();
            return (from f in CurrentSchema.ForeignKeys
                    where f.ReferencedTable.ToLowerInvariant().Equals(lowered)
                    select f).ToArray();
        }

        protected ForeignKeyColumn[] GetForeignKeysForTable(string tableName)
        {
            string lowered = tableName.ToLowerInvariant();
            return (from f in CurrentSchema.ForeignKeys
                    where f.TableName.ToLowerInvariant().Equals(lowered)
                    select f).ToArray();
        }

        public void Save()
        {
            lock (_sync)
            {
                StringBuilder builder = new StringBuilder();
                serializer.Serialize(CurrentSchema, builder);
                builder.ToString().SafeWriteToFile(CurrentSchema.File, true);
            }
        }

        public void RemoveTable(string tableName)
        {
            CurrentSchema.RemoveTable(tableName);
        }
            
        public Result Generate(FileInfo databaseDotJs, bool compile = false, bool keepSource = false, string genTo = ".\\tmp")
        {
            string result = databaseDotJs.JsonFromJsLiteralFile("database");//JsonFromJsLiteralFile(databaseDotJs);

            return Generate(result, compile ? new DirectoryInfo(BinDir): null, keepSource, genTo);
        }

        object _sync = new object();
        /// <summary>
        /// Generate 
        /// </summary>
        /// <param name="simpleSchemaJson"></param>
        /// <returns></returns>
        public Result Generate(string simpleSchemaJson, DirectoryInfo compileTo = null, bool keepSource = false, string tempDir = ".\\tmp")
        {
            try
            {
                lock (_sync)
                {
                    bool compile = compileTo != null;
                    Result result = new Result("Generation completed");
                    dynamic rehydrated = JsonConvert.DeserializeObject<dynamic>(simpleSchemaJson);
                    if (rehydrated["nameSpace"] == null || rehydrated["schemaName"] == null)
                    {
                        result.ExceptionMessage = "Please specify nameSpace and schemaName";
                        result.Success = false;
                    }
                    else
                    {
                        SchemaManager manager = new SchemaManager();
                        string nameSpace = (string)rehydrated["nameSpace"];
                        string schemaName = (string)rehydrated["schemaName"];
                        result.Namespace = nameSpace;
                        result.SchemaName = schemaName;
                        List<dynamic> foreignKeys = new List<dynamic>();
                        
                        manager.SetSchema(schemaName, false);

                        ProcessTables(rehydrated, manager, foreignKeys);
                        ProcessXrefs(rehydrated, manager, foreignKeys);

                        foreach (dynamic fk in foreignKeys)
                        {
                            manager.AddColumn(fk.ForeignKeyTable, new Column(fk.ReferencingColumn, DataTypes.Long));
                            manager.SetForeignKey(fk.PrimaryTable, fk.ForeignKeyTable, fk.ReferencingColumn);
                        }

                        DirectoryInfo daoDir = new DirectoryInfo(tempDir);//new DirectoryInfo(HttpContext.Current.Server.MapPath(string.Format("~/dao/{0}", nameSpace)));
                        if (!daoDir.Exists)
                        {
                            daoDir.Create();
                        }

                        RazorBaseTemplate.DefaultInspector = (s) => { }; // turn off output to console
                        DaoGenerator generator = new DaoGenerator(nameSpace);
                        if (compile)
                        {
                            if (!compileTo.Exists)
                            {
                                compileTo.Create();
                            }

                            generator.GenerateComplete += (g, s) =>
                            {
                                Compile(daoDir, generator, nameSpace, compileTo);
                                if (CompilerErrors != null)
                                {
                                    result.Success = false;
                                    result.Message = string.Empty;
                                    foreach (CompilerError err in GetErrors())
                                    {
                                        result.Message = string.Format("{0}\r\nFile=>{1}\r\n{2}:::Line {3}, Column {4}::{5}",
                                                result.Message, err.FileName, err.ErrorNumber, err.Line, err.Column, err.ErrorText);
                                    }
                                }
                                else
                                {
                                    result.Message = string.Format("{0}\r\n{1}", result.Message, "Dao compiled");
                                    result.Success = true;
                                }

                                if (!keepSource)
                                {
                                    daoDir.Delete(true);
                                }
                            };
                        }

                        generator.Generate(manager.CurrentSchema, daoDir.FullName);
                    }
                    return result;
                }
            }
            catch(Exception ex)
            {
                Result r = new Result(ex.Message);
                r.StackTrace = ex.StackTrace ?? "";
                r.Success = false;
                return r;
            }
        }

        private void ProcessXrefs(dynamic rehydrated, SchemaManager manager, List<dynamic> foreignKeys)
        {
            foreach (dynamic xref in rehydrated["xrefs"])
            {
                string leftTableName = (string)xref[0];
                string rightTableName = (string)xref[1];
                
                Args.ThrowIfNullOrEmpty(leftTableName, "xref[0]");
                Args.ThrowIfNullOrEmpty(rightTableName, "xref[1]");
                
                string xrefTableName = string.Format("{0}{1}", leftTableName, rightTableName);
                string leftColumnName = string.Format("{0}Id", leftTableName);
                string rightColumnName = string.Format("{0}Id", rightTableName);
                                
                manager.AddXref(leftTableName, rightTableName);

                manager.AddTable(xrefTableName);
                manager.AddColumn(xrefTableName, new Column("Id", DataTypes.Long, false));
                manager.SetKeyColumn(xrefTableName, "Id");
                manager.AddColumn(xrefTableName, new Column(leftColumnName, DataTypes.Long, false));
                manager.AddColumn(xrefTableName, new Column(rightColumnName, DataTypes.Long, false));
                
                AddForeignKey(foreignKeys, leftTableName, xrefTableName, leftColumnName);
                AddForeignKey(foreignKeys, rightTableName, xrefTableName, rightColumnName);
            }
        }

        private void ProcessTables(dynamic rehydrated, SchemaManager manager, List<dynamic> foreignKeys)
        {
            foreach (dynamic table in rehydrated["tables"])
            {
                string tableName = (string)table["name"];
                Args.ThrowIfNullOrEmpty(tableName, "Table.name");

                manager.AddTable(tableName);
                manager.AddColumn(tableName, new Column("Id", DataTypes.Long, false));
                manager.SetKeyColumn(tableName, "Id");

                if (table["cols"] != null)
                {
                    foreach (dynamic column in table["cols"])
                    {
                        PropertyDescriptorCollection columnProperties = TypeDescriptor.GetProperties(column);
                        bool allowNull = column["Null"] == null ? true : (bool)column["Null"];
                        string maxLength = column["MaxLength"] == null ? "" : (string)column["MaxLength"];
                        foreach (PropertyDescriptor pd in columnProperties)
                        {
                            if (!pd.Name.Equals("Null") && !pd.Name.Equals("MaxLength"))
                            {
                                DataTypes type = (DataTypes)Enum.Parse(typeof(DataTypes), (string)pd.GetValue(column));
                                string name = pd.Name;
                                manager.AddColumn(tableName, new Column(name, type, allowNull, maxLength));
                            }
                        }
                    }
                }

                if (table["fks"] != null)
                {
                    foreach (dynamic fk in table["fks"])
                    {
                        PropertyDescriptorCollection fkProperties = TypeDescriptor.GetProperties(fk);
                        foreach (PropertyDescriptor pd in fkProperties)
                        {
                            string referencingColumn = pd.Name;
                            string primaryTable = (string)pd.GetValue(fk);
                            string foreignKeyTable = tableName;
                            AddForeignKey(foreignKeys, primaryTable, foreignKeyTable, referencingColumn);
                        }
                    }
                }
            }
        }

        private static void AddForeignKey(List<dynamic> foreignKeys, string primaryTable, string foreignKeyTable, string referencingColumnName)
        {
            foreignKeys.Add(new { PrimaryTable = primaryTable, ForeignKeyTable = foreignKeyTable, ReferencingColumn = referencingColumnName });
        }
        
        private FileInfo Compile(DirectoryInfo dir, DaoGenerator generator, string nameSpace, DirectoryInfo copyTo)
        {
            string[] referenceAssemblies = DaoGenerator.DefaultReferenceAssemblies.ToArray();
            for (int i = 0; i < referenceAssemblies.Length; i++)
            {
                string assembly = referenceAssemblies[i];
                string binPath = Path.Combine(BinDir, assembly);

                referenceAssemblies[i] = File.Exists(binPath) ? binPath : assembly;
            }

            CompilerResults results = generator.Compile(dir, string.Format("{0}.dll", nameSpace), referenceAssemblies);
            if (results.Errors.Count > 0)
            {
                CompilerErrors = results.Errors;
                return null;
            }
            else
            {
                CompilerErrors = null;
                DirectoryInfo bin = new DirectoryInfo(BinDir);
                FileInfo dll = new FileInfo(results.CompiledAssembly.CodeBase.Replace("file:///", ""));
                string binFile = Path.Combine(bin.FullName, dll.Name);
                string copy = Path.Combine(copyTo.FullName, dll.Name);
                if (File.Exists(binFile))
                {
                    BackupFile(binFile);
                }
                dll.CopyTo(binFile);
                if (!binFile.ToLowerInvariant().Equals(copy.ToLowerInvariant()))
                {
                    if (File.Exists(copy))
                    {
                        BackupFile(copy);
                    }

                    dll.CopyTo(copy);
                    if (!dll.FullName.ToLowerInvariant().Equals(copy.ToLowerInvariant()))
                    {
                        dll.Delete();
                    }
                }
                return new FileInfo(copy);                
            }
        }

        private static void BackupFile(string fileName)
        {
            FileInfo binFileInfo = new FileInfo(fileName);
            FileInfo backupFile = new FileInfo(Path.Combine(
                        binFileInfo.Directory.FullName,
                        "backup",
                        string.Format("{0}_{1}_{2}.dll", Path.GetFileNameWithoutExtension(fileName), "".RandomLetters(4), DateTime.Now.ToJulianDate().ToString())));

            if (!backupFile.Directory.Exists)
            {
                backupFile.Directory.Create();
            }
            binFileInfo.MoveTo(backupFile.FullName);
        }

        public string BinDir
        {
            get
            {
                return Path.Combine(RootDir, "bin");
            }
        }

        string _rootDir; 
        public string RootDir
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Server.MapPath("~/");
                }
                else
                {
                    return _rootDir ?? ".";
                }
            }

            set
            {
                _rootDir = value;
            }
        }

        /// <summary>
        /// Gets the most recent set of exceptions that occurred during an attempted
        /// Generate -> Compile
        /// </summary>
        public CompilerErrorCollection CompilerErrors
        {
            get;
            private set;
        }

        public CompilerError[] GetErrors()
        {
            if (CompilerErrors == null)
            {
                return new CompilerError[] { };
            }

            CompilerError[] results = new CompilerError[CompilerErrors.Count];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = CompilerErrors[i];
            }

            return results;
        }
    }
}
