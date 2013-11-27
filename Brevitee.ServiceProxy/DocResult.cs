using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Brevitee;
using Brevitee.Incubation;
using System.Reflection;
using System.Web.Script.Serialization;
using Brevitee.Logging;

namespace Brevitee.ServiceProxy
{
    public class DocResult: ActionResult
    {
        public DocResult(string[] classNames)
        {
            this.ClassNames = classNames;
        }

        public DocResult(string xmlPath)
        {
            this.XmlPath = xmlPath;
        }

        public string[] ClassNames { get; set; }
        public string XmlPath { get; set; }

        public static DocAttributeRenderDelegate DefaultAttributeRenderer { get; set; }

        DocAttributeRenderDelegate _attributeRenderer;
        object _attributeRendererLock = new object();
        public DocAttributeRenderDelegate AttributeRenderer
        {
            get
            {
                return _attributeRendererLock.DoubleCheckLock(ref _attributeRenderer, () => DefaultAttributeRenderer);
            }
            set
            {
                _attributeRenderer = value;
            }
        }

        public static DocXmlRenderDelegate DefaultXmlRenderer { get; set; }
        DocXmlRenderDelegate _xmlRenderer;
        object _xmlRendererLock = new object();
        public DocXmlRenderDelegate XmlRenderer
        {
            get
            {
                return _xmlRendererLock.DoubleCheckLock(ref _xmlRenderer, () => DefaultXmlRenderer);
            }
            set
            {
                _xmlRenderer = value;
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (ClassNames.Length > 0)
            {
                RenderFromDocAttributes(context);
            }
            else if(!string.IsNullOrEmpty(XmlPath))
            {
                RenderFromXmlPath(context);
            }
        }

        private void RenderFromXmlPath(ControllerContext context)
        {
            StringBuilder output = new StringBuilder();
            Dictionary<string, List<DocInfo>> documentation = new Dictionary<string, List<DocInfo>>();

            if (!string.IsNullOrEmpty(XmlPath))
            {
                Incubator container = ServiceProxySystem.Incubator;
                HttpServerUtilityBase server = context.HttpContext.Server;
                ClassNames.Each(cn =>
                {
                    Type type = container[cn];
                    documentation = DocInfo.FromXmlFilesIn(XmlPath);
                });
            }

            if (XmlRenderer != null)
            {
                XmlRenderer(documentation, output);
                context.HttpContext.Response.Write(output.ToString());
            }
            else
            {
                context.HttpContext.Response.Write("Xml documentation renderer not specified.  Set DocResult.XmlRenderer per request or set DocResult.DefaultXmlRenderer for global effect");
            }
        }

        private void RenderFromDocAttributes(ControllerContext context)
        {
            StringBuilder output = new StringBuilder();
            Dictionary<Type, DocInfo[]> documentation = new Dictionary<Type, DocInfo[]>();

            if (ClassNames.Length > 0)
            {
                Incubator container = ServiceProxySystem.Incubator;
                HttpServerUtilityBase server = context.HttpContext.Server;
                ClassNames.Each(cn =>
                {
                    Type type = container[cn];
                    documentation = DocInfo.FromDocAttributes(type);
                });
            }

            if (AttributeRenderer != null)
            {
                AttributeRenderer(documentation, output);
                context.HttpContext.Response.Write(output.ToString());
            }
            else
            {
                context.HttpContext.Response.Write("Attribute documentation renderer not specified.  Set DocResult.AttributeRenderer per request or set DocResult.DefaultAttributeRenderer for global effect");
            }
        }

        internal static void GetDocInfosFromBinXml(Dictionary<string, DocInfo[]> documentation, HttpServerUtilityBase server)
        {
            DirectoryInfo bin = new DirectoryInfo(server.MapPath("~/bin"));
            FileInfo[] xmlFiles = bin.GetFiles("*.xml");
            xmlFiles.Each(file =>
            {
                try
                {
                    Dictionary<string, List<DocInfo>> temp = new Dictionary<string, List<DocInfo>>();
                    doc doc = file.FromXmlFile<doc>();
                    
                    doc.members.Items.Each(o =>
                    {
                        //o.
                        //docMembers docMembers = o as docMembers;
                        //if (docMembers != null)
                        //{
                        //    docMembers.member.Each((m, i) =>
                        //    {
                        //        DocInfo info = DocInfo.FromXmlDocMember(m);
                        //        string declaringType = info.DeclaringTypeName;

                        //    });
                        //}
                    });
                }
                catch (Exception ex)
                {
                    Log.AddEntry("An error occurred extrracting documentation from {0}", ex, file.FullName);
                }
            });
        }            

    }
}
