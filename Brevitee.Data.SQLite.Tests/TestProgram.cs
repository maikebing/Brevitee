using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Naizari.Extensions;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
//using Naizari.Testing;
using System.IO;
//using Naizari.Data;
using Brevitee.CommandLine;
using Brevitee.Data;
using Brevitee;
using Brevitee.Testing;
using SampleData;
using System.Data.SQLite;

namespace Brevitee.Data.Tests
{
    public class TestProgram : CommandLineTestInterface
    {
        // Add optional code here to be run before initialization/argument parsing.
        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
        }

        /*
          * Methods addorned with the ConsoleAction attribute can be run
          * interactively from the command line while methods addorned with
          * the TestMethod attribute will be run automatically when the
          * compiled executable is run.  To run ConsoleAction methods use
          * the command line argument /i.
          * 
          * All methods addorned with ConsoleAction and TestMethod attributes 
          * must be static for the purposes of extending CommandLineTestInterface
          * or an exception will be thrown.
          * 
          */

        // To run ConsoleAction methods use the command line argument /i.        
        [ConsoleAction("This is a main menu option")]
        public static void ExampleMainMenuOption(string parameter)
        {
            Out(parameter, ConsoleColor.Green);
        }

        [ConsoleAction("Output SQLite assembly name")]
        public static void OuputSQLite()
        {
            SQLiteRegistrar.OutputAssemblyQualifiedName();
        }

        private static void SetSQLiteDatabase(string connectionName)
        {
            DeleteTestDb();
            Dao.ProxyConnection(typeof(Item), connectionName);
            SQLiteRegistrar.Register(connectionName);

            SQLiteSqlStringBuilder sql = new SQLiteSqlStringBuilder();
            sql.WriteSchemaScript<Item>();

            sql.Execute(_.Db.For<Item>());
        }

        private static void SetMSSqlDatabase(string connectionName)
        {
            DeleteTestDb();
            Dao.ProxyConnection(typeof(Item), connectionName);
            SqlClientRegistrar.Register(connectionName);

            SqlClientSqlStringBuilder sql = new SqlClientSqlStringBuilder();
            sql.WriteSchemaScript<Item>();

            sql.Execute(_.Db.For<Item>());
        }

        private static void DeleteTestDb()
        {
            try
            {
                string dbFile = "c:\\Storage\\test.sqlite";
                if (File.Exists(dbFile))
                {
                    File.Delete(dbFile);
                }
            }
            catch (Exception ex)
            {
                OutFormat("Error occurred attempting to delete test.sqlite: {0}", ConsoleColor.Red, ex.Message);
            }
        }

        [ConsoleAction("init db")]
        public static void InitDb()
        {
            SetMSSqlDatabase("SQLTest");
        }

        [UnitTest]
        public static void ShouldBeAbleToUndo()
        {
            SetSQLiteDatabase("SQLiteTest");
            Item test = new Item();
            test.Name = "monkey";
            test.Commit();

            Expect.IsNotNull(test.Id);

            Item check = Item.OneWhere(c => c.Id == test.Id);

            Expect.AreEqual(test.Name, check.Name);
            Expect.AreEqual("monkey", check.Name);

            check.Name = "Gorilla";
            check.Commit();
            Expect.AreEqual("Gorilla", check.Name);
            check.Undo();
            Expect.AreEqual("monkey", check.Name);

            Item checkAgain = Item.OneWhere(c => c.Id == test.Id);
            Expect.AreEqual("monkey", checkAgain.Name);            
        }

        [UnitTest]
        public static void ShouldBeAbleToCommitAfterDelete()
        {
            SetSQLiteDatabase("SQLiteTest");
            Item test = new Item();
            test.Name = "".RandomString(8);
            test.Commit();
            Expect.IsNotNull(test.Id);

            test.Delete();
            Expect.IsNotNull(test.Id);
            Item check = Item.OneWhere(c => c.Id == test.Id);
            Expect.IsNull(check);

            test.IsNew = true;
            test.Commit();

            check = Item.OneWhere(c => c.Id == test.Id);
            Expect.IsNotNull(check);
            Expect.AreEqual(test.Name, check.Name);
        }

