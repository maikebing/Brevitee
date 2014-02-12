using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Brevitee;
using Brevitee.Web;
using Brevitee.Data;
using Brevitee.Data.Schema;
using Brevitee.Configuration;
using Brevitee.Logging;
using Brevitee.Incubation;
using Brevitee.Javascript;
using Brevitee.ServiceProxy;
using Brevitee.Server.Renderers;
using Yahoo.Yui.Compressor;

using System.Reflection;

namespace Brevitee.Server
{
    /// <summary>
    /// The responder responsible for generating dynamic 
    /// proxy javascripts that enable client side code to
    /// execute server side .Net Dao methods over Ajax.
    /// </summary>
    public class DaoResponder : ResponderBase, IInitialize<DaoResponder>
    {
        Dictionary<string, Func<string, bool, string>> _dynamicResponders;

        Incubator _serviceProvider;

        public DaoResponder(BreviteeConf conf)
            : base(conf)
        {
            Init();
        }

        public DaoResponder(BreviteeConf conf, ILogger logger, RequestHandler requestHandler)
            : base(conf, logger, requestHandler)
        {
            Init();
        }

        private void Init()
        {
            _dynamicResponders = new Dictionary<string, Func<string, bool, string>>();
            _dynamicResponders.Add("proxies", Proxies);
            _dynamicResponders.Add("ctors", Ctors);
            _dynamicResponders.Add("templates", Templates);
        }
        
        Dictionary<string, string> _compiledTemplates;
        object _compiledTemplatesLock = new object();
        public string Templates(string appName, bool min = false)
        {
            string result = string.Empty;
            
            if (_compiledTemplates == null)
            {
                _compiledTemplates = new Dictionary<string, string>();
            }

            lock (_compiledTemplatesLock)
            {

                if (_compiledTemplates.ContainsKey(appName))
                {
                    result = _compiledTemplates[appName];
                }
                else
                {
                    // templates are in ~s:/dao/dust and ~a:/dao/dust
                    string dustRelativePath = "~/dao/dust";
                    DirectoryInfo commonTemplateDir = new DirectoryInfo(Fs.GetAbsolutePath(dustRelativePath));
                    Fs appFs = AppFs(appName);
                    DirectoryInfo appTemplateDir = new DirectoryInfo(appFs.GetAbsolutePath(dustRelativePath));

                    StringBuilder tmp = new StringBuilder();
                    tmp.AppendLine(DustScript.CompileDirectory(commonTemplateDir));
                    tmp.AppendLine(DustScript.CompileDirectory(appTemplateDir));                    
                    _compiledTemplates[appName] = tmp.ToString();
                    result = _compiledTemplates[appName];
                }
            }

            return result;
        }

        public string Ctors(string appName, bool min = false)
        {
            StringBuilder result = new StringBuilder();
            CommonDaoProxyRegistrations.Keys.Each(cx =>
            {
                string ctorScript = min ? CommonDaoProxyRegistrations[cx].MinCtors.ToString(): CommonDaoProxyRegistrations[cx].Ctors.ToString();
                result.AppendLine(";\r\n");
                result.AppendLine(ctorScript);
            });

            if (AppDaoProxyRegistrations.ContainsKey(appName))
            {
                AppDaoProxyRegistrations[appName].Each((reg, i) =>
                {
                    string ctorScript = min ? AppDaoProxyRegistrations[appName][i].MinCtors.ToString() : AppDaoProxyRegistrations[appName][i].Ctors.ToString();
                    result.AppendLine(";\r\n");
                    result.AppendLine(ctorScript);
                });
            }

            return result.ToString();
        }

        public string Proxies(string appName, bool min = false)
        {
            StringBuilder result = new StringBuilder();
            CommonDaoProxyRegistrations.Keys.Each(cx =>
            {
                string ctorScript = min ? CommonDaoProxyRegistrations[cx].MinProxies.ToString() : CommonDaoProxyRegistrations[cx].Proxies.ToString();
                result.AppendLine(";\r\n");
                result.AppendLine(ctorScript);
            });

            if (AppDaoProxyRegistrations.ContainsKey(appName))
            {
                AppDaoProxyRegistrations[appName].Each((reg, i) =>
                {
                    string ctorScript = min ? AppDaoProxyRegistrations[appName][i].MinProxies.ToString() : AppDaoProxyRegistrations[appName][i].Proxies.ToString();
                    result.AppendLine(";\r\n");
                    result.AppendLine(ctorScript);
                });            
            }

            return result.ToString();            
        }


