using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;
using Brevitee.Logging;
using Brevitee.ServiceProxy;
using System.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public partial class Stickerization
    {
        public void Unstickerize()
        {
            UndoneAt = DateTime.UtcNow;
            IsUndone = true;
            Save();            
        }

        public static Stickerization Create(long stickerizableId, long stickerizeeId)
        {
            return Create(DateTime.UtcNow, Sticker.Default.Id.Value, stickerizableId, stickerizeeId);
        }

        public static Stickerization Create(DateTime at, long stickerId, long stickerizableId, long stickerizeeId, IHttpContext context = null)
        {
            return Create(at, stickerId, stickerizableId, stickerizeeId, Stickerizer.Get(context).Id.Value);
        }

        public static Stickerization Create(DateTime at, long stickerId, long stickerizableId, long stickerizeeId, long stickerizerId)
        {
            //CreationCollection creations = Creation.Where(c => c.At == at.Date);
            Stickerization result = Stickerization
                .OneWhere(c => c.StickerId == stickerId &&
                    c.StickerizableId == stickerizableId &&
                    c.StickerizeeId == stickerizeeId &&
                    c.StickerizerId == stickerizerId &&
                    c.ForDate == at.Date);

            if(result == null)
            {
                try
                {
                    result = new Stickerization();
                    result.StickerId = stickerId;
                    result.StickerizableId = stickerizableId;
                    result.StickerizeeId = stickerizeeId;
                    result.StickerizerId = stickerizerId;
                    result.Created = DateTime.UtcNow.Date;
                    result.ForDate = at.Date;
                    result.Save();
                }
                catch (Exception ex)
                {
                    Log.AddEntry("An error occurred creating stickerization: {0}", ex, ex.Message);
                    result = null;
                }
            }

            return result;
        }
    }
}
