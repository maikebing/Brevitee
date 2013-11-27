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
using System.Threading;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Net;
using System.Web;

using Brevitee.CommandLine;
using Brevitee.Logging;
using Brevitee;
using Brevitee.Testing;
using Brevitee.Data;
using Brevitee.Data.MsSql;
using Brevitee.Data.Schema;
using Brevitee.Drawing;
using Brevitee.Encryption;
using Brevitee.Yaml;
using Brevitee.Javascript;
using Brevitee.Configuration;
using Brevitee.Incubation;

using Ionic.Zip;
using BoneSoft.CSS;

using CsQuery.Web;
using CsQuery.Engine;
using CsQuery;

using EcmaScript.NET;
using EcmaScript.NET.Types.Cli;
using EcmaScript.NET.Types;
using Bam.core;

public partial class bam
{
    [ConsoleAction("hn", "hostname", "Set the hostname to listen for requests on")]
    public void SetHostName()
    {
        string hostName = GetArgVal("hn", "");
        if (string.IsNullOrEmpty(hostName))
        {
            hostName = Prompt("Please enter the host name", ConsoleColor.Cyan);
        }
        OutFormat("Setting host name to {0}", ConsoleColor.Green, hostName);
        Conf.HostName = hostName;
        Conf.Save();
    }

    [ConsoleAction("p", "portNumber", "Set the port number to listen on when the server is started")]
    public void SetPort()
    {
        string port = GetArgVal("p", "");
        if (string.IsNullOrEmpty(port))
        {
            port = Prompt("Please enter the port number", ConsoleColor.Cyan);
        }
        OutFormat("Setting port to {0}", ConsoleColor.Green, port);
        Conf.Port = port;
        Conf.Save();
    }

    [ConsoleAction("c", "connectionName", "Set the default connection name to use from the app.config file")]
    public void SetConnectionName()
    {
        string conx = GetArgVal("c", GetArgVal("c", GetArgVal("connection", "")));
        if (string.IsNullOrEmpty(conx))
        {
            conx = Prompt("Please enter the connection name", ConsoleColor.Cyan);
        }

        OutFormat("Setting connection name to {0}", ConsoleColor.Green, conx);
        Conf.ConnectionName = conx;
        Conf.Save();
    }

    [ConsoleAction("an", "appName", "Set the application name")]
    public void SetAppName()
    {
        string appName = GetArgVal("an", "");
        if (string.IsNullOrEmpty(appName))
        {
            appName = Prompt("Please enter the application name", ConsoleColor.Cyan);
        }
        SetAppName(appName);
    }

    [ConsoleAction("init", "applicationName" , "Create default fs structures")]
    public void Initialize()
    {
        string appName = GetArgVal("init", GetArgVal("an", ""));
        if(!string.IsNullOrEmpty(appName))
        {
            SetAppName(appName);
        }
        
        CreateDirectories();

        WriteSampleDataModel();

        WriteSamplePageModels();

        WriteResoucesToDisk();
    }

    [ConsoleAction("list", "dir", "List the contents of a folder")]
    public void ListFiles()
    {
        string dir = GetArgVal("list", GetArgVal("t", GetArgVal("target", "")));

        if (string.IsNullOrEmpty(dir))
        {
            Out("Target not specified, use /t:<path> or /target:<path>", ConsoleColor.Red);
            Exit(1);
        }

        dir = GetAbsolutePath(dir);
        DirectoryInfo dirInfo = new DirectoryInfo(dir);
        foreach (DirectoryInfo info in dirInfo.GetDirectories())
        {
            Out(info.FullName, ConsoleColor.Blue);
        }

        foreach (FileInfo file in dirInfo.GetFiles())
        {
            Out(file.FullName, ConsoleColor.Cyan);
        }
    }

    //[ConsoleAction("run", "scriptPath", "Run javascript")]
    //public void RunScript()
    //{
    //    string path = GetArgVal("run", GetArgVal("t", GetArgVal("target", "")));
    //    if (string.IsNullOrEmpty(path))
    //    {
    //        Out("Target not specified", ConsoleTextColor.Red);
    //    }
    //    else
    //    {
    //        path = GetAbsolutePath(path);
    //        JsContext js = path.RunJavascriptFile(new CliProvider("ve", this.Ve), new CliProvider("fs", this.Fs));
    //    }
    //}

    [ConsoleAction("combine", "comma separated list of scripts in the scripts folder", "Combine the specified list of scripts into one file")]
    public void CombineJs()
    {
        string file;
        string[] scriptNames;
        GetFileInfo("combine", out file, out scriptNames);

        Scripts sc = new Scripts(Fs);
        Fs.WriteFile(string.Format("~/content/scripts/combined/{0}", file), sc.Combine(scriptNames));
    }