        #region IResponder Members

        public override bool TryRespond(IContext context)
        {
            if (!this.IsInitialized)
            {
                Initialize();
            }

            IRequest request = context.Request;
            IResponse response = context.Response;
            bool handled = false;
            string path = request.Url.AbsolutePath;
            byte[] content = new byte[] { };
            string appName = AppConf.AppNameFromUri(request.Url);
            string[] chunks = path.DelimitSplit("/");
            
            HttpArgs queryString = new HttpArgs(request.Url.Query);
            bool min = !string.IsNullOrEmpty(queryString["min"]);

            if (chunks[0].ToLowerInvariant().Equals("dao") && chunks.Length > 1)
            {
                string method = chunks[1];
                if (_dynamicResponders.ContainsKey(method))
                {
                    response.ContentType = GetContentType("temp.js"); // looks at the extension
                    string script = _dynamicResponders[method](appName, min);
                    SendResponse(response, script);
                    handled = true;
                }
            }

            return handled;
        }

        #endregion

        public bool IsInitialized
        {
            get;
            set;
        }

        List<ILogger> _subscribers = new List<ILogger>();
        object _subscriberLock = new object();
        public ILogger[] Subscribers
        {
            get
            {
                if (_subscribers == null)
                {
                    _subscribers = new List<ILogger>();
                }
                lock (_subscriberLock)
                {
                    return _subscribers.ToArray();
                }
            }
        }

        public bool IsSubscribed(ILogger logger)
        {
            lock (_subscriberLock)
            {
                return _subscribers.Contains(logger);
            }
        }
        public void Subscribe(ILogger logger)
        {
            if (!IsSubscribed(logger))
            {
                lock (_subscriberLock)
                {
                    _subscribers.Add(logger);
                }

                string className = typeof(DaoResponder).Name;
                this.Initializing += (d) =>
                {
                    logger.AddEntry("{0}::Initializ(ING)", className);
                };
                this.Initialized += (d) =>
                {
                    logger.AddEntry("{0}::Initializ(ED)", className);
                };
                this.GeneratingCommonDao += (dbJs, daoBin) =>
                {
                    StringBuilder format = new StringBuilder();
                    format.AppendLine("{0}::Generat(ING) dao classes");
                    format.AppendLine("\t{0}::DbJSFile::{1}");
                    format.AppendLine("\t{0}::DaoBin::{2}");
                    logger.AddEntry(format.ToString(), className, dbJs.FullName, daoBin.FullName);
                };
                this.GenerateCommonDaoSucceeded += (dbJsFi, daoBin, result) =>
                {
                    StringBuilder format = new StringBuilder();
                    format.AppendLine("*** Dao Generation SUCCEEDED ***");
                    format.AppendLine("\t{0}::DbJSFile::{1}");
                    format.AppendLine("\t{0}::DaoBin::{2}");
                    format.AppendLine("*** /end Dao Generation SUCCEEDED ***");
                    logger.AddEntry(format.ToString(), className, dbJsFi.FullName, daoBin.FullName);
                };
                this.GenerateCommonDaoFailed += (dbJsFi, result) =>
                {
                    StringBuilder format = new StringBuilder();
                    format.AppendLine("*** Dao Generation FAILED ***");
                    format.AppendLine("\t{0}::DbJSFile::{1}");
                    format.AppendLine("\t{0}::Result.Message::{2}");
                    format.AppendLine("\t{0}::Result.ExceptionMessage::{3}");
                    format.AppendLine("\t{0}::Result.StackTrace::{4}");
                    format.AppendLine("*** /end Dao Generation FAILED ***");
                    logger.AddEntry(format.ToString(), LogEventType.Error, className, dbJsFi.FullName, result.Message, result.ExceptionMessage, result.StackTrace);
                };
                this.GeneratingAppDao += (name, dbJs, daoBin) =>
                {
                    StringBuilder format = new StringBuilder();
                    format.AppendLine("{0}::Generat(ING) (APP[{1}]) dao classes;");
                    format.AppendLine("\t{0}::DbJSFile::{2}");
                    format.AppendLine("\t{0}::DaoBin::{3}");
                    logger.AddEntry(format.ToString(), className, name, dbJs.FullName, daoBin.FullName);
                };
                this.GenerateAppDaoSucceeded += (appName, dbJs, daoBin, result) =>
                {
                    StringBuilder format = new StringBuilder();
                    format.AppendLine("*** APP Dao Generation SUCCEEDED ***");
                    format.AppendLine("{0}::AppName::{1}");
                    format.AppendLine("\t{0}::DbJSFile::{2}");
                    format.AppendLine("\t{0}::DaoBin::{3}");
                    format.AppendLine("*** /end APP Dao Generation SUCCEEDED ***");
                    logger.AddEntry(format.ToString(), className, appName, dbJs.FullName, daoBin.FullName);
                };
                this.GenerateAppDaoFailed += (appName, dbJs, result) =>
                {
                    StringBuilder format = new StringBuilder();
                    format.AppendLine("*** APP Dao Generation FAILED ***");
                    format.AppendLine("{0}::AppName::{1}");
                    format.AppendLine("\t{0}::DbJSFile::{2}");
                    format.AppendLine("\t{0}::Exception::{3}");
                    format.AppendLine("*** /end APP Dao Generation FAILED ***");
                    logger.AddEntry(format.ToString(), LogEventType.Error, className, appName, dbJs.FullName, result.ExceptionMessage);
                };
                this.RegisteringCommonDaoBin += (daoBin) =>
                {
                    logger.AddEntry("{0}::Register(ING) dao bin {1}", className, daoBin.FullName);
                };
                this.RegisteredCommonDaoBin += (daoBin) =>
                {
                    logger.AddEntry("{0}::Register(ED) dao bin {1}", className, daoBin.FullName);
                };
            }
        }

