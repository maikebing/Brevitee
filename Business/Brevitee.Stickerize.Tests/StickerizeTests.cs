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
using Brevitee.UserAccounts.Data;
using Brevitee.UserAccounts;
using Brevitee.Stickerize.Business;
using Brevitee.Stickerize.Services;
using Brevitee.UserAccounts.Tests;
using Brevitee.ServiceProxy;

namespace Brevitee.Stickerize.Data.Tests
{
    [Serializable]
    public class Program : CommandLineTestInterface
    {

        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }

        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true then only the name is necessary.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion

            AddValidArgument("pass", "Run a passing test for testing");
            AddValidArgument("fail", "Run a failing test for testing");

            DefaultMethod = typeof(Program).GetMethod("Start");
        }

        public static void Start()
        {
            string assemblyName = Assembly.GetExecutingAssembly().FullName;
            if (Arguments.Contains("t"))
            {
                RunAllTests(Assembly.GetExecutingAssembly());
            }
            else
            {
                Interactive();
            }
        }

        [UnitTest]
        public void LookAtInitSchemaAsItRuns()
        {
            InitSchemas();
        }

        [UnitTest("InitSchemas", true)]
        public void ShouldOnlyHaveOneStickerizationPerDayPerStickerizable()
        {
            DateTime now = DateTime.UtcNow;
            Stickerizee ee = Stickerizee.Create("Test Stickerizee");
            Stickerization zation = Stickerization.Create(now, Sticker.Default.Id.Value, Stickerizable.Default.Id.Value, ee.Id.Value);
            Stickerization check = Stickerization.Create(now, Sticker.Default.Id.Value, Stickerizable.Default.Id.Value, ee.Id.Value);
            Expect.AreEqual(check.Id, zation.Id);
        }

        [UnitTest("InitSchemas", true)]
        public void IsUndoneShouldBehaveCorrectlyAsBooleanInSqlite()
        {
            CreateResult<object> result = StickerizeMe.Default.Stickerize(DateTime.Now, Stickerizable.Default.Id.Value, Stickerizee.Default.Id.Value);

            Type resultType = result.Result.GetType();
            PropertyInfo prop = resultType.GetProperty("IsUndone");
            object val = prop.GetValue(result.Result);
            Out(val.GetType().Name);
        }

        [UnitTest("InitSchemas", true)]
        public void DaoCollectionAddNewShouldIncrementCount()
        {
            StickerizableList list = new StickerizableList();
            list.Name = "Test_".RandomLetters(4);
            list.Public = true;
            list.Created = DateTime.UtcNow.Date;
            list.Save();
            SubSection section = list.SubSectionsByStickerizableListId.AddNew();
            Expect.IsTrue(list.SubSectionsByStickerizableListId.Count == 1);
        }
                

        [UnitTest("InitSchemas", true)]
        public void SaveParentShouldSaveChildren()
        {
            StickerizableList list = new StickerizableList();
            list.Name = "Test_".RandomLetters(4);
            list.Public = true;
            list.Created = DateTime.UtcNow.Date;
            list.Save();
            SubSection section = list.SubSectionsByStickerizableListId.AddNew();
            section.Name = "monkey";
            section.Created = DateTime.UtcNow.Date;
            list.Save();
            Expect.IsTrue(list.SubSectionsByStickerizableListId.Count > 0);
            Expect.IsTrue(list.SubSectionsByStickerizableListId.Contains(section));
        }

        [UnitTest("InitSchemas", true)]
        public void ShouldBeAbleToAddSubSectionToList()
        {
            StickerizableList list = new StickerizableList();        
            list.Public = true;
            list.Name = "Test_".RandomLetters(4);
            list.Created = DateTime.UtcNow.Date;
            list.Save();
            Expect.IsTrue(list.SubSectionsByStickerizableListId.Count == 0);
            SubSection section = list.AddSubSection("Monkey");

            Expect.IsTrue(list.SubSectionsByStickerizableListId.Count == 1);
        }

		[UnitTest("InitSchemas", "", "")]
		public void ShouldBeAbleToAddSubSectionToListThroughTopLevelProvider()
		{
			string listName = "Test_List_".RandomLetters(5);
			string stickerizableName = "The Name ".RandomLetters(5);
			string stickerizableFor = "For ".RandomLetters(5);

			// object under test
			long? id = StickerizeMe.Default.AddList(listName).Id;
			//

			StickerizableList list = StickerizableList.OneWhere(c => c.Id == id);
			SubSection subSection = list.SubSectionsByStickerizableListId.AddNew();
			subSection.Save();
			
			Stickerizable able = subSection.AddStickerizable(stickerizableName, Stickerizer.System.Id.Value, stickerizableFor);
			
			Expect.AreEqual(stickerizableName, able.Name);
			Expect.AreEqual(stickerizableFor, able.For);

			StickerizableList reRetrieved = StickerizableList.OneWhere(c => c.Name == listName);
			Expect.AreEqual(list.Id, reRetrieved.Id);
			Expect.AreEqual(reRetrieved.Name, listName);
			Expect.AreEqual(1, list.SubSectionsByStickerizableListId.Count);
		}


        [UnitTest("InitSchemas", true)]
        public void StickerizerShouldGetCorrectUserIfLoggedIn()
        {
            string userName = MethodBase.GetCurrentMethod().Name;
            IHttpContext context;
            LoginResponse result;

            UserAccountTestsProgram.SignUpAndLogin(userName, out context, out result);

            Stickerizer stickerizer = Stickerizer.Get(context);
            Expect.AreEqual(userName, stickerizer.UserName, "Usernames didn't match");
            Expect.IsFalse(stickerizer.UserName.Equals(Stickerizer.Anonymous.UserName), "Stickerizer was anonymous");
        }

		[UnitTest("InitSchemas", true)]
		public void StickerizeMeGetStickerizableListsShouldGetPublicAndOwnLists()
		{
			StickerizableListCollection all = StickerizableList.LoadAll();
			all.Delete();

			string userName = MethodBase.GetCurrentMethod().Name;
			IHttpContext context;
			LoginResponse result;
			UserAccountTestsProgram.SignUpAndLogin(userName, out context, out result);
			StickerizeMe.Default.HttpContext = context;
			Stickerizer izer = Stickerizer.Get(context);

			StickerizableList someoneElsesPrivateList = CreateList("Private List Of Someone Else", 999);
			StickerizableList publicList = CreateList("Public List", 999);
			publicList.Public = true;
			publicList.Save();
			StickerizableList myPrivateList = CreateList("My Private List", izer.Id.Value);

			StickerizableListInfo[] listsToCheck = StickerizeMe.Default.GetStickerizableLists();
			List<long> ids = listsToCheck.Select(l => l.Id).ToList();
			Expect.IsTrue(ids.Contains(publicList.Id.Value), "Should have returned the public list");
			Expect.IsTrue(ids.Contains(myPrivateList.Id.Value), "Should have return my private list");
			Expect.IsFalse(ids.Contains(someoneElsesPrivateList.Id.Value), "Should NOT have returned someone else's private list");
		}

		private StickerizableList CreateList(string name, long creatorId)
		{
			StickerizableList list = new StickerizableList();
			list.Name = name;
			list.Created = DateTime.UtcNow;
			list.CreatorId = creatorId;
			list.Public = false;
			list.Save();
			return list;
		}

		[UnitTest("InitSchemas", true)]
		public void CreateListShouldSetCorrectCreator()
		{	
			string userName = MethodBase.GetCurrentMethod().Name;
			IHttpContext context;
			LoginResponse result;
			UserAccountTestsProgram.SignUpAndLogin(userName, out context, out result);
			StickerizeMe.Default.HttpContext = context;

			string testListName = "Test_List_Name_".RandomLetters(5);
			StickerizableListInfo list = StickerizeMe.Default.AddList(testListName);
			StickerizableList check = StickerizableList.OneWhere(c => c.Id == list.Id);
			
			Stickerizer stickerizer = Stickerizer.Get(context);
			Expect.AreEqual(stickerizer.Id, check.CreatorId);
		}

        [UnitTest("InitSchemas", true)]
        public void ShouldBeAbleToAddStickerizableToSubSection()
        {
            StickerizableList list = new StickerizableList();
            list.Public = true;
            list.Name = "Test_".RandomLetters(4);
            list.Created = DateTime.UtcNow.Date;
            list.Save();
            SubSection section = list.AddSubSection("monkey");
            Stickerizable stickerizable = section.AddStickerizable("Eating candy");
            Expect.IsTrue(section.Stickerizables.Count > 0);
            Expect.IsTrue(section.Stickerizables.Contains(stickerizable));
        }
		
        [UnitTest("InitSchemas", true)]
        public void AddSubSectionShouldntDuplicate()
        {
            string subSectionName = "Sub Test_".RandomLetters(4);
            string listName = "Test_".RandomLetters(4);
            StickerizableList list = StickerizableList.GetOrCreate(listName);
            Expect.IsTrue(list.SubSectionsByStickerizableListId.Count == 0);
            list.AddSubSection(subSectionName);
            Expect.AreEqual(1, list.SubSectionsByStickerizableListId.Count);
            list.AddSubSection(subSectionName);
            Expect.AreEqual(1, list.SubSectionsByStickerizableListId.Count);
        }

        [UnitTest("InitSchemas", true)]
        public void AddStickerizableToSectionShouldntDuplicate()
        {
            string subSectionName = "Sub Test_".RandomLetters(4);
            string listName = "Test_".RandomLetters(4);
            string izableName = "Test_".RandomLetters(4);
            StickerizableList list = StickerizableList.GetOrCreate(listName);
            SubSection section = list.AddSubSection(subSectionName);
            Expect.AreEqual(0, section.Stickerizables.Count);
            section.AddStickerizable(izableName);
            Expect.AreEqual(1, section.Stickerizables.Count);
            section.AddStickerizable(izableName);
            Expect.AreEqual(1, section.Stickerizables.Count);
        }

        [UnitTest]
        public void SelectAllTest()
        {
            SqlStringBuilder sql = new SqlStringBuilder();
            sql.Select<StickerizableList>();
            Out(sql.ToString(), ConsoleColor.Yellow);
        }

        [UnitTest]
        public void CreateShouldNotDuplicateStickerizee()
        {
            InitSchemas();

            string name = "Test_".RandomLetters(4);
            string desc = "Description ".RandomLetters(4);
            Stickerizee result = Stickerizee.Create(name, Gender.Unspecified, desc);
            Expect.IsNotNull(result);
            result = Stickerizee.Create( result.Name, Gender.Male, desc);
            Expect.IsNotNull(result);

            result.Delete();
        }
        
        [UnitTest]
        public void CreateShouldNotDuplicateStickerizable()
        {
            InitSchemas();

            string name = "Test_".RandomLetters(4);
            string desc = "Description ".RandomLetters(4);
            CreateResult<Stickerizable> result = Stickerizable.Create(name, desc);
            Expect.IsNotNull(result);
            Expect.IsNotNull(result.Result);
            Expect.IsTrue(result.Success);
            Expect.AreEqual(result.Status, CreateStatus.Success);
            result = Stickerizable.Create(result.Result.Name, desc);
            Expect.IsNotNull(result);
            Expect.IsNotNull(result.Result);
            Expect.IsTrue(result.Success);
            Expect.AreEqual(result.Status, CreateStatus.Duplicate);

            result.Result.Delete();
        }

        [UnitTest]
        public void GetShouldNotDuplicateList()
        {
            InitSchemas();

            string listName = "Test_list_".RandomLetters(4);
            StickerizableList list = StickerizableList.Create(listName);
            Expect.IsNotNull(list);
            StickerizableList check = StickerizableList.GetByName(listName);
            Expect.IsNotNull(check);
            Expect.AreEqual(list.Id, check.Id);
            list.Delete();
        }

        [UnitTest]
        public void AddStickerizableToListTest()
        {
            InitSchemas();

            string listName = "Test_List_".RandomLetters(4);
            StickerizableList list = StickerizableList.Create(listName);
            Expect.IsNotNull(list);

            CreateResult<Stickerizable> result = Stickerizable.Create("making bed_".RandomLetters(4));
            Stickerizable izable = result.Result;
            StickerizableListStickerizable xref = StickerizableListStickerizable
                .OneWhere(c => c.StickerizableId == izable.Id && c.StickerizableListId == list.Id);
            Expect.IsNull(xref);

            list.AddStickerizable(izable);
            xref = StickerizableListStickerizable
                .OneWhere(c => c.StickerizableId == izable.Id && c.StickerizableListId == list.Id);

            Expect.IsNotNull(xref);
            xref.Delete();
        }

        public static void InitSchemas()
        {            
            SQLiteRegistrar.Register<Stickerizee>();
            SQLiteRegistrar.Register<User>();

            Db.TryEnsureSchema<Stickerizee>();
            Db.TryEnsureSchema<User>();
        }


        [UnitTest("InitSchemas", "", "")]
        public void CreateStickerizeeShouldCreateForCurrentStickerizer()
        {
            Sticker sticker = Sticker.Default;
            Stickerizer er = Stickerizer.Get(null);
            string name = "test ee ({0})"._Format("".RandomLetters(4));
            StickerizeMe.Default.AddStickerizee(name, Gender.Unspecified);//, Gender.Male, "display name for {0}"._Format(name));
            Stickerizee created = Stickerizee.OneWhere(c => c.Name == name);

            er.Stickerizees.Reload();
            Expect.IsTrue(er.Stickerizees.Contains(created));
        }

		[UnitTest("InitSchemas", "", "")]
		public void ShouldBeAbleToCreateStickerizableWithoutException()
		{
			string listName = "TestList_".RandomLetters(5);
			long? id = StickerizeMe.Default.AddList(listName).Id;
			StickerizableList created = StickerizableList.OneWhere(c => c.Id == id);
			Expect.AreEqual(listName, created.Name);
			OutFormat("Created list {0}", listName);
		}

	
        [UnitTest("InitSchemas", "", "")]
        public void CurrentUserShouldBeAnonymous()
        {
            User current = User.GetCurrent(null);
            Expect.IsTrue(current == User.Anonymous);
        }

        [UnitTest("InitSchemas", "", "")]
        public void CurrentStickerizerShouldBeAnonymous()
        {
            Stickerizer current = Stickerizer.Get(null);
            Expect.IsTrue(current.Equals(Stickerizer.Anonymous));
        }

        [UnitTest("InitSchemas", "", "")]
        public void StickerizerCreateShouldNotCreateDuplicates()
        {
            string userName = "".RandomLetters(6);
            Stickerizer izer = Stickerizer.OneWhere(c => c.Name == userName);

            Expect.IsNull(izer);

            izer = Stickerizer.Create(userName);
            Expect.IsNotNull(izer);
            Expect.AreEqual(userName, izer.Name);

            izer = Stickerizer.Create(userName);
            Expect.IsNotNull(izer);
            Expect.AreEqual(userName, izer.Name);

            StickerizerCollection ers = Stickerizer.Where(c => c.Name == userName);
            Expect.AreEqual(1, ers.Count, "Multiple records were created and that shouldn't be");
        }

        [UnitTest("InitSchemas", "","")]
        public void ShouldBeAbleToAddSameStickerizee()
        {
            Stickerizer mom;
            Stickerizer dad;
            Stickerizee johnny;
            CreateFamily(out mom, out dad, out johnny);
            Expect.IsTrue(dad.Stickerizees.Contains(johnny));
            Expect.IsTrue(mom.Stickerizees.Contains(johnny));

            StickerizeeCollection check = Stickerizee.Where(c => c.Name == johnny.Name && c.Gender == johnny.Gender && c.DisplayName == johnny.DisplayName);
            Expect.IsTrue(check.Count > 0);

            OutFormat("there are {0} Johnny entries", check.Count);

            bool? found = false;
            check.Each(ee =>
            {
                if (ee.Id == johnny.Id)
                {
                    found = true;
                }
            });

            Expect.IsTrue(found.Value);
        }

        private static void CreateFamily(out Stickerizer mom, out Stickerizer dad, out Stickerizee johnny)
        {
            Stickerizee ignore;
            CreateFamily(out mom, out dad, out johnny, out ignore);
        }
        private static void CreateFamily(out Stickerizer mom, out Stickerizer dad, out Stickerizee johnny, out Stickerizee jane)
        {
            StickerizerCollection allStickerizers = Stickerizer.LoadAll();
            allStickerizers.Delete();
            StickerizeeCollection allStickerizees = Stickerizee.LoadAll();
            allStickerizees.Delete();

            mom = Stickerizer.Create("Mom");
            dad = Stickerizer.Create("Dad");
            johnny = mom.AddStickerizee("John Quincy Adams", Gender.Male, "Johnny");
            jane = mom.AddStickerizee("Jane Quincy Adams", Gender.Female, "Jane");
            dad.AddStickerizee(johnny);
            dad.AddStickerizee(jane);
        }

        [UnitTest()]
        public void WriteSqlClientQualifiedName()
        {
            Out(typeof(SqlClientFactory).AssemblyQualifiedName);
        }

        [UnitTest("InitSchemas", "", "")]
        public void ShouldBeAbleToRetrieveStickerizations()
        {
            Stickerizer mom;
            Stickerizer dad;
            Stickerizee johnny;
            Stickerizee jane;
            CreateFamily(out mom, out dad, out johnny, out jane);
            mom.Stickerize(johnny);
            dad.Stickerize(jane);

            RetrieveResult<object> result = StickerizeMe.Default.GetStickerizations(DateTime.UtcNow, johnny.Id.Value);
            Expect.AreEqual(RetrieveStatus.Success, result.Status);
            Expect.IsTrue(result.Result.Length > 0);
        }


        [UnitTest("InitSchemas", "", "")]
        public void ShouldBeAbleToStickerizeThroughSystem()
        {
            Stickerizer mom;
            Stickerizer dad;
            Stickerizee johnny;
            Stickerizee jane;
            CreateFamily(out mom, out dad, out johnny, out jane);
            DateTime date = DateTime.UtcNow;
            CreateResult<dynamic> stickerization = StickerizeMe.Default.Stickerize(date, Stickerizable.Default.Id.Value, johnny.Id.Value);
            RetrieveResult<dynamic> result = StickerizeMe.Default.GetStickerizations(date, johnny.Id.Value);
            Expect.IsTrue(result.Result.Length > 0);
            long?[] ids = new long?[result.Result.Length];
            bool? foundId = false;
            result.Result.Each((o, i) =>
            {
                ids[i] = o.Id;
                long? currentId = o.Id;
                if (stickerization.Result.Id == currentId)
                {
                    foundId = true;
                }
            });

            Expect.IsTrue(foundId.Value);//result.Result.Contains(stickerization.Result));
        }

        [UnitTest()]
        public void ShouldBeABleToSearchStartsWithSQLite()
        {
            InitSchemas();

            Stickerizee test = new Stickerizee();
            test.Name = "Banana";
            test.Gender = Gender.Male.ToString();
            test.Created = DateTime.UtcNow.Date;
            test.Save();

            StickerizeeCollection check = Stickerizee.Where(c => c.Name.StartsWith("B"));
            Expect.IsTrue(check.Count > 0);
            check.Each(s =>
            {
                Out(s.Name);
            });

            test.Delete();
        }

        [UnitTest]
        public void ShouldBeAbleToSearchDoesntStartWithInSQLite()
        {
            InitSchemas();
            StickerizeeCollection all = Stickerizee.LoadAll();
            all.Delete();

            Stickerizee test = new Stickerizee();
            test.Name = "Banana";
            test.Gender = Gender.Male.ToString();
            test.Created = DateTime.UtcNow.Date;
            test.Save();

            Stickerizee test2 = new Stickerizee();
            test2.Name = "Not Banana";
            test2.Gender = Gender.Female.ToString();
            test2.Created = DateTime.UtcNow.Date;
            test2.Save();

            StickerizeeCollection check = Stickerizee.Where(c => c.Name.DoesntStartWith("B"));
            Expect.IsTrue(check.Count > 0);
            bool? found = false;
            check.Each(s =>
            {
                if (s.Id == test2.Id)
                {
                    found = true;
                }
            });
            Expect.IsTrue(found.Value);
        }

        

    }

}
