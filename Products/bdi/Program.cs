using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Brevitee;
using Brevitee.CommandLine;
using Brevitee.Logging;
using Brevitee.Incubation;
using Brevitee.Configuration;
using System.IO;
using Brevitee.Yaml;
using Brevitee.Testing;
using System.Reflection;

namespace Brevitee.Server
{
    class Program : ServiceExe
    {
        /// <summary>
        /// Utility class used to access CommandLineTestInterface functionality
        /// </summary>
        class CommandLineUtil: CommandLineTestInterface
        {
            public ParsedArguments ParseInputArguments(string[] args)
            {
                ParseArgs(args); 
                return Arguments;
            }

            public void AddValidArg(string name, bool allowNull)
            {
                AddValidArgument(name, allowNull);
            }
        }

        static CommandLineUtil _util;
        static void Main(string[] args)
        {   
            SetInfo(new ServiceInfo("BreviteeDeamon", "Brevitee Daemon", "Brevitee http application server"));

            if (!ProcessCommandLineArgs(args))
            {
                _util = new CommandLineUtil();
                _util.AddValidArg("i", true);

                ParsedArguments parsedArgs = _util.ParseInputArguments(args);
                if(parsedArgs.Contains("i") || parsedArgs.Contains("interactive"))
                {
                    StartServerInteractively();
                }
                else
                {
                    RunService<Program>();
                }
            }
        }

        bool _stop = false;
        AutoResetEvent _reset = new AutoResetEvent(false);
        // call _reset.Set() to signal that there is work to be done
        protected override void OnStart(string[] args)
        {
            Server.Start();         
        }

        protected override void OnStop()
        {
            Server.Stop();
            Thread.Sleep(1000);
        }

        static BreviteeServer _server;
        static object _serverLock = new object();
        public static BreviteeServer Server
        {
            get
            {
                return _serverLock.DoubleCheckLock(ref _server, () => new BreviteeServer(BreviteeConf.Load()));
            }
        }

        static Dictionary<string, BreviteeServer> _servers;
        private static Dictionary<string, BreviteeServer> Servers
        {
            get
            {
                if (_servers == null)
                {
                    _servers = new Dictionary<string, BreviteeServer>();
                }

                return _servers;
            }
        }

        internal static BreviteeServer CreateServer(string port, string rootDir = "", string name = "")
        {
            BreviteeServer server = new BreviteeServer();
            if (string.IsNullOrEmpty(rootDir))
            {
                rootDir = ".\\BreviteeRoot_".RandomLetters(5);
            }
            server.ContentRoot = rootDir;
            server.Port = port;

            if (string.IsNullOrEmpty(name) || Servers.ContainsKey(name))
            {
                int num = 1;
                if (string.IsNullOrEmpty(name))
                {
                    name = "Server";
                }
                string format = "{0}_{1}";
                name = format._Format(name, num);
                while (Servers.ContainsKey(name))
                {
                    num++;
                    name = format._Format(name, num);
                }
            }

            Servers[name] = server;
            return server;
        }
        
        [ConsoleAction("Start server")]
        public static void StartServerInteractively()
        {
            string rootDir = BreviteeHappyPrompt("Enter the name of the root directory to create\r\n");
            if (Directory.Exists(rootDir))
            {
                bool delete = CommandLineInterface.ConfirmFormat("Directory {0} already exists, reinitialize?\r\n [y][N]", ConsoleColor.Cyan, false, rootDir);
                if (delete)
                {
                    CommandLineInterface.OutFormat("Attempting to delete directory {0}\r\n", ConsoleColor.Cyan, rootDir);
                    Directory.Delete(rootDir, true);
                    CommandLineInterface.OutFormat("The directory {0} was deleted and will be recreated when the server is started\r\n", ConsoleColor.Yellow, rootDir);
                }
            }

            string port = BreviteeHappyPrompt("Enter the port number to listen on\r\n");

            BreviteeServer server = CreateServer(port, rootDir, rootDir);
            BreviteeConf conf = server.GetCurrentConf();
            conf.GenerateDao = true;
            server.SetConf(conf);
            server.SaveConf();

            _startTrap = new AutoResetEvent(false);
            _stopTrap = new AutoResetEvent(false);
            CommandLineInterface.OutLine("Starting Server ", ConsoleColor.Cyan);
            server.Started += (s) =>
            {
                _startTrap.Set();
            };
            server.Stopped += (s) =>
            {
                _stopTrap.Set();
            };

            server.Start();
            CommandLoop();
            server.Stop();

            _stopTrap.WaitOne();
            Thread.Sleep(2000);
        }

        static AutoResetEvent _startTrap;
        static AutoResetEvent _stopTrap;

        static string[] _quits = new string[] { "q", "quit", "bye", "exit" };
        private static void CommandLoop()
        {
            _startTrap.WaitOne();

            Thread.Sleep(2000);
            CommandLineInterface.OutLine("Now listening for commands, logs will continue to output to this window", ConsoleColor.Cyan);
            CommandLineInterface.OutLine("Type ? or help for command list", ConsoleColor.Yellow);
            string command = BreviteeHappyPrompt();
            while (!_quits.Contains(command))
            {
                try
                {
                    ParseCommand(command);

                    command = BreviteeHappyPrompt();
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    string stack = ex.StackTrace ?? "None";
                    CommandLineInterface.OutFormat("Exception:\r\n{0}\r\nStack:\r\n{1}", ConsoleColor.Red, msg, stack);
                }
            }
        }

