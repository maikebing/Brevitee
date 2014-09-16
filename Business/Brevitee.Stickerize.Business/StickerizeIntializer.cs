using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using Brevitee.Data;
using Brevitee.Server;
using Brevitee.Logging;
using Brevitee.Stickerize.Business;
using Brevitee.Stickerize.Business.Data;
using Brevitee.UserAccounts;

namespace Brevitee.Stickerize.Business
{
    public class StickerizeIntializer: Loggable, IInitialize
    {
		public const long SystemId = 99999;
        public StickerizeIntializer()
        {

        }

        public bool IsInitialized
        {
            get;
            private set;
        }

        public void Initialize()
        {
            SQLiteRegistrar.Register<Stickerizee>();
            Db.TryEnsureSchema<Stickerizee>();

            string listName = "Yong In Taigon Tae Kwon Do Citizenship Awards";
            StickerizableList yongInTaigonList = StickerizableList.OneWhere(c=>c.Name == listName);
            if (yongInTaigonList == null)
            {
                yongInTaigonList = new StickerizableList();
                yongInTaigonList.Name = listName;
                yongInTaigonList.Public = true;
				yongInTaigonList.CreatorId = SystemId;
                yongInTaigonList.Created = DateTime.UtcNow.Date;
                yongInTaigonList.Save();
            }

            SubSection section = yongInTaigonList.AddSubSection("Clean My Room: Responsibility");
            section.AddStickerizable("Make my bed");
            section.AddStickerizable("Put away personal belongings");
            section.AddStickerizable("Vacuum my room");

            section = yongInTaigonList.AddSubSection("Personal Hygiene: Self-Confidence");
            section.AddStickerizable("Brush my teeth");
            section.AddStickerizable("Take my bath or shower");
            section.AddStickerizable("Put all dirty clothes in the laundry hamper");

            section = yongInTaigonList.AddSubSection("Study/Practice: Character Development");
            section.AddStickerizable("Complete Homework");
            section.AddStickerizable("Get an 'A' on a test");
            section.AddStickerizable("Practice Tae Kwon Do 10 minutes or more");
            section.AddStickerizable("Read a book for a half hour or more");

            section = yongInTaigonList.AddSubSection("Wonderful Family: Respect and Love");
            section.AddStickerizable("Help clean up after meals and snacks");
            section.AddStickerizable("Take out the trash");
            section.AddStickerizable("Listen to and respect parents");
            section.AddStickerizable("Share and cooperate with siblings");
            section.AddStickerizable("Tell my parents I love them");
        }
    }
}
