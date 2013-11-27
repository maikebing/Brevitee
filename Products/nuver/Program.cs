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
using nuver.Nuget;

namespace nuver
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
            AddValidArgument("major", true, "Set or increment the major version", "<value> or /major to increment");
            AddValidArgument("minor", true, "Set or increment the minor version", "<value> or /minor to increment");
            AddValidArgument("patch", true, "Set or increment the patch version", "<value> or /patch to to increment");
            AddValidArgument("p", false, "The path to the .nuspec file");
            AddValidArgument("path", false, "The path to the .nuspec file");
            DefaultMethod = typeof(Program).GetMethod("Start");            
        }

        #region do not modify
        public static void Start()
        {
            if(!Arguments.Contains("p") && !Arguments.Contains("path"))
            {
                Out("/p:<path> must be specified", ConsoleColor.Red);
                Exit(1);
            }
            string path = Arguments["p"];
            if (string.IsNullOrEmpty(path))
            {
                path = Arguments["path"];
            }

            if (!File.Exists(path))
            {
                OutFormat("file not found: {0}", ConsoleColor.Red, path);
                Exit(1);
            }

            NuspecFile file = new NuspecFile(path);
            if (Arguments.Contains("major"))
            {
                string major = Arguments["major"];
                if (!string.IsNullOrEmpty(major))
                {
                    file.Version.Major = major;
                }
                else
                {
                    file.Version.IncrementMajor();
                }
            }

            if (Arguments.Contains("minor"))
            {
                string minor = Arguments["minor"];
                if (!string.IsNullOrEmpty(minor))
                {
                    file.Version.Minor = minor;
                }
                else
                {
                    file.Version.IncrementMinor();
                }
            }

            if (Arguments.Contains("patch"))
            {
                string patch = Arguments["patch"];
                if (!string.IsNullOrEmpty(patch))
                {
                    file.Version.Patch = patch;
                }
                else
                {
                    file.Version.IncrementPatch();
                }
            }

            file.Save();
        }
        #endregion
    }

}
