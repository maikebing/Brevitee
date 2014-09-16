using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Brevitee.Logging;
using Brevitee.CommandLine;
using Brevitee.Configuration;
using Brevitee;

namespace Brevitee.Testing
{
    [Serializable]
    public abstract class CommandLineTestInterface: CommandLineInterface
    {
        static CommandLineTestInterface()
        {
            InitLogger();
        }
    
        public const string UnitTestAttribute = "UnitTestAttribute";

        protected static ILogger logger;
        public static LogEntryAddedListener MessageToConsole;
        protected static bool interactive;

        protected static MethodInfo DefaultMethod { get; set; }

        protected static void Initialize(string[] args)
        {
            ArgsParsedError += delegate(ParsedArguments a)
            {
                throw new ArgumentException(a.Message);
            };

            ArgsParsed += new ConsoleArgsParsedDelegate(ArgumentParsingComplete);

            interactive = false;

            AddValidArgument("i", true, "Run interactively");
            AddValidArgument("?", true, "Show usage");

            ParseArgs(args);
            int? exitCode = 0;
            TestFailed += (o, t) =>
            {
                Out("Test Failed: " + t.ConsoleInvokeableMethod.Information + "\r\n", ConsoleColor.Red);
                Out(t.Exception.Message, ConsoleColor.Magenta);
                Out();
                Out(t.Exception.StackTrace, ConsoleColor.Red);
                Out("---", ConsoleColor.Red);
                Out();
                exitCode = 1;
            };

            if (Arguments.Contains("?"))
            {
                Usage(Assembly.GetEntryAssembly());
                Exit(exitCode.Value);
            }
            else if (!Arguments.Contains("i"))
            {
                if (DefaultMethod != null)
                {
                    Expect.IsTrue(DefaultMethod.IsStatic, "DefaultMethod must be static.");
                    if (DefaultMethod.GetParameters().Length > 0)
                    {
                        DefaultMethod.Invoke(null, new object[] { Arguments });
                    }
                    else
                    {
                        DefaultMethod.Invoke(null, null);
                    }
                    return;
                }
                else
                {
                    RunAllTests(Assembly.GetEntryAssembly());
                    return;
                }
            }

            Interactive();
        }

        protected static void InitLogger()
        {
            logger = Log.CreateLogger(typeof(ConsoleLogger));
            ((ConsoleLogger)logger).UseColors = true;
            logger.EntryAdded += new LogEntryAddedListener(logger_EntryAdded);
        }

        protected static void Interactive()
        {
            AddMenu(Assembly.GetEntryAssembly(), "Main", 'm', new ConsoleMenuDelegate(ShowMenu));
            AddMenu(Assembly.GetEntryAssembly(), "Test", 't', new ConsoleMenuDelegate(TestMenu));

            ShowMenu(Assembly.GetEntryAssembly(), OtherMenus.ToArray(), "Main");
        }

        protected static void MainMenu(string header)
        {
            AddMenu(Assembly.GetEntryAssembly(), header, 'm', new ConsoleMenuDelegate(ShowMenu));

            ShowMenu(Assembly.GetEntryAssembly(), OtherMenus.ToArray(), header);
        }

        private static void ArgumentParsingComplete(ParsedArguments arguments)
        {
            if (arguments.Contains("i"))
                interactive = true;
        }


        protected static void Pass()
        {
            Pass("");
        }

        protected static void Pass(string text)
        {
            OutLineFormat("{0}:Passed", ConsoleColor.Green, text);
        }

        public static void TestMenu(Assembly assemblyToAnalyze, ConsoleMenu[] otherMenus, string header)
        {
            Console.WriteLine(header);
            List<ConsoleInvokeableMethod> tests = GetTests(assemblyToAnalyze);
            ShowActions(tests);
            Console.WriteLine();
            Console.WriteLine("Q to quit\ttype all to run all tests.");
            string answer = ShowSelectedMenuOrReturnAnswer(otherMenus);
            Console.WriteLine();

            try
            {
                answer = answer.Trim().ToLowerInvariant();
                string[] individuals = answer.DelimitSplit(",", true);
                string[] range = answer.DelimitSplit("-", true);
                if (answer.Equals("all"))
                {
                    bool enterManually = true;
                    if (interactive)
                    {
                        enterManually = Confirm("Would you like to enter parameters manually? [y][N]");
                    }
                    if (enterManually)
                    {
                        RunAllTestsInteractively(assemblyToAnalyze);
                    }
                    else
                    {
                        RunAllTests(assemblyToAnalyze);
                    }
                }
                else if(range.Length == 2)
                {
                    int first = Convert.ToInt32(range[0]);
                    int end = Convert.ToInt32(range[1]);
                    List<string> answers = new List<string>();
                    for (int i = first; i <= end; i++)
                    {
                        answers.Add(i.ToString());
                    }
                    answers.Each(num =>
                    {
                        RunTest(tests, num);
                    });
                }
                else if (individuals.Length > 1)
                {
                    individuals.Each(num =>
                    {
                        RunTest(tests, num);
                    });
                }
                else
                {
                    RunTest(tests, answer);
                }
            }
            catch (Exception ex)
            {                
                Error("An error occurred running tests", ex);                
            }

            if (Confirm("Return to the Test menu? [y][N]"))
            {
                TestMenu(assemblyToAnalyze, otherMenus, header);
            }
            else
            {
                Exit(0);
            }
        }

