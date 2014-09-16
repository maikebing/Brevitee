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
using Brevitee.UserAccounts.Data;
using Brevitee.UserAccounts;
using System.IO;
using System.Reflection;

namespace Brevitee.Server
{
    /// <summary>
    /// Configuratoin for the BreviteeServer
    /// </summary>
    public class BreviteeConf
    {
        public const string ContentRootConfigKey = "ContentRoot";

        public BreviteeConf()
        {
            this.Fs = new Fs(Fs.GetAppDataFolder());
            this.MaxThreads = 50;
            this.GenerateDao = true;
            this.InitializeTemplates = true;
            this.DaoSearchPattern = "*Dao.dll";
            this.LoggerPaths = new string[] { "." };
            this.LoggerSearchPattern = "*Logging.dll";
            this.ServiceSearchPattern = "*Services.dll";
            this.LoggerName = "ConsoleLogger";
            this.InitializeFileSystemFromEnum = InitializeFrom.Resource;
            this.ZipPath = "~/bkg/content.root";
            
            List<SchemaInitializer> schemaInitInfos = new List<SchemaInitializer>();
            schemaInitInfos.Add(new SchemaInitializer(typeof(UserAccountsContext), typeof(SQLiteRegistrarCaller)));

            this._schemaInitializers = schemaInitInfos;
        }

        internal string LoadedFrom { get; set; }

        internal ConfFormat Format
        {
            get;
            set;
        }

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
            AppConf conf = AppConfigs.Where(ac => ac.Name.Equals(appName) || ac.Name.Equals(appName.ToLowerInvariant())).FirstOrDefault();
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

        public string ServiceSearchPattern
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

        string _zipPath;
        public string ZipPath
        {
            get
            {
                return _zipPath;
            }
            set
            {
                _zipPath = Fs.GetAbsolutePath(value);
            }
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
                loggerType = AvailableLoggers.FirstOrDefault(type => type.Name.Equals(LoggerName));
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
                
        int _maxThreads;
        public int MaxThreads
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

        List<SchemaInitializer> _schemaInitializers;
        public SchemaInitializer[] SchemaInitializers
        {
            get
            {
                return _schemaInitializers.ToArray();
            }
            set
            {
                _schemaInitializers = new List<SchemaInitializer>(value);
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
        /// <summary>
        /// Deserializes each appConf found in subdirectories of
        /// the ~s:/apps folder.  For example, if there is a subfolder named
        /// Monkey in ~s:/apps then this method will search for ~s:/apps/Monkey/appConf.json
        /// then ~s:/apps/Monkey/appConf.yaml if the json file isn't found.  If neither
        /// is found a new AppConf is created and and serialized to the json file
        /// specified above.
        /// </summary>
        /// <returns></returns>
        public List<AppConf> InitializeAppConfigs()
        {
            lock(_initAppConfigsLock)
            {
                List<AppConf> configs = new List<AppConf>();
                DirectoryInfo appRoot = new DirectoryInfo(Path.Combine(ContentRoot, "apps"));
                if (!appRoot.Exists)
                {
                    appRoot.Create();
                }
                DirectoryInfo[] appDirs = appRoot.GetDirectories();
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
                        conf.GenerateDao = this.GenerateDao;                        
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
            if (conf == null)
            {
                conf = new AppConf();
            }
            conf.Name = appDir.Name;
            conf.ToJsonFile(appsConf);
            return conf;
        }

        public static BreviteeConf Load()
        {
            return Load(DefaultConfiguration.GetAppSetting(ContentRootConfigKey, new object().GetAppDataFolder()));
        }

        /// <summary>
        /// Load the BreviteeConf from one of BreviteeConf.json, BreviteeConf.yaml or
        /// the Default configuration file whichever is found first.  Default 
        /// will always be provided and will never return null.
        /// </summary>
        /// <returns></returns>
        public static BreviteeConf Load(string contentRootDir)
        {
            BreviteeConf c = null;
           
            string jsonConfig = Path.Combine(contentRootDir, string.Format("{0}.json", typeof(BreviteeConf).Name));

            if (File.Exists(jsonConfig))
            {
                c = jsonConfig.FromJsonFile<BreviteeConf>();
                c.LoadedFrom = new FileInfo(jsonConfig).FullName;
                c.Format = ConfFormat.Json;
            }

            if (c == null)
            {
                string yamlConfig = Path.Combine(contentRootDir, string.Format("{0}.yaml", typeof(BreviteeConf).Name));
                if (File.Exists(yamlConfig))
                {
                    c = (BreviteeConf)(yamlConfig.FromYamlFile().FirstOrDefault());
                    c.LoadedFrom = new FileInfo(yamlConfig).FullName;
                    c.Format = ConfFormat.Yaml;
                }
            }

            if (c == null)
            {
                c = new BreviteeConf();
                DefaultConfiguration.SetProperties(c);
                c.LoadedFrom = new FileInfo(jsonConfig).FullName;
                c.Save();
            }
            
            c.ContentRoot = contentRootDir;
            return c;
        }

        public void Save()
        {
            FileInfo file = new FileInfo(LoadedFrom);
            Save(file.Directory.FullName, true, Format);
        }

        public void Save(bool overwrite = false, ConfFormat format = ConfFormat.Json)
        {
            Save(ContentRoot, overwrite, format);
        }

        public void Save(string rootDir, bool overwrite = false, ConfFormat format = ConfFormat.Json)
        {
            switch (format)
            {
                case ConfFormat.Yaml:
                    string filePath = Path.Combine(rootDir, "{0}.yaml"._Format(typeof(BreviteeConf).Name));
                    if (overwrite || !File.Exists(filePath))
                    {
                        this.ToYaml().SafeWriteToFile(filePath, overwrite);
                    }
                    break;
                case ConfFormat.Json:
                    filePath = Path.Combine(rootDir, "{0}.json"._Format(typeof(BreviteeConf).Name));
                    if (overwrite || !File.Exists(filePath))
                    {
                        this.ToJson(true).SafeWriteToFile(filePath, overwrite);
                    }
                    break;
            }
        }
    }
}
