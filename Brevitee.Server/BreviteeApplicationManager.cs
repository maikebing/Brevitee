using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using Brevitee;
using Brevitee.Server.Renderers;
using Brevitee.Logging;
using Brevitee.ServiceProxy;

namespace Brevitee.Server
{
    [Proxy("bam")]
    public class BreviteeApplicationManager: IRequiresHttpContext//: IInitialize<BreviteeApplicationManager>
    {
        string _pagesNamedFormatPath = "~/apps/{appName}/pages";

        public BreviteeApplicationManager(BreviteeConf conf)
        {
            this.BreviteeConf = conf;
        }

        ILogger _logger;
        object _loggerLock = new object();
        public ILogger Logger
        {
            get
            {
                return _loggerLock.DoubleCheckLock(ref _logger, () =>
                {
                    Log.Start();
                    return Log.Default;
                });
            }
            set
            {
                if (_logger != null)
                {
                    _logger.StopLoggingThread();
                }

                _logger = value;
                _logger.RestartLoggingThread();
            }
        }

        public BreviteeConf BreviteeConf
        {
            get;
            private set;
        }

        public string DustTemplates()
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            string appName = AppConf.AppNameFromUri(HttpContext.Request.Url);
            Dictionary<string, AppContentResponder> appResponders = BreviteeConf.Server.ContentResponder.AppContentResponders;            
            AppContentResponder app = appResponders[appName];
            return Regex.Unescape(app.AppDustRenderer.CompiledDustTemplates);
        }

        /// <summary>
        /// Called by client code
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public BreviteeApplicationManagerResult GetPages(string appName = "localhost")
        {
            try
            {
                return GetSuccessWrapper(GetPageNamesFromDomAppId(appName), MethodBase.GetCurrentMethod().Name);
            }
            catch (Exception ex)
            {
                return GetErrorWrapper(ex, true, MethodBase.GetCurrentMethod().Name);
            }
        }

        protected internal string[] GetPageNamesFromDomAppId(string appId)
        {
            string appName = appId;
            if (AppConf.AppNamesByDomAppId.ContainsKey(appId))
            {
                appName = AppConf.AppNamesByDomAppId[appId];
            }

            return GetPageNames(appName);
        }

        protected internal string[] GetPageNames(string appName)
        {
            List<string> pageNames = new List<string>();
            DirectoryInfo pagesDir = new DirectoryInfo(BreviteeConf.Fs.GetAbsolutePath(_pagesNamedFormatPath.NamedFormat(new { appName = appName })));

            if (pagesDir.Exists)
            {
                AddPageNames(pagesDir, pageNames, pagesDir);
            }

            return pageNames.ToArray();
        }

        
        private void AddPageNames(DirectoryInfo appPagesDir, List<string> pageNames, DirectoryInfo currentPageSubDir)
        {
            FileInfo[] files = currentPageSubDir.GetFiles("*.html");

            string prefix = currentPageSubDir.FullName.TruncateFront(appPagesDir.FullName.Length).Replace("\\", "/");//.Replace(pagesDir.FullName, "");
            if (!string.IsNullOrEmpty(prefix))
            {
                prefix = string.Format("{0}/", prefix.Substring(1, prefix.Length - 1));
            }
            foreach (FileInfo file in files)
            {
                string pageName = string.Format("{0}{1}", prefix, Path.GetFileNameWithoutExtension(file.Name));
                pageNames.Add(pageName);
            }

            Traverse(appPagesDir, pageNames, currentPageSubDir);
        }

        private void Traverse(DirectoryInfo pagesDir, List<string> pageNames, DirectoryInfo currentPageSubDir)
        {
            DirectoryInfo[] childDirs = currentPageSubDir.GetDirectories();
            for (int i = 0; i < childDirs.Length; i++)
            {
                currentPageSubDir = childDirs[i];
                AddPageNames(pagesDir, pageNames, currentPageSubDir);
            }
        }

        public BreviteeApplicationManagerResult GetSuccessWrapper(object toWrap, string methodName = "Unspecified")
        {
            Logger.AddEntry("Success::{0}", methodName);
            return new BreviteeApplicationManagerResult(true, "", toWrap);
        }

        public BreviteeApplicationManagerResult GetErrorWrapper(Exception ex, bool stack = true, string methodName = "Unspecified")
        {
            Logger.AddEntry("Error::{0}\r\n***{1}\r\n***", ex, methodName, ex.Message);
            string message = GetMessage(ex, stack);
            return new BreviteeApplicationManagerResult(false, message, null);
        }

        private string GetMessage(Exception ex, bool stack)
        {
            string st = stack ? ex.StackTrace : "";
            return string.Format("{0}:\r\n\r\n{1}", ex.Message, st);
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }
    }
}
