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

namespace Brevitee.Data.Tests
{
    public class SchemaTests: CommandLineTestInterface
    {
        [UnitTest]
        public static void SchemaManagerGetSchema()
        {
            SchemaManager manager = new SchemaManager();
            SchemaDefinition schema = manager.SetSchema("test");
            Expect.IsNotNull(schema);
            Expect.IsTrue(File.Exists(schema.File));
            DeleteSchema(schema);
        }

        internal static void DeleteSchema(SchemaDefinition schema)
        {
            try
            {
                File.Delete(schema.File);
            }
            catch (Exception ex)
            {
                OutFormat("An error occurred deleting schema file: {0}", ConsoleColor.Red, ex.Message);
            }
        }

        [UnitTest]
        public static void SchemaManagerAddColumnShouldAddColumn()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition schema = mgr.SetSchema("test");
            string tableName = "Babboons";
            mgr.AddTable(tableName);
            mgr.AddColumn(tableName, new Column("PutColumnName", DataTypes.Boolean));

            Table table = mgr.GetTable(tableName);
            Expect.IsTrue(table.Columns.Length == 1);
            DeleteSchema(schema);
        }

        [UnitTest]
        public static void AddTableShouldSetConnectionNameToNameOfSchema()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition schema = mgr.SetSchema("test");
            Table testTable = new Table("".RandomString(5));
            Expect.IsNullOrEmpty(testTable.ConnectionName);

            schema.AddTable(testTable);

