using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Ionic.Zip;
using System.IO;
using Brevitee.Logging;
using Brevitee.ServiceProxy;
using Brevitee.Server.Renderers;
using Brevitee.Javascript;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Brevitee.Server
{
    public class AppContentResponder: ContentResponder
    {
        public AppContentResponder(ContentResponder serverRoot, string appName)
            : base(serverRoot.BreviteeConf)
        {
            this.ContentResponder = serverRoot;
            this.ContentRoot = serverRoot.Fs;
            this.AppConf = new AppConf(serverRoot.BreviteeConf, appName);
            this.AppRoot = this.AppConf.AppRoot;
            this.AppDustRenderer = new AppDustRenderer(this);
            this.UseCache = serverRoot.UseCache;
            this.ContentLocator = ContentLocator.Load(this);

            this.SetBaseIgnorePrefixes();
        }

        public AppContentResponder(ContentResponder serverRoot, AppConf conf)
            : base(serverRoot.BreviteeConf)
        {
            if (conf.BreviteeConf == null)
            {
                conf.BreviteeConf = serverRoot.BreviteeConf;
            }

            this.ContentResponder = serverRoot;
            this.ContentRoot = serverRoot.Fs;
            this.AppConf = conf;
            this.AppRoot = this.AppConf.AppRoot;
            this.AppDustRenderer = new AppDustRenderer(this);
            this.UseCache = serverRoot.UseCache;
            this.ContentLocator = ContentLocator.Load(this);

            this.SetBaseIgnorePrefixes();
        }

        private void SetBaseIgnorePrefixes()
        {
            AddIgnorPrefix("dao");
            AddIgnorPrefix("serviceproxy");
            AddIgnorPrefix("api");
            AddIgnorPrefix("bam");
            AddIgnorPrefix("get");
            AddIgnorPrefix("post");
        }

        public ContentLocator ContentLocator
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the main ContentResponder, which is the content responder
        /// for the server level of the current BreviteeServer
        /// </summary>
        public ContentResponder ContentResponder
        {
            get;
            private set;
        }

        public AppDustRenderer AppDustRenderer
        {
            get;
            private set;
        }

        public AppConf AppConf
        {
            get;
            private set;
        }

        public event Action<AppContentResponder> AppInitializing;
        public event Action<AppContentResponder> AppInitialized;

        protected void OnAppInitializing()
        {
            if (AppInitializing != null)
            {
                AppInitializing(this);
            }
        }

        protected void OnAppInitialized()
        {
            if(AppInitialized != null)
            {
                AppInitialized(this);
            }
        }

        public string ApplicationName
        {
            get
            {
                return AppConf.Name;
            }         
        }

        /// <summary>
        /// The server content root
        /// </summary>
        public Fs ContentRoot { get; private set; }

        /// <summary>
        /// The application content root
        /// </summary>
        public Fs AppRoot { get; private set; }

        /// <summary>
        /// Initializes the file system from the embedded zip resource
        /// that represents a bare bones app.
        /// </summary>
        public override void Initialize()
        {
            OnAppInitializing();
            string baseDirectory = Path.Combine(BreviteeConf.ContentRoot, "apps", ApplicationName);
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string[] resourceNames = currentAssembly.GetManifestResourceNames();
            resourceNames.Each(rn =>
            {
                bool isBase = Path.GetExtension(rn).ToLowerInvariant().Equals(".base");
                if (isBase)
                {
                    Stream zipStream = currentAssembly.GetManifestResourceStream(rn);
                    ZipFile zipFile = ZipFile.Read(zipStream);
                    zipFile.Each(entry =>
                    {
                        entry.Extract(baseDirectory, ExtractExistingFileAction.DoNotOverwrite);
                    });
                }
            });

            AppRoot.WriteFile("appConf.json", AppConf.ToJson(true));

            OnAppInitialized();
        }

        Dictionary<string, LayoutModel> _layoutModelsByPath;
        object _layoutsByPathSync = new object();
        protected internal Dictionary<string, LayoutModel> LayoutModelsByPath
        {
            get
            {
                return _layoutsByPathSync.DoubleCheckLock(ref _layoutModelsByPath, () => new Dictionary<string, LayoutModel>());
            }
        }
     
        protected internal LayoutModel GetLayoutModelForPath(string path, string ext = ".layout")
        {
			if (path.Equals("/"))
			{
				path = "/{0}"._Format(AppConf.DefaultPage.Or(AppConf.DefaultLayoutConst));
			}

            string lowered = path.ToLowerInvariant();
            string[] layoutSegments = string.Format("~/pages/{0}{1}", path, ext).DelimitSplit("/", "\\");
            string[] htmlSegments = string.Format("~/pages/{0}.html", path).DelimitSplit("/", "\\");

            LayoutModel result = null;
            if (LayoutModelsByPath.ContainsKey(lowered))
            {
                result = LayoutModelsByPath[lowered];
            }
            else if (AppRoot.FileExists(layoutSegments))
            {
                LayoutConf layoutConf = AppRoot.ReadAllText(layoutSegments).FromJson<LayoutConf>();
                layoutConf.AppConf = AppConf;
                result = layoutConf.CreateLayoutModel(htmlSegments);
                LayoutModelsByPath[lowered] = result;
            }
            else
            {
                LayoutConf defaultLayoutConf = new LayoutConf(AppConf);
                result = defaultLayoutConf.CreateLayoutModel(htmlSegments);
                FileInfo file = new FileInfo(AppRoot.GetAbsolutePath(layoutSegments));
                if (!file.Directory.Exists)
                {
                    file.Directory.Create();
                }
                // write the file to disk                 
                defaultLayoutConf.ToJsonFile(file);
                LayoutModelsByPath[lowered] = result;
            }

            if(string.IsNullOrEmpty(Path.GetExtension(path)))
            {
                string page = path.TruncateFront(1);
                if (!string.IsNullOrEmpty(page))
                {
                    result.StartPage = page;
                }
            }
            return result;
        }
        
        public override bool TryRespond(IHttpContext context)
        {
            IRequest request = context.Request;
            IResponse response = context.Response;

            string path = request.Url.AbsolutePath;
            string ext = Path.GetExtension(path);
            string mgmtPrefix = "/bam/apps/{0}"._Format(AppConf.DomApplicationIdFromAppName(ApplicationName));
            if (path.ToLowerInvariant().StartsWith(mgmtPrefix.ToLowerInvariant()))
            {
                path = path.TruncateFront(mgmtPrefix.Length);
            }

            string[] split = path.DelimitSplit("/");
            byte[] content = new byte[] { };
            bool result = false;

            string locatedPath;
            string[] checkedPaths;
            if (string.IsNullOrEmpty(ext) && !ShouldIgnore(path) ||
                (AppRoot.FileExists("~/pages{0}.html"._Format(path))))
            {
                CommonDustRenderer.SetContentType(response);
                MemoryStream ms = new MemoryStream();
                CommonDustRenderer.RenderLayout(GetLayoutModelForPath(path), ms);
                ms.Seek(0, SeekOrigin.Begin);
                content = ms.GetBuffer();
                result = true;
            }
            else if (ContentLocator.Locate(path, out locatedPath, out checkedPaths))
            {
                if (Cache.ContainsKey(locatedPath) && UseCache)
                {
                    content = Cache[path];
                    result = true;
                }
                else if (MinCache.ContainsKey(locatedPath) && UseCache) // check the min cache
                {
                    content = MinCache[locatedPath];
                    result = true;
                }
                else if (AppRoot.FileExists(locatedPath))
                {
                    byte[] temp = ReadFile(AppRoot, locatedPath);

                    content = temp;
                    result = true;
                }
            }
            else
            {
                if (AppConf.LogNotFoundFilesWithTheseExtensions.Contains(ext))
                {
                    StringBuilder checkedPathString = new StringBuilder();
                    checkedPaths.Each(p =>
                    {
                        checkedPathString.AppendLine(p);
                    });

                    Logger.AddEntry(
                        "App[{0}]::Path='{1}'::Not Found\r\nChecked Paths\r\n{2}", 
                        LogEventType.Warning, 
                        AppConf.Name, 
                        path,
                        checkedPathString.ToString()
                    );
                }
            }

            
            if (result)
            {
                SetContentType(response, path);
                SendResponse(response, content);
            }
            return result;
        }
    }
}
