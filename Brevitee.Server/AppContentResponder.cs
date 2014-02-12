using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Ionic.Zip;
using System.IO;
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

            this.SetBaseIgnorePrefixes();
        }

        private void SetBaseIgnorePrefixes()
        {
            AddIgnorPrefix("dao");
            AddIgnorPrefix("serviceproxy");
            AddIgnorPrefix("api");
            AddIgnorPrefix("bam");
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

        public void Initialize()
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

        LayoutModel _defaultLayoutModel;
        object _defaultLayoutModelLock = new object();
        public LayoutModel DefaultLayoutModel
        {
            get
            {   
                return _defaultLayoutModelLock.DoubleCheckLock(ref _defaultLayoutModel, () =>
                {
                    LayoutConf info = new LayoutConf(AppConf);
                    return info.CreateLayoutModel(ApplicationName);
                });
            }
        }
        
        protected internal LayoutModel GetLayoutModelForPath(string path, string ext = ".ba")
        {
            string filePath = string.Format("{0}{1}", path, ext);            
            string[] segments = new string[] { "pages", filePath };
            LayoutModel result = DefaultLayoutModel;

            if (AppRoot.FileExists(segments))
            {
                LayoutConf page = AppRoot.ReadAllText(segments).FromJson<LayoutConf>();
                result = page.CreateLayoutModel(AppConf);
            }

            if(string.IsNullOrEmpty(Path.GetExtension(path)))
            {
                result.StartPage = path.TruncateFront(1);
            }
            return result;
        }

        public override bool TryRespond(IContext context)
        {
            IRequest request = context.Request;
            IResponse response = context.Response;

            string path = request.Url.AbsolutePath;
            string ext = Path.GetExtension(path);
            string mgmtPrefix = "/bam/apps/{0}"._Format(ApplicationName);
            if (path.ToLowerInvariant().StartsWith(mgmtPrefix.ToLowerInvariant()))
            {
                path = path.TruncateFront(mgmtPrefix.Length);
            }

            string[] split = path.DelimitSplit("/");
            byte[] content = new byte[] { };
            bool result = false;
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
            else if (Cache.ContainsKey(path) && UseCache)
            {
                content = Cache[path];
                result = true;
            }
            else if (MinCache.ContainsKey(path) && UseCache) // check the min cache
            {
                content = MinCache[path];
                result = true;
            }
            else if (AppRoot.FileExists(path))
            {
                byte[] temp = ReadFile(AppRoot, path);

                content = temp;
                result = true;
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
