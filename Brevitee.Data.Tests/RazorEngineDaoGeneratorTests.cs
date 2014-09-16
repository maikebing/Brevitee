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
using Brevitee;
using System.Configuration;
using MySql.Data.MySqlClient;
using Brevitee.Incubation;
using System.Web.Razor.Generator;
using System.Data.OleDb;
using System.Web.Razor;
using Brevitee.Data.Schema;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using Brevitee.CommandLine;
using Brevitee.Testing;

namespace Brevitee.Data.Tests
{
    public class DaoRazorGeneratorTests : CommandLineTestInterface
    {
        [UnitTest]
        public static void TestRazorParser()
        {
            RazorParser<DaoRazorTemplate<Table>> razorParser = new RazorParser<DaoRazorTemplate<Table>>();
            Table value = new Table();
            value.Name = "Monkey";
            string result = "";
            using (StreamReader sr = new StreamReader(".\\Dao.tmpl"))
            {
                result = razorParser.Execute(sr, new { Model = value });
            }

            Expect.IsNotNullOrEmpty(result);
            OutLine(result, ConsoleColor.Cyan);
            FileInfo compareToFile = new FileInfo(string.Format(".\\{0}.txt", MethodBase.GetCurrentMethod().Name));
            Compare(result, compareToFile);
        }
        
        [UnitTest]
        public static void TestRazorParserParsePropertyTemplate()
        {
            RazorParser<DaoRazorTemplate<Column>> razorParser = new RazorParser<DaoRazorTemplate<Column>>();
            Column column = new Column("ColumnName", DataTypes.DateTime);
            string result = razorParser.ExecuteResource("Property.tmpl", new { Model = column });
            OutLine(result, ConsoleColor.Cyan);

            FileInfo compareToFile = new FileInfo(string.Format(".\\{0}.txt", MethodBase.GetCurrentMethod().Name));
            Compare(result, compareToFile);
        }

        [UnitTest]
        public static void WriteDaoCollectionPropertyTest()
        {
            ForeignKeyColumn fk = new ForeignKeyColumn(new Column("ColumnName", DataTypes.Long), "RTable");
            fk.TableName = "testTable";
            OutLine(fk.RenderListProperty(), ConsoleColor.Cyan);
        }

        private static Table GetTable()
        {
            Table value = new Table();            
            value.Name = "Gorrilla";
            value.ConnectionName = "TestConnection";
            Column col = new Column("ColumnOne", DataTypes.Decimal);
            value.AddColumn(col);
            return value;
        }

        [UnitTest]
        public static void RazorParseTableWithKeyColumn()
        {
            RazorParser<TableTemplate> razorParser = new RazorParser<TableTemplate>();
            Table value = GetTable();
            string id = "".RandomString(5);
            value.AddColumn(new KeyColumn(id, DataTypes.Long, false));
            value.AddColumn(new ForeignKeyColumn 
                { 
                    AllowNull = false, 
                    Name = "ForeignKeyColumn", 
                    ReferencedKey = id, 
                    ReferencedTable = "bananas", 
                    Type = DataTypes.Long 
                });
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition schema = mgr.GetCurrentSchema();
            string result = razorParser.ExecuteResource("Class.tmpl", new { Model = value, Schema = schema, Namespace = "Monkey.balls" });
            Expect.IsTrue(result.Contains(id));
            OutLine(result, ConsoleColor.Cyan);
        }

        [UnitTest]
        public static void LettersOnlyTest()
        {
            Expect.AreEqual("Monkey", "**()@@__98374M`~1!22#$45 %6^7&8*9(0)-_=+{[}]|;;:'\",<.>/?onkey".LettersOnly());
        }
        
        [UnitTest]
        public static void AssemlyNameTest()
        {
            OutLine(typeof(Table).Assembly.CodeBase.Replace("file:///", "").Replace("/", "\\"), ConsoleColor.Cyan);
        }

        private static void Compare(string result, FileInfo compareToFile)
        {
            string compare = "";
            if (!compareToFile.Exists)
            {
                OutLine("The comparison file was not found, using result as comparison", ConsoleColor.Yellow);
                using (StreamWriter sw = new StreamWriter(compareToFile.FullName))
                {
                    sw.Write(result);
                }
            }

            using (StreamReader sr = new StreamReader(compareToFile.FullName))
            {
                compare = sr.ReadToEnd();
            }

            Expect.IsNotNullOrEmpty(compare);
            Expect.AreEqual(compare, result);
            OutLine(compare, ConsoleColor.Cyan);
        }
    }
}