        protected static void RunTest(List<ConsoleInvokeableMethod> tests, string answer)
        {
            int selectedNumber = -1;
            try
            {
                ConsoleInvokeableMethod test;
                string testName = answer;
                if (int.TryParse(answer.ToString(), out selectedNumber) && (selectedNumber - 1) > -1 && (selectedNumber - 1) < tests.Count)
                {
                    test = tests[selectedNumber - 1];
                    testName = "({0}) {1}"._Format(answer, test.Information);

                    StringBuilder header = new StringBuilder();
                    header.AppendFormat("******* Starting Test {0} ********\r\n", testName);
                    StringBuilder footer = new StringBuilder();
                    footer.AppendFormat("******* Finished Test {0} ********\r\n", testName);

                    InvokeSelection(tests, header.ToString(), footer.ToString(), selectedNumber);
                }
                else
                {
                    Console.WriteLine("Invalid entry");
                    Environment.Exit(1);
                }

                Pass(testName);
            }
            catch (Exception ex)
            {
                OnTestFailed(tests[selectedNumber - 1], ex);
            }            
        }

        protected static void RunAllTests(Assembly assemblyToAnalyze)
        {
            RunAllTests(assemblyToAnalyze, true);
        }

        protected static void RunAllTestsInteractively(Assembly assemblyToAnalyze)
        {
            RunAllTests(assemblyToAnalyze, false);
        }

        /// <summary>
        /// Event fires when a test fails.
        /// </summary>
        public static event EventHandler<TestExceptionEventArgs> TestFailed;

	    public static event EventHandler<ConsoleInvokeableMethod> TestPassed;

        protected static void RunAllTests(Assembly assemblyToAnalyze, bool generateParameters)
        {
            List<ConsoleInvokeableMethod> tests = GetTests(assemblyToAnalyze);
            if (tests.Count == 0)
            {
                Info("No tests were found in {0}", assemblyToAnalyze.FullName);
                return;
            }

            Info("Running all tests in {0}", assemblyToAnalyze.FullName);
            int passedCount = 0;
            int failedCount = 0;
            foreach (ConsoleInvokeableMethod consoleMethod in tests)
            {
                try
                {
                    InvokeTest(consoleMethod, generateParameters);
                    Pass(consoleMethod.Method.Name.PascalSplit(" "));
                    passedCount++;
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        ex = ex.InnerException;

                    OnTestFailed(consoleMethod, ex);
                    failedCount++;
                }
            }
            Out();
            OutLine("********");
            if (failedCount > 0)
            {
                OutLineFormat("({0}) tests passed", ConsoleColor.Green, passedCount);
                OutLineFormat("({0}) tests failed", ConsoleColor.Red, failedCount);
            }
            else
            {
                OutLineFormat("All ({0}) tests passed", ConsoleColor.Green, passedCount);
            }
            OutLine("********");
        }

	    private static void OnTestPassed(ConsoleInvokeableMethod consoleMethod) 
		{
		    if (TestPassed != null) {
			    TestPassed(null, consoleMethod);}
	    }

        private static void OnTestFailed(ConsoleInvokeableMethod consoleMethod, Exception ex)
        {
            if (TestFailed != null)
            {
                TestFailed(null, new TestExceptionEventArgs(consoleMethod, ex));
            }
        }

        private static void InvokeTest(ConsoleInvokeableMethod consoleMethod)
        {
            InvokeTest(consoleMethod, false);
        }

