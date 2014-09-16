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
//using Naizari.Helpers;
using Brevitee.Data;
using Brevitee;
using Brevitee.Testing;
using System.Configuration;
using MySql.Data.MySqlClient;
using Brevitee.Incubation;
using System.Web.Razor.Generator;
using System.Data.OleDb;
using System.Web.Razor;
using Brevitee.CommandLine;
using Brevitee.Data.Schema;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Brevitee.Data.Tests
{
    public class DaoGeneratorTests : CommandLineTestInterface
    {
        private static SchemaDefinition GetTestSchema()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition schema = mgr.SetSchema("test");
            mgr.AddTable("monkey");
            return schema;

        }

        class TestRazorEngineDaoGenerator : DaoGenerator
        {
            public TestRazorEngineDaoGenerator():base("Test") { }
            public bool Called { get; set; }
            protected override void WriteClassToStream(string result, Stream s)
            {
                Called = true;
                //base.WriteResult(result, s);
            }

            public void TestWriteResult(string result, Stream s)
            {
                base.WriteClassToStream(result, s);
            }
        }
        
        [UnitTest]
        public static void WriteResultShouldWriteToSpecifiedStream()
        {
            MemoryStream stream = new MemoryStream();
            TestRazorEngineDaoGenerator gen = new TestRazorEngineDaoGenerator();
            string randomString = "".RandomString(8);
            gen.TestWriteResult(randomString, stream);
            
            Expect.AreEqual(randomString, Encoding.UTF8.GetString(stream.GetBuffer().Take(8).ToArray()));
        }

        [UnitTest]
        public static void GenerateShouldUseSpecifiedTargetResolver()
        {
            string filePath = MethodBase.GetCurrentMethod().Name;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            DaoGenerator generator = new DaoGenerator("Test");

            SchemaDefinition schema = GetTestSchema();
            generator.DisposeOnComplete = false;
            generator.Generate(schema, (s) => {
                return new FileStream(filePath, FileMode.Create);
            });

            string output = File.ReadAllText(filePath);

            Expect.IsGreaterThan(output.Length, 0);            
            
            OutLine(output, ConsoleColor.Cyan);
            SchemaTests.DeleteSchema(schema);
        }
        
        [UnitTest]
        public static void GenerateShouldFireAssociatedEvents()
        {
            DaoGenerator gen = new DaoGenerator("Test.Ns");
            bool? started = false;
            bool? complete = false;
            bool? beforeParse = false;
            bool? afterParse = false;
            bool? beforeResolved = false;
            bool? afterResolved = false;
            bool? beforeWrite = false;
            bool? afterWrite = false;

            bool? beforeCollectionParse = false;
            bool? afterCollectionParse = false;

            bool? beforeCollectionStreamResolved = false;
            bool? afterCollectionStreamResolved = false;

            bool? beforeWriteCollection = false;
            bool? afterWriteCollection = false;

            bool? beforeColumnClassParse = false;
            bool? afterColumnClassParse = false;

            bool? beforeColumnStreamResolved = false;
            bool? afterColumnStreamResolved = false;

            bool? beforeWriteColumns = false;
            bool? afterWriteColumns = false;

            gen.GenerateStarted += (g, s) => { started = true; };
            gen.GenerateComplete += (g, s) => { complete = true; };

            gen.BeforeClassParse += (ns, t) => { beforeParse = true; };
            gen.AfterClassParse += (ns, t) => { afterParse = true; };

            gen.BeforeClassStreamResolved += (s, t) => { beforeResolved = true; };
            gen.AfterClassStreamResolved += (s, t) => { afterResolved = true; };
            gen.BeforeWriteClass += (c, s) => { beforeWrite = true; };
            gen.AfterWriteClass += (c, s) => { afterWrite = true; };

            gen.BeforeCollectionParse += (n, t) => { beforeCollectionParse = true; };
            gen.AfterCollectionParse += (n, t) => { afterCollectionParse = true; };

            gen.BeforeCollectionStreamResolved += (n, t) => { beforeCollectionStreamResolved = true; };
            gen.AfterCollectionStreamResolved += (n, t) => { afterCollectionStreamResolved = true; };

            gen.BeforeWriteCollection += (c, s) => { beforeWriteCollection = true; };
            gen.AfterWriteCollection += (c, s) => { afterWriteCollection = true; };

            gen.BeforeColumnsClassStreamResolved += (s, t) => { beforeColumnStreamResolved = true; };
            gen.AfterColumnsClassStreamResolved += (s, t) => { afterColumnStreamResolved = true; };

            gen.BeforeWriteColumnsClass += (c, s) => { beforeWriteColumns = true; };
            gen.AfterWriteColumnsClass += (c, s) => { afterWriteColumns = true; };

            gen.BeforeColumnsClassParse += (c, s) => { beforeColumnClassParse = true; };
            gen.AfterColumnsClassParse += (c, s) => { afterColumnClassParse = true; };

            gen.Generate(GetTestSchema());

            Expect.IsTrue(started.Value, "GenerateStarted didn't fire");
            Expect.IsTrue(complete.Value, "GenerateComplete didn't fire");
            Expect.IsTrue(beforeParse.Value, "BeforeParse didn't fire");
            Expect.IsTrue(afterParse.Value, "AfterParse didn't fire");
            Expect.IsTrue(beforeResolved.Value, "BeforeStreamResolved didn't fire");
            Expect.IsTrue(afterResolved.Value, "AfterStreamResolved didn't fire");
            Expect.IsTrue(beforeWrite.Value, "BeforeWriteResult didn't fire");
            Expect.IsTrue(afterWrite.Value, "AfterWriteResult didn't fire");

            Expect.IsTrue(beforeCollectionParse.Value, "BeforeCollectionParse didn't fire");
            Expect.IsTrue(afterCollectionParse.Value, "AfterCollectionParse didn't fire");

            Expect.IsTrue(beforeCollectionStreamResolved.Value, "BeforeCollectionStreamResolved didn't fire");
            Expect.IsTrue(afterCollectionStreamResolved.Value, "AfterCollectionStreamResolved didn't fire");

            Expect.IsTrue(beforeWriteCollection.Value, "BeforeWriteCollection didn't fire");
            Expect.IsTrue(afterWriteCollection.Value, "AfterWriteCollection didn't fire");

            Expect.IsTrue(beforeColumnStreamResolved.Value, "BeforeColumnStreamResolved didn't fire");
            Expect.IsTrue(afterColumnStreamResolved.Value, "AfterColumnStreamResolved didn't fire");

            Expect.IsTrue(beforeWriteColumns.Value, "BeforeWriteColumns didn't fire");
            Expect.IsTrue(afterWriteColumns.Value, "AfterWriteColumns didn't fire");

            Expect.IsTrue(beforeColumnClassParse.Value, "BeforeColumnClassParse didn't fire");
            Expect.IsTrue(afterColumnClassParse.Value, "AfterColumnClassParse didn't fire");
        }

        
        [UnitTest]
        public static void CamelCaseShouldLeaveFirstLetterLower()
        {
            string test = "The quick brown fox jumps over the lazy dog";

            Expect.AreEqual("TheQuickBrownFoxJumpsOverTheLazyDog", test.PascalCase(true, new string[]{" "}));
            Expect.AreEqual("theQuickBrownFoxJumpsOverTheLazyDog", test.CamelCase(true, new string[] { " " }));
        }

        [UnitTest]
        public static void ShouldGenerateAndCompile()
        {
            string dir = ".\\GenTest\\";
            FileInfo file = new FileInfo(".\\DaoTest.dll");
            if (file.Exists)
            {
                file.Delete();
            }

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            DaoGenerator gen = new DaoGenerator("Brevitee.Data");
            SchemaDefinition schema = SchemaExtractorTests.ExtractTestSchemaFromDaoRefConfig();
            gen.Generate(schema, (className) => {
                string fp = string.Format("{0}{1}.cs", dir, className);
                FileInfo fi = new FileInfo(fp);
                if(!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                return new FileStream(fp, FileMode.Create);
            });

            CompilerResults results = gen.Compile(dirInfo, file.FullName);

            OutputCompilerErrors(results);

            Expect.IsTrue(results.Errors.Count == 0);
            file.Refresh();
            Expect.IsTrue(file.Exists);
            
            //Directory.Delete(dir, true);
        }

        private static void OutputCompilerErrors(CompilerResults results)
        {
            foreach (CompilerError error in results.Errors)
            {
                OutLineFormat("File=>{0}", ConsoleColor.Yellow, error.FileName);
                OutLineFormat("{0}, {1}::{2}", error.Line, error.Column, error.ErrorText);
            }
        }
    }
}
