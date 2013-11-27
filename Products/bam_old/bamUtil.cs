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
    static List<string> _directories;
    static List<string> _dustLayoutResources;
    static List<string> _dustPartialResources;
    static List<string> _lessResources;

    static bam()
    {
        ResourceTextFiles.AddExtensionToLoad(".dust");
        ResourceTextFiles.AddExtensionToLoad(".html");
        ResourceTextFiles.AddExtensionToLoad(".less");

        _directories = new List<string>();
        _directories.Add("~/app_data");
        _directories.Add("~/bin");
        _directories.Add("~/content");
        _directories.Add("~/content/css");
        _directories.Add("~/content/images");
        _directories.Add("~/content/less");
        _directories.Add("~/content/scripts");
        _directories.Add("~/content/templates");
        _directories.Add("~/content/templates/layout");
        _directories.Add("~/content/templates/partial");        
        _directories.Add("~/controllers");
        _directories.Add("~/models");
        _directories.Add("~/models/pages/yaml");
        _directories.Add("~/models/pages/json");
        _directories.Add("~/models/dao");
        _directories.Add("~/models/dao/json");
        _directories.Add("~/models/dao/yaml");
        _directories.Add("~/models/dao/dll");
        _directories.Add("~/models/dao/gen");
        _directories.Add("~/models/dao/partials");
        _directories.Add("~/scripts");

        _dustLayoutResources = new List<string>();
        _dustLayoutResources.Add("sidebarleft");
        _dustLayoutResources.Add("sidebarright");
        _dustLayoutResources.Add("sanssidebar");

        _dustPartialResources = new List<string>();
        _dustPartialResources.Add("subsection");
        _dustPartialResources.Add("hexcolor");
        _dustPartialResources.Add("hexcolor_w_delete");
        
        _lessResources = new List<string>();
        _lessResources.Add("qunit");
        _lessResources.Add("jquery-ui");
    }

    /// <summary>
    /// Gets script file information from the provided command line arguments
    /// or prompts the user
    /// </summary>
    /// <param name="file"></param>
    /// <param name="scriptNames"></param>
    private static void GetFileInfo(string arg, out string file, out string[] scriptNames)
    {
        string scripts = GetArgVal(arg, "");
        if (string.IsNullOrEmpty(scripts))
        {
            scripts = Prompt("Please enter a comma separated list of scripts");
        }

        file = GetArgVal("t", GetArgVal("target", ""));
        if (string.IsNullOrEmpty(file))
        {
            file = Prompt("Please enter the name of the file to write");
        }

        scriptNames = scripts.DelimitSplit(",", ";");
        if (scriptNames.Length <= 1)
        {
            file = scriptNames[0];
        }

        if (file.Equals("auto"))
        {
            AutoNameScriptFile(scriptNames);
        }
    }

    private void CreateDirectories()
    {
        foreach (string dir in _directories)
        {
            CreateDirectory(dir);
        }
    }

    private void WriteSampleDataModel()
    {
        // write yaml example
        Tbl userTable = new Tbl("User");
        userTable.AddColumn("nickName");
        userTable.AddColumn("first");
        userTable.AddColumn("last");

        Tbl passwordTable = new Tbl("Password");
        passwordTable.AddFk("User", "userId", false);
        passwordTable.AddColumn("SHA1");

        Schema yamlSchema = new Schema();
        yamlSchema.AddTable(userTable);
        yamlSchema.AddTable(passwordTable);

        WriteFile("~/models/dao/yaml/Schema.yaml", yamlSchema.ToYaml());

        // write json example, they'll be mixed together
        Tbl sessionTable = new Tbl("Session");
        sessionTable.AddFk("User", "userId", false);
        sessionTable.AddColumn("hash");
        sessionTable.AddColumn("hashAlgorithm");
        sessionTable.AddColumn("key");
        sessionTable.AddColumn("value");

        Tbl emailTable = new Tbl("Email");
        emailTable.AddFk("User", "userId", false);
        emailTable.AddColumn("address");
        emailTable.AddColumn(new Col { Empty = true, Name = "optOut", Type = DataTypes.Boolean });

        Tbl rolesTable = new Tbl("Role");
        rolesTable.AddColumn("Name");

        Tbl userRolesTable = new Tbl("UserRole");
        userRolesTable.AddFk("User", "userId", false);
        userRolesTable.AddFk("Role", "roleId", false);

        Schema jsonSchema = new Schema();
        jsonSchema.AddTable(sessionTable);
        jsonSchema.AddTable(emailTable);
        jsonSchema.AddTable(rolesTable);
        jsonSchema.AddTable(userRolesTable);

        WriteFile("~/models/dao/json/Schema.json", jsonSchema.ToJson(true));
    }

    private void WriteSamplePageModels()
    {
        PageModel pageData = CreateBasePageAndContentModels();

        pageData.LayoutTemplate = "layout_sidebarright";
        WriteFile("~/models/pages/yaml/Index.yaml", pageData.ToYaml());
        
        pageData.Name = "About";  
        pageData.LayoutTemplate = "layout_sidebarleft";
        pageData.Content = ResourceTextFiles.ReadTextFile("about.html");        
        WriteFile("~/models/pages/json/About.json", pageData.ToJson());

        pageData.Name = "Design";
        pageData.Content = ResourceTextFiles.ReadTextFile("design.html");
        pageData.LayoutTemplate = "layout_sanssidebar";
        pageData.AddScript("design.js");
        pageData.AddScript("tests.js");
        pageData.AddScript("qunit.js");
        pageData.AddStylesheet("qunit.css");
        
        WriteFile("~/models/pages/yaml/Design.yaml", pageData.ToYaml());
    }

    private PageModel CreateBasePageAndContentModels()
    {
        PageModel pageData = new PageModel();
        pageData.Scripts = new string[] {
            "jquery.js", 
            "jquery-ui.js",
            "underscore.js",
            "dust.js",
            "schema.js",
            "qi.js", 
            "bam.js", 
            "dataset.js",
            "dao.js",
            "eazel.js",
            "proxies",
            "ctors",
            "templates"
        };

        pageData.Stylesheets = new string[]{
            "style.css",
            "jquery-ui.css"
        };
        
        pageData.Name = "Index";
        pageData.Title = Conf.ApplicationName.PascalSplit(" ");
        pageData.ApplicationName = Conf.ApplicationName;

        pageData.Year = DateTime.Now.Year.ToString();

        pageData.NavList = new LinkModel[]{
            new LinkModel{ Href="about.html", Text="About us"},
            new LinkModel{ Href="account.html", Text="My Account"},
            new LinkModel{ Href="login.html", Text="Login"},
        };
        pageData.Pages = new LinkModel[]{
            new LinkModel{ Href="index.html", Text="Home"}          
        };

        pageData.SectionList = new LinkModel[]{
            new LinkModel{ Text="Section 1", Name="Section1", Href = "#Section1", SubLinks = new LinkModel[]{
                new LinkModel{ Href="#Subsection1", Text="Subsection 1", Name="Subsection1"},
                new LinkModel{ Href="#Subsection2", Text="Subsection 2", Name="Subsection2"}
            }},
            new LinkModel{ Text="Section 2", Name="Section2", Href="#Section2", SubLinks = new LinkModel[]{
                new LinkModel{ Href="#NextSubsection1", Text="Next Subsection 1", Name="NextSubsection1"},
                new LinkModel{ Href="#AndSoON", Text="And so on", Name="AndSoON"}
            }}
        };

        return pageData;
    }

    private void WriteResoucesToDisk()
    {
        WriteDustLayoutResourcesToDisk();
        WriteDustPartialResourcesToDisk();
        WriteLessResourcesToDisk();

        string bam = ResourceTextFiles.ReadTextFile("bam.txt", typeof(bam).Assembly);
        WriteFile("~/bam.txt", bam);
        string images = ResourceTextFiles.ReadTextFile("images.txt", typeof(bam).Assembly);
        WriteFile("~/images.txt", images);
    }

    private void WriteDustLayoutResourcesToDisk()
    {
        foreach (string name in _dustLayoutResources)
        {
            WriteDustResourceToDisk(name, "~/content/templates/layout/{0}.dust");
        }
    }
    
    private void WriteDustPartialResourcesToDisk()
    {
        foreach (string name in _dustPartialResources)
        {
            WriteDustResourceToDisk(name, "~/content/templates/partial/{0}.dust");
        }
    }

    private void WriteDustResourceToDisk(string name, string pathFormat)
    {
        string resourceFile = string.Format("{0}.dust", name);
        string diskFile = string.Format(pathFormat, name);
        string content = ResourceTextFiles.ReadTextFile(resourceFile, typeof(bam).Assembly);

        WriteFile(diskFile, content);
    }

    private void WriteLessResourcesToDisk()
    {
        foreach (string lessFile in _lessResources)
        {
            WriteLessResourceToDisk(lessFile);
        }
    }

    private void WriteLessResourceToDisk(string name)
    {
        string resourceFile = string.Format("{0}.less", name);
        string diskFile = string.Format("~/content/less/{0}.less", name);
        string content = ResourceTextFiles.ReadTextFile(resourceFile, typeof(bam).Assembly);

        WriteFile(diskFile, content);
    }

    private static string GetLoremIpsum()
    {
        try
        {
            WebClient client = GetWebClient();
            string html = client.DownloadString("http://www.lipsum.com/feed/html");
            CQ cq = CQ.Create(html);
            return new Tag("div").Html(cq["#lipsum"].Html()).ToHtmlString();
        }
        catch (Exception ex)
        {
            return string.Format("An error occurred retrieving lorem ipsum: {0}", ex.Message);
        }
    }

    private static WebClient GetWebClient()
    {
        WebClient client = new WebClient();
        client.Headers["User-Agent"] =
    "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
    "(compatible; MSIE 6.0; Windows NT 5.1; " +
    ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        return client;
    }

    public string ModelsNamespace
    {
        get
        {
            return string.Format("{0}.Models", ApplicationName.PascalCase(true, " ", "_", "."));
        }
    }

    public string ControllersNamespace
    {
        get
        {
            return string.Format("{0}.Controllers", ApplicationName.PascalCase(true, " ", "_", "."));
        }
    }

    private void SetAppName(string appName)
    {
        OutFormat("Setting application name to {0}", ConsoleColor.Green, appName);
        Conf.ApplicationName = appName;
        Conf.Save();
    }
    
    private void ForEachController(Action<Type[]> doForEachControllerType)
    {
        DirectoryInfo ctrlrDir = new DirectoryInfo(GetAbsolutePath("~/bin"));
        FileInfo[] files = ctrlrDir.GetFiles("*.dll");
        int ol = files.Length;
        for (int i = 0; i < ol; i++)
        {
            FileInfo file = files[i];
            Assembly controllerAssembly = Assembly.LoadFrom(file.FullName);
            Type[] controllerTypes = (from type in controllerAssembly.GetTypes()
                                      where type.Namespace.Equals(ControllersNamespace) &&
                                      type.HasCustomAttributeOfType<ProxyAttribute>()
                                      select type).ToArray();
            doForEachControllerType(controllerTypes);
        }
    }

    private void ForEachDaoType(Action<Type[]> doForEachDaoType)
    {
        // analyze dlls for dao classes  
        DirectoryInfo daoFolder = new DirectoryInfo(GetAbsolutePath("~/models/dao/dll"));
        FileInfo[] files = daoFolder.GetFiles("*.dll");
        int ol = files.Length;
        for (int i = 0; i < ol; i++)
        {
            FileInfo file = files[i];
            Assembly daoAssembly = Assembly.LoadFrom(file.FullName);
            Type[] daoTypes = (from type in daoAssembly.GetTypes()
                               where type.HasCustomAttributeOfType<Brevitee.Data.TableAttribute>()
                               select type).ToArray();

            doForEachDaoType(daoTypes);
        }
    }

    private object ConstructAndSetTemplateProperties(Type type)
    {
        object o = type.Construct();
        PropertyInfo[] properties = type.GetProperties();
        foreach (PropertyInfo prop in properties)
        {
            if (prop.PropertyType == typeof(string) ||
                prop.PropertyType == typeof(int) ||
                prop.PropertyType == typeof(long))
            {
                prop.SetValue(o, "{" + prop.Name + "}", null);
            }
        }

        return o;
    }


    
    static bool _subscribed;
    static object _subscribeLock = new object();
    private static void SetHandler(MultiTargetLogger mtl, RequestHandler handler)
    {
        Incubator.Default.Set<IRequestHandler>(() =>
        {
            ResponderEventHandler responded = (r, c) =>
            {
                mtl.AddEntry("{0}: responded for {1}", r.GetType().Name, c.Request.RawUrl, c.Request.UserHostAddress);
            };

            ResponderEventHandler notResponded = (r, c) =>
            {
                mtl.AddEntry("{0}: NO response for {1}", r.GetType().Name, c.Request.RawUrl);
            };

            if (!_subscribed)
            {
                lock (_subscribeLock)
                {
                    _subscribed = true;

                    foreach (IResponder responder in handler.Responders)
                    {
                        responder.Responded += responded;
                        responder.NotResponded += notResponded;
                    }
                }
            }
            return handler;
        });
    }

    private Table Convert(Tbl tbl, Dictionary<Tbl, Table> keyDic)
    {
        Table result = new Table(tbl.Name, tbl.Conx);
        foreach (Col c in tbl.Cols)
        {
            result.AddColumn(c.Name, c.Type, c.Empty);
        }

        if (tbl.Pk != null)
        {
            Col pk = tbl.Pk;
            result.AddColumn(pk.Name, pk.Type, pk.Empty);
            result.SetKeyColumn(pk.Name);
        }
        else
        {
            // make sure that there is an Id/Pk if none is defined in the yaml
            result.AddColumn("Id", DataTypes.Long, false);
            result.SetKeyColumn("Id");
        }

        keyDic.Add(tbl, result);

        return result;
    }

    private void CreateDirectory(string directory)
    {
        Fs.CreateDirectory(directory);
    }

    private void WriteFile(string relativeFilePath, string text)
    {
        Fs.WriteFile(relativeFilePath, text);
    }

    private void AppendToFile(string relativeFilePath, string text)
    {
        Fs.AppendToFile(relativeFilePath, text);
    }

    private string GetAbsolutePath(string relativePath)
    {
        return Fs.GetAbsolutePath(relativePath);
    }

    private static string GetArgVal(string argName, string defaultVal = "", bool dieIfNotSpecified = false)
    {
        if (string.IsNullOrEmpty(argName))
        {
            throw new ArgumentNullException("argName");
        }

        string val = string.Empty;
        string shortName = argName[0].ToString();
        if (Arguments.Contains(argName))
        {
            val = Arguments[argName];
        }
        else if (Arguments.Contains(shortName))
        {
            val = Arguments[shortName];
        }
        else if (dieIfNotSpecified)
        {
            OutFormat("{0} wasn't specified", ConsoleColor.Red, argName);
            Exit(1);
        }

        if (string.IsNullOrEmpty(val) || val.Equals("true"))
        {
            val = defaultVal;
        }

        return val;
    }


    private static MultiTargetLogger CreateLogger()
    {
        MultiTargetLogger logger = new MultiTargetLogger();
        DaoLogger dbLogger = new DaoLogger();
        ConsoleLogger cl = new ConsoleLogger();
        cl.UseColors = true;
        cl.AddDetails = false;

        logger.AddLogger(dbLogger);
        logger.AddLogger(cl);
        logger.StartLoggingThread();
        return logger;
    }

    private void RegisterControllers(RequestHandler handler)
    {
        handler.ResponderAdded += (h, r) =>
        {
            Execution exe = r as Execution;
            if (exe != null)
            {
                RegisterControllers(exe);
            }
        };
        handler.InitializeResponders();
    }

    private void RegisterControllers(Execution executor)
    {
        List<string> registered = new List<string>();
        ForEachController((t) =>
        {
            int l = t.Length;
            for (int i = 0; i < l; i++)
            {
                Type current = t[i];
                executor.Add(current, current.Construct());
            }
        });
    }

    private void EnsureSchemas()
    {
        _.TryEnsureSchema<Brevitee.Logging.Data.LogEvent>();
        ForEachDaoType((t) =>
        {
            int l = t.Length;
            for (int i = 0; i < l; i++)
            {
                _.TryEnsureSchema(t[i]);
            }
        });
    }

    private bool SetDatabaseInitializer()
    {
        bool set = false;
        Type initializer = Type.GetType(Conf.DatabaseInitializer);
        if (initializer == null)
        {
            Out("Unable to get DatabaseInitializer type from Conf.yaml", ConsoleColor.Red);
        }
        else
        {
            DatabaseInitializers.AddInitializer((IDatabaseInitializer)initializer.Construct());
            set = true;
        }

        return set;
    }
    
}


