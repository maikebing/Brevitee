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
using System.Configuration;
using Brevitee.Configuration;
using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Testing;
using Brevitee.Encryption;
using Brevitee.Profiguration;

namespace Brevitee.Profiguration.Tests
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

        [ConsoleAction("List .prof files")]
        public void ListProfFiles()
        {
            string dirPath = Prompt("Please enter the directory path");
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            FileInfo[] files = dir.GetFiles("*.prof");
            foreach (FileInfo file in files)
            {
                OutFormat("{0}", ConsoleColor.Yellow, file.Name);
            }
        }

        [ConsoleAction("Show Profiguration Values")]
        public void ShowProfigurationValues()
        {
            string filePath = Prompt("Please enter the path to the file", ConsoleColor.Cyan);
            Profiguration prof = Profiguration.Load(filePath);
            Out("*** AppSettings ***", ConsoleColor.Cyan);
            foreach (string key in prof.AppSettings.Keys)
            {
                OutFormat("{0}={1}\r\n", ConsoleColor.Yellow, key, prof.AppSettings[key]);                
            }
            Out();
            Out("*** Connection Strings ***", ConsoleColor.Cyan);
            foreach (string key in prof.ConnectionStrings.Names)
            {
                OutFormat("{0}::{1}={2}", ConsoleColor.Yellow, prof.ConnectionStrings[key].Provider, prof.ConnectionStrings[key].ConnectionString.Key, prof.ConnectionStrings[key].ConnectionString.Value);
            }
        }

        [UnitTest]
        public void ShouldBeAbleToSaveSpecificAppSettingsToAProfigurationFile()
        {
            string key = "Take1";
            string testFile = "{0}.prof"._Format(MethodBase.GetCurrentMethod().Name);
            string fromConfig = DefaultConfiguration.GetAppSetting(key);
            Expect.IsNotNull(fromConfig);
            Expect.IsTrue(!string.IsNullOrEmpty(fromConfig));

            Profiguration prof = new Profiguration();
            Expect.IsNull(prof.AppSettings[key]);
            prof.CopyAppSetting(key);
            Expect.IsNotNull(prof.AppSettings[key]);
            Expect.AreEqual(fromConfig, prof.AppSettings[key]);

            prof.Save(testFile);

            Profiguration check = Profiguration.Load(testFile);
            Expect.AreEqual(fromConfig, check.AppSettings[key]);
        }

        [UnitTest]
        public void ShouldBeABleToSaveSpecificConnectionStringsToAnExistingProfigurationFileAndNotLoseSettings()
        {
            string key = "Take1";
            string testFile = "{0}.prof"._Format(MethodBase.GetCurrentMethod().Name);
            string appSettingValue = DefaultConfiguration.GetAppSetting(key);
            Expect.IsFalse(string.IsNullOrEmpty(appSettingValue));
            Profiguration prof = new Profiguration();
            prof.CopyAppSetting(key);
            prof.Save(testFile);

            string conn = "TestConn1";
            ConnectionStringValue c = prof.ConnectionStrings[conn];
            prof.CopyConnectionString(conn);
            prof.Save(testFile);

            Profiguration check = Profiguration.Load(testFile);
            Expect.AreEqual(prof.AppSettings[key], check.AppSettings[key]);

            ConnectionStringValue firstCon = prof.ConnectionStrings[conn];
            ConnectionStringValue saved = check.ConnectionStrings[conn];
            Expect.IsNotNull(firstCon);
            Expect.IsNotNull(saved);
            Expect.IsNotNull(firstCon.ConnectionString);
            Expect.IsNotNull(saved.ConnectionString);
            Expect.AreEqual(firstCon.ConnectionString.Value, saved.ConnectionString.Value);
            Expect.AreEqual(appSettingValue, check.AppSettings[key]);
        }
        
        [UnitTest]
        public void ShouldBeAbleToInjectProfigurationIntoDefault()
        {
            string key = "Take1";
            string connName = "TestConn1";            
            string originalAppSetting = DefaultConfiguration.GetAppSetting(key);
            ConnectionStringSettings originalConnSetting = DefaultConfiguration.GetConnectionStrings()[connName];
            string fromConfig = originalAppSetting;

            Profiguration prof = new Profiguration();
            prof.CopyAppSetting(key);
            prof.CopyConnectionString(connName);
            Expect.AreEqual(prof.AppSettings[key], fromConfig);
            Expect.AreEqual(prof.ConnectionStrings[connName].ConnectionString.Value, originalConnSetting.ConnectionString);
            Expect.AreEqual(prof.ConnectionStrings[connName].Provider, originalConnSetting.ProviderName);

            string newAppSettingValue = "changed_".RandomLetters(4);
            string newConnString = "changed_conn_".RandomLetters(4);
            string newConnProv = "changed_conn_prov_".RandomLetters(4);

            prof.AppSettings[key] = newAppSettingValue;
            prof.ConnectionStrings[connName] = new ConnectionStringValue { ConnectionString = new KeyValuePair { Key = connName, Value = newConnString }, Provider = newConnProv };

            prof.Inject();

            fromConfig = DefaultConfiguration.GetAppSetting(key);
            ConnectionStringSettingsCollection conns = DefaultConfiguration.GetConnectionStrings();
            Expect.AreEqual(fromConfig, newAppSettingValue);
            Expect.AreEqual(newConnString, conns[connName].ConnectionString);
            Expect.AreEqual(newConnProv, conns[connName].ProviderName);

            prof.Revert();
            fromConfig = DefaultConfiguration.GetAppSetting(key);
            conns = DefaultConfiguration.GetConnectionStrings();            
            Expect.AreEqual(originalAppSetting, fromConfig);

            Expect.IsFalse(newConnString.Equals(originalConnSetting.ConnectionString));
            Expect.IsFalse(newConnProv.Equals(originalConnSetting.ProviderName));

            Expect.AreEqual(originalConnSetting.ConnectionString, conns[connName].ConnectionString);
            Expect.AreEqual(originalConnSetting.ProviderName, conns[connName].ProviderName);
        }

        [UnitTest]
        public void ShouldntLoseOriginalConfigsToProfig()
        {
            string key1 = "Take1";
            string key2 = "Take2";
            string key3 = "Take3";
            string fromConfig1 = DefaultConfiguration.GetAppSetting(key1);
            string fromConfig2 = DefaultConfiguration.GetAppSetting(key2);
            string fromConfig3 = DefaultConfiguration.GetAppSetting(key3);

            Profiguration.Default.CopyAppSetting(key1);
            Profiguration.Default.AppSettings[key2] = "From Profig";

            Expect.AreEqual(fromConfig1, Profiguration.Default.AppSettings[key1]);
            Expect.IsFalse(fromConfig2.Equals(Profiguration.Default.AppSettings[key2]));
            Expect.IsFalse(fromConfig3.Equals(Profiguration.Default.AppSettings[key3]));

            Profiguration.Default.Inject();

            Expect.AreEqual(fromConfig1, DefaultConfiguration.GetAppSetting(key1));
            Expect.AreEqual("From Profig", DefaultConfiguration.GetAppSetting(key2));
            Expect.AreEqual(fromConfig3, DefaultConfiguration.GetAppSetting(key3));
        }
    }

}
