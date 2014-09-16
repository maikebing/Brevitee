using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Stickerize.Business.Data;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business
{
	/// <summary>
	/// DTO for Stickerizee
	/// </summary>
    public class StickerizeeInfo
    {
        public static implicit operator StickerizeeInfo(Stickerizee stickerizee)
        {
            return new StickerizeeInfo(stickerizee);
        }

        public static implicit operator Stickerizee(StickerizeeInfo item)
        {
            return Stickerizee.OneWhere(c => c.Id == item.Id);
        }

        public StickerizeeInfo(Stickerizee stickerizee)
        {
            this.Name = stickerizee.Name;
            Image image = stickerizee.ImageOfImageId;//.Images.FirstOrDefault();
            if (image != null)
            {
                this.StickerizeeImageSource = image.Url;
            }
            else
            {
                this.StickerizeeImageSource = Image.GetDefaultStickerizeeImage(stickerizee.Gender).Url;
            }

            this.Id = stickerizee.Id.Value;
        }

        public long Id { get; set; }
        public string StickerizeeImageSource { get; set; }
        public string Name { get; set; }
        
        public void Delete()
        {
            Stickerizee toDelete = Stickerizee.OneWhere(c => c.Id == Id);
            toDelete.Delete();
        }
    }
}
