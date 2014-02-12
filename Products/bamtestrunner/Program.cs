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

namespace Brevitee.Server.Tests
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
            AddValidArgument("search", false, "The search pattern to use to locate test assemblies");
            AddValidArgument("dir", false, "The directory to look for test assemblies in");
            DefaultMethod = typeof(Program).GetMethod("Start");
        }

        #region do not modify
        public static void Start()
        {
            DirectoryInfo testDir = new DirectoryInfo(".");
            if (Arguments.Contains("dir"))
            {
                string dir = Arguments["dir"];
                testDir = new DirectoryInfo(dir);
                if (!testDir.Exists)
                {
                    OutLineFormat("The specified directory ({0}) was not found", ConsoleColor.Red, dir);
                    Exit(1);
                }
            }

            FileInfo[] files = null;
            if (Arguments.Contains("search"))
            {
                files = testDir.GetFiles(Arguments["search"]);
            }
            else
            {
                files = testDir.GetFiles();
            }

            TestFailed += Program_TestFailed;

            files.Each(fi =>
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(fi.FullName);
                    RunAllTests(assembly);
                    Exit(0);
                }
                catch (Exception ex)
                {
                    OutLine(ex.Message, ConsoleColor.Red);
                    Exit(1);
                }
            });
        }

        static void Program_TestFailed(object sender, TestExceptionEventArgs e)
        {
            Exit(1);
        }
        #endregion
    }

}
