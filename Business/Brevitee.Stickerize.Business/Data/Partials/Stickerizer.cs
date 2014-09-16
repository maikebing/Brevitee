using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;
using Brevitee.Logging;
using Brevitee.UserAccounts;
using Brevitee.UserAccounts.Data;
using Brevitee.ServiceProxy;
using System.Data;
using Brevitee.Stickerize.Business;

namespace Brevitee.Stickerize.Business.Data
{
    public partial class Stickerizer
    {
        public const string SystemStickerizerName = "Bob Saggington";

        static Stickerizer _default;
        static object _defaultLock = new object();
        public static Stickerizer System
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => Create(SystemStickerizerName));
            }
        }

        static Stickerizer _anon;
        static object _anonLock = new object();
        public static Stickerizer Anonymous
        {
            get
            {
                return _anonLock.DoubleCheckLock(ref _anon, () => Create(User.Anonymous.UserName));
                    
            }
        }

        public Image GetImage()
        {
            Image result = this.ImageOfImageId;
            if (result == null)
            {
                result = Image.DefaultStickerizer;
            }

            return result;
        }

        /// <summary>
        /// Get the stickerizer entry for the current user.
        /// It will be created if it doesn't exist.
        /// </summary>
        /// <returns></returns>
        public static Stickerizer Get(IHttpContext context)
        {
            User current = User.GetCurrent(context);
            Stickerizer result = Stickerizer.OneWhere(c => c.Name == current.UserName);
            if (current == User.Anonymous)
            {
                result = Anonymous;
            }
            else if (result == null)
            {
                result = Create(current.UserName);
            }
                        
            return result;
        }

        static object _createLock = new object();
        /// <summary>
        /// Create a Stickerizer with the specified name or retirev an existing one
        /// if one exists.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Stickerizer Create(string name)
        {
            lock (_createLock)
            {
                Stickerizer result = Stickerizer.OneWhere(c => c.Name == name);
                if (result == null)
                {
                    result = new Stickerizer();
                    result.Name = name;
                    result.DisplayName = name;
                    result.UserName = name;
                    result.Created = DateTime.UtcNow.Date;
                    result.Save();
                }

                return result;
            }
        }

        public Stickerization Stickerize(long stickerizeeId)
        {
            return Stickerize(Stickerizable.Default, Stickerizee.OneWhere(c => c.Id == stickerizeeId));
        }

        public Stickerization Stickerize(Stickerizee stickerizee)
        {
            return Stickerize(Stickerizable.Default, stickerizee);
        }

        public Stickerization Stickerize(long stickerizableId, long stickerizeeId)
        {
            return Stickerize(Stickerizable.OneWhere(c => c.Id == stickerizableId), Stickerizee.OneWhere(c => c.Id == stickerizeeId));
        }

        public Stickerization Stickerize(Stickerizable stickerizable, Stickerizee stickerizee)
        {
            return Stickerize(DateTime.UtcNow, Sticker.Default, stickerizable, stickerizee);
        }

        public Stickerization Stickerize(DateTime at, Sticker sticker, Stickerizable stickerizable, Stickerizee stickerizee)
        {
            Args.ThrowIfNull(sticker, "sticker");
            Args.ThrowIfNull(stickerizable, "stickerizable");
            Args.ThrowIfNull(stickerizee, "stickerizee");

            return Stickerize(at, sticker.Id.Value, stickerizable.Id.Value, stickerizee.Id.Value);
        }

        public Stickerization Stickerize(DateTime at, long stickerizableId, long stickerizeeId)
        {
            return Stickerize(at, Sticker.Default.Id.Value, stickerizableId, stickerizeeId);
        }

        public Stickerization Stickerize(DateTime at, long stickerId, long stickerizableId, long stickerizeeId)
        {
            Stickerization ize = Stickerization.Create(at, stickerId, stickerizableId, stickerizeeId, Id.Value);
            if (ize.IsUndone.Value)
            {
                ize.IsUndone = false;
                ize.Save();
            }

            return ize;
        }         
        

        public Stickerizee AddStickerizee(string name, Gender gender = Gender.Unspecified, string displayName = null)
        {
            Args.ThrowIfNullOrEmpty(name, "name");
            if (string.IsNullOrEmpty(displayName))
            {
                displayName = name;
            }
            return Stickerizee.Create(this, name, gender, displayName);
        }

        public void RemoveStickerizee(long id)
        {
            Stickerizee ee = Stickerizees.Where(c => c.Id.Value == id).FirstOrDefault();
            if (ee != null)
            {
                Stickerizees.Remove(ee);
            }
        }

        public void AddStickerizee(Stickerizee stickerizee)
        {
            Stickerizees.Add(stickerizee);
            Save();
        }
    }
}
