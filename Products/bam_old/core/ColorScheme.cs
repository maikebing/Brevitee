using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;
using Brevitee.Drawing;
using Brevitee.Yaml;
using System.Drawing;
using System.Drawing.Drawing2D;

using dotless.Core;

namespace Bam.core
{
    public class ColorScheme//: ColorPalette
    {
        public ColorScheme()
            : base()
        {
            this.Name = "Default";
            this.Init();
        }

        public ColorScheme(string name, Fs fs)
            : this()
        {
            this.Name = name;
            this.Fs = fs;
        }

        public ColorScheme(Fs fs)
            : this("Default", fs)
        {
        }

        public ColorScheme(string name, Fs fs, HexColor[] colors)
            : this()
        {
            this.Name = name;
            if (colors.Length > 0)
            {
                this.Colors = colors;
            }
            this.Fs = fs;
        }

        public string Name { get; set; }

        Dictionary<string, HexColor> _colors;
        internal HexColor[] Colors
        {
            get
            {
                return _colors.Values.ToArray();
            }
            set
            {
                _colors = new Dictionary<string, HexColor>();
                value.Each<HexColor>((c) =>
                {
                    _colors.Add(c.Name, c);
                });
            }
        }

        internal  virtual void Init()
        {
            _colors = new Dictionary<string, HexColor>();
            AddColor("Tertiary", "#FFFFFF");
            AddColor("TertiaryText", "#FFFFFF");
            AddColor("Main", "#FFFFFF");
            AddColor("MainText", "#FFFFFF");
            AddColor("Secondary", "#FFFFFF");
            AddColor("SecondaryText", "#FFFFFF");
            AddColor("Background", "#FFFFFF");
            AddColor("Footer", "#FFFFFF");            
        }

        private void AddColor(string name, string hex)
        {
            HexColor color = new HexColor(name, hex);
            _colors.Add(name, color);
        }

        internal Fs Fs
        {
            get;
            set;
        }

