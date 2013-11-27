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

namespace Brevitee.Drawing.Tests
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

        [ConsoleAction("Add your action here")]
        public void AddYourActionHere()
        {
        }

        [UnitTest]
        public void YourUnitTestHere()
        {
        }

        // See the below for examples of ConsoleActions and UnitTests

        #region ConsoleAction examples
        [ConsoleAction("Non Static Test")]
        public void NonStatic()
        {
            Pass("Test passed");
        }

        [ConsoleAction("Static Test")]
        public static void StaticTest()
        {
            Pass("Test passed");
        }

        [ConsoleAction("With Parameters")]
        public static void WithParameters(string inputString)
        {
            OutFormat("You typed {0}", inputString);
        }

        [ConsoleAction("Warn")]
        public void DoWarn()
        {
            Warn("This is a warning message");
        }

        [ConsoleAction("Error")]
        public void DoError()
        {
            Error("This is an error", new Exception("This is an exception"));
        }
        #endregion

        #region UnitTest samples
        [UnitTest]
        public void SampleUnitTest()
        {
            string outter = "outer_";
            After.Setup(c =>
            // c represents the SetupContext which is itself 
            // an Incubator (dependency injection container)
            {
                c.Set<string>(() =>
                {
                    return outter.RandomString(5);
                });
            })
            .WhenA<object>("has its toString method called", (o) =>
            // o is an instance of the generic
            // type passed to WhenA
            {
                o.ToString();
            })
            .TheTest
            .ShouldPass(because =>
            {
                // this is code, no way to make it fall in line, though there are other
                // ways to get at the object under test
                object testObj = because.ObjectUnderTest<object>();

                // this is an assertion
                because.ItsTrue("the object under test was not null", testObj != null, "the object under test was null");
                because.ItsFalse("the object under test was not null", testObj == null, "the object under test was null");
            })
            .SoBeHappy(c =>
            {
                OutFormat("This is from the cleanup (SoBeHappy) method, the outer text value is {0}", c.Get<string>());
            });
        }

        [UnitTest]
        public void SampleUnitTestWithoutSetup()
        {
            When.A<object>("has its toString method called", (o) =>
            // o is an instance of the generic
            // type passed to When.A.  In this case
            // its just an instance of an object
            {
                o.ToString();
            })
            .TheTest
            .ShouldPass(because =>
            {
                // this is code, no way to make it fall in line, though there are other
                // ways to get at the object under test
                object testObj = because.ObjectUnderTest<object>();

                // this is an assertion
                because.ItsTrue(/* success message */"the object under test was not null", testObj != null, /* failure message */"the object under test was null");
                because.ItsTrue(/* success message */"Big-Bird is Brevitee's favorite", true, /* failure message */"big bird is not cool");
                because.ItsTrue(/* success message */"Oscar-the-Grouch is a very grouchy guy", true, /* failure message */"Oscar-the-Grouch is happy");
            })
            .SoBeHappy(c =>
            {
                // no setup so nothing to clean up.
                // clean up isn't always necessary.
            })
            .UnlessItFailed();
        }

        [UnitTest]
        public void TestFailure()
        {
            When.A<object>("is instantiated", (o) =>
            {

            })
            .TheTest
            .ShouldPass(because =>
            {
                because.ItsFalse("the test passed", true, "the test failed");
            })
            .SoBeHappy()
            .UnlessItFailed("The test failed, but in this case that's a good thing since that's what we're testing");
        }
        #endregion
    }

}
