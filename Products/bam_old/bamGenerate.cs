using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.CodeDom.Compiler;
using System.Threading;
using System.Net;
using System.Drawing;

using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Logging;
using Brevitee.Testing;
using Brevitee.Data;
using Brevitee.Data.Schema;
using Brevitee.Encryption;
using Brevitee.Html;
using Brevitee.Yaml;
using Brevitee.Javascript;
using Brevitee.Incubation;
using Brevitee.Configuration;
using Brevitee.Dust;
using Brevitee.Drawing;

using EcmaScript.NET;
using EcmaScript.NET.Types.Cli;
using EcmaScript.NET.Types;
using Bam.core;
using CsQuery;
using CsQuery.Web;

using Newtonsoft.Json;

public partial class bam
{
    Dictionary<string, Action> _generators;
    Dictionary<string, Action> _regenerators;
    public bam()
    {
        _generators = new Dictionary<string, Action>();
        _generators.Add("m", GenerateModels);
        _generators.Add("model", GenerateModels);
        _generators.Add("pv", GeneratePartialViews);
        _generators.Add("partialviews", GeneratePartialViews);
        _generators.Add("c", GenerateControllers);
        _generators.Add("controllers", GenerateControllers);
        _generators.Add("p", GeneratePages);
        _generators.Add("pages", GeneratePages);
        _generators.Add("i", GenerateImages);
        _generators.Add("images", GenerateImages);
        _generators.Add("a", GenerateAll);
        _generators.Add("all", GenerateAll);

        _regenerators = new Dictionary<string, Action>();
        //_regenerators.Add("p", 
    }

    private void GeneratePages()
    {
        Dust.DustRoot = GetAbsolutePath("~/content/templates");
        Dust.Initialize();

        GeneratePagesFromYaml();
        GeneratePagesFromJson();
    }

    private void GenerateAll()
    {
        string action = "generating models";
        try
        {
            GenerateModels();
        }
        catch (Exception ex)
        {
            OutputException(action, ex);
        }

        action = "generating partial views";
        try
        {
            GeneratePartialViews();
        }
        catch (Exception ex)
        {
            OutputException(action, ex);
        }

        action = "gerating controllers";
        try
        {
            GenerateControllers();
        }
        catch (Exception ex)
        {
            OutputException(action, ex);
        }

        action = "generating pages";
        try
        {
            GeneratePages();
        }
        catch (Exception ex)
        {
            OutputException(action, ex);
        }

        action = "generating images";
        try
        {
            GenerateImages();
        }
        catch (Exception ex)
        {
            OutputException(action, ex);
        }
    }

    private static void OutputException(string action, Exception ex)
    {
        OutFormat("An error occurred {0}: {1}", ConsoleColor.Red, action, ex.Message);
    }

    private void GenerateTests()
    {
        
        ForEachDaoType((t) => {
            foreach (Type type in t)
            {
                PropertyInfo[] props = type.GetPropertiesWithAttributeOfType<ColumnAttribute>();
                throw new NotImplementedException();
            }
        });
    }

    private void GeneratePagesFromYaml()
    {
        ForEachYamlFile(WritePageFromYamlFile);
    }

    private void ForEachYamlFile(Action<FileInfo> action)
    {
        DirectoryInfo yamlFolder = new DirectoryInfo(GetAbsolutePath("~/models/pages/yaml"));
        foreach (FileInfo file in yamlFolder.GetFiles("*.yaml"))
        {
            action(file);
        }
    }

    private void WritePageFromYamlFile(FileInfo file)
    {
        string yaml = File.ReadAllText(file.FullName);
        PageModel page = yaml.FromYaml<PageModel>();
        string fileName = string.Format("~/content/{0}.html", page.Name);
        string output = Dust.Render(page.LayoutTemplate, page).XmlToHumanReadable();
        Fs.WriteFile(fileName, string.Format("<!DOCTYPE html>\r\n{0}", output), Arguments.Contains("overwrite"));
    }