        private static void ParseCommand(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string[] chunks = input.DelimitSplit(" ");
                string command = chunks[0];
                StringBuilder argBuilder = new StringBuilder();
                if (chunks.Length > 1)
                {
                    chunks.Rest(1, s =>
                    {
                        argBuilder.AppendFormat("{0} ", s);
                    });
                }

                if (CommandActions.ContainsKey(command))
                {
                    TryAction(CommandActions[command], argBuilder.ToString().Trim());
                }
                else
                {
                    CommandLineInterface.OutLine("Command not supported", ConsoleColor.Red);
                }
            }
        }

        private static string BreviteeHappyPrompt(string message = "")
        {
            return CommandLineInterface.Prompt(message, " B) ", ConsoleColor.Cyan);
        }


        #region COMMAND ACTIONs handled here
        static List<ILogger> _addedLoggers;
        static Dictionary<string, Action<string>> _commandActions;
        static object _commandActionsLock = new object();
        private static Dictionary<string, Action<string>> CommandActions
        {
            get
            {
                return _commandActionsLock.DoubleCheckLock(ref _commandActions, () =>
                {
                    _addedLoggers = new List<ILogger>();
                    Dictionary<string, Action<string>> commands = new Dictionary<string, Action<string>>();
                    commands.Add("run", (args) =>
                    {
                        ProcessOutput output = args.Run();
                        CommandLineInterface.OutFormat("Exit Code {0}\r\n", ConsoleColor.Yellow, output.ExitCode);
                        CommandLineInterface.Out("Output (if any):\r\n", ConsoleColor.Cyan);
                        CommandLineInterface.Out(output.StandardOutput, ConsoleColor.White);
                        CommandLineInterface.Out("Error Output (if any):\r\n", ConsoleColor.Yellow);
                        CommandLineInterface.Out(output.StandardError, ConsoleColor.Red);
                        CommandLineInterface.OutLine("\r\nNo .Net exception was thrown", ConsoleColor.Cyan);
                    });

                    commands.Add("show", (args) =>
                    {
                        CommandLineInterface.OutLine("Servers:", ConsoleColor.Cyan);
                        Servers.Keys.Each(server =>
                        {
                            CommandLineInterface.OutLine(server, ConsoleColor.Green);
                        });
                    });

                    commands.Add("restart", (serverName) =>
                    {
                        if (ValidateServer(serverName))
                        {
                            CommandLineInterface.OutLineFormat("Restarting server ({0})", ConsoleColor.Yellow, serverName);
                            BreviteeServer server = Servers[serverName];
                            server.Restart();
                            Thread.Sleep(1500);
                            CommandLineInterface.OutLineFormat("Restarted server ({0})", ConsoleColor.Green, serverName);
                        }
                    });

                    commands.Add("list_loggers", (serverName) =>
                    {
                        if (ValidateServer(serverName))
                        {
                            BreviteeServer server = Servers[serverName];
                            BreviteeConf conf = server.GetCurrentConf(false);
                            CommandLineInterface.OutLine("Available logger types:", ConsoleColor.Cyan);
                            conf.AvailableLoggers.Each(type =>
                            {
                                CommandLineInterface.OutLine(type.Name, ConsoleColor.Green);
                            });
                        }
                    });

                    commands.Add("add_logger", (input) =>
                    {
                        string[] args = input.DelimitSplit(" ");
                        if (args.Length < 2)
                        {
                            CommandLineInterface.OutLine("Server name and logger type must be specified in that order", ConsoleColor.Red);
                        }
                        else
                        {
                            string serverName = args[0];
                            string loggerTypeName = args[1];
                            if (ValidateServer(serverName))
                            {
                                BreviteeServer server = Servers[serverName];
                                BreviteeConf conf = server.GetCurrentConf(false);
                                Type loggerType = conf.AvailableLoggers.Where(type => type.Name.Equals(loggerTypeName)).FirstOrDefault();
                                if (loggerType == null)
                                {
                                    CommandLineInterface.OutLine("The specified logger was not found", ConsoleColor.Red);
                                }
                                else
                                {
                                    server.AddLogger((ILogger)loggerType.Construct());
                                }
                            }
                        }
                    });
                    
                    Action<string> help = (args) =>
                    {
                        CommandLineInterface.OutLine("Available commands:", ConsoleColor.Cyan);
                        commands.Keys.Each(key =>
                        {
                            CommandLineInterface.OutLine(key, ConsoleColor.Green);
                        });
                    };
                    commands.Add("?", help);
                    commands.Add("help", help);

                    return commands;
                });
            }
        }
        #endregion

        private static bool ValidateServer(string serverName)
        {
            bool result = true;
            if (string.IsNullOrEmpty(serverName))
            {
                CommandLineInterface.OutLineFormat("Server name was not specified", ConsoleColor.Red);
                result = false;
            }
            else if (!Servers.ContainsKey(serverName))
            {
                CommandLineInterface.OutLineFormat("Specified server name ({0}) was not found", ConsoleColor.Red, serverName);
                result = false;
            }

            return result;
        }

        private static void TryAction(Action<string> action, string args = "")
        {
            try
            {
                action(args);
            }
            catch (Exception ex)
            {
                CommandLineInterface.OutLineFormat(".Net exception was thrown:\r\n{0}\r\n", ConsoleColor.Red, ex.Message);
                if (!string.IsNullOrEmpty(ex.StackTrace))
                {
                    CommandLineInterface.OutLineFormat("{0}", ConsoleColor.Magenta, ex.StackTrace);
                }
            }
        }
    }
}