        [UnitTest]
        public static void ShouldBeAbleToUndelete()
        {
            SetSQLiteDatabase("SQLiteTest");
            Item test = new Item();
            test.Name = "".RandomString(8);
            test.Commit();
            Expect.IsNotNull(test.Id);
            OutFormat("inserted item id {0}", test.Id);

            test.Delete();
            Item check = Item.OneWhere(c => c.Id == test.Id);
            Expect.IsNull(check);

            test.Undelete();

            check = Item.OneWhere(c => c.Id == test.Id);
            Expect.IsNotNull(check);
            Expect.AreEqual(test.Name, check.Name);
            OutFormat("item id after undelete {0}", test.Id);
        }

        [UnitTest]
        public static void RegistrarShouldSetCorrectParameterBuilderType()
        {
            string cn = "BAMvc4";
            SQLiteRegistrar.Register(cn);
            Database db = _.Db.For(cn);
            IParameterBuilder pb = db.ServiceProvider.Get<IParameterBuilder>();
            QuerySet qs = db.ServiceProvider.Get<QuerySet>();
            Expect.IsTrue(pb is SQLiteParameterBuilder);
            Expect.IsTrue(qs is SQLiteQuerySet);
        }

        [UnitTest]
        public static void NewItemSQL()
        {
            SetMSSqlDatabase("SQLTest");
            Out("starting");
            Item test = new Item();
            test.Name = "monkey";
            test.Commit();
            Out(test.Id.ToString());
        }

        [UnitTest]
        public static void NewItemSQLite()
        {
            SetSQLiteDatabase("SQLiteTest");
            Out("starting");
            Item test = new Item();
            test.Name = "monkey";
            test.Commit();
            Out(test.Id.ToString());
        }

        [UnitTest]
        public static void TransactionShouldRollback()
        {
            SetSQLiteDatabase("SQLiteTest");
            bool? rolledBack = false;
            Item test = new Item();
            using (DaoTransaction tx = new DaoTransaction(_.Db.For<Item>()))
            {
                tx.RolledBack += (o, a) =>
                {
                    rolledBack = true;
                };
            
                test.Name = "".RandomString(8);

                test.Commit(tx);

                Expect.IsNotNull(test.Id);

                Item check = Item.OneWhere(c => c.Id == test.Id);

                Expect.IsNotNull(check);
                Expect.AreEqual(test.Name, check.Name);
            }

            Item check2 = Item.OneWhere(c => c.Id == test.Id);
            Expect.IsNull(check2);
        }

        [UnitTest]
        public static void TransactionShouldSetDb()
        {
            SetSQLiteDatabase("SQLiteTest");
            Database db = _.Db[typeof(Item)];
            using (DaoTransaction tx = _.BeginTransaction<Item>())
            {
                Expect.IsFalse(_.Db[typeof(Item)] == db);
            }

            Expect.IsTrue(_.Db[typeof(Item)] == db);
        }

        [UnitTest]
        public static void TransactionShouldSetDbFromDb()
        {
            SetSQLiteDatabase("SQLiteTest");
            Database db = _.Db.For<Item>();
            using (DaoTransaction tx = _.Db.For<Item>().BeginTransaction())
            {
                Expect.IsFalse(_.Db[typeof(Item)] == db);
            }

            Expect.IsTrue(_.Db[typeof(Item)] == db);
        }

        [UnitTest]
        public static void TransactionShouldRollbackFromContainer()
        {
            SetSQLiteDatabase("SQLiteTest");
            using (DaoTransaction tx = _.BeginTransaction<Item>())
            {
                Item item = new Item();
                item.Name = "Name_".RandomString(8);
                item.Commit();

                Expect.IsNotNull(item.Id);
                Item check = Item.OneWhere(c => c.Id == item.Id);
                Expect.IsNotNull(check);
                Expect.AreEqual(item.Name, check.Name);
            }

        }
        #region do not modify
        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }


        #endregion
    }
}
