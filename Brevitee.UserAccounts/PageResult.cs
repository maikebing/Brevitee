using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Mvc;
using Brevitee;
using Brevitee.Web;
using Brevitee.ServiceProxy;

namespace Brevitee.UserAccounts
{
    /// <summary>
    /// The single page application equivalent of
    /// RedirectAction
    /// </summary>
    public class PageResult: RedirectResult
    {
        public PageResult(Uri url)
            : base(url.ToString())
        {
            this.PageName = Path.GetFileNameWithoutExtension(url.AbsolutePath);
        }

        public PageResult(string uri)
            : this(new Uri(uri))
        { }

        public string PageName
        {
            get;
            set;
        }

        public void ExecutResult(IHttpContext context)
        {
            context.Response.Redirect(Url);
        }

        public override void ExecuteResult(ControllerContext context)
        {            
            context.HttpContext.Response.Redirect(Url);
        }
    }
}
