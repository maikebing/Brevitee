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
using Brevitee.Encryption;
using Newtonsoft.Json;
using System.ComponentModel;
using Brevitee.Javascript;
using Brevitee.Data.Schema;

namespace Brevitee.Data.Schema.Tests
{
    [Serializable]
    class Program : CommandLineTestInterface
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
        }

        [UnitTest]
        public void SchemaShouldExistAfterSetSchema()
        {
            string schemaName = "XrefTest";
            SchemaManager sm = new SchemaManager();
            sm.SetSchema(schemaName);

            Expect.IsTrue(SchemaManager.SchemaExists(schemaName));
        }

        [UnitTest]
        public void GetTableShouldReturnTable()
        {
            string table = "TableName";
            SchemaManager sm = new SchemaManager();
            sm.SetSchema("test_schema".RandomString(4));

            sm.AddTable(table);

            Table t = sm.GetTable(table);
            Expect.IsNotNull(t);
            Expect.AreEqual(t.Name, table);
        }

        [UnitTest]
        public void SetAndGetXrefTableTest()
        {
            string table = "TableName".RandomString(4);
            SchemaManager sm = new SchemaManager();
            sm.SetSchema("test_schema".RandomString(4));

            sm.AddXref("Left", "Right");

            Table t = sm.GetXref("LeftRight");
            Expect.IsNotNull(t);
            Expect.AreEqual("LeftRight", t.Name);
        }

        [UnitTest]
        public void SetAndGetXrefTableShouldBeXrefTableType()
        {
            string table = "TableName".RandomString(4);
            SchemaManager sm = new SchemaManager();
            sm.SetSchema("test_schema".RandomString(4));

            sm.AddXref("Left", "Right");

            Table t = sm.GetXref("LeftRight");
            Expect.IsNotNull(t);
            Expect.AreEqual("LeftRight", t.Name);
            Expect.IsInstanceOfType<XrefTable>(t);
        }

        [UnitTest]
        public void SetAndGetXrefTableAsXrefTableType()
        {
            string table = "TableName".RandomString(4);
            SchemaManager sm = new SchemaManager();
            sm.SetSchema("test_schema".RandomString(4));

            sm.AddXref("Left", "Right");

            SchemaManager sm2 = new SchemaManager();
            sm2.SetSchema(sm.CurrentSchema.Name);

            Table t = sm2.GetXref("LeftRight");
            Expect.IsNotNull(t);
            XrefTable x = t as XrefTable;
            Expect.IsNotNull(x);
            Expect.AreEqual("Left", x.Left);
            Expect.AreEqual("Right", x.Right);
        }
        


        [UnitTest]
        public void ReadJson2FromResource()
        {
            string json = ResourceScripts.Get("json2.js");
            Out(json);
        }

        [UnitTest]
        public void SimpleSchemaJsToJson()
        {
            string json = File.ReadAllText(".\\json2.js");
            string database = File.ReadAllText(".\\database_example.js");
            string command = "var dbjson = JSON.stringify(database);";

            string script = json + database + command;
            JsContext context = script.RunJavascript();
            string result = context.GetValue<string>("dbjson");
            Out(result, ConsoleColor.Cyan);
        }

        [UnitTest]
        public void SimpleSchemaJsonToDynamicTestUsingNewtonSoft()
        {
            var schema = new
            {
                nameSpace = "Crawlers.Data",
                contextName = "Crawlers",
                tables = new object[]{ new { name= "Protocol", Value= "String" },
                        new { name= "Domain", fks= new string[]{}, Value= "String" },
                        new { name= "Port", Value= "String" },
                        new { name= "Protocol", Value= "String" },
                        new { name= "Path", Value= "String" },
                        new { name= "QueryString", Value= "String" },
                        new { name= "Url", fks= new string[]{"Protocol", "Domain", "Port", "Path", "QueryString" } },
                        new { name= "Tag", Value= "String" },
                        new { name= "Image", fks= new string[]{"Url"} },
                        new { name= "TagUrl", fks= new string[] {"Tag", "Url"} },
                        new { name= "TagImage", fks= new []{"Tag", "Image"}
                        }
                    }
            };

            string simpleSchemaJson = JsonConvert.SerializeObject(schema, Formatting.Indented);
            Out(simpleSchemaJson, ConsoleColor.Cyan);

            dynamic rehydrated = JsonConvert.DeserializeObject<dynamic>(simpleSchemaJson);
            

            Expect.IsNotNull(rehydrated["nameSpace"]);
            Expect.AreEqual("Crawlers.Data", (string)rehydrated["nameSpace"]);
            Expect.AreEqual("Crawlers", (string)rehydrated["contextName"]);

            foreach (dynamic t in rehydrated["tables"])
            {
                Out((string)t["name"], ConsoleColor.Green);
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(t);
                foreach (PropertyDescriptor pd in props)
                {
                    if (pd.Name.Equals("fks"))
                    {
                        foreach (dynamic fk in t["fks"])
                        {
                            OutFormat("\t{0}", ConsoleColor.Cyan, (string)fk);
                        }
                    }
                    else if(!pd.Name.Equals("name"))
                    {
                        OutFormat("\t{0}", ConsoleColor.Blue, (string)pd.GetValue(t));
                    }
                }                
            }
        }

      
    }

}