    private void ForEachJsonFile(Action<FileInfo> action)
    {
        DirectoryInfo jsonFolder = new DirectoryInfo(GetAbsolutePath("~/models/pages/json"));
        foreach (FileInfo file in jsonFolder.GetFiles("*.json"))
        {
            action(file);
        }
    }

    private void GeneratePagesFromJson()
    {
        ForEachJsonFile(WritePageFromJsonFile);
    }

    private void WritePageFromJsonFile(FileInfo file)
    {
        string json = File.ReadAllText(file.FullName);
        PageModel page = json.FromJson<PageModel>();
        string fileName = string.Format("~/content/{0}.html", page.Name);
        string output = Dust.Render(page.LayoutTemplate, page).XmlToHumanReadable();
        Fs.WriteFile(fileName, string.Format("<!DOCTYPE html>\r\n{0}", output), Arguments.Contains("overwrite"));
    }

    private void GenerateImages()
    {
        string name = Conf.ApplicationName.PascalSplit(" ");
        Font font = new Font(new FontFamily("Arial"), 25, FontStyle.Italic | FontStyle.Bold, GraphicsUnit.Pixel);
        Bitmap image = GraphicsManager.GetStringImage(254, 110, name, font, new SolidBrush(Color.Black));
        image.Save(Fs.GetAbsolutePath("~/content/images/logo.gif"));
        ColorScheme.LoadDefault(Fs).GenerateImages();
    }

    private bool TryGenerateModels(out Exception ex)
    {
        ex = null;
        bool result = true;
        try
        {
            GenerateModels();            
        }
        catch (Exception e)
        {
            ex = e;
            result = false;
        }

        return result;
    }

    private void GenerateModels()
    {
        DirectoryInfo info = new DirectoryInfo(GetAbsolutePath("~/models/dao/gen"));
        info.Delete(true);
        info.Create();
        //      create SchemaDefinition from yaml and json
        SchemaDefinition schema = BuildSchema(GetArgVal("c", GetArgVal("connection", "Default")));
        string nspace = ModelsNamespace;

        //      generate dao objects
        RazorBaseTemplate.DefaultInspector = (s) => { }; // turn off output to console
        DaoGenerator generator = new DaoGenerator(nspace);
        generator.BeforeClassParse += (ns, t) =>
        {
            Out(string.Format("Generating code for {0}.{1}", ns, t.Name), ConsoleColor.Yellow);
        };

        generator.AfterClassParse += (ns, t) =>
        {
            WritePartial(ns, t.Name);
        };

        generator.GenerateComplete += (g, s) =>
        {
            CompileModels(nspace, generator);
        };

        generator.Generate(schema, GetAbsolutePath("~/models/dao/gen/"));
    }

    private void CompileModels(string nspace, DaoGenerator generator)
    {
        DirectoryInfo dir = new DirectoryInfo(GetAbsolutePath("~/models/dao/gen"));
        DirectoryInfo partials = new DirectoryInfo(GetAbsolutePath("~/models/dao/partials"));

        string dllFile = string.Format("{0}.dll", nspace);
        //      compile                
        OutFormat("Compiling Models: {0} => {1}", ConsoleColor.Yellow, nspace, dllFile);
        CompilerResults results = generator.Compile(new DirectoryInfo[] { dir, partials }, dllFile);
        if (results.Errors.Count > 0)
        {
            OutputCompilerErrors(results);
        }
        else
        {
            string assembly = results.CompiledAssembly.CodeBase.Replace("file:///", "");
            OutFormat("Model assembly {0}", ConsoleColor.Green, assembly);

            //      copy dlls to ~/models/dao and ~/bin
            string workingCopy = GetAbsolutePath(string.Format("~/models/dao/dll/{0}", dllFile));
            string binCopy = GetAbsolutePath(string.Format("~/bin/{0}", dllFile));
            File.Copy(assembly, workingCopy, true);
            if (File.Exists(binCopy))
            {
                File.Delete(binCopy);
            }
            File.Move(assembly, binCopy);
        }
    }


