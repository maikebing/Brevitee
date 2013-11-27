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

using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Logging;
using Brevitee.Testing;
using Brevitee.Data;
using Brevitee.Data.Schema;
using Brevitee.Encryption;
using Brevitee.Yaml;
using Brevitee.Javascript;
using Brevitee.Incubation;
using Brevitee.Configuration;

using EcmaScript.NET;
using EcmaScript.NET.Types.Cli;
using EcmaScript.NET.Types;
using Bam.core;
//namespace bam
//{   
[Serializable]
partial class bam : CommandLineTestInterface
{
    static Dictionary<string, ConsoleInvokeableMethod> _actions;

    static void Main(string[] args)
    {
        if (args.Length == 1 && args[0].Equals("-?") || args[0].Equals("/?"))
        {
            Usage();
        }
        else
        {
            PreInit();
            Initialize(args);
        }
    }

    public static void PreInit()
    {
        #region expand for PreInit help
        // To accept custom command line arguments you may use            
        /*
         * AddValidArgument(string argumentName, bool allowNull)
        */

        // All arguments are assumed to be name value pairs in the format
        // /name:value unless allowNull is true then only the name is necessary.

        // to access arguments and values you may use the protected member
        // arguments. Example:

        /*
         * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
         * arguments[argName]; // returns the specified value associated with the named argument
         */

        // the arguments protected member is not available in PreInit() (this method)
        #endregion

        AddValidArgument("root", "the root of the install, default is the current directory");

        string actionDescription = "specify one of: init, list, run, gen, pack, start or deploy";
        AddValidArgument("a", actionDescription);
        AddValidArgument("action", actionDescription);

        string targetDesc = "specify the path to the target that will be acted on";
        AddValidArgument("t", targetDesc);
        AddValidArgument("target", targetDesc);

        string nsDesc = "specify the namespace to compile models into";
        AddValidArgument("ns", nsDesc);
        AddValidArgument("namespace", nsDesc);

        string overwriteDesc = "overwrite existing views";
        AddValidArgument("o", true, overwriteDesc);
        AddValidArgument("overwrite", true, nsDesc);

        string conNameDesc = "the connection name";
        AddValidArgument("c", conNameDesc);
        AddValidArgument("connection", conNameDesc);

        AddValidArgument("design", true, "enable the design interface, this should only be used for development purposes");

        AddSwitches(typeof(bam));

        ((ConsoleLogger)(logger)).AddDetails = false;
        DefaultMethod = typeof(bam).GetMethod("RouteArguments");        
    }

    public static void RouteArguments(ParsedArguments arguments)
    {
        ReadActions();
        if(Arguments.Keys.Length > 0)
        {
            foreach (string action in Arguments.Keys)
            {
                Invoke(action);
            }
        }
        else
        {
            //string p = GetArgVal("install", GetArgVal("port", DefaultConfiguration.GetAppSetting("port", "8080")));
            //ServiceExe.RunService<bamServer>(new bamServer(50, Log.Default, int.Parse(p)));
            RunAllTests(typeof(bam).Assembly);
        }
    }

    private static bool Invoke(string action)
    {
        bool value = false;
        try
        {
            if (_actions.ContainsKey(action))
            {
                _actions[action].Invoke();
                value = true;
            }
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            OutFormat("An error occurred: {0}", ConsoleColor.Red, ex.Message);
            if (Arguments.Contains("stacktrace") && !string.IsNullOrEmpty(ex.StackTrace))
            {
                Out();
                Out(ex.StackTrace, ConsoleColor.Red);
            }
        }

        return value;
    }

    public static void Usage()
    {
        AddSwitches(typeof(bam));

        Assembly assembly = Assembly.GetEntryAssembly();
        FileInfo info = new FileInfo(assembly.Location);
        OutFormat("{0} [arguments]", info.Name);
            
        foreach (ArgumentInfo argInfo in ValidArgumentInfo)
        {
            string valEx = string.IsNullOrEmpty(argInfo.ValueExample) ? "" : string.Format(":{0}\r\n", argInfo.ValueExample);
            OutFormat("/{0}{1}\t\t{2}", argInfo.Name, valEx, argInfo.Description);            
        }
    }

    protected static void ReadActions()
    {
        _actions = new Dictionary<string, ConsoleInvokeableMethod>();
        bam provider = new bam();
        provider.InitConf();

        MethodInfo[] methods = typeof(bam).GetMethods();
        ReadActions(provider, methods);
    }

    private static void ReadActions(object provider, MethodInfo[] methods)
    {
        foreach (MethodInfo method in methods)
        {
            ConsoleAction action = null;
            if (method.HasCustomAttributeOfType<ConsoleAction>(out action))
            {
                if (!string.IsNullOrEmpty(action.CommandLineSwitch))
                {
                    _actions.Add(action.CommandLineSwitch, new ConsoleInvokeableMethod(method, action, provider, action.ValueExample));
                }
            }
        }
    }
    
    private void InitConf(bool load = true)
    {
        string root = DefaultConfiguration.GetAppSetting("Root", ".");
        string yamlConfig = string.Format("./{0}.yaml", typeof(Conf).Name);
        Conf c = new Conf();

        if (File.Exists(yamlConfig) && load)
        {
            c = (Conf)(yamlConfig.FromYamlFile().FirstOrDefault());
        }
     
        Root = Arguments.Contains("root") ? Arguments["root"] : root;
        
        if (!Root.EndsWith("/"))
        {
            Root = string.Format("{0}/", Root);
        }

        Conf = c;
    }

    private Conf Conf
    {
        get;
        set;
    }

    string _root;
    Fs _fs;
    /// <summary>
    /// Represents access to the file system
    /// </summary>
    private Fs Fs
    {
        get
        {
            return _fs;
        }
    }

    Scripts _js;
    object _jsLock = new object();
    private Scripts Js
    {
        get
        {
            if (_js == null)
            {
                lock (_jsLock)
                {
                    if (_js == null)
                    {
                        _js = new Scripts(Fs);
                    }
                }
            }

            return _js;
        }
    }

    protected string Root
    {
        get
        {
            return _root;
        }
        set
        {
            _fs = new Fs(value);
            _fs.DirectoryCreated += (p) =>
            {
                OutFormat("Created directory {0}", ConsoleColor.Green, p);
            };
            _fs.DirectoryExists += (p) =>
            {
                OutFormat("Directory already exists {0}", ConsoleColor.Yellow, p);
            };
            _fs.FileWritten += (p) =>
            {
                OutFormat("File written {0}", ConsoleColor.Green, p);
            };

            _root = value;
        }
    }

    public string ApplicationName
    {
        get
        {
            return Conf.ApplicationName;
        }
    }


}

//}
