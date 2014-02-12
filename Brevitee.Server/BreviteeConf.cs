using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;
using Brevitee.Logging;
using Brevitee.Web;
using Brevitee.Yaml;
using Brevitee.Configuration;
using Brevitee.Data;
using Brevitee.CommandLine;
using System.IO;
using System.Reflection;

namespace Brevitee.Server
{
    /// <summary>
    /// Configuratoin for the BreviteeServer
    /// </summary>
    public class BreviteeConf
    {
        public BreviteeConf()
        {
            this.Fs = new Fs(".");
            this.MaxThreads = "25";
            this.GenerateDao = true;
            this.InitializeTemplates = true;
            this.DaoSearchPattern = "*.dll";
            this.LoggerPaths = new string[] { "." };
            this.LoggerSearchPattern = "*Logging.dll";
            this.LoggerName = "ConsoleLogger";
            this.InitializeFileSystemFromEnum = InitializeFrom.Resource;
            this.ZipPath = "~/bkg/content.root";
        }

        public string LoadedFrom { get; set; }

        internal BreviteeServer Server
        {
            get;
            set;
        }

        /// <summary>
        /// Server content fs root
        /// </summary>
        internal Fs Fs
        {
            get;
            set;
        }

        internal Fs AppFs(string appName)
        {
            Fs result = null;
            AppConf conf = AppConfigs.Where(ac => ac.Name.Equals(appName)).FirstOrDefault();
            if (conf != null)
            {
                result = conf.AppRoot;
            }

            return result;
        }

        public bool GenerateDao
        {
            get;
            set;
        }

        InitializeFrom _initializeFrom;
        protected internal InitializeFrom InitializeFileSystemFromEnum
        {
            get
            {
                return _initializeFrom;
            }
            set
            {
                _initializeFrom = value;
            }
        }

        public string InitializeFileSystemFrom
        {
            get
            {
                return InitializeFileSystemFromEnum.ToString();
            }
            set
            {
                Enum.TryParse<InitializeFrom>(value, out _initializeFrom);
            }
        }

        public string ZipPath
        {
            get;
            set;
        }

        public bool InitializeTemplates
        {
            get;
            set;
        }

        public bool UseCache
        {
            get;
            set;
        }

        /// <summary>
        /// The file search pattern used to filter 
        /// assemblies for Dao registration
        /// </summary>
        public string DaoSearchPattern
        {
            get;
            set;
        }

        public string ContentRoot
        {
            get
            {
                return Fs.Root;
            }
            set
            {
                Fs.Root = value;
            }
        }

        public void AddProxyAlias(string alias, Type typeToAlias)
        {
            _proxyAliases.Add(new ProxyAlias(alias, typeToAlias));
        }

        List<ProxyAlias> _proxyAliases = new List<ProxyAlias>();
        public ProxyAlias[] ProxyAliases
        {
            get
            {
                return _proxyAliases.ToArray();
            }
            set
            {
                _proxyAliases = new List<ProxyAlias>();
                if (value != null)
                {
                    _proxyAliases.AddRange(value);
                }
            }
        }

        /// <summary>
        /// A list of additional absolute or relative paths
        /// to search for proxyable classes (classes that
        /// have the ProxyAttribute)
        /// </summary>
        public string[] ServiceProxyPaths
        {
            get;
            set;
        }

        /// <summary>
        /// Directory paths to search for ILogger implementations
        /// </summary>
        public string[] LoggerPaths
        {
            get;
            set;
        }

        /// <summary>
        /// The file search pattern used to 
        /// load assemblies that contain ILogger implementations
        /// </summary>
        public string LoggerSearchPattern
        {
            get;
            set;
        }

        public string LoggerName
        {
            get;
            set;
        }

        Type[] _availableLoggers;
        object _availableLoggersLock = new object();
        protected internal Type[] AvailableLoggers
        {
            get
            {
                return _availableLoggersLock.DoubleCheckLock(ref _availableLoggers, () =>
                {
                    return LoadLoggers();
                });
            }
        }

        internal protected ILogger GetLogger(out Type loggerType)
        {
            loggerType  = null;
            if (!string.IsNullOrEmpty(LoggerName))
            {
                loggerType = AvailableLoggers.Where(type => type.Name.Equals(LoggerName)).FirstOrDefault();
            }

            ILogger logger = null;
            if (loggerType == null)
            {
                loggerType = typeof(ConsoleLogger);
                ConsoleLogger tmp = new ConsoleLogger();
                tmp.AddDetails = false;
                tmp.UseColors = true;
                logger = tmp;
            }
            else
            {
                logger = (ILogger)loggerType.Construct();
            }

            return logger;
        }

        private Type[] LoadLoggers()
        {
            List<Type> results = new List<Type>();
            Assembly baseAssembly = typeof(ILogger).Assembly;
            results.AddRange(baseAssembly.GetTypes().Where(type => type.ImplementsInterface<ILogger>()).ToArray());

            LoggerPaths.Each(path =>
            {
                DirectoryInfo curDir = new DirectoryInfo(path);
                FileInfo[] files = curDir.GetFiles(LoggerSearchPattern);
                files.Each(file =>
                {
                    try
                    {
                        Assembly currentAssembly = Assembly.LoadFrom(file.FullName);
                        Type[] types = currentAssembly.GetTypes().Where(type => type.ImplementsInterface<ILogger>()).ToArray();
                        results.AddRange(types);
                    }
                    catch //(Exception ex)
                    {
                        // failed
                        // this is acceptable, we're just looking for loggers
                    }
                });
            });

            return results.ToArray();
        }