        internal string LessHeader
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                string format = "@{0}: {1};\r\n";
                builder.AppendFormat(format, Tertiary.Name, Tertiary.Hex);
                builder.AppendFormat(format, TertiaryText.Name, TertiaryText.Hex);
                builder.AppendFormat(format, Main.Name, Main.Hex);
                builder.AppendFormat(format, MainText.Name, MainText.Hex);
                builder.AppendFormat(format, Secondary.Name, Secondary.Hex);
                builder.AppendFormat(format, SecondaryText.Name, SecondaryText.Hex);
                builder.AppendFormat(format, Background.Name, Background.Hex);
                return builder.ToString();
            }
        }

        HexColor _tertiary;
        object _tertiaryLock = new object();
        public HexColor Tertiary
        {
            get
            {
                return _tertiaryLock.DoubleCheckLock<HexColor>(ref _tertiary, () => GetNamedColor("Tertiary"));
            }
            set
            {
                _tertiary = value;
            }
        }

        HexColor _tertiaryText;
        object _tertiaryTextLock = new object();
        public HexColor TertiaryText
        {
            get
            {
                return _tertiaryTextLock.DoubleCheckLock<HexColor>(ref _tertiaryText, () => GetNamedColor("TertiaryText"));
            }
            set
            {
                _tertiaryText = value;
            }
        }

        HexColor _main;
        object _mainLock = new object();
        public HexColor Main
        {
            get
            {
                return _mainLock.DoubleCheckLock<HexColor>(ref _main, () => GetNamedColor("Main"));
            }
            set
            {
                _main = value;
            }
        }

        HexColor _mainText;
        object _mainTextLock = new object();
        public HexColor MainText
        {
            get
            {
                return _mainTextLock.DoubleCheckLock<HexColor>(ref _mainText, () => GetNamedColor("MainText"));
            }
            set
            {
                _mainText = value;
            }
        }

        HexColor _secondary;
        object _secondaryLock = new object();
        public HexColor Secondary
        {
            get
            {
                return _secondaryLock.DoubleCheckLock<HexColor>(ref _secondary, () => GetNamedColor("Secondary"));
            }
            set
            {
                _secondary = value;
            }
        }

        HexColor _secondaryText;
        object _secondaryTextLock = new object();
        public HexColor SecondaryText
        {
            get
            {
                return _secondaryTextLock.DoubleCheckLock<HexColor>(ref _secondaryText, () => GetNamedColor("SecondaryText"));
            }
            set
            {
                _secondaryText = value;
            }
        }

        HexColor _background;
        object _backgroundLock = new object();
        public HexColor Background
        {
            get
            {
                return _backgroundLock.DoubleCheckLock<HexColor>(ref _background, () => GetNamedColor("Background"));
            }
            set
            {
                _background = value;
            }
        }

        HexColor _footer;
        object _footerLock = new object();
        public HexColor Footer
        {
            get
            {
                return _footerLock.DoubleCheckLock<HexColor>(ref _footer, () => GetNamedColor("Footer"));
            }
            set
            {
                _footer = value;
            }
        }
        
        private HexColor GetNamedColor(string name)
        {
            return (from color in Colors
                        where color.Name.Equals(name)
                        select color).FirstOrDefault();
        }

        public void Save(string filePath, bool overwrite = false)
        {
            this.ToYaml().SafeWriteToFile(filePath, overwrite);
            GenerateImages();
            WriteCss();
        }

        public void WriteCss()
        {
            StringBuilder lessContent = new StringBuilder();
            lessContent.AppendLine(LessHeader);
            lessContent.AppendLine(ResourceTextFiles.ReadTextFile("style.txt", typeof(bam).Assembly));
            string css = Less.Parse(lessContent.ToString());
            Fs.WriteFile("~/content/css/style.css", css);
        }

        public void GenerateImages()
        {
            string path = string.Empty;
            // bg-navigation.gif - 2x40 - main
            Color mainColor = Main.ToColor();            
            SolidBrush mainBrush = new SolidBrush(Main.ToColor());
            Bitmap bgNavigation;
            using (Graphics graphics = GraphicsManager.GetCanvas(2, 40, mainBrush, out bgNavigation))
            {
                path = GetImagePath("bg-navigation.gif");
                bgNavigation.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            }

            // bg-footer.png - 2x2 - Footer
            Bitmap bgFooter;
            SolidBrush footerBrush = new SolidBrush(Tertiary.ToColor());
            using (Graphics graphics = GraphicsManager.GetCanvas(2, 2, footerBrush, out bgFooter))
            {
                path = GetImagePath("bg-footer.png");
                bgFooter.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            }

            // bg-top-navigation2.png - 2x2 Secondary
            Bitmap bgTopNav;
            SolidBrush secondaryBrush = new SolidBrush(Secondary.ToColor());
            using (Graphics graphics = GraphicsManager.GetCanvas(2, 2, secondaryBrush, out bgTopNav))
            {
                path = GetImagePath("bg-top-navigation2.png");
                bgTopNav.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            }

            // bg-body-bottom.jpg - 1400x626 --  gradient from bottom up then 10px line at 80px from the bottom - main
            Bitmap bgBodyBottom;
            LinearGradientBrush bgBottomBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, 626), Color.White, Background.ToColor());
            using (Graphics graphics = GraphicsManager.GetCanvas(1400, 626, bgBottomBrush, out bgBodyBottom))
            {
                graphics.FillRectangle(footerBrush, new Rectangle(0, 546, 1400, 80));
                graphics.FillRectangle(mainBrush, new Rectangle(0, 546, 1400, 10));
                bgBodyBottom.Save(GetImagePath("bg-body-bottom.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            // bg-body-top.jpg - 1400x648 -- gradient from top down then 110px solid - background
            Bitmap bgBodyTop;
            SolidBrush tertiaryBrush = new SolidBrush(Tertiary.ToColor());
            LinearGradientBrush bgTopBrush = new LinearGradientBrush(new Point(0, 110), new Point(0, 648), Background.ToColor(), Color.White);
            using (Graphics graphics = GraphicsManager.GetCanvas(1400, 648, bgTopBrush, out bgBodyTop))
            {
                graphics.FillRectangle(tertiaryBrush, new Rectangle(0, 0, 1400, 110));
                path = GetImagePath("bg-body-top.jpg");
                bgBodyTop.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        public ColorScheme SaveDefault()
        {
            this.Save(Fs.GetAbsolutePath("~/ColorScheme.yaml"), true);
            return this;
        }

        public static ColorScheme LoadDefault(Fs fs)
        {
            ColorScheme colorScheme;
            string path = "~/ColorScheme.yaml";
            if (fs.FileExists(path))
            {
                colorScheme = fs.ReadAllText(path).FromYaml<ColorScheme>();
            }
            else
            {
                colorScheme = new ColorScheme(fs);
                colorScheme.Save(fs.GetAbsolutePath(path));
            }

            colorScheme.Fs = fs;

            return colorScheme;
        }

        private string GetImagePath(string imageName)
        {
            return Fs.GetAbsolutePath(string.Format("~/content/images/{0}", imageName));
        }
    }
}
