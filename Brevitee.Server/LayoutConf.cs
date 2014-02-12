using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Server
{
    public class LayoutConf
    {
        public LayoutConf() { }

        public LayoutConf(AppConf conf)
        {
            this.IncludeCommon = true;
            this.LayoutName = conf.DefaultLayout;
            this.AppConf = conf;
        }

        protected internal AppConf AppConf
        {
            get;
            set;
        }
        
        public string LayoutName { get; set; }

        public bool IncludeCommon { get; set; }


        public LayoutModel CreateLayoutModel(AppConf conf)
        {
            this.AppConf = conf;
            return CreateLayoutModel();
        }

        public LayoutModel CreateLayoutModel()
        {
            if (AppConf == null)
            {
                throw new InvalidOperationException("AppConf was null; for deserialized LayoutConf applicationName must be specified; use CreateLayoutModel(<appName>);");
            }

            return CreateLayoutModel(AppConf.Name);
        }

        public LayoutModel CreateLayoutModel(string applicationName, string applicationDisplayName = null)
        {
            LayoutModel model = new LayoutModel();
            model.ApplicationName = applicationName;
            model.LayoutName = LayoutName;
            if (string.IsNullOrEmpty(applicationDisplayName))
            {
                applicationDisplayName = applicationName.PascalSplit(" ");
            }
            model.ApplicationDisplayName = applicationDisplayName;

            SetTags(AppConf, model);

            return model;
        }

        protected internal void SetTags(AppConf conf, LayoutModel layout)
        {
            Includes includes = AppContentResponder.GetAppIncludesFromIncludeJs(conf);
            if (IncludeCommon)
            {
                Includes commonIncludes = ContentResponder.GetCommonIncludesFromIncludeJs(conf.BreviteeConf.ContentRoot, false);
                includes = commonIncludes.Combine(includes);
            }

            layout.ScriptTags = includes.GetScriptTags().ToHtmlString();
            layout.StyleSheetLinkTags = includes.GetStyleSheetLinkTags().ToHtmlString();
        }
    }
}
