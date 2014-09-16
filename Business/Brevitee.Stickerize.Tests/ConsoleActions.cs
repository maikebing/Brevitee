using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Testing;
using Brevitee.Data;
using Brevitee.Encryption;
using Brevitee.Stickerize.Business.Data;
using Brevitee.Stickerize.Business;
using Brevitee.UserAccounts.Data;
using Brevitee.UserAccounts;
//using DotNetOpenAuth;
//using DotNetOpenAuth.Configuration;
//using DotNetOpenAuth.Loggers;
//using DotNetOpenAuth.Messaging;
using System.IO;
using System.Web.WebPages.Deployment;
using System.Reflection;

namespace Brevitee.Stickerize.Data.Tests
{
    [Serializable]
    public class ConsoleActions: CommandLineTestInterface
    {
        [ConsoleAction()]
        public void ShowSchemaInitializerJson()
        {
            SchemaInitializer initializer = new SchemaInitializer(typeof(StickerizeContext), typeof(SQLiteRegistrarCaller));
            string fileName = ".\\stickerizeSchemaInitializer.json";
            initializer.ToJson(true).SafeWriteToFile(fileName, true);
            "notepad {0}"._Format(fileName).Run();
        }

        [ConsoleAction()]
        public void ShowAppInitializerAssemblyQualifiedName()
        {
            string fileName = ".\\StickerizeInitializer.txt";
            
            typeof(StickerizeIntializer).AssemblyQualifiedName.SafeWriteToFile(fileName, true);
            "notepad {0}"._Format(fileName).Run();
        }

        [ConsoleAction()]
        public void LoadFileAndGetType()
        {
            Assembly assembly = Assembly.LoadFrom(@"C:\BreviteeContentRoot\apps\stickerize.me\services\Brevitee.Stickerize.Services.dll");
            Type type = assembly.GetType(@"Brevitee.Stickerize.Services.Data.StickerizeContext");
            Expect.IsNotNull(type);
            OutLine(type.AssemblyQualifiedName);
        }

        [ConsoleAction()]
        public void LoadFileAndListTypes()
        {
            Assembly assembly = Assembly.LoadFrom(@"C:\BreviteeContentRoot\apps\stickerize.me\services\Brevitee.Stickerize.Services.dll");
            assembly.GetTypes().Each(type =>
            {
                OutLine(type.AssemblyQualifiedName);
            });
        }

        [ConsoleAction("List Stickerizers")]
        public StickerizerCollection ListStickerizers()
        {
            InitSchemas();
            StickerizerCollection all = Stickerizer.Where(c => c.Name != null);
            all.Each((er, i) =>
            {
                OutFormat("{0}. {1}", ConsoleColor.Cyan, i + 1, er.Name);
            });
            return all;
        }

        [ConsoleAction("Add stickerizer")]
        public void AddStickerizer()
        {
            InitSchemas();
            string userName = Prompt("Enter the name of the stickerizer to create");
            Stickerizer.Create(userName);
        }

        [ConsoleAction("List Stickerizees")]
        public void ListStickerizees()
        {
            InitSchemas();
            StickerizeeCollection ees = Stickerizee.LoadAll();
            ees.Each(e =>
            {
                Out(e.Name);
            });
        }

        [ConsoleAction("Search Stickerizee")]
        public void SearchStickerizees()
        {
            InitSchemas();
            string startsWith = Prompt("Starting with");
            StickerizeeCollection ees = Stickerizee.Where(c => c.Name.StartsWith(startsWith));
            ees.Each(e =>
            {
                Out(e.Name);
            });
        }

        [ConsoleAction("List StickerizableLists")]
        public void ListStickerizableLists()
        {
            InitSchemas();
            string search = Prompt("Please enter the name of the list, blank to get top 1000");
            StickerizableListCollection listCollection = new StickerizableListCollection();
            if (!string.IsNullOrEmpty(search))
            {
                listCollection = StickerizableList.Top(1000, (c) => c.Name.Contains(search) || c.Name.StartsWith(search));
            }
            else
            {
                listCollection = StickerizableList.Top(1000, (c) => c.Id != null);
            }

            for (int i = 0; i < listCollection.Count; i++)
            {
                StickerizableList list = listCollection[i];
                OutFormat("{0}.{1}\r\n", ConsoleColor.Cyan, i + 1, list.Name);
            }
        }

        [ConsoleAction("Add StickerizableList")]
        public void AddStickerizableList()
        {
            InitSchemas();
            string name = Prompt("Enter the name of the list to add");
            if (string.IsNullOrEmpty(name))
            {
                OutFormat("Invalid name: {0}", ConsoleColor.Magenta, name);
            }
            else
            {
                StickerizableList.GetOrCreate(name);
            }
        }

        [ConsoleAction("Delete List")]
        public void DeleteList()
        {
            InitSchemas();
            string name = Prompt("Enter the name of the list to delete");
            if (string.IsNullOrEmpty(name))
            {
                OutFormat("Invalid name: {0}", ConsoleColor.Magenta, name);
            }
            else
            {
                StickerizableList list = StickerizableList.OneWhere(c => c.Name == name);
                if (list != null)
                {
                    list.Delete();
                }
                else
                {
                    OutFormat("List not found", ConsoleColor.Yellow);
                }
            }
        }


        [ConsoleAction("Export database for migration")]
        public void ExportDatabase()
        {
            //	get all stickerizers and save to Stickerizers/<Uuid>
			//	for each stickerizer 
			//		create a folder <Stickerizer.Uuid>_Stickerizees
			//			for each stickerizee for current stickerizer save them into <Stickerizer.Uuid>_Stickerizees/Stickerizee.Uuid
			//	for each stickerization create a folder Stickerizations
			//		Filename: Stickerization.Uuid => save a representation of them using Uuid instead of Id of Stickerizer and Stickerizee Sticker
			//	create a folder StickerizableLists
			//	for each StickerizableList 
			//		save file Uuid => CreatorId: Stickerizer.Uuid, ...the rest
        }

        public static void InitSchemas()
        {
            SQLiteRegistrar.Register<Stickerizee>();
            SQLiteRegistrar.Register<User>();

            Db.TryEnsureSchema<Stickerizee>();
            Db.TryEnsureSchema<User>();
        }
    }
}