    /// <summary>
    /// Writes a cs file to the ~/models/dao/partials/
    /// </summary>
    /// <param name="ns"></param>
    /// <param name="name"></param>
    /// <param name="classCode"></param>
    /// <param name="pathFormat"></param>
    private void WritePartial(string ns, string name, string classCode = "", string pathFormat = "~/models/dao/partials/{0}.cs", string extraUsings = "", string customClassAttribute = "")
    {
        Args.ThrowIf<InvalidOperationException>(string.IsNullOrEmpty(ns), "namespace must be specified");
        Args.ThrowIf<InvalidOperationException>(string.IsNullOrEmpty(name), "name must be specified");

        string code = @"using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Schema; 
using Brevitee.Data.Qi;
{3}

namespace {0}
{{
    {4}
    public partial class {1}
    {{
        {2}
    }}
}}";
        code = string.Format(code, ns, name, classCode, extraUsings, customClassAttribute);
        string partial = string.Format(pathFormat, name);
        if (Fs.FileExists(partial) && Arguments.Contains("overwrite"))
        {
            File.Delete(Fs.GetAbsolutePath(partial));
        }

        Fs.WriteFile(partial, code);
    }

    private static void OutputCompilerErrors(CompilerResults results)
    {
        foreach (CompilerError error in results.Errors)
        {
            OutFormat("File=>{0}", ConsoleColor.Yellow, error.FileName);
            OutFormat("Line {0}, Column {1}::{2}", error.Line, error.Column, error.ErrorText);
            Out();
        }
    }

    private SchemaDefinition BuildSchema(string connectionName = "Default")
    {
        SchemaDefinition schema = SchemaDefinition.Load(GetAbsolutePath("~/models/dao/schema.json"));
        // this is where the schema manager gets the current schema
        //SchemaManager.CurrentSchema = schema;
        SchemaManager dbm = new SchemaManager();
        List<Table> tables = new List<Table>();
        Dictionary<Tbl, Table> keyDic = new Dictionary<Tbl, Table>();

        AddYamlTables(tables, keyDic);
        AddJsonTables(tables, keyDic);

        schema.Tables = tables.ToArray(); // set the tables
        schema.Tables.Each<Table>((t) =>  // set the connectionName on each
        {
            t.ConnectionName = connectionName;  
        });

        foreach (Tbl tbl in keyDic.Keys)
        {
            foreach (Fk fk in tbl.Fks)
            {
                dbm.AddColumn(tbl.Name, new Column { Name = fk.Name, AllowNull = fk.Empty, Type = DataTypes.Long });
                dbm.SetForeignKey(fk.Ref, tbl.Name, fk.Name);
            }
        }
        SchemaDefinition current = dbm.GetCurrentSchema();
        return current;
    }

    private void AddYamlTables(List<Table> tables, Dictionary<Tbl, Table> keyDic)
    {
        DirectoryInfo yamlDir = new DirectoryInfo(GetAbsolutePath("~/models/dao/yaml"));
        FileInfo[] yamlFiles = yamlDir.GetFiles("*.yaml");
        int length = yamlFiles.Length;
        for (int i = 0; i < length; i++)
        {
            string yaml = "";
            FileInfo file = yamlFiles[i];
            using (StreamReader r = new StreamReader(file.OpenRead()))
            {
                yaml = r.ReadToEnd();
            }

            Tbl[] tbls = new Tbl[] { };
            if (file.Name.ToLowerInvariant().Equals("schema.yaml"))
            {
                Schema sch = yaml.FromYaml<Schema>();
                tbls = sch.Tables;
            }
            else
            {
                tbls = yaml.ArrayFromYaml<Tbl>();
            }
            // convert the Tbl instances to Tables and put them into the list
            tables.AddRange(tbls.Each((o) =>
            {
                return Convert((Tbl)o, keyDic);
            }));
        }
    }