        Dictionary<string, DaoProxyRegistration> _commonDaoProxyRegistrations;
        /// <summary>
        /// The DaoProxyRegistrations keyed by connectionName/contextName
        /// </summary>
        public Dictionary<string, DaoProxyRegistration> CommonDaoProxyRegistrations
        {
            get
            {
                if (_commonDaoProxyRegistrations == null)
                {
                    _commonDaoProxyRegistrations = new Dictionary<string, DaoProxyRegistration>();
                }
                return _commonDaoProxyRegistrations;
            }
        }

        Dictionary<string, List<DaoProxyRegistration>> _appDaoProxyRegistrations;
        /// <summary>
        /// The DaoProxyRegistrations keyed by application name
        /// </summary>
        public Dictionary<string, List<DaoProxyRegistration>> AppDaoProxyRegistrations
        {
            get
            {
                if (_appDaoProxyRegistrations == null)
                {
                    _appDaoProxyRegistrations = new Dictionary<string, List<DaoProxyRegistration>>();
                }

                return _appDaoProxyRegistrations;
            }
        }
        private void RegisterNewAppDaoDll(string appName, FileInfo dbJs, DirectoryInfo daoBin, Result result)
        {
            FileInfo daoDll = new FileInfo(Path.Combine(daoBin.FullName, "{0}.dll"._Format(result.Namespace)));
            DaoProxyRegistration reg = RegisterCommonDaoDll(daoDll);
            string name = appName.ToLowerInvariant();
            if (!AppDaoProxyRegistrations.ContainsKey(name))
            {
                AppDaoProxyRegistrations.Add(name, new List<DaoProxyRegistration>());
            }

            AppDaoProxyRegistrations[name].Add(reg);
        }
        private void RegisterNewCommonDaoDll(FileInfo dbJs, DirectoryInfo daoBin, Result result)
        {
            FileInfo daoDll = new FileInfo(Path.Combine(daoBin.FullName, "{0}.dll"._Format(result.Namespace)));
            RegisterCommonDaoDll(daoDll);
        }

        object _initializeLock = new object();
        public void Initialize()
        {
            OnInitializing();
            lock (_initializeLock)
            {
                string daoRoot = "~/dao";

                // if server.conf.generatedao
                if (BreviteeConf.GenerateDao)
                {
                    InitializeCommonDao(daoRoot);
                }
                 
                DirectoryInfo daoBinDir = BreviteeConf.Fs.GetDirectory(Path.Combine(daoRoot, "bin"));
                RegisterCommonDaoBin(daoBinDir);

                BreviteeConf.ReloadAppConfigs();

                // for each appconfig
                BreviteeConf.AppConfigs.Each(appConf =>
                {
                    DirectoryInfo appDaoBinDir = appConf.AppRoot.GetDirectory(Path.Combine(daoRoot, "bin"));                    
                    //  if appconf.generatedao
                    if (appConf.GenerateDao)
                    {
                        InitializeAppDao(daoRoot, appConf, appDaoBinDir);
                    }

                    // register each dao type using DaoRegistration
                    RegisterAppDaoBin(appConf.Name, appDaoBinDir);
                });
                
                IsInitialized = true;
            }            
            OnInitialized();
        }

       

