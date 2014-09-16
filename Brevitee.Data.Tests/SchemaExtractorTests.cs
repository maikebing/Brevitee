using System;
using System.Collections.Generic;
using System.Collections;
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
using Brevitee.Data.MsSql;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Configuration;

namespace Brevitee.Data.Tests
{
    public class SchemaExtractorTests : CommandLineTestInterface
    {
        [UnitTest]
        public static void ExtractSchemaTest()
        {
            SchemaDefinition schema = ExtractTestSchemaFromDaoRefConfig();
            Expect.IsNotNull(schema, "Schema was not extracted");

            Expect.AreEqual(2, schema.Tables.Length);
            Table daoRef = schema.GetTable("DaoReferenceObject");
            Expect.IsNotNull(daoRef);
            Expect.AreEqual(10, daoRef.Columns.Length);
            Expect.AreEqual(1, daoRef.ReferencingForeignKeys.Length);

            Table fkTable = schema.GetTable("DaoReferenceObjectWithForeignKey");

            Expect.IsNotNull(fkTable);
            Expect.AreEqual(3, fkTable.Columns.Length + fkTable.ForeignKeys.Length);
        }

        [UnitTest]
        public static void ReferencingForeignKeysShouldHaveReferencedKey()
        {
            SchemaDefinition schema = ExtractTestSchemaFromDaoRefConfig();
            Expect.IsTrue(schema.Tables.Length > 0);
            bool tested = false;
            
            foreach (Table table in schema.Tables)
            {
                foreach (ForeignKeyColumn column in table.ReferencingForeignKeys)
                {
                    Expect.IsNotNullOrEmpty(column.ReferencedKey);
                    tested = true;
                }
            }

            Expect.IsTrue(tested);
        }

        [UnitTest]
        public static void DaoReferenceObjectWithForeignKeyShouldHaveNoReferencingKeys()
        {
            SchemaDefinition schema = ExtractTestSchemaFromDaoRefConfig();
            Expect.IsTrue(schema.Tables.Length == 2);

            var table = (from t in schema.Tables
                        where t.Name == "DaoReferenceObjectWithForeignKey"
                        select t).FirstOrDefault();

            Expect.IsNotNull(table);

            Expect.IsTrue(table.ReferencingForeignKeys.Length == 0);
        }
        
        [UnitTest]
        public static void DbDataTypeShouldNotBeNullForExtractedSchema()
        {
            SchemaDefinition schema = ExtractTestSchemaFromDaoRefConfig();
            schema = SchemaDefinition.Load(schema.File);
            OutLineFormat("file = {0}", schema.File);
            bool tested = false;
            foreach (Table table in schema.Tables)
            {
                foreach (Column column in table.Columns)
                {
                    Expect.IsNotNullOrEmpty(column.DbDataType);
                    Expect.IsFalse(column.DbDataType.ToLowerInvariant().Equals("null"));
                    OutLineFormat("DbDataType={0}", column.DbDataType);
                    tested = true;
                }
            }
            
            Expect.IsTrue(tested);
        }

        [UnitTest]
        public static void ExtractedSchemaShouldHaveForeignKeysDefined()
        {
            DeleteDaoRefSchemaFile();
            SchemaDefinition schema = ExtractTestSchemaFromDaoRefConfig();
            schema = SchemaDefinition.Load(schema.File);
            OutLineFormat("file = {0}", schema.File);
            
            List<Table> tables = schema.Tables.Where<Table>(t => t.Name.Equals("DaoReferenceObjectWithForeignKey")).ToList();
            Expect.IsTrue(tables.Count == 1);
            Expect.IsTrue(tables[0].ForeignKeys.Length == 1);            
        }

        [UnitTest]
        public static void ExtractedForeignKeysShouldHaveReferencingTable()
        {
            DeleteDaoRefSchemaFile();
            SchemaDefinition schema = ExtractTestSchemaFromDaoRefConfig();
            schema = SchemaDefinition.Load(schema.File);
            OutLineFormat("file = {0}", schema.File);

            List<Table> tables = schema.Tables.Where(t => t.ForeignKeys.Length > 0).ToList();
            Expect.IsTrue(tables.Count == 1);
            string referencedClass = tables[0].ForeignKeys[0].ReferencedClass;
            string tableName = tables[0].ForeignKeys[0].TableName;
            Expect.IsNotNullOrEmpty(tableName);
            Expect.IsNotNullOrEmpty(referencedClass);
            OutLineFormat("Table: {0}", tableName);
            OutLineFormat("ReferencedClass: {0}", referencedClass);
        }

        internal static void DeleteDaoRefSchemaFile()
        {
            File.Delete(ExtractTestSchemaFromDaoRefConfig().File);
        }

        internal static SchemaDefinition ExtractTestSchemaFromDaoRefConfig()
        {
            string connectionName = "DaoRef";
            string connString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            Expect.IsNotNullOrEmpty(connString, "The connection string named DaoRef wasn't found in the config file");

            SqlClientSchemaExtractor extractor = new SqlClientSchemaExtractor(connectionName);
            SchemaDefinition schema = extractor.Extract();
            return schema;
        }

        [UnitTest]
        public static void GetForeignKeyDataShouldntThrowException()
        {
            try
            {
                string connectionName = "DaoRef";
                string connString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                Expect.IsNotNullOrEmpty(connString, "The connection string named DaoRef wasn't found in the config file");

                SqlClientSchemaExtractor extractor = new SqlClientSchemaExtractor(connectionName);

                DataTable data = SqlClientSchemaExtractor.GetForeignKeyData(connectionName);
                foreach (DataRow row in data.Rows)
                {
                    foreach (DataColumn column in data.Columns)
                    {
                        OutLineFormat("Name: {0}\r\nValue: {1}", column.ColumnName, row[column].ToString());
                    }
                    OutLine();
                }
            }
            catch (Exception ex)
            {
                Expect.Fail(ex.Message);
            }
        }

        [UnitTest]
        public static void GetForeignKeyColumnsTest()
        {
            string connectionName = "DaoRef";
            SqlClientSchemaExtractor extractor = new SqlClientSchemaExtractor(connectionName);
            ForeignKeyColumn[] fks = extractor.GetForeignKeyColumns();
            Expect.AreEqual(1, fks.Length);
            
            foreach (ForeignKeyColumn fk in fks)
            {
                OutLine(fk.PropertiesToString(), ConsoleColor.Cyan);
                Expect.IsNotNullOrEmpty(fk.ReferencedKey);
            }
        }
    }
}
