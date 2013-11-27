using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Collections;
using Brevitee;
using Brevitee.Data;
using Brevitee.Incubation;
using Yahoo.Yui.Compressor;

namespace Brevitee.Data
{
    public class DaoProxyResult: JavaScriptResult
    {
        Incubator _serviceProvider = Incubator.Default;
        public DaoProxyResult(bool min)
        {
            this.Script = DaoProxyRegistration.GetScript().ToString();
            if (min) Compress();
        }

        private void Compress()
        {
            JavaScriptCompressor jsc = new JavaScriptCompressor();
            this.Script = jsc.Compress(this.Script);
        }

    }
}
