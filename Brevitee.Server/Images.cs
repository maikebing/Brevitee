using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using Brevitee;
using Brevitee.Drawing;
using Brevitee.Logging;

namespace Brevitee.Server
{
    public class Images: ResponderBase
    {
        List<string> _prefixes;
        Dictionary<string, ImageFormat> _imageFormats;
        public Images(Fs fs)
            : base(fs)
        {
            Init();
        }

        public Images(Fs fs, ILogger logger)
            : base(fs, logger)
        {
            Init();
        }

        private void Init()
        {
            this._prefixes = new List<string>();
            this._prefixes.Add("~");
            this._prefixes.Add("~/images");
            this._prefixes.Add("~/content");
            this._prefixes.Add("~/content/images");

            ResourceImages.Load(typeof(AppServer).Assembly);
            _imageFormats = new Dictionary<string, ImageFormat>();
            _imageFormats.Add(".png", ImageFormat.Png);
            _imageFormats.Add(".bmp", ImageFormat.Bmp);
            _imageFormats.Add(".jpg", ImageFormat.Jpeg);
            _imageFormats.Add(".gif", ImageFormat.Gif);
        }

        public override bool TryRespond(IContext context)
        {
            bool handled = TryRespondWithResource(context);
            if (!handled)
            {
                foreach (string prefix in _prefixes)
                {
                    string path = string.Format("{0}{1}", prefix, context.Request.RawUrl);

                    if (Fs.FileExists(path))
                    {
                        using (BinaryWriter w = new BinaryWriter(context.Response.OutputStream))
                        {
                            w.Write(Fs.ReadBytes(path));
                            w.Flush();
                            w.Close();
                            handled = true;
                        }
                    }
                }
            }

            return handled;
        }

        private bool TryRespondWithResource(IContext context)
        {
            string path = context.Request.RawUrl;
            string fileName = Path.GetFileName(path);
            string ext = Path.GetExtension(path);
            if (string.IsNullOrEmpty(ext))
            {
                ext = ".png";
            }

            Image image;
            bool handled = false;
            if (ResourceImages.Has(fileName, out image))
            {
                image.Save(context.Response.OutputStream, _imageFormats[ext]);
                context.Response.OutputStream.Flush();
                context.Response.OutputStream.Close();
                handled = true;
            }

            return handled;
        }
    }
}