        private static void InvokeTest(ConsoleInvokeableMethod consoleMethod, bool generateParameters)
        {
            object[] parameters = GetParameterInput(consoleMethod.Method, generateParameters);
            MethodInfo invoke = typeof(ConsoleInvokeableMethod).GetMethod("Invoke");
            if (consoleMethod.Method.IsStatic)
            {
                InvokeInSeparateAppDomain(invoke, consoleMethod);           
            }
            else
            {
                string typeName = consoleMethod.Method.DeclaringType.Name;
                ConstructorInfo ctor = consoleMethod.Method.DeclaringType.GetConstructor(Type.EmptyTypes);
                if (ctor == null)
                    ExceptionHelper.ThrowInvalidOperation("The declaring type {0} of method {1} does not have a parameterless constructor, test cannot be run.", typeName, consoleMethod.Method.Name);

                object instance = ctor.Invoke(null);
                Expect.IsNotNull(instance, string.Format("Unable to instantiate declaring type {0} of method {1}", typeName, consoleMethod.Method.Name));

                consoleMethod.Provider = instance;
                InvokeInSeparateAppDomain(invoke, consoleMethod);
            }
        }

        static void logger_EntryAdded(string applicationName, LogEvent logEvent)
        {
            if (MessageToConsole != null)
                MessageToConsole(applicationName, logEvent);

            if (logEvent.Severity == LogEventType.Fatal)
            {
                Environment.Exit(1);
            }
        }

        public static List<ConsoleInvokeableMethod> GetTests(Assembly assemblyToAnalyze )
        {
            Type[] testMethodAttributes = GetTestMethodAttributeTypes();
            List<ConsoleInvokeableMethod> tests =  new List<ConsoleInvokeableMethod>();
            foreach (Type testMethodAttribute in testMethodAttributes)
            {
                 tests.AddRange(GetActions(assemblyToAnalyze, testMethodAttribute));
            }
            tests.Sort((l, r) => l.Information.CompareTo(r.Information));
            return tests;
        }

        private static Type[] GetTestMethodAttributeTypes()
        {
            List<Type> returnValues = new List<Type>();
            returnValues.Add(typeof(UnitTest));

            string attrName = DefaultConfiguration.GetAppSetting(UnitTestAttribute);
            Type fromConfig = Type.GetType(attrName);
            if (fromConfig != null)
            {
                returnValues.Add(fromConfig);
            }
            
            return returnValues.ToArray();
        }

        public static void Info(string message)
        {
            Info(message, new object[] { });
        }
        /// <summary>
        /// Outputs an information message to the console.
        /// </summary>
        /// <param name="message">The message text to output.</param>
        public static void Info(string messageSignature, params object[] signatureVariableValues)
        {
            //List<string> variableValues = ToStringArray(signatureVariableValues);
            logger.AddEntry(messageSignature, ToStringArray(signatureVariableValues));
        }

        private static string[] ToStringArray(object[] signatureVariableValues)
        {
            List<string> variableValues = new List<string>(signatureVariableValues.Length);
            foreach (object obj in signatureVariableValues)
            {
                variableValues.Add(obj.ToString());
            }
            return variableValues.ToArray();
        }

        public static void Warn(string message)
        {
            Warn(message, new object[] { });
        }
        /// <summary>
        /// Outputs a warning to the console.
        /// </summary>
        /// <param name="message">The message text to output</param>
        public static void Warn(string messageSignature, params object[] signatureVariableValues)
        {
            logger.AddEntry(messageSignature, LogEventType.Warning, ToStringArray(signatureVariableValues));
            logger.BlockUntilEventQueueIsEmpty();
            logger.RestartLoggingThread();
        }

        public static void Error(string message, Exception ex)
        {
            Error(message, ex, new object[] { });
        }
        /// <summary>
        /// Outputs an error to the console.
        /// </summary>
        /// <param name="message">The message text to output</param>
        /// <param name="ex">The Exception that occurred.</param>
        public static void Error(string messageSignature, Exception ex, params object[] signatureVariableValues)
        {
            logger.AddEntry(messageSignature, ex, ToStringArray(signatureVariableValues));
            logger.BlockUntilEventQueueIsEmpty();
            logger.RestartLoggingThread();
        }

        public static void Fatal(string message, Exception ex)
        {
            Fatal(message, ex, new object[] { });
        }
        /// <summary>
        /// Outputs an error to the console and exits.
        /// </summary>
        /// <param name="message">The message to output.</param>
        /// <param name="ex">The Exception that occurred.</param>
        public static void Fatal(string messageSignature, Exception ex, params object[] signatureVariableValues)
        {
            logger.AddEntry(messageSignature, LogEventType.Fatal, ex, ToStringArray(signatureVariableValues));
            logger.BlockUntilEventQueueIsEmpty();
            logger.RestartLoggingThread();
            Exit(1);
        }


    }
}