        protected internal virtual void InitializeCommonDao(string daoRoot)
        {
            GenerateCommonDaoSucceeded += RegisterNewCommonDaoDll;
            //  generate common dao for each *.db.js in ~s:/dao/
            GenerateCommonDao(daoRoot);

            GenerateCommonDaoSucceeded -= RegisterNewCommonDaoDll; // only stays attached for the generation process if GenerateDao is true
        }

        protected internal virtual void InitializeAppDao(string daoRoot, AppConf appConf, DirectoryInfo appDaoBinDir)
        {
            GenerateAppDaoSucceeded += RegisterNewAppDaoDll;

            if (!appDaoBinDir.Exists)
            {
                appDaoBinDir.Create();
            }

            GenerateAppDaos(daoRoot, appConf, appDaoBinDir, "*.db.js");
            GenerateAppDaos(daoRoot, appConf, appDaoBinDir, "*.db.json");

            GenerateAppDaoSucceeded -= RegisterNewAppDaoDll;
        }

        protected internal void GenerateAppDaos(string daoRoot, AppConf appConf, DirectoryInfo appDaoBinDir, string fileSearchPattern)
        {
            DirectoryInfo daoTemp = appConf.AppRoot.GetDirectory(Path.Combine(daoRoot, "appdaotmp_".RandomLetters(4)));
            if (!daoTemp.Exists)
            {
                daoTemp.Create();
            }

            // get the saved hashes to determine if changes were made
            string hashPath = GetHashFilePath(appDaoBinDir);
            List<FileContentHash> hashes = GetHashes(hashPath);

            //      generate app dao from *.db.js ~a:/dao/
            FileInfo[] dbJsFiles = appConf.AppRoot.GetFiles(daoRoot, fileSearchPattern);
            //      compile into ~a:/dao/bin
            dbJsFiles.Each(dbJs =>
            {
                string path = dbJs.FullName.ToLowerInvariant();
                FileContentHash currentHash = new FileContentHash(path);

                if (!hashes.Contains(currentHash) && appConf.CheckDaoHashes)
                {
                    FileContentHash remove = hashes.Where(h => h.FilePath.ToLowerInvariant().Equals(path)).FirstOrDefault();
                    if (remove != null)
                    {
                        hashes.Remove(remove);
                    }
                    hashes.Add(currentHash);
                    hashes.ToArray().ToJsonFile(hashPath);
                    GenerateAppDao(appConf.Name, appDaoBinDir, daoTemp, dbJs);
                }
                else if (!appConf.CheckDaoHashes)
                {
                    GenerateAppDao(appConf.Name, appDaoBinDir, daoTemp, dbJs);
                }
            });
        }

        private static string GetHashFilePath(DirectoryInfo parentDir)
        {
            string hashPath = Path.Combine(parentDir.FullName, "{0}.json"._Format(typeof(FileContentHash).Name.Pluralize()));
            return hashPath;
        }
        
        private void GenerateCommonDao(string daoRoot)
        {
            DirectoryInfo daoBinDir = BreviteeConf.Fs.GetDirectory(Path.Combine(daoRoot, "bin"));
            if (!daoBinDir.Exists)
            {
                daoBinDir.Create();
            }
            GenerateCommonDao(daoRoot, daoBinDir, "*.db.js");
            GenerateCommonDao(daoRoot, daoBinDir, "*.db.json");
        }

        private void GenerateCommonDao(string daoRoot, DirectoryInfo daoBinDir, string fileSearchPattern)
        {
            DirectoryInfo daoTemp = BreviteeConf.Fs.GetDirectory(Path.Combine(daoRoot, "commondaotmp_".RandomLetters(4)));

            string hashPath = GetHashFilePath(daoBinDir);
            List<FileContentHash> hashes = GetHashes(hashPath);

            FileInfo[] dbJsFiles = BreviteeConf.Fs.GetFiles(daoRoot, fileSearchPattern);
            dbJsFiles.Each(dbJs =>
            {
                string path = dbJs.FullName.ToLowerInvariant();
                FileContentHash currentHash = new FileContentHash(path);

                if (!hashes.Contains(currentHash))
                {
                    FileContentHash remove = hashes.Where(h => h.FilePath.ToLowerInvariant().Equals(path)).FirstOrDefault();
                    if (remove != null)
                    {
                        hashes.Remove(remove);
                    }
                    hashes.Add(currentHash);
                    hashes.ToArray().ToJsonFile(hashPath);
                    //  compile into ~s:/dao/bin
                    GenerateCommonDao(daoBinDir, daoTemp, dbJs);
                }
            });
        }

