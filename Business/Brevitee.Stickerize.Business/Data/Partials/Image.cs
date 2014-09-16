using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Http;
using System.Web.Mvc;
using Brevitee.Analytics.Data;
using Brevitee.Analytics;
using Brevitee.Logging;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;
using Brevitee.Stickerize.Business;

namespace Brevitee.Stickerize.Business.Data
{
    public partial class Image
    {
        public const string DefaultStickerizerName = "M4.jpg";
        public const string DefaultStickerName = "smileyface.png";
        public const string DefaultMaleStickerizeeName = "M0.png";
        public const string DefaultFemaleStickerizeeName = "F0.png";
        
        static Image _defaultStickerizer;
        static object _defaultStickerizerLock = new object();
        public static Image DefaultStickerizer
        {
            get
            {
                return _defaultStickerizerLock.DoubleCheckLock(ref _defaultStickerizer, () =>
                {
                    return Image.GetByUrl("/img/{0}"._Format(DefaultStickerizerName));
                });
            }
        }

        public static Image GetDefaultStickerizeeImage(string genderString)
        {
            return GetDefaultStickerizeeImage(genderString.TryToEnum<Gender>());
        }

        public static Image GetDefaultStickerizeeImage(Gender gender)
        {
            Image defaultImage = DefaultMaleStickerizee;
            if (gender == Gender.Female)
            {
                defaultImage = DefaultFemaleStickerizee;
            }

            return defaultImage;
        }

        static Image _defaultSticker;
        static object _defaultStickerLock = new object();
        public static Image DefaultSticker
        {
            get
            {
                return _defaultStickerLock.DoubleCheckLock(ref _defaultSticker, () =>
                {
                    return GetByUrl("/img/{0}"._Format(DefaultStickerName));
                });
            }
        }

        static Image _defaultMaleStickerizee;
        static object _defaultMaleStickerizeeLock = new object();
        public static Image DefaultMaleStickerizee
        {
            get
            {
                return _defaultMaleStickerizeeLock.DoubleCheckLock(ref _defaultMaleStickerizee, () =>
                {
                    return GetByUrl("/img/{0}"._Format(DefaultMaleStickerizeeName));
                });
            }
        }

        static Image _defaultFemaleStickerizee;
        static object _defaultFemaleStickerizeeLock = new object();
        public static Image DefaultFemaleStickerizee
        {
            get
            {
                return _defaultFemaleStickerizeeLock.DoubleCheckLock(ref _defaultFemaleStickerizee, () =>
                {
                    return GetByUrl("/img/{0}"._Format(DefaultFemaleStickerizeeName));
                });
            }
        }

        /// <summary>
        /// Gets an image instance with the specified url,
        /// creating it if necessary.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Image GetByUrl(string url)
        {
            Image image = Image.OneWhere(c => c.Url == url);
            if (image == null)
            {
                image = new Image();
                image.Url = url;
                image.Save();
            }

            return image;
        }
    }
}
