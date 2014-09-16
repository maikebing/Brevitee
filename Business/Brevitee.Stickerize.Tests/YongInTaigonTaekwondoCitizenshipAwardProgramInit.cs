using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Testing;
using Brevitee.Data;
using Brevitee.Encryption;
using Brevitee.Stickerize.Business.Data;
using Brevitee.Stickerize.Business;
using Brevitee.Stickerize.Services;

namespace Brevitee.Stickerize.Data.Tests
{
    [Serializable]
    public class YongInTaigonTaekwondoCitizenshipAwardProgramInit: CommandLineTestInterface
    {
        public static void InitSchema()
        {
            SQLiteRegistrar.Register<Stickerizee>();
            Db.TryEnsureSchema<Stickerizee>();
        }

        [ConsoleAction("Add stickerizee")]
        public void AddStickerizee()
        {
            Stickerizer er = Stickerizer.Get(null);
            OutFormat("Stickerizer is: {0}", ConsoleColor.Cyan, er.Name);

            string eeName = Prompt("Enter the name of the stickerizee");

            StickerizeMe.Default.AddStickerizee(eeName, Gender.Unspecified);//, Gender.Unspecified, eeName);
        }

        [ConsoleAction("Delete Lists")]
        public void DeleteLists()
        {
            InitSchema();
            StickerizableListCollection allLists = StickerizableList.LoadAll();
            allLists.Delete();
        }

        [ConsoleAction("Initialize YongInTaigon Tae Kwon Do Citizenship Awards")]
        public void InitializeCitizenshipAwards()
        {
            InitSchema();
            StickerizeIntializer izer = new StickerizeIntializer();
            izer.Initialize();
            //StickerizableList yongInTaigonList = new StickerizableList();
            //yongInTaigonList.Name = "Yong In Taigon Tae Kwon Do Citizenship Awards";
            //yongInTaigonList.Public = true;
            //yongInTaigonList.Save();

            //SubSection section = yongInTaigonList.AddSubSection("Clean My Room: Responsibility");
            //section.AddStickerizable("Make my bed");
            //section.AddStickerizable("Put away personal belongings");
            //section.AddStickerizable("Vacuum my room");

            //section = yongInTaigonList.AddSubSection("Personal Hygiene: Self-Confidence");
            //section.AddStickerizable("Brush my teeth");
            //section.AddStickerizable("Take my bath or shower");
            //section.AddStickerizable("Put all dirty clothes in the laundry hamper");

            //section = yongInTaigonList.AddSubSection("Study/Practice: Character Development");
            //section.AddStickerizable("Complete Homework");
            //section.AddStickerizable("Get an 'A' on a test");
            //section.AddStickerizable("Practice Tae Kwon Do 10 minutes or more");
            //section.AddStickerizable("Read a book for a half hour or more");

            //section = yongInTaigonList.AddSubSection("Wonderful Family: Respect and Love");
            //section.AddStickerizable("Help clean up after meals and snacks");
            //section.AddStickerizable("Take out the trash");
            //section.AddStickerizable("Listen to and respect parents");
            //section.AddStickerizable("Share and cooperate with siblings");
            //section.AddStickerizable("Tell my parents I love them");

            Out(Db.For<Sticker>().ConnectionString);
        }
    }
}
