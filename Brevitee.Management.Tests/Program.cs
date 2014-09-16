using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Server;
using Brevitee.CommandLine;
using Brevitee.Configuration;
using Brevitee;
using Brevitee.Testing;
using Brevitee.Encryption;
using Brevitee.Data;
using Brevitee.Analytics.Crawlers;
using Brevitee.Analytics.Data;
using Brevitee.Analytics.Classification;

namespace Brevitee.Management.Tests
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

        [ConsoleAction("Show Assembly Qualified Name of Fs")]
        public void ShowAssemblyQualifiedNameOfFs()
        {
            string n = typeof(Fs).AssemblyQualifiedName;
            Out(n);
            n.SafeWriteToFile("C:\\src\\tmp\\fs.txt");
        }

        [UnitTest]
        public void UnitTest()
        {
           
        }

    }

}