            Expect.IsNotNullOrEmpty(testTable.ConnectionName);
            Expect.AreEqual(schema.Name, testTable.ConnectionName);
        }

        [UnitTest]
        public static void SettingTablesShouldSetConnectionNameForTables()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition schema = mgr.SetSchema("test");
            Table testTable = new Table("".RandomString(5));
            Expect.IsNullOrEmpty(testTable.ConnectionName);

            List<Table> tables = new List<Table>();
            tables.Add(testTable);

            schema.Tables = tables.ToArray();

            Expect.IsNotNullOrEmpty(testTable.ConnectionName);
            Expect.AreEqual(schema.Name, testTable.ConnectionName);
        }

        [UnitTest]
        public static void ColumnWithSameNameAsFKShouldBeEqual()
        {
            ForeignKeyColumn fk = new ForeignKeyColumn();
            fk.TableName = "test";
            fk.Name = "columnName";

            Column col = new Column();
            col.TableName = fk.TableName;
            col.Name = fk.Name;

            Expect.IsTrue(fk.Equals(col));
            Expect.IsFalse(fk == col);
        }

        [UnitTest]
        public static void ListContainsShouldBeTrueForSameNameAndTable()
        {
            List<ForeignKeyColumn> fks = new List<ForeignKeyColumn>();
            fks.Add(new ForeignKeyColumn("columnName", "test", "target"));

            Expect.IsTrue(fks.Contains(new ForeignKeyColumn("columnName", "test", "target")));
        }
        
        [UnitTest]
        public static void ForeignKeyReferencedTableMustNotBeNull()
        {
            ForeignKeyColumn col = new ForeignKeyColumn("ignore", "ignore", "ReferencedTable");
            Expect.IsNotNullOrEmpty(col.ReferencedTable);
        }

        class TestSchemaManager : SchemaManager
        {
            public Result TestAddForeignKey(Table table, Table target, ForeignKeyColumn fk)
            {
                return SetForeignKey(table, target, fk);
            }
        }

        [UnitTest]
        public static void AddForeignKeyShouldIncrementReferencingFKsForTargetTable()
        {
            TestSchemaManager tsm = new TestSchemaManager();
            string ingTable ="referencing";
            string edTable = "referred";
            Table referencing = new Table { Name = ingTable };
            Table referred = new Table { Name = edTable };

            int initial = referred.ReferencingForeignKeys.Length;
            ForeignKeyColumn fk = new ForeignKeyColumn("referredId", ingTable, edTable);
            
            tsm.TestAddForeignKey(referencing, referred, fk);
            Expect.AreEqual(initial + 1, referred.ReferencingForeignKeys.Length);
            Expect.AreEqual(0, referencing.ReferencingForeignKeys.Length);
        }
        
        [UnitTest]
        public static void SchemaManagerSetFKShouldAddFK()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition schema = mgr.SetSchema("test");
            string tableName = "Referencee";
            mgr.AddTable(tableName);
            mgr.AddColumn(tableName, new Column("PutColumnName", DataTypes.Boolean));

            string refering = "Referencer";
            mgr.AddTable(refering);
            mgr.AddColumn(refering, new Column("fk", DataTypes.Long));// { Name = "fk", Type = DataTypes.Long });

            int initCount = schema.ForeignKeys.Length;
            mgr.SetForeignKey(tableName, refering, "fk");

            Expect.AreEqual(initCount + 1, schema.ForeignKeys.Length);

            Table referenced = mgr.GetTable(tableName);
            Table referencer = mgr.GetTable(refering);

            Expect.AreEqual(schema.ForeignKeys.Length, referenced.ReferencingForeignKeys.Length);
            Expect.AreEqual(0, referencer.ReferencingForeignKeys.Length); 
            DeleteSchema(schema);
        }

        [UnitTest]
        public static void AddForeignKeyShouldFailIfColumnNotDefined()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition s = mgr.SetSchema("test");
            mgr.AddTable("TableOne");
            mgr.AddTable("ReferringTable");
            Result r = mgr.SetForeignKey("TableOne", "ReferringTable", "TableOneID");

            Expect.IsFalse(r.Success);
            Out(r.Message, ConsoleColor.Yellow);

            TryDeleteSchema(s);
        }

        [UnitTest]
        public static void SchemaMgrAddTableShouldSuccess()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition s = mgr.SetSchema("test");
            Result r = mgr.AddTable("tableOne");
            Expect.IsTrue(r.Success);

            TryDeleteSchema(s);
        }

        private static void TryDeleteSchema(SchemaDefinition s)
        {
            try
            {
                DeleteSchema(s);
            }
            catch (Exception ex)
            {
                OutFormat("An error occurred deleting test data. {0}", ConsoleColor.Red, ex.Message);
            }
        }

        [UnitTest]
        public static void SchemaMgrAddColumnShouldSuccess()
        {
            string tableName = "tableOne";
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition s = mgr.SetSchema("test");
            Result r = mgr.AddTable(tableName);
            r = mgr.AddColumn(tableName, new Column("ColumnOne", DataTypes.Long, false));
            Expect.IsTrue(r.Success);
        }
        
        private static SchemaDefinition CreateTestTables(string targetTable, string referencingTable)
        {
            SchemaManager mgr = new SchemaManager();
            return CreateTestTables(targetTable, referencingTable, mgr);
        }

        private static SchemaDefinition CreateTestTables(string targetTable, string referencingTable, SchemaManager mgr)
        {
            SchemaDefinition schema = mgr.SetSchema("fkTest");
            DeleteSchema(schema);
            schema = mgr.SetSchema("fkTest");

            mgr.AddTable(targetTable);
            mgr.AddColumn(targetTable, new Column("PutColumnName", DataTypes.Long));


            mgr.AddTable(referencingTable);
            mgr.AddColumn(referencingTable, new Column("PutColumnName", DataTypes.Long));

            mgr.SetForeignKey(targetTable, referencingTable, "fk");
            return schema;
        }

        [UnitTest]
        public static void SetNewSchemaShouldThrowExceptionIfSchemaExists()
        {
            SchemaManager mgr = new SchemaManager();
            mgr.SetSchema("test");
            bool thrown = false;
            try
            {
                mgr.SetNewSchema("test");
            }
            catch (Exception ex)
            {
                thrown = true;
                TryDeleteSchema(mgr.SetSchema("test"));
            }

            Expect.IsTrue(thrown);
        }
    }
}