        private DaoProxyRegistration RegisterCommonDaoDll(FileInfo daoDll)
        {
            DaoProxyRegistration reg = DaoProxyRegistration.Register(daoDll);
            CommonDaoProxyRegistrations[reg.ContextName] = reg;

            return reg;
        }

        public event Action<DirectoryInfo> RegisteringCommonDaoBin;
        protected void OnRegisteringCommonDaoBin(DirectoryInfo daoBin)
        {
            if (RegisteringCommonDaoBin != null)
            {
                RegisteringCommonDaoBin(daoBin);
            }
        }
        public event Action<DirectoryInfo> RegisteredCommonDaoBin;
        protected void OnRegisteredCommonDaoBin(DirectoryInfo daoBin)
        {
            if (RegisteredCommonDaoBin != null)
            {
                RegisteredCommonDaoBin(daoBin);
            }
        }
        public event Action<DirectoryInfo, Exception> RegisterCommonDaoBinFailed;
        protected void OnRegisterCommonDaoBinFailed(DirectoryInfo daoBin, Exception ex)
        {
            if (RegisterCommonDaoBinFailed != null)
            {
                RegisterCommonDaoBinFailed(daoBin, ex);
            }
        }
        private void RegisterCommonDaoBin(DirectoryInfo daoBinDir)
        {
            try
            {
                OnRegisteringCommonDaoBin(daoBinDir);
                DaoProxyRegistration[] daoRegistrations = DaoProxyRegistration.Register(daoBinDir, BreviteeConf.DaoSearchPattern);
                daoRegistrations.Each(daoReg =>
                {
                    CommonDaoProxyRegistrations[daoReg.ContextName] = daoReg;
                });
                OnRegisteredCommonDaoBin(daoBinDir);
            }
            catch (Exception ex)
            {
                OnRegisterCommonDaoBinFailed(daoBinDir, ex);
            }
        }

        public event Action<DirectoryInfo> RegisteringAppDaoBin;
        protected void OnRegisteringAppDaoBin(DirectoryInfo daoBin)
        {
            if (RegisteringAppDaoBin != null)
            {
                RegisteringAppDaoBin(daoBin);
            }
        }
        public event Action<DirectoryInfo> RegisteredAppDaoBin;
        protected void OnRegisteredAppDaoBin(DirectoryInfo daoBin)
        {
            if (RegisteredAppDaoBin != null)
            {
                RegisteredAppDaoBin(daoBin);
            }
        }
        public event Action<DirectoryInfo, Exception> RegisterAppDaoBinFailed;
        protected void OnRegisterAppDaoBinFailed(DirectoryInfo daoBin, Exception ex)
        {
            if (RegisterAppDaoBinFailed != null)
            {
                RegisterAppDaoBinFailed(daoBin, ex);
            }
        }
        private void RegisterAppDaoBin(string appName, DirectoryInfo daoBinDir)
        {
            try
            {
                OnRegisteringAppDaoBin(daoBinDir);
                DaoProxyRegistration[] daoRegistrations = DaoProxyRegistration.Register(daoBinDir, BreviteeConf.DaoSearchPattern);
                daoRegistrations.Each(daoReg =>
                {
                    AppDaoProxyRegistrations[appName].AddRange(daoRegistrations);
                });
                OnRegisteredAppDaoBin(daoBinDir);
            }
            catch (Exception ex)
            {
                OnRegisterAppDaoBinFailed(daoBinDir, ex);
            }
        }
        
        public event Action<string, FileInfo, Result> GenerateAppDaoFailed;
        protected void OnGenerateAppDaoFailed(string appName, FileInfo dbJsFile, Result result)
        {
            if (GenerateAppDaoFailed != null)
            {
                GenerateAppDaoFailed(appName, dbJsFile, result);
            }
        }

