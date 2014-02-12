using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Html;
using System.Web.Mvc;
using System.IO;

namespace Brevitee.Server.Renderers
{
    /// <summary>
    /// The renderer used to render a template for 
    /// a given object.  In other words writes
    /// the default template for an object.
    /// </summary>
    public class CommonDustTemplateRenderer: RendererBase
    {
        public CommonDustTemplateRenderer(ContentResponder content)
            : base("text/html", ".dust")
        {
            this.ContentResponder = content;
        }

        public ContentResponder ContentResponder
        {
            get;
            set;
        }

        object _renderLock = new object();
        public override void Render(object toRender)
        {
            base.Render(toRender);
            DirectoryInfo dustDir = GetDustRoot();
            if (!dustDir.Exists)
            {
                dustDir.Create();
            }

            lock (_renderLock)
            {
                string fileName = "{0}.dust"._Format(toRender.GetType().Name);
                using (FileStream fs = File.Create(Path.Combine(dustDir.FullName, fileName), (int)OutputStream.Length))
                {
                    OutputStream.Seek(0, SeekOrigin.Begin);
                    OutputStream.CopyTo(fs);
                }
            }
        }

        protected virtual DirectoryInfo GetDustRoot()
        {
            DirectoryInfo dustDir = new DirectoryInfo(Path.Combine(ContentResponder.Root, "dust"));
            return dustDir;
        }

        /// <summary>
        /// Writes a FieldSet for the specified object toRender.
        /// </summary>
        /// <param name="toRender"></param>
        /// <param name="output"></param>
        public override void Render(object toRender, Stream output)
        {
            string fieldSet = FieldSetFor(toRender.GetType()).ToString();            
            byte[] data = Encoding.UTF8.GetBytes(fieldSet);
            output.Write(data, 0, data.Length);
        }

        protected internal static MvcHtmlString FieldSetFor(string json, string legendText = null, object wrapperAttrs = null , bool setValues = false)
        {
            return HtmlHelperExtensions.FieldsetFor(null, json, legendText, wrapperAttrs, setValues);
        }

        protected internal static MvcHtmlString FieldSetFor(dynamic obj, string legendText, object wrapperAttrs = null, bool setValues = false)
        {
            return HtmlHelperExtensions.FieldsetFor(null, obj, legendText, wrapperAttrs, setValues);
        }

        public static MvcHtmlString FieldSetFor(Type type, object defaults = null, string legendText = null, object wrapperAttrs = null)
        {
            return HtmlHelperExtensions.FieldSetFor(null, type, defaults, legendText, wrapperAttrs);
        }

        protected internal static MvcHtmlString InputsFor(Type type, object defaults = null, object wrapperAttrs = null)
        {
            return HtmlHelperExtensions.InputsFor(null, type, defaults, wrapperAttrs);
        }

    }
}
