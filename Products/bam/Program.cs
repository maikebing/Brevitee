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
using Brevitee.Javascript;
using Brevitee.ServiceProxy;
using Brevitee.Server;
using Brevitee.Dust;
using Brevitee.Web;
using Brevitee.Data;
using dotless.Core;

namespace bam
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
            AutoReturn = true;
            AddValidArgument("baseurl", false, "The base url to download from", "http://localhost:8080/");
            AddValidArgument("ns", false, "The namespace to place downloaded clients in", "My.Name.Space");
            AddValidArgument("classes", false, "A comma or semi-colon separated list of class names to download clients for", "ClassOne, ClassTwo");
            AddValidArgument("run", true, "Automated mode");
            AddValidArgument("hosts", false, "Specify set to add the entries specified in BamConfig.TemporaryHostsEntries to the hosts file after backing it up. Specify bak to restore the backup");
            AddValidArgument("conf", false, "The path to the config file to use");
            AddValidArgument("file", false, "The name of the file to save the clients to");

            DefaultMethod = typeof(Program).GetMethod("Start");
            Expect.IsNotNull(DefaultMethod);
            #endregion
        }

        public static void Start()
        {
            if (Arguments.Contains("hosts"))
            {
                if (!WeHaveAdminRights())
                {
                    Elevate();
                    Exit(0);
                }

                string action = Arguments["hosts"];

                FileInfo backupFile = new FileInfo(".\\hosts.bak");
                FileInfo hostsFile = new FileInfo(Path.Combine(Environment.SystemDirectory, "drivers", "etc"));

                if (action.Equals("set"))
                {
                    AddHostsEntries(backupFile, hostsFile);
                }
                else if (action.Equals("restore"))
                {
                    backupFile.CopyTo(hostsFile.FullName, true);
                }
                else
                {
                    OutLineFormat("Unrecognised host command ({0}), specify 'set' or 'bak' or 'restore'", ConsoleColor.Red, action);
                }
            }
            else if (Arguments.Contains("run"))
            {
                BamConfig config = new BamConfig();
                if (Arguments.Contains("conf"))
                {
                    FileInfo file = new FileInfo(Arguments["conf"]);
                    if (!file.Exists)
                    {
                        OutLineFormat("File not found ({0})", ConsoleColor.Red, file.FullName);
                        Exit(1);
                    }

                    config = BamConfig.Load(Arguments["conf"]);
                }
                else
                {
                    string baseUrl = Arguments["baseurl"];
                    string ns = Arguments["ns"];
                    string classes = Arguments["classes"];

                    if (string.IsNullOrEmpty(baseUrl))
                    {
                        OutLine("Base url not specified", ConsoleColor.Red);
                    }

                    config.ServiceProxyBaseAddress = baseUrl;

                    if (!string.IsNullOrEmpty(classes))
                    {
                        config.ClassNames = classes.DelimitSplit(",", ";");
                    }

                    if (!string.IsNullOrEmpty(ns))
                    {
                        config.Namespace = ns;
                    }
                }

                string fileName = Arguments.Contains("file") ? Arguments["file"] : "{0}.cs"._Format(config.Namespace);

                DownloadClients(config, fileName);
            }
            else
            {
                MainMenu("Brevitee Application Manager");
            }
        }

		[ConsoleAction("Show configuration")]
		public void ShowConfiguration()
		{
			BamConfig config = BamConfig.Load();
			OutLineFormat("Base Address: \t{0}", ConsoleColor.Cyan, config.ServiceProxyBaseAddress);
			OutLineFormat("Namespace: \t{0}", ConsoleColor.Cyan, config.Namespace);
			Out("Class Names:", ConsoleColor.Cyan);
			if (config.ClassNames.Length > 0)
			{
				config.ClassNames.Each((cn, i) =>
				{
					if (i > 0)
					{
						Out("\t");
					}
					OutLineFormat("\t{0}", ConsoleColor.Cyan, cn);
				});
			}
			else
			{
				OutLine("\t[all available]", ConsoleColor.Yellow);
			}

			if (config.TemporaryHostsEntries.Length > 0)
			{
				OutLine("Temporary Hosts Entries:", ConsoleColor.Cyan);
				config.TemporaryHostsEntries.Each((he) =>
				{
					OutLineFormat("\t{0}", ConsoleColor.Cyan, he);
				});
			}			
		}
        
        [ConsoleAction("Add local host entries to BamConfig")]
        public void AddLocalHostEntries()
        {
            List<string> quits = new List<string>(new string[] { "q", "quit", "bye", "exit", "done" });
			string entry = string.Empty;
            BamConfig config = BamConfig.Load();
            List<string> hosts = new List<string>();
            while (!quits.Contains(entry))
            {
				if (!hosts.Contains(entry) && !string.IsNullOrEmpty(entry))
				{
					hosts.Add(entry.ToString());
				}
				entry = Prompt("Enter the host name to add ([q]uit to finish)");                
            }
            config.TemporaryHostsEntries = hosts.ToArray();
            config.Save();
        }

        [ConsoleAction("Set hosts file entries")]
        public void SetHostsFileEntries()
        {
            FileInfo backupFile = new FileInfo(".\\hosts.bak");
            FileInfo hostsFile = new FileInfo(Path.Combine(Environment.SystemDirectory, "drivers", "etc"));
            string content = File.ReadAllText(hostsFile.FullName);
            OutLine("**** Current Hosts File content ****", ConsoleColor.Cyan);
            OutLine(content, ConsoleColor.Green);
            if (Confirm("Continue? [y/n]"))
            {
                if (backupFile.Exists)
                {
                    OutLine("**** Current Hosts Backup File content ****", ConsoleColor.Yellow);
                    content = File.ReadAllText(backupFile.FullName);
                    OutLine(content, ConsoleColor.Yellow);
                    if (Confirm("Replace current backup? [y/n]"))
                    {
                        backupFile.Delete();
                    }                    
                }

                AddHostsEntries(backupFile, hostsFile);
            }
        }

        [ConsoleAction("Set default ServiceProxy Base Address")]
        public void SetDefaultServiceProxyBaseAddress()
        {
            BamConfig config = BamConfig.Load();
            string url = Prompt("Enter the default ServiceProxy Base Address to use");
            config.ServiceProxyBaseAddress = url;
            config.Save();
        }

        [ConsoleAction("Set namespace")]
        public void SetNamespace()
        {
            BamConfig config = BamConfig.Load();
            config.Namespace = Prompt("Enter the default namespace to place downloaded clients in");
            config.Save();
        }

        [ConsoleAction("Set class names")]
        public void SetClassNames()
        {
            BamConfig config = BamConfig.Load();
            List<string> quits = new List<string>(new string[] { "q", "quit", "bye" });
            string message = "Enter class name to add (q to quit)";
            string className = Prompt(message);
            while (!quits.Contains(className))
            {
                config.AddClassName(className);
                className = Prompt(message);
            }

            config.Save();
        }

        [ConsoleAction("Download Clients")]
        public void DownloadClients()
        {
            BamConfig config = BamConfig.Load();
            DownloadClients(config, ".\\{0}.cs"._Format(config.Namespace));
        }

        [ConsoleAction("Set local server root")]
        public void SetLocalServerRoot()
        {
            BamConfig config = BamConfig.Load();
            config.LocalServerRoot = Prompt("Enter the path to the local server content root to use");
            config.Save();
        }

        [ConsoleAction("Process .less files")]
        public void ProcessDotLessFiles()
        {
            string folder = Prompt("Enter the folder path containing .less files");
            DirectoryInfo dir = new DirectoryInfo(folder);
            FileInfo[] lessFiles = dir.GetFiles("*.less");
            lessFiles.Each(file =>
            {
                string fileName = Path.GetFileNameWithoutExtension(file.Name);
                string content = File.ReadAllText(file.FullName);
                string newFileName = Path.Combine(dir.FullName, string.Format("{0}.css", fileName));
                string css = Less.Parse(content);
                css.SafeWriteToFile(newFileName, true);
            });
        }
		
		[ConsoleAction("Combine scripts (& optionally minify)")]
		public void CombineScripts()
		{
			string[] filePaths = ArrayPrompt("Enter the path of a script to include.", "q", "done", "quit", "bye");
			string fileName = PromptForFileName();
			bool minify = Confirm("Minify final script?");
			StringBuilder combined = CombineFiles(filePaths);
			StringBuilder final = new StringBuilder();
			if (minify)
			{
				final.Append(combined.ToString().Compress());
			}
			else
			{
				final = combined;
			}

			final.ToString().SafeWriteToFile(fileName);
		}

		[ConsoleAction("Combine scripts in directory")]
		public void CombineScriptsInDirectory()
		{
			string directoryPath = Prompt("Enter the directory path containing scripts to combine.");
			bool minify = Confirm("Minify after combining?");
			string finalFileName = PromptForFileName();
			string searchPattern = PromptForSearchPattern();

			StringBuilder final = new StringBuilder();
			DirectoryInfo directory = new DirectoryInfo(directoryPath);
			if (!directory.Exists)
			{
				OutLineFormat("Specified directory was not found: {0}", ConsoleColor.Red, directory.FullName);
			}
			else
			{
				FileInfo[] files = directory.GetFiles(searchPattern);
				StringBuilder combined = CombineFiles(files);

				if (minify)
				{
					string minified = combined.ToString().Compress();
					final.Append(minified);
				}
				else
				{
					final = combined;
				}
			}

			DirectoryInfo destination = directory.Parent;
			FileInfo finalFile = new FileInfo(Path.Combine(destination.FullName, finalFileName));
			final.ToString().SafeWriteToFile(finalFile.FullName);
		}

		[ConsoleAction("Minify script")]
		public void MinifyScript()
		{
			string filePath = Prompt("Enter the name of the script to minify");
			string minFilePath = Prompt("Enter the name of the minified script to create");
			if (!File.Exists(filePath))
			{
				OutLineFormat("File not found: {0}", ConsoleColor.Red, filePath);
			}
			string minified = File.ReadAllText(filePath).Compress();
			minified.SafeWriteToFile(new FileInfo(minFilePath).FullName);
		}

        [ConsoleAction("Create application")]
        public void CreateApplication()
        {
            BreviteeServer server;
            string appName;
            GetServerAndAppName(out server, out appName);

            DirectoryInfo layoutsDir = new DirectoryInfo(Path.Combine(server.ContentRoot, "dust", "layouts"));
            FileInfo[] layouts = layoutsDir.GetFiles("*.dust");
            string layout = "basic";
            int layoutIndex = 0;
            if (layouts.Length > 1)
            {
                layoutIndex = SelectLayout(layouts);
                layout = Path.GetFileNameWithoutExtension(layouts[layoutIndex].FullName);
            }
            server.CreateApp(appName, layout);
            OutLineFormat("Created application ({0}) in ({1}): ({2})", ConsoleColor.Green, appName, server.ContentRoot, Path.Combine(server.ContentRoot, "apps", appName));
        }

        [ConsoleAction("List apps")]
        public void ListApps()
        {
            BamConfig config = BamConfig.Load();
            DirectoryInfo appsRoot = new DirectoryInfo(Path.Combine(config.LocalServerRoot, "apps"));
            DirectoryInfo[] apps = appsRoot.GetDirectories();
            OutLineFormat("Apps in ({0})", ConsoleColor.Cyan, config.LocalServerRoot);
            apps.Each((app, i) =>
            {
                OutLineFormat("{0}. {1}", ConsoleColor.Yellow, i + 1, app);
            });
            Out();
        }


        // TODO: Pack app
        [ConsoleAction("")]
        public void PackApp()
        {

        }

        // TODO: Forward app
        // TODO: Add app
        // TODO: List dao/schemas
        // TODO: Forward dao/schemas assembly
        // TODO: Add dao/schemas assembly
        // TODO: List service proxies: server level and app level
        // TODO: Forward service proxy
        // TODO: Add service proxy assembly
        // TODO: Add service proxies from project folder
        //      zip folder, upload, compile project, register service proxy        
        // TODO: Add schemas (*.db.js | *.db.json)


		private static StringBuilder CombineFiles(params string[] filePaths)
		{
			List<FileInfo> files = new List<FileInfo>();
			filePaths.Each(path =>
			{
				files.Add(new FileInfo(path));
			});

			return CombineFiles(files.ToArray());
		}

		private static StringBuilder CombineFiles(FileInfo[] files)
		{
			StringBuilder combined = new StringBuilder();
			files.Each(f =>
			{
				combined.AppendLine(File.ReadAllText(f.FullName));
			});
			return combined;
		}

		private static string PromptForFileName()
		{
			string finalFileName = Prompt("Enter the name of the file to create.");
			if (string.IsNullOrEmpty(finalFileName))
			{
				finalFileName = "ScriptFile.js";
			}
			return finalFileName;
		}

		private static string PromptForSearchPattern()
		{
			string searchPattern = Prompt("Enter search pattern to use (blank for default) [default=*.js]:");
			if (string.IsNullOrEmpty(searchPattern))
			{
				searchPattern = "*.js";
			}
			return searchPattern;
		}

		private static void AddHostsEntries(FileInfo backupFile, FileInfo hostsFile)
		{
			if (!backupFile.Exists)
			{
				File.Copy(hostsFile.FullName, backupFile.FullName);
			}

			BamConfig config = BamConfig.Load();
			using (StreamWriter sw = hostsFile.AppendText())
			{
				sw.WriteLine();
				config.TemporaryHostsEntries.Each(hostEntry =>
				{
					string entry = "127.0.0.1 {0}"._Format(hostEntry.Replace(" ", ""));
					sw.WriteLine(entry);
				});
			}
		}
		
		private void GetServerAndAppName(out BreviteeServer server, out string appName)
		{
			BamConfig config = BamConfig.Load();
			string contentRoot = config.LocalServerRoot;
			bool go = ConfirmRoot();
			while (!go)
			{
				SetLocalServerRoot();
				contentRoot = BamConfig.Load().LocalServerRoot;
				go = ConfirmRoot();
			}

			server = new BreviteeServer(BreviteeConf.Load(contentRoot));

			go = ConfirmAppName(out appName);
			while (!go)
			{
				go = ConfirmAppName(out appName);
			}
		}

		private static int SelectLayout(FileInfo[] layouts)
		{
			layouts.Each((l, i) =>
			{
				OutLineFormat("{0}. {1}", i + 1, l);
			});

			int num = NumberPrompt("Select a layout", ConsoleColor.Yellow);
			int index = num - 1;
			if (index < 0 || index >= layouts.Length)
			{
				OutLineFormat("Invalid selection", ConsoleColor.Red);
				index = SelectLayout(layouts);
			}

			return index;
		}

		private bool ConfirmAppName(out string appName)
		{
			appName = Prompt("Enter the name of the app to create", ConsoleColor.Cyan);
			return ConfirmFormat("Is this correct [y/n]: {0}", ConsoleColor.Yellow, appName);
		}

		private bool ConfirmRoot()
		{
			BamConfig config = BamConfig.Load();
			return ConfirmFormat("Use this content root [y/n]: {0}", ConsoleColor.Yellow, config.LocalServerRoot);
		}

		private static void DownloadClients(BamConfig config, string saveTo)
		{
			string classNames = "";
			config.ClassNames.Each(cn =>
			{
				classNames += "&classNames={0}"._Format(cn);
			});
			string urlFormat = "{0}serviceproxy/csharpproxies?namespace={1}{2}";
			string url = urlFormat._Format(config.ServiceProxyBaseAddress, config.Namespace, classNames);
			Http.Get(url, saveTo);
		}

    }
}
