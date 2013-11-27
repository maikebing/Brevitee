using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using Brevitee.Testing;
using System.IO;
using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Data;
using Brevitee.Incubation;
using Brevitee.Configuration;

namespace CommandLineTests
{
    public class TestProgram : CommandLineTestInterface
    {
        // Add optional code here to be run before initialization/argument parsing.
        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
        }

        /*
          * Methods addorned with the ConsoleAction attribute can be run
          * interactively from the command line while methods addorned with
          * the TestMethod attribute will be run automatically when the
          * compiled executable is run.  To run ConsoleAction methods use
          * the command line argument /i.
          * 
          * All methods addorned with ConsoleAction and TestMethod attributes 
          * must be static for the purposes of extending CommandLineTestInterface
          * or an exception will be thrown.
          * 
          */

        // To run ConsoleAction methods use the command line argument /i.        
        [ConsoleAction("This is a main menu option")]
        public static void ExampleMainMenuOption(string parameter)
        {
            Out(parameter, ConsoleColor.Green);
        }

        class Primate
        {
        }

        class Monkey : Primate
        {
        }

        [UnitTest]
        public static void IncubatorShouldGiveMeWhatISet()
        {
            Incubator i = new Incubator();
            i.Set(typeof(Primate), new Monkey());
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldGiveMeWhatISet2()
        {
            Incubator i = new Incubator();
            i.Set<Primate>(new Monkey());
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldTakeAFuncAndReturnResult()
        {
            Incubator i = new Incubator();
            Func<Primate> f = () => { return new Monkey(); };
            i.Set(typeof(Primate), f);
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldTakeAFuncAndReturnResult2()
        {
            Incubator i = new Incubator();            
            i.Set<Primate>(() => { return new Monkey(); });
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        #region do not modify
        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }


        #endregion
    }
}
