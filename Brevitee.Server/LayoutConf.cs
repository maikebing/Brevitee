using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;

namespace Brevitee.Server
{
    public class LayoutConf
    {
        /// <summary>
        /// Required for deserialization
        /// </summary>
        public LayoutConf() { }

        public LayoutConf(AppConf conf)
        {
            this.IncludeCommon = true;
            this.LayoutName = conf.DefaultLayout;
            this.AppConf = conf;
        }

        public AppConf AppConf
        {
            get;
            set;
        }
        
        public string LayoutName { get; set; }

        public bool IncludeCommon { get; set; }


        public LayoutModel CreateLayoutModel(string[] htmlPathSegments = null)
        {
            LayoutModel model = new LayoutModel();
            model.ApplicationName = AppConf.Name;
            
            model.LayoutName = LayoutName;
            model.ApplicationDisplayName = AppConf.DisplayName;            

            SetTags(AppConf, model);

            if (htmlPathSegments != null) 
            {
                SetBody(model, htmlPathSegments);
            }

            return model;
        }

        protected internal void SetTags(AppConf conf, LayoutModel layout)
        {
            Includes includes = AppContentResponder.GetAppIncludes(conf);
            if (IncludeCommon)
            {
                Includes commonIncludes = ContentResponder.GetCommonIncludesFromIncludeJs(conf.BreviteeConf.ContentRoot, false);
                includes = commonIncludes.Combine(includes);
            }

            layout.ScriptTags = includes.GetScriptTags().ToHtmlString();
            layout.StyleSheetLinkTags = includes.GetStyleSheetLinkTags().ToHtmlString();
        }

        protected internal void SetBody(LayoutModel layout, string[] pathSegments) 
        {
            Fs appRoot = AppConf.AppRoot;
            if (appRoot.FileExists(pathSegments)) 
            {
                string html = appRoot.ReadAllText(pathSegments);
                CQ dollarSign = CQ.Create(html);
                StringBuilder headLinks = new StringBuilder();
                dollarSign["link", dollarSign["head"]].Each(el => 
                {
                    AddLink(headLinks, el);
                });

                string body = dollarSign["body"].Html().Replace("\r", "").Replace("\n", "");
                StringBuilder links = new StringBuilder(layout.StyleSheetLinkTags);
                links.Append(headLinks.ToString());
                layout.StyleSheetLinkTags = links.ToString();
                layout.PageContent = body;
            }
        }

        private void AddLink(StringBuilder headLinks, IDomObject el) 
        {
            CQ cq = CQ.Create(el);
            string relAttr = cq.Attr("rel");
            string typeAttr = cq.Attr("type");
            string hrefAttr = cq.Attr("href");
            var obj = new
            {
                rel = string.IsNullOrEmpty(relAttr) ? "" : "rel=\"{0}\""._Format(relAttr),
                type = string.IsNullOrEmpty(typeAttr) ? "" : "type=\"{0}\""._Format(typeAttr),
                href = string.IsNullOrEmpty(hrefAttr) ? "" : "href=\"{0}\""._Format(hrefAttr)
            };
            headLinks.Append("<link {rel} {type} {href}>".NamedFormat(obj));
        }
    }
}
