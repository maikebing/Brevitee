using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Ionic.Zip;
using Brevitee.CommandLine;
using Brevitee.Testing;
using Brevitee;
using Brevitee.ServiceProxy;
using Brevitee.Server;
using Brevitee.Dust;

namespace Brevitee.Management
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

            AddValidArgument("run", true, "Run the copy and rename process");
            AddValidArgument("conf", false, "Path to the config file to use", "<.\\rencopy.json>");

            DefaultMethod = typeof(Program).GetMethod("Start");
            Expect.IsNotNull(DefaultMethod);
            #endregion
        }

        public static void Start()
        {
            MainMenu("Brevitee Application Manager");
        }
        
        [ConsoleAction("Initialize application")]
        public void InitializeApplication()
        {
            Out("Preparing server files");
            SetupFileSystem();
            string applicationName = Prompt("Please enter the application name to initialize");
            string displayName = "";
            if (applicationName.Contains(' '))
            {
                displayName = applicationName;
                applicationName = applicationName.PascalCase(true, " ");
            }
            else
            {
                displayName = applicationName.PascalSplit(" ");
            }
            Out("Preparing application files");
            SetupApplication(applicationName);
        }

        private void SetupApplication(string appName)
        {
            BreviteeConf conf = BreviteeConf.Load();
            //Assembly currentAssembly = Assembly.GetExecutingAssembly();
            //string[] resourceNames = currentAssembly.GetManifestResourceNames();
            //resourceNames.Each(rn =>
            //{
            //    bool isBase = Path.GetExtension(rn).ToLowerInvariant().Equals(".base");
            //    if (isBase)
            //    {
            //        Stream zipStream = currentAssembly.GetManifestResourceStream(rn);
            //        ZipFile zipFile = ZipFile.Read(zipStream);
            //        zipFile.Each(entry =>
            //        {
            //            entry.Extract(Path.Combine(conf.ContentRoot, "apps", appName), ExtractExistingFileAction.OverwriteSilently);
            //        });
            //    }
            //});
        }

        [ConsoleAction("Select layout")]
        public void SelectLayout()
        {
            BreviteeConf conf = BreviteeConf.Load();
            DirectoryInfo layoutDir = new DirectoryInfo(Path.Combine(conf.ContentRoot, "dust/layouts"));
            FileInfo[] dustFiles = layoutDir.GetFiles("*.dust");
            if (dustFiles.Length > 0)
            {
                int selected = SelectFile(dustFiles, "Select the layout to use");
                if (selected < 0 || selected > dustFiles.Length)
                {
                    OutFormat("Invalid selection, enter a number between {0} and {1} which corresponds to one of the layout files listed", ConsoleColor.Yellow, 1, dustFiles.Length);
                    SelectLayout();
                }
                else
                {
                    FileInfo layoutFile = dustFiles[selected];
                    DustTemplate template = new DustTemplate(layoutFile, "layout");
                    
                }
            }
            else
            {
                Out("No layout files were found", ConsoleColor.Yellow);
            }
        }

        private static int SelectFile(FileInfo[] dustFiles, string msg)
        {
            dustFiles.Each((file, i) =>
            {
                OutFormat("{0}. {1}", ConsoleColor.Cyan, i + 1, Path.GetFileNameWithoutExtension(file.Name));
            });
            int selected = NumberPrompt(msg);
            --selected;
            return selected;
        }

        private static void SetupFileSystem()
        {
            
        }        
    }
}
