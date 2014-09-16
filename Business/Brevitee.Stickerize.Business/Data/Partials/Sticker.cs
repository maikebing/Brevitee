using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using Brevitee.Data;
using Brevitee.Logging;

namespace Brevitee.Stickerize.Business.Data
{
    public partial class Sticker
    {
        public const string DefaultStickerName = "Smiley Face";

        static Sticker _defaultSticker;
        static object _defaultStickerLock = new object();
        public static Sticker Default
        {
            get
            {
                return _defaultStickerLock.DoubleCheckLock(ref _defaultSticker, () =>
                {
                    Sticker result = Sticker.OneWhere(c => c.Name == DefaultStickerName);
                    if (result == null)
                    {
                        result = new Sticker();
                        result.Name = DefaultStickerName;
                        result.ImageId = Image.DefaultSticker.Id.Value;
                        result.Created = DateTime.UtcNow.Date;
                        result.Save();
                    }

                    return result;
                });
            }
        }

        public Stickerization Stickerize(DateTime at, Stickerizee stickerizee, Stickerizable stickerizable)
        {
            Args.ThrowIfNull(stickerizee, "stickerizee");
            Args.ThrowIfNull(stickerizable, "stickerizable");

            return Stickerize(at, stickerizee.Id.Value, stickerizable.Id.Value);
        }

        public Stickerization Stickerize(DateTime at, long stickerizeeId, long stickerizableId)
        {
            return Stickerization.Create(at, this.Id.Value, stickerizableId, stickerizeeId);
        }
    }
}
