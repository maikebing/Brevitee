using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Stickerize.Business.Data;
using System.Web.Mvc;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business
{
    public class FeedItem
    {
        public FeedItem()
        {
            SetCreationInfo(DateTime.UtcNow);
            SetItemInfo(null, null, null);
            this.StickerSource = "JSmiley.png";
        }

        public FeedItem(Stickerization stickerization)
        {
			this.StickerSource = "JSmiley.png";
            DateTime created = stickerization.Created.Value;//dateTimeInfo.At.Value;
            SetCreationInfo(created);

            Stickerizer er = stickerization.StickerizerOfStickerizerId;
            Stickerizee ee = stickerization.StickerizeeOfStickerizeeId;
            Stickerizable izable = stickerization.StickerizableOfStickerizableId;

            SetItemInfo(er, ee, izable);
        }
        
        public FeedItem(Stickerizer stickerizer, Stickerizee stickerizee, Stickerizable stickerizable, Stickerization stickerization)
        {
            //Creation dateTimeInfo = stickerization.CreationOfCreationId;
            DateTime created = stickerization.Created.Value;//dateTimeInfo.At.Value;
            SetCreationInfo(created);
            SetItemInfo(stickerizer, stickerizee, stickerizable);
        }

        private void SetCreationInfo(DateTime created)
        {
            this.Month = created.Month;
            this.Date = created.Day;
            this.Year = created.Year;
            this.Hour = created.Hour;
            this.Minute = created.Minute;
            this.Second = created.Second;
            this.Millisecond = created.Millisecond;
        }

        private void SetItemInfo(Stickerizer er, Stickerizee ee, Stickerizable izable)
        {
			//this.StickerizerImageSource = "https://en.gravatar.com/userimage/2382491/16301946ee142cbf006292f905fc9055.jpg";
			//this.StickerizerName = "Er";

			//this.StickerizeeImageSource = "https://en.gravatar.com/userimage/2382491/16301946ee142cbf006292f905fc9055.jpg";

			//this.Stickerizable = "For a monkey";

			this.StickerizerImageSource = er.GetImage().Url;
			this.StickerizerName = er.Name;
			this.StickerizeeImageSource = ee.GetImage().Url;
			this.StickerizeeName = ee.Name;
			this.Stickerizable = izable.For;

            //this.StickerizeeImageSource = ee.GetImage().Url;
            //this.StickerizeeName = ee.Name;
            
            //this.Stickerizable = izable.Name;
        }

        public int Month { get; set; }
        public int Date { get; set; }
        public int Year { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Millisecond { get; set; }
        public string StickerizerImageSource { get; set; }
        public string StickerizerName { get; set; }
        public string StickerizeeImageSource { get; set; }
        public string StickerizeeName { get; set; }
        public string StickerSource { get; set; }
        public string Stickerizable { get; set; }
    }
}