    private void AddJsonTables(List<Table> tables, Dictionary<Tbl, Table> keyDic)
    {
        DirectoryInfo jsonDir = new DirectoryInfo(GetAbsolutePath("~/models/dao/json"));
        FileInfo[] jsonFiles = jsonDir.GetFiles("*.json");
        int length = jsonFiles.Length;
        for (int i = 0; i < length; i++)
        {
            string json = "";
            FileInfo file = jsonFiles[i];
            using (StreamReader r = new StreamReader(file.OpenRead()))
            {
                json = r.ReadToEnd();
            }

            Tbl[] tbls = new Tbl[] { };
            if (file.Name.ToLowerInvariant().Equals("schema.json"))
            {
                Schema sch = json.FromJson<Schema>();
                tbls = sch.Tables;
            }
            else
            {
                tbls = new Tbl[] { json.FromJson<Tbl>() };
            }

            tables.AddRange(tbls.Each((o) =>
            {
                return Convert((Tbl)o, keyDic);
            }));
        }
    }
    
    private void GeneratePartialViews()
    {
        ForEachDaoType(WritePartialViews);
    }

    private void GenerateControllers()
    {
        ForEachDaoType(WriteControllers);
        CompileControllers();
        // TODO: generate the tests
    }


    private void WriteControllers(Type[] daoTypes)
    {
        int il = daoTypes.Length;
        for (int i = 0; i < il; i++)
        {
            Type type = daoTypes[i];

            string filePathFormat = "~/controllers/{0}.cs";
            string classBody = @"
        public object Create({2}.{0} {1})
        {{
            return Update({1});
        }}

        public object Retrieve(long id)
        {{
            return {2}.{0}.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
        }}

        public object Update({2}.{0} {1})
        {{
            {1}.Save();
            return {1}.ToJsonSafe();
        }}
        
        public void Delete({2}.{0} {1})
        {{
            {1}.Delete();            
        }}

        public object[] Search(QiQuery query)
        {{
            return new {2}.Qi.{0}().Where(query);
        }}

        [Exclude]
        public static Type GetModelType()
        {{
            return typeof({2}.{0});          
        }}
";
            string typeName = type.Name;
            string filePath = string.Format(filePathFormat, typeName);

            if (Fs.FileExists(filePath) && !Arguments.Contains("overwrite"))
            {
                OutFormat("File exists and won't be overwritten {0}", ConsoleColor.Yellow, filePath);
            }

            WritePartial(ControllersNamespace,
                typeName,
                string.Format(classBody, typeName, typeName.ToLowerInvariant(), ModelsNamespace),
                filePathFormat,
                string.Format("using {0};", ModelsNamespace),
                string.Format("[Proxy(\"{0}\")]", typeName.CamelCase(true))
                );
        }
    }

    private void WritePartialViews(Type[] daoTypes)
    {
        int il = daoTypes.Length;
        for (int ii = 0; ii < il; ii++)
        {
            Type daoType = daoTypes[ii];
            WritePartialView(daoType);
        }
    }

    private void WritePartialView(Type daoType)
    {
        Type safeType = daoType.CreateDynamicType<Brevitee.Data.ColumnAttribute>();
        object instance = ConstructAndSetTemplateProperties(safeType);
        string htm = InputFor(safeType, instance).XmlToHumanReadable();
        string fileName = GetAbsolutePath(string.Format("~/content/templates/partial/{0}/Default.dust", daoType.Name));
        bool overwrite = false;
        if (File.Exists(fileName))
        {
            overwrite = Arguments.Contains("overwrite");
        }
        OutFormat("Writing {0}", ConsoleColor.Green, fileName);
        htm.SafeWriteToFile(fileName, overwrite);
    }

    public string InputFor(Type type, object defaults = null, string name = null)
    {
        InputFormBuilder builder = new InputFormBuilder();
        return builder.FieldsetFor(type, defaults, name).ToString();
    }

}