    [ConsoleAction("minify", "comma separted js files", "minify the specified comma separted list of scripts")]
    public void Minify()
    {
        string file;
        string[] scriptNames;
        GetFileInfo("minify", out file, out scriptNames);

        Scripts sc = new Scripts(Fs);
        Fs.WriteFile(string.Format("~/content/scripts/min/{0}", file), sc.Compress(scriptNames));
    }

    //[ConsoleAction("pagejs", "combine scripts defined on each page into a single file")]
    //public void PageJs()
    //{
    //    FileInfo[] files = Fs.GetFiles("~/content", "*.html");
    //    foreach (FileInfo file in files)
    //    {
    //        OutFormat("Reading file {0}", ConsoleTextColor.Cyan, file.FullName);
    //        string fileName = string.Format("{0}.js", Path.GetFileNameWithoutExtension(file.Name));

    //        string html = File.ReadAllText(file.FullName);
    //        CQ cq = CQ.Create(html);
           
    //        List<string> srcs = new List<string>();
    //        cq["head script"].Each((i, o) =>
    //        {
    //            srcs.Add(cq[o].Attr("src"));
    //        })
    //        .Remove();

    //        CQ modified = CQ.Create(cq.Render());
    //        modified["head"].Append(cq["<script>"].Attr("src", string.Format("/scripts/{0}", fileName)));
    //        modified["head"].Append(cq["<script>"].Attr("src", string.Format("/scripts/proxies", fileName)));
    //        if (srcs.Count > 0)
    //        {
    //            file.CopyTo(string.Format("{0}_{1}", file.FullName, DateTime.Now.ToJulianDate().ToString()));                
    //            Fs.WriteFile(string.Format("~/content/{0}", file.Name), modified.Render());

    //            Js.CompressToContentFile(fileName, Arguments.Contains("overwrite"), srcs.ToArray());
    //        }
    //    }
    //}

    private static string AutoNameScriptFile(string[] splitted)
    {
        StringBuilder s = new StringBuilder();
        for (int i = 0; i < splitted.Length; i++)
        {
            s.Append(splitted[i]);
            if (i != splitted.Length - 1)
            {
                s.Append("_");
            }
        }
        s.Append(".js");
        return s.ToString();
    }

    [ConsoleAction("gen", "m(odels) | v(iews) | c(ontrollers) | p(ages) | a(ll)", "Generate files")]
    public void Generate()
    {
        string target = GetArgVal("gen", GetArgVal("t", GetArgVal("target", "")));
        if (string.IsNullOrEmpty(target))
        {
            Out("Target not specified use one of => m(odels) | v(iews) | c(ontrollers) | p(ages) | i(mages) | a(ll)");
        }
        else
        {
            if (_generators.ContainsKey(target))
            {
                _generators[target]();
            }
            else
            {
                OutFormat("Unrecognized generator specified: {0}", ConsoleColor.Red, target);
            }
        }
    }

