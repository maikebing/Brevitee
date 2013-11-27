using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;
using Brevitee.Javascript;
using Brevitee.Logging;
using dotless.Core;
using System.IO;

namespace Brevitee.Server
{
    public class Css: Content
    {
        public Css(Fs fs)
            : base(fs)
        {
            this.Js = new Scripts(fs);
        }

        public Css(Fs fs, ILogger logger)
            : base(fs, logger)
        {
            this.Js = new Scripts(fs);
        }

        public Css(Fs fs, Images images, ILogger logger)
            : this(fs, logger)
        {
            this.Images = images;
        }

        public Scripts Js
        {
            get;
            private set;
        }

        public Images Images
        {
            get;
            private set;
        }

        public void Less(Action<string> onNotFound = null)
        {
            FileInfo[] lessFiles = Fs.GetFiles("~/content/less", "*.less");
            foreach (FileInfo file in lessFiles)
            {
                Less(file.Name, onNotFound);
            }
        }
        
        public void Less(string lessFileName, Action<string> onNotFound = null)
        {
            string fileName = string.Format("~/content/less/{0}", lessFileName);
            string val = string.Empty;
            if (Fs.FileExists(fileName))
            {
                string src = Fs.ReadAllText(fileName);
                //LessEngine less = new LessEngine();                
                val = dotless.Core.Less.Parse(src); //less.TransformToCss(src, fileName);
                string sansExt = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = string.Format("{0}.css", sansExt);
                Fs.WriteFile(string.Format("~/content/css/{0}", newFileName), val);
            }
            else if(onNotFound != null)
            {
                onNotFound(lessFileName);
            }            
        }

        public override bool TryRespond(IContext context)
        {
            bool handled = Images.TryRespond(context);
            if (!handled)
            {
                handled =  base.TryRespond(context);
            }

            return handled;
        }
    }
}
