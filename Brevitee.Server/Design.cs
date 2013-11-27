using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

using Brevitee;
using Brevitee.Yaml;
using Brevitee.Logging;
using Brevitee.Data;
using Brevitee.Dust;
using Brevitee.Drawing;

namespace Brevitee.Server
{
    /// <summary>
    /// The administrative interface to core Brevitee developer
    /// functionality.  Not implemented as an IResponder 
    /// to prevent somehow adding it to the BreviteeRequestHandler's
    /// responder list.   
    /// </summary>
    [Proxy("design")]
    public class Design
    {
        List<string> _exts;
        Dictionary<string, Func<string, PageModel>> _pageModelDeserializers;

        public Design(Fs fs, ILogger logger)
        {
            this._exts = new List<string>();

            this._pageModelDeserializers = new Dictionary<string, Func<string, PageModel>>();
            this._pageModelDeserializers.Add("yaml", (s) =>
            {
                return s.FromYaml<PageModel>();
            });
            this._pageModelDeserializers.Add("json", (s) =>
            {
                return s.FromJson<PageModel>();
            });

            this.Fs = fs;
            this.Logger = logger;
        }

        public Fs Fs
        {
            get;
            private set;
        }


        public object GetColorScheme()
        {
            return new { Colors = ColorScheme.Colors };
        }

        public ColorScheme SaveColorScheme(ColorScheme scheme)
        {
            scheme.Save(Fs, true);
            return scheme;
        }

        ColorScheme _colorScheme;
        public ColorScheme ColorScheme
        {
            get
            {
                if (_colorScheme == null || (_colorScheme != null && _colorScheme.Colors.Length == 0))
                {
                    _colorScheme = ColorScheme.LoadDefault(Fs);
                    //string path = "~/ColorScheme.yaml";
                    //if (Fs.FileExists(path))
                    //{
                    //    _colorScheme = Fs.ReadAllText(path).FromYaml<ColorScheme>();
                    //}
                    //else
                    //{
                    //    _colorScheme = new ColorScheme();
                    //    _colorScheme.Save(Fs.GetAbsolutePath(path));
                    //}
                }

                return _colorScheme;
            }
        }

        public ILogger Logger
        {
            get;
            private set;
        }

        public void ProxyConnection(string real, string fake)
        {
            Dao.ProxyConnection(real, fake);
        }

        public void UnproxyConnection(string real)
        {
            Dao.UnproxyConnection(real);
        }

        /// <summary>
        /// Regenerate the specified page.  Intended to 
        /// be called after setting the page json or
        /// yaml
        /// </summary>
        /// <param name="pageName"></param>
        public void Regenerate(string pageName)
        {
            Brevitee.Dust.Dust.DustRoot = Fs.GetAbsolutePath("~/content/templates");
            PageModel page = GetPageModel(pageName);
            if (page != null)
            {
                Regenerate(page);
            }
        }

        public void RegenerateFromJson(string pageModelJson)
        {
            PageModel pageModel = pageModelJson.FromJson<PageModel>();
            SetPageJson(pageModel.Name, pageModelJson);
            Regenerate(pageModel);
        }

        public void RegenerateFromYaml(string pageModelYaml)
        {
            PageModel pageModel = pageModelYaml.FromYaml<PageModel>();
            SetPageYaml(pageModel.Name, pageModelYaml);
            Regenerate(pageModel);
        }

        private void Regenerate(PageModel page)
        {
            string fileName = string.Format("~/content/{0}.html", page.Name);
            string output = Brevitee.Dust.Dust.Render(page.LayoutTemplate, page);
            Fs.WriteFile(fileName, string.Format("<!DOCTYPE html>\r\n{0}", output), true);
        }

        public void SetPageYaml(string pageName, string yaml)
        {
            string file = "~/models/pages/yaml/{0}.yaml";
            Fs.WriteFile(string.Format(file, pageName), yaml, true);
        }

        public void SetPageJson(string pageName, string json)
        {
            string file = "~/models/pages/json/{0}.json";
            Fs.WriteFile(string.Format(file, pageName), json, true);
        }

        public string GetPageYaml(string pageName)
        {
            return GetPageModel(pageName).ToYaml();
        }

        public string GetPageJson(string pageName)
        {
            return GetPageModel(pageName).ToJson();
        }

        public string[] GetPageNames()
        {
            string dirFormat = "~/models/pages/{0}";
            string[] exts = new string[] { "yaml", "json" };
            List<string> names = new List<string>();
            foreach (string ext in exts)
            {             
                DirectoryInfo dir = new DirectoryInfo(Fs.GetAbsolutePath(string.Format(dirFormat, ext)));
                foreach (FileInfo file in dir.GetFiles(string.Format("*.{0}", ext)))
                {
                    string txt = File.ReadAllText(file.FullName);
                    PageModel pageModel = _pageModelDeserializers[ext](txt);
                    names.Add(pageModel.Name);
                }
            }
            return names.ToArray();
        }

        //public ColorPalette GetColorPalette()
        //{
        //    return ColorPalette;
        //}

        //public ColorPalette ChangeColorName(string oldName, string newName)
        //{
        //    HexColor color = GetColorOrDie(oldName);
            
        //    color.Name = newName;
        //    ColorPalette.Save(Fs, true);
        //    return ColorPalette;
        //}

        //public ColorPalette ChangeColor(string name, string hexValue)
        //{
        //    HexColor color = GetColorOrDie(name);

        //    color.Hex = hexValue;
        //    ColorPalette.Save(Fs, true);
        //    return ColorPalette;
        //}

        //private HexColor GetColorOrDie(string oldName)
        //{
        //    HexColor color = ColorPalette[oldName];
        //    Args.ThrowIf<ArgumentException>(color == null, "The color named {0} was not found", oldName);
        //    return color;
        //}

        //public ColorPalette AddColor(string name, string hexValue)
        //{
        //    ColorPalette.Add(name, hexValue);
        //    ColorPalette.Save(Fs, true);
        //    return ColorPalette;
        //}

        //public ColorPalette DeleteColor(string name)
        //{
        //    ColorPalette.Remove(name);
        //    ColorPalette.Save(Fs, true);
        //    return ColorPalette;
        //}

        public void WriteLessColorsFile()
        {
            StringBuilder less = new StringBuilder();
            foreach (HexColor color in ColorScheme.Colors)
            {
                less.AppendFormat("{0}: {1};\r\n", color.LessName(), color.Hex);
            }

            Fs.WriteFile("~/colors.less", less.ToString(), true);
        }

        private PageModel GetPageModel(string pageName)
        {
            string ext;
            string text = GetPageModel(pageName, out ext);
            if(!string.IsNullOrEmpty(text))
            {
                return _pageModelDeserializers[ext](text);
            }
            return null;
        }

        private string GetPageModel(string pageName, out string ext)
        {
            string file = "~/models/pages/{1}/{0}.{1}";
            ext = "yaml";
            string txt = "";
            string[] exts = new string[] { "yaml", "json" };
            foreach (string ex in exts)
            {
                string fileName = string.Format(file, pageName, ex);
                if (Fs.FileExists(fileName))
                {
                    ext = ex;
                    txt = Fs.ReadAllText(fileName);
                }
            }

            return txt;
        }
    }
}
