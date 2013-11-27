using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using System.Web;
using Brevitee.ServiceProxy;
using Brevitee;
using Brevitee.Logging;
using System.IO;
using Brevitee.Configuration;
using Brevitee.Incubation;
using Brevitee.Html;

namespace Brevitee.Management
{
    public class DocsController: Controller
    {
        static DocsController()
        {
            DocResult.DefaultAttributeRenderer = (infos, output) =>
            {
                Tag container = new Tag("div");
                infos.Keys.Each(type =>
                {

                    //container.Child(new Tag("h1").Text(type.FullName));
                    //infos[type]
                });
            };

            DocResult.DefaultXmlRenderer = (infos, output) =>
            {

            };
        }
        public ActionResult Get(string[] classNames = null)
        {
            if (classNames == null)
            {
                classNames = ServiceProxySystem.Incubator.ClassNames;
            }

            return new DocResult(classNames);
        }
    }
}
