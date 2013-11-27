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
using Brevitee.CommandLine;
using Brevitee.Data;
using Brevitee;
using Brevitee.Testing;

namespace Brevitee.Data.Tests
{
    public class ParameterFactoryTests : CommandLineTestInterface
    {
        [UnitTest]
        public static void ParameterBuilderShouldReturnParameterCountMatchingComparisonCount()
        {
            TableColumns test = new TableColumns();
            QueryFilter<TableColumns> q = test.TestOne == "gorilla" && test.TestOne != "gobots";
            SqlClientParameterBuilder paramBuilder = new SqlClientParameterBuilder();
            Out(q.Parse(), ConsoleColor.Cyan);
            foreach (DbParameter parameter in paramBuilder.GetParameters(q))
            {
                OutFormat("Name: {0}, Value: {1}", parameter.ParameterName, parameter.Value, ConsoleColor.Yellow);
            }
        }

    }
}