    [ConsoleAction("wdb", "connectionName", "Write the database schema(s)")]
    public void WriteDbSchema()
    {
        try
        {
            string conx = GetArgVal("c", GetArgVal("connection", ""));
            if (string.IsNullOrEmpty(conx))
            {
                conx = Conf.ConnectionName;
                if (string.IsNullOrEmpty(conx))
                {
                    conx = GetArgVal("wdb", "Default");
                }

                if (conx.Equals("true")) // allowed to be null by the Argument parser, bool gets converted to a string
                {
                    conx = "Default";
                }
            }
            
            OutFormat("Writing schema for connection name {0}", ConsoleColor.Green, conx);
            WriteDatabaseSchema(conx);
            Out("Done", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            Error("An error occurred: {0}", ex, ex.Message);
        }
    }

    [ConsoleAction("cc", "Compile controllers")]
    public void CompileControllers()
    {
        string ns = ControllersNamespace;//string.Format("{0}.Controllers", ApplicationName);
        string dllName = string.Format("{0}.dll", ns);
        DaoGenerator gen = new DaoGenerator(ns);
        OutFormat("Compiling controllers: {0} => {1}", ConsoleColor.Yellow, ns, dllName);
        List<string> referenceAssemblies = DaoGenerator.DefaultReferenceAssemblies;
        DirectoryInfo bin = new DirectoryInfo(Fs.GetAbsolutePath("~/bin/"));
        foreach (FileInfo file in bin.GetFiles("*.dll"))
        {
            referenceAssemblies.Add(file.FullName);
        }
        CompilerResults results = gen.Compile(new DirectoryInfo(Fs.GetAbsolutePath("~/controllers")), dllName, referenceAssemblies.ToArray());
        if (results.Errors.Count > 0)
        {
            OutputCompilerErrors(results);
        }
        else
        {
            string assembly = results.CompiledAssembly.CodeBase.Replace("file:///", "");
            string existing = GetAbsolutePath(string.Format("~/bin/{0}", dllName));
            if (File.Exists(existing))
            {
                File.Delete(existing);
            }

            File.Copy(assembly, existing);
        }
    }

    [ConsoleAction("less", "Run less on .less files and output to css")]
    public void Less()
    {
        Css css = new Css(Fs);
        css.Less((s) =>
        {
            OutFormat("{0} file not found", ConsoleColor.Red, s);
        });
    }

    //[ConsoleAction("pack", "Package the current bam app")]
    //public void Package()
    //{
    //    using (ZipFile zip = new ZipFile())
    //    {
    //        string[] files = File.ReadAllLines("./bam.txt");
    //        zip.AddFiles(files, "bin");
    //        zip.AddDirectory(Fs.GetAbsolutePath("~/app_data/"), "app_data");
    //        zip.AddDirectory(Fs.GetAbsolutePath("~/bin/"), "bin");
    //        zip.AddDirectory(Fs.GetAbsolutePath("~/content/"), "content");
    //        zip.AddDirectory(Fs.GetAbsolutePath("~/controllers/"), "controllers");
    //        zip.AddDirectory(Fs.GetAbsolutePath("~/models/"), "models");
            
    //        zip.Save(string.Format("{0}.zip", Conf.ApplicationName));
    //    }
    //}

    static BamServer _currentServer;
    [ConsoleAction("start", "Start the bam server")]
    public void StartServer()
    {
        ConnectionStringResolvers.AddResolver(ConnectionStringResolver);
        DatabaseInitializers.Clear();
        bool set = SetDatabaseInitializer();
        if (!set)
        {
            return;
        }

        EnsureSchemas();
        MultiTargetLogger logger = CreateLogger();
        InitConf();

        string port = GetArgVal("start", Conf.Port);
        Conf.Port = port;
        bool designMode = Arguments.Contains("design");

        InitRequestHandler(logger, designMode);

        _currentServer = StartServer(logger);

        Thread.Sleep(1000);
        
        Out("-----");
        CommandLoop();
        _currentServer.Stop();
    }

    private void InitRequestHandler(MultiTargetLogger logger, bool designMode)
    {
        RequestHandler handler = new RequestHandler(Incubator.Default, new Fs(Root), logger);
        RegisterControllers(handler);
        SetHandler(logger, handler);
        if (designMode)
        {
            handler.AddExecutor(new Design(handler.Fs, handler.Logger));
        }
    }

    private BamServer RestartServer(BamServer server)
    {
        ILogger logger = server.Logger;
        return StartServer(logger);
    }

    private BamServer StartServer(ILogger logger)
    {
        BamServer server = new BamServer();
        DefaultConfiguration.CopyProperties(Conf, server);
        server.Logger = logger;
        server.Start();
        return server;
    }

    private void CommandLoop()
    {
        List<string> exitCommands = new List<string>(new string[] { "exit", "bye", "end", "quit", "q" });

        string command = string.Empty;
        while (!exitCommands.Contains(command))
        {
            command = Console.ReadLine();
            if (!Invoke(command))
            {

            }
        }
    }

    [ConsoleAction("ep", "url", "extract colors from the specified path")]
    public void ExtractPalette()
    {
        string defaultPath = GetAbsolutePath("~/content/css/style.css");
        string path = GetArgVal("ep", "");
        ColorPalette palette = new ColorPalette();
        Out("extracting palette", ConsoleColor.Green);
        if (!string.IsNullOrEmpty(path))
        {
            palette = ColorPalette.DeriveFrom(new Uri(path));
        }
        else
        {
            palette = ColorPalette.DeriveFrom(defaultPath);
        }

        palette.Save(Fs.GetAbsolutePath("~/Palette.yaml"), GetArgVal("overwrite", "") == string.Empty);
        Out("file written", ConsoleColor.Green);
        //ColorPalette reloaded = ColorPalette.Load(Fs.GetAbsolutePath("~/Palette.yaml"));
        //foreach (HexColor color in reloaded.Colors)
        //{
        //    OutFormat("{0}: {1}", color.Name, color.Hex);
        //}
    }

    [ConsoleAction("show", "Show the current configuration settings")]
    public void Show()
    {
        OutFormat("Application Name: {0}", ConsoleColor.Yellow, Conf.ApplicationName);
        OutFormat("Default Connection: {0}", ConsoleColor.Yellow, Conf.ConnectionName);
        OutFormat("Host Name: {0}", ConsoleColor.Yellow, Conf.HostName);
        OutFormat("Port: {0}", ConsoleColor.Yellow, Conf.Port);
    }

    //[ConsoleAction("deploy", "hostname", "Deploy to the specified host")]
    //public void Deploy()
    //{
    //    throw new NotImplementedException("Deploy");
    //}

    //[ConsoleAction("test", "run all unit tests")]
    //public void RunTests()
    //{
    //    throw new NotImplementedException("RunTests");
    //}

}