        string _port;
        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        string _maxThreads;
        public string MaxThreads
        {
            get
            {
                return _maxThreads;
            }
            set
            {
                _maxThreads = value;
            }
        }
        
        List<AppConf> _appConfigs;
        object _appConfigsLock = new object();
        /// <summary>
        /// Represents the configs for each application found in ~s:/apps 
        /// (where each subdirectory is assumed to be a Brevitee application)
        /// </summary>
        protected internal AppConf[] AppConfigs
        {
            get
            {
                return _appConfigsLock.DoubleCheckLock(ref _appConfigs, () => InitializeAppConfigs()).ToArray();
            }
        }
        
        protected internal AppConf[] ReloadAppConfigs()
        {
            lock (_appConfigsLock)
            {
                _appConfigs = null;
                return AppConfigs;
            }
        }
        Dictionary<string, AppConf> _appConfigsByAppName;
        object _appConfigsByAppNameLock = new object();
        protected internal AppConf this[string appName]
        {
            get
            {
                Dictionary<string, AppConf> dictionary = _appConfigsByAppNameLock.DoubleCheckLock(ref _appConfigsByAppName, () =>
                {
                    Dictionary<string, AppConf> result = new Dictionary<string, AppConf>();
                    AppConfigs.Each(conf =>
                    {
                        result.Add(conf.Name, conf);
                    });

                    return result;
                });

                return dictionary[appName];
            }
        }

        object _initAppConfigsLock = new object();
        public List<AppConf> InitializeAppConfigs()
        {
            lock(_initAppConfigsLock)
            {
                List<AppConf> configs = new List<AppConf>();
                DirectoryInfo contentRoot = new DirectoryInfo(Path.Combine(ContentRoot, "apps"));
                if (!contentRoot.Exists)
                {
                    contentRoot.Create();
                }
                DirectoryInfo[] appDirs = contentRoot.GetDirectories();
                appDirs.Each(appDir =>
                {
                    bool configFound = false;
                    FileInfo jsonConfig = new FileInfo(Path.Combine(appDir.FullName, "appConf.json"));
                    FileInfo appsConf = jsonConfig;
                    if (appsConf.Exists)
                    {
                        configFound = true;
                        AppConf conf = SetAppNameInJson(appDir, appsConf);
                        conf.BreviteeConf = this;
                        configs.Add(conf);
                    }
                    else
                    {
                        appsConf = new FileInfo(Path.Combine(appDir.FullName, "appConf.yaml"));
                        if (appsConf.Exists)
                        {
                            configFound = true;
                            AppConf conf = SetAppNameInYaml(appDir, appsConf);
                            conf.BreviteeConf = this;
                            configs.Add(conf);
                        }
                    }

                    if (!configFound)
                    {
                        AppConf conf = new AppConf(this, appDir.Name);
                        conf.GenerateDao = true;                        
                        conf.ToJsonFile(jsonConfig);
                        configs.Add(conf);
                    }
                });

                return configs;
            }
        }

        private static AppConf SetAppNameInYaml(DirectoryInfo appDir, FileInfo appsConf)
        {
            AppConf conf = appsConf.FromYaml<AppConf>();
            conf.Name = appDir.Name;
            conf.ToYamlFile(appsConf);
            return conf;
        }

        private static AppConf SetAppNameInJson(DirectoryInfo appDir, FileInfo appsConf)
        {
            AppConf conf = appsConf.FromJson<AppConf>();
            conf.Name = appDir.Name;
            conf.ToJsonFile(appsConf);
            return conf;
        }

        /// <summary>
        /// Load the BreviteeConf from one of BreviteeConf.json, BreviteeConf.yaml or
        /// the Default configuration file whichever is found first.  Default 
        /// will always be provided and will never return null.
        /// </summary>
        /// <returns></returns>
        public static BreviteeConf Load()
        {
            BreviteeConf c = null;
            string jsonConfig = string.Format("./{0}.json", typeof(BreviteeConf).Name);

            if (File.Exists(jsonConfig))
            {
                c = jsonConfig.FromJsonFile<BreviteeConf>();
                c.LoadedFrom = new FileInfo(jsonConfig).FullName;
            }

            if (c == null)
            {
                string yamlConfig = string.Format("./{0}.yaml", typeof(BreviteeConf).Name);
                if (File.Exists(yamlConfig))
                {
                    c = (BreviteeConf)(yamlConfig.FromYamlFile().FirstOrDefault());
                    c.LoadedFrom = new FileInfo(yamlConfig).FullName;
                }
            }

            if (c == null)
            {
                c = new BreviteeConf();
                DefaultConfiguration.SetProperties(c);
            }

            return c;
        }

        public void Save(bool overwrite = false, ConfFormat format = ConfFormat.Json)
        {
            switch (format)
            {
                case ConfFormat.Yaml:
                    string fileName = "{0}.yaml"._Format(typeof(BreviteeConf).Name);
                    if (overwrite || !File.Exists(fileName))
                    {
                        this.ToYaml().SafeWriteToFile(fileName, overwrite);
                    }
                    break;
                case ConfFormat.Json:
                    fileName = "{0}.json"._Format(typeof(BreviteeConf).Name);
                    if (overwrite || !File.Exists(fileName))
                    {
                        this.ToJson(true).SafeWriteToFile(fileName, overwrite);
                    }
                    break;
            }
        }
    }
}
