using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;
using Brevitee.Stickerize.Business;
using Brevitee;
using Brevitee.ServiceProxy;

namespace Brevitee.Stickerize.Business.Data
{
    public partial class Stickerizee
    {
        public const string DefaultName = "Everyone";

        static Stickerizee _default;
        static object _defaultLock = new object();
        public static Stickerizee Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => GetByName(DefaultName));
            }
        }

        public Image GetImage()
        {
            Image result = this.ImageOfImageId;
            if (result == null)
            {
                result = Image.DefaultMaleStickerizee;
            }

            return result;
        }

        /// <summary>
        /// Get the stickerizee with the specified name
        /// they will be created if they do not exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Stickerizee GetByName(string name)
        {
            Stickerizee result = Stickerizee.OneWhere(c => c.Name == name);
            if (result == null)
            {
                result = Create(name);
            }

            return result;
        }

        //public Stickerization[] GetStickerizationsForDate(DateTime date)
        //{
        //    Creation creations = Creation.Where(c => c.At == date);
        //}

        public Stickerization Stickerize(DateTime at, Sticker sticker, Stickerizable stickerizable)
        {
            Args.ThrowIfNull(sticker, "sticker");
            Args.ThrowIfNull(stickerizable, "stickerizable");

            return Stickerize(at, sticker.Id.Value, stickerizable.Id.Value);
        }

        public Stickerization Stickerize(DateTime at, long stickerId, long stickerizableId)
        {
            Args.ThrowIfNull(this.Id, "Stickerizee.Id");

            return Stickerization.Create(at, stickerId, stickerizableId, this.Id.Value);
        }

        static object _createLock = new object();
        /// <summary>
        /// Create a stickerizee with the specified name or get an 
        /// existing entry if there is one
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public static Stickerizee Create(string name, Brevitee.Stickerize.Business.Gender gender = Brevitee.Stickerize.Business.Gender.Unspecified, string displayName = null, IHttpContext context = null)
        {
            Stickerizer current = Stickerizer.Get(context);
            return Create(current, name, gender, displayName);
        }

        public static Stickerizee Create(Stickerizer current, string name, Gender gender, string displayName)
        {
            lock (_createLock)
            {
                Stickerizee it = current.Stickerizees.Where(ee => ee.Name.Equals(name)).FirstOrDefault();

                if (it == null)
                {
                    it = current.Stickerizees.AddNew();

                    SetInfo(it, name, gender, displayName);

                    current.Created = DateTime.UtcNow.Date;
                    current.Save();
                }

                return it;
            }
        }

        public static void SetInfo(Stickerizee it, string name, Gender gender, string displayName)
        {
            it.Created = DateTime.UtcNow.Date;

            if (!it.Name.Equals(name))
            {
                it.Name = name;
            }

            if (!it.DisplayName.Equals(displayName))
            {

                it.DisplayName = displayName; 
            }

            if (!it.Gender.Equals(gender.ToString()))
            {
                it.Gender = gender.ToString();
            }

			if (it.ImageId == null)
			{
				switch (gender)
				{
					case Brevitee.Stickerize.Business.Gender.Female:
						it.ImageId = Image.DefaultFemaleStickerizee.Id;
						break;
					case Brevitee.Stickerize.Business.Gender.Unspecified:
					case Brevitee.Stickerize.Business.Gender.Male:
					default:
						it.ImageId = Image.DefaultMaleStickerizee.Id;
						break;
				}
			}

            it.Save();
        }
    }
}