        public event Action<string, FileInfo, DirectoryInfo, Result> GenerateAppDaoSucceeded;
        protected void OnGerateAppDaoSucceeded(string appName, FileInfo dbJsFile, DirectoryInfo daoBin, Result result)
        {
            if (GenerateAppDaoSucceeded != null)
            {
                GenerateAppDaoSucceeded(appName, dbJsFile, daoBin, result);
            }
        }

        public event Action<string, FileInfo, DirectoryInfo> GeneratingAppDao;
        protected void OnGeneratingAppDao(string appName, FileInfo dbJsFile, DirectoryInfo daoBin)
        {
            if (GeneratingAppDao != null)
            {
                GeneratingAppDao(appName, dbJsFile, daoBin);
            }
        }

        private void GenerateAppDao(string appName, DirectoryInfo daoBinDir, DirectoryInfo daoTemp, FileInfo jsOrJsonDb)
        {
            OnGeneratingAppDao(appName, jsOrJsonDb, daoBinDir);

            Result schemaResult = GenerateDaoForFile(daoBinDir, daoTemp, jsOrJsonDb);

            if (!schemaResult.Success)
            {
                OnGenerateAppDaoFailed(appName, jsOrJsonDb, schemaResult);
            }
            else
            {
                OnGerateAppDaoSucceeded(appName, jsOrJsonDb, daoBinDir, schemaResult);
            }
        }

        private static Result GenerateDaoForFile(DirectoryInfo daoBinDir, DirectoryInfo daoTemp, FileInfo dbJs)
        {
            SchemaManager schemaManager = new SchemaManager();
            Result schemaResult = new Result("Generator Not Run, invalid file extension", false);
            if (dbJs.Extension.ToLowerInvariant().Equals(".js"))
            {
                schemaResult = schemaManager.Generate(dbJs, daoBinDir, daoTemp);
            }
            else if (dbJs.Extension.ToLowerInvariant().Equals(".json"))
            {
                string json = File.ReadAllText(dbJs.FullName);
                schemaResult = schemaManager.Generate(json, daoBinDir, daoTemp);
            }
            return schemaResult;
        }


        private static List<FileContentHash> GetHashes(string hashPath)
        {
            List<FileContentHash> hashes = new List<FileContentHash>();
            if (File.Exists(hashPath))
            {
                hashes = new List<FileContentHash>(hashPath.FromJsonFile<FileContentHash[]>());
            }
            return hashes;
        }
        
        public event Action<FileInfo, Result> GenerateCommonDaoFailed;
        protected void OnGenerateCommonDaoFailed(FileInfo dbJsFile, Result result)
        {
            if (GenerateCommonDaoFailed != null)
            {
                GenerateCommonDaoFailed(dbJsFile, result);
            }
        }

        public event Action<FileInfo, DirectoryInfo, Result> GenerateCommonDaoSucceeded;
        protected void OnGenerateCommonDaoSucceeded(FileInfo dbJsFile, DirectoryInfo daoBin, Result result)
        {
            if (GenerateCommonDaoSucceeded != null)
            {
                GenerateCommonDaoSucceeded(dbJsFile, daoBin, result);
            }
        }

        public event Action<FileInfo, DirectoryInfo> GeneratingCommonDao;
        protected void OnGeneratingCommonDao(FileInfo dbJsFile, DirectoryInfo daoBin)
        {
            if (GeneratingCommonDao != null)
            {
                GeneratingCommonDao(dbJsFile, daoBin);
            }
        }

        private void GenerateCommonDao(DirectoryInfo daoBinDir, DirectoryInfo daoTemp, FileInfo jsOrJsonDb)
        {
            OnGeneratingCommonDao(jsOrJsonDb, daoBinDir);

            Result schemaResult = GenerateDaoForFile(daoBinDir, daoTemp, jsOrJsonDb);

            if (!schemaResult.Success)
            {
                OnGenerateCommonDaoFailed(jsOrJsonDb, schemaResult);
            }
            else
            {
                OnGenerateCommonDaoSucceeded(jsOrJsonDb, daoBinDir, schemaResult);
            }
        }

        public event Action<DaoResponder> Initializing;

        protected void OnInitializing()
        {
            if (Initializing != null)
            {
                Initializing(this);
            }
        }

        public event Action<DaoResponder> Initialized;
        protected void OnInitialized()
        {
            if (Initialized != null)
            {
                Initialized(this);
            }
        }
    }
}
