using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using Brevitee;
using Brevitee.Dust;
using Brevitee.Web;
using Brevitee.CommandLine;
using Brevitee.Html;
using Brevitee.Testing;
using Brevitee.Encryption;
using Brevitee.Incubation;
using Brevitee.Server.Renderers;
using Brevitee.Server;
using Brevitee.ServiceProxy;
using Brevitee.Logging;
using Moq;

namespace Brevitee.Server.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTestInterface
    {
        [UnitTest]
        public void WhatDoesEndOfLineLookLike()
        {
            string test = @"this is the first
";
            string test2 = "this is the second\r\n";

            Expect.AreEqual("\n", test.Tail(1));
            Expect.AreEqual("\n", test2.Tail(1));
        }

        [UnitTest]
        public void HeadShouldFunctionAsDesigned()
        {
            string testString = "The Quick Brown Fox Jumps Over The Lazy Dog";
            string the;
            string rest = testString.Head(3, out the).Trim();

            Expect.AreEqual("The", the);
            Expect.AreEqual("Quick Brown Fox Jumps Over The Lazy Dog", rest);
        }

        [UnitTest]
        public void TailShouldFunctionAsDesigned()
        {
            string testString = "The Quick Brown Fox Jumps Over The Lazy Dog";
            string dog;
            string rest = testString.Tail(3, out dog).Trim();

            Expect.AreEqual("Dog", dog);
            Expect.AreEqual("The Quick Brown Fox Jumps Over The Lazy", rest);
        }

        [UnitTest]
        public void ShouldBeAbleToSetTheContentRoot()
        {
            BreviteeServer server = new BreviteeServer();//BreviteeServerFactory.Default.Create();
            string root = ".\\{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(4);
            server.ContentRoot = root;
            FileInfo validate = new FileInfo(root);
            string valid = validate.FullName.Replace("\\", "/") + "/";
            Expect.AreEqual(valid, server.ContentRoot);
            OutLine(server.ContentRoot);
        }
        
        [UnitTest]
        public void SettingContentRootShouldSetRootForRequestHandlerContent()
        {
            BreviteeServer server = new BreviteeServer();//BreviteeServerFactory.Default.Create();
            server.ContentRoot = ".\\{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(4);
            Expect.AreEqual(server.ContentRoot, server.RequestHandler.Content.Root);
        }

        [UnitTest]
        public void ShouldBeAbleToSetThePortAfterCreation()
        {
            BreviteeServer server = new BreviteeServer();
            string port = server.Port;
            OutLineFormat("port is currently {0}", ConsoleColor.Yellow, port);
            string newPort = RandomNumber.Between(8000, 8900).ToString();
            server.Port = newPort;
            Expect.AreEqual(newPort, server.Port);
            OutLineFormat("port is now {0}", ConsoleColor.Yellow, server.Port);
            Expect.IsFalse(newPort.Equals(port));
        }

        [UnitTest]
        public void StartShouldFireInitEvents()
        {
            BreviteeServer server = new BreviteeServer();
            bool? ingCalled = false;
            server.Initializing += (bs) =>
            {
                ingCalled = true;
            };

            bool? izedCalled = false;
            server.Initialized += (bs) =>
            {
                izedCalled = true;
            };

            Expect.IsFalse(ingCalled.Value);
            Expect.IsFalse(izedCalled.Value);

            server.Start();
            server.Stop();

            Expect.IsTrue(ingCalled.Value, "Initializing was not fired");
            Expect.IsTrue(izedCalled.Value, "Initialized was not fired");
            
        }

        [UnitTest]
        public void RequestHandlerSetShouldFireOnStart()
        {
            BreviteeServer server = new BreviteeServer();
            server.Port = "8989";
            server.ContentRoot = ".\\{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(4);
            server.Logger = new ConsoleLogger();

            bool? handlerSet = false;
            server.RequestHandlerSet += (handler) =>
            {
                handlerSet = true;
            };

            Expect.IsFalse(handlerSet.Value);
            server.Start();
            server.Stop();

            Expect.IsTrue(handlerSet.Value, "handlerSet should have been true");
        }

        [UnitTest]
        public void ConfigurationsShouldMatchBetweenServerAndRequestHandler()
        {
            BreviteeServer server = CreateServer("9090");
            Expect.AreEqual(server.Port, server.RequestHandler.BreviteeConf.Port, "Ports didn't match");
            Expect.AreEqual(server.MaxThreads, server.RequestHandler.BreviteeConf.MaxThreads, "MaxThreads didn't match");
            Expect.AreEqual(server.ContentRoot, server.RequestHandler.BreviteeConf.ContentRoot, "ContentRoot didn't match");
        }

        [UnitTest]
        public void FileSystemInitializationEventsShouldFireOnFirstRequest()
        {
            BreviteeServer server = CreateServer("9092");
            bool? ingFired = false;
            server.ContentResponder.FileSystemInitializing += (content) =>
            {
                ingFired = true;
            };

            bool? izedFired = false;
            server.ContentResponder.FileSystemInitialized += (content) =>
            {
                izedFired = true;
            };

            Expect.IsFalse(ingFired.Value);
            Expect.IsFalse(izedFired.Value);

            server.Start();
            try
            {
                string value = Http.GetString("http://localhost:9092");
            }catch(Exception ex)
            {
                // catch the 404 that may happen in the GET
            }

            Expect.IsTrue(ingFired.Value);
            Expect.IsTrue(izedFired.Value);

            server.Stop();
            Thread.Sleep(1000);
            Directory.Delete(server.ContentRoot, true);
        }

        [UnitTest]
        public void AppsInitializationEventsShouldFireOnFirstRequest()
        {
            BreviteeServer server = CreateServer("9093");
            bool? ingFired = false;
            server.ContentResponder.AppsInitializing += (content) =>
            {
                ingFired = true;
            };

            bool? izedFired = false;
            server.ContentResponder.AppsInitialized += (content) =>
            {
                izedFired = true;
            };

            Expect.IsFalse(ingFired.Value);
            Expect.IsFalse(izedFired.Value);

            server.Start();
            try
            {
                string value = Http.GetString("http://localhost:9093");
            }
            catch (Exception ex)
            {
                // catch the 404 that may happen in the GET
            }

            Expect.IsTrue(ingFired.Value);
            Expect.IsTrue(izedFired.Value);

            server.Stop();
            Thread.Sleep(1000);
            Directory.Delete(server.ContentRoot, true);
        }

        [UnitTest]
        public void TemplateInitializationEventsShouldFireOnFirstRequest()
        {
            BreviteeServer server = CreateServer("9090");
            bool? ingFired = false;
            server.ContentResponder.CommonDustRendererInitializing += (content) =>
            {
                ingFired = true;
            };

            bool? izedFired = false;
            server.ContentResponder.CommonDustRendererInitialized += (content) =>
            {
                izedFired = true;
            };

            Expect.IsFalse(ingFired.Value);
            Expect.IsFalse(izedFired.Value);

            server.Start();
            try
            {
                string value = Http.GetString("http://localhost:9090");
            }
            catch (Exception ex)
            {
                // catch the 404 that may happen in the GET
            }

            Expect.IsTrue(ingFired.Value);
            Expect.IsTrue(izedFired.Value);
            Out("Dust template contents: \r\n", ConsoleColor.Cyan);
            
            server.Stop();
            Thread.Sleep(1000);
            Directory.Delete(server.ContentRoot, true);
        }

        [UnitTest]
        public void AppContentConfShouldNotBeNull()
        {
            ContentResponder content = new ContentResponder(BreviteeConf.Load());
            AppContentResponder appContent = new AppContentResponder(content, "Monkey");
            Expect.IsNotNull(appContent.AppConf);
        }

        [UnitTest]
        public void AppContentNameShouldNotBeNull()
        {
            ContentResponder content = new ContentResponder(BreviteeConf.Load());
            AppContentResponder appContent = new AppContentResponder(content, "Monkey");
            Expect.IsNotNull(appContent.ApplicationName);
            Expect.AreEqual("Monkey", appContent.ApplicationName);
        }

        [UnitTest]
        public void ContentRootShouldMatchConf()
        {
            BreviteeConf conf = new BreviteeConf();
            DirectoryInfo root = new DirectoryInfo(".\\{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(5));
            conf.ContentRoot = root.FullName;
            ContentResponder content = new ContentResponder(conf);
            DirectoryInfo check = new DirectoryInfo(content.Root); // don't compare strings because the content flips backslashes with forward slashes
            Expect.AreEqual("{0}\\"._Format(root.FullName), check.FullName); // content.Root adds a trailing slash
        }

        [UnitTest]
        public void AppContentInitializeShouldSetupFiles()
        {
            BreviteeConf conf = new BreviteeConf();
            DirectoryInfo root = new DirectoryInfo(".\\{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(5));
            conf.ContentRoot = root.FullName;
            ContentResponder content = new ContentResponder(conf);
            AppConf appConf = new AppConf("monkey");            
            AppContentResponder appContent = new AppContentResponder(content, appConf);
            // should create the folder <conf.ContentRoot>\\apps\\monkey
            string appPath = Path.Combine(conf.ContentRoot, "apps", appConf.Name);
            Expect.IsFalse(Directory.Exists(appPath));
            appContent.Initialize();
            Expect.IsTrue(Directory.Exists(appPath));
            Directory.Delete(appPath, true);
        }

        [UnitTest]
        public void AppContentInitializeShouldCreateAppConfDotJson()
        {
            DirectoryInfo root = new DirectoryInfo(".\\{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(5));
            BreviteeConf conf = new BreviteeConf();
            conf.ContentRoot = root.FullName;
            ContentResponder content = new ContentResponder(conf);
            AppConf appConf = new AppConf("monkey");
            string layout = "".RandomLetters(4);
            appConf.DefaultLayout = layout;
            AppContentResponder appContent = new AppContentResponder(content, appConf);
            string appConfPath = Path.Combine(conf.ContentRoot, "apps", appConf.Name, "appConf.json");
            Expect.IsFalse(File.Exists(appConfPath));

            appContent.Initialize();

            Expect.IsTrue(File.Exists(appConfPath), "appConf.json did not get created");
            AppConf check = appConfPath.FromJsonFile<AppConf>();
            Expect.AreEqual(layout, check.DefaultLayout);

            root.Delete(true);
        }

        [UnitTest]
        public void DustTemplateRendererOutputStreamShouldNotBeNull()
        {
            DirectoryInfo root = new DirectoryInfo(".\\{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(5));
            ContentResponder content = GetTestContentResponder(root);
            CommonDustTemplateRenderer renderer = new CommonDustTemplateRenderer(content);
            Expect.IsNotNull(renderer.OutputStream);
        }

        [UnitTest]
        public void ShouldBeAbleToRenderDustTemplateForType()
        {
            DirectoryInfo root = new DirectoryInfo(".\\{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(5));
            ContentResponder content = GetTestContentResponder(root);
            DirectoryInfo dustRoot = new DirectoryInfo(Path.Combine(content.Root, "dust"));
            CommonDustTemplateRenderer templateRenderer = new CommonDustTemplateRenderer(content);
            TestMonkey monkey = new TestMonkey();
            
            Expect.IsFalse(File.Exists(Path.Combine(dustRoot.FullName, "TestMonkey.dust")), "Template was already there");

            // should render a template into the dustRoot folder
            templateRenderer.Render(monkey);

            Expect.IsTrue(File.Exists(Path.Combine(dustRoot.FullName, "TestMonkey.dust")), "Template was not written as expected");
        }

        [UnitTest]
        public void ShouldBeAbleToCompileSourceWithDustScript()
        {
            string source = "Hello {Name}";
            string compiled = DustScript.Compile(source, "test");
            OutLine(compiled);
        }

        [UnitTest]
        public void ShouldBeAbleToRenderWithDustScript()
        {
            string source = "Hello {Name}";
            string compiled = DustScript.Compile(source, "test");
            string output = DustScript.Render(compiled, "test", new { Name = "Bananas" });
            OutLine(output);
        }

        [UnitTest]
        public void ShouldBeAbleToRenderTemplateFromDirectory()
        {
            // create a test directory
            DirectoryInfo root = new DirectoryInfo(".\\{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(5));
            // write test templates into it
            string source = "Hello {Name}";
            source.SafeWriteToFile(Path.Combine(root.FullName, "test.dust"));
            // render something using the directory templates
            string output = DustScript.Render(root, "test", new { Name = "Gorilla" });
            OutLine(output);
        }

        [UnitTest]
        public void ShouldBeAbleToCompileDustDirectory()
        {
            DirectoryInfo root = new DirectoryInfo(".\\{0}_"._Format(MethodBase.GetCurrentMethod().Name).RandomLetters(5));
            ContentResponder content = GetTestContentResponder(root);
            DirectoryInfo dustRoot = new DirectoryInfo(Path.Combine(content.Root, "dust"));
            CommonDustTemplateRenderer templateRenderer = new CommonDustTemplateRenderer(content);
            TestMonkey monkey = new TestMonkey();
            templateRenderer.Render(monkey);
            AppContentResponder appResponder = new AppContentResponder(content, "Test");
            CommonDustRenderer renderer = new CommonDustRenderer(appResponder);
            Expect.IsTrue(!string.IsNullOrEmpty(renderer.CompiledDustTemplates));
            OutLine(renderer.CompiledDustTemplates);
        }


        [UnitTest]
        public void GenerateDaoInConfShouldMatchServerSetting()
        {
            BreviteeConf conf = BreviteeConf.Load();
            conf.GenerateDao = false;
            conf.Save(true);
            Expect.IsFalse(conf.GenerateDao);
            BreviteeServer server = new BreviteeServer();            
            server.GenerateDao = true;
            conf = server.GetCurrentConf();
            Expect.IsTrue(conf.GenerateDao);
            server.SaveConf(true);
            conf = BreviteeConf.Load();
            Expect.IsTrue(conf.GenerateDao);
        }

        class TestResponder: ResponderBase
        {
            public TestResponder() : base(null) { }
            public override bool TryRespond(IContext context)
            {
                throw new NotImplementedException();
            }
        }

        [UnitTest]
        public void MayRespondShouldBeTrueIfPrefixAdded()
        {
            Mock<IContext> ctx = CreateMockContext("http://blah.cxm/Monkey");

            TestResponder test = new TestResponder();
            test.AddRespondToPrefix("Monkey");

            Expect.IsTrue(test.MayRespond(ctx.Object));
        }

        private static Mock<IContext> CreateMockContext(string url)
        {
            Mock<IContext> mockContext = new Mock<IContext>();
            Mock<IContext> ctx = new Mock<IContext>();
            FakeRequest fakeRequest = new FakeRequest();
            fakeRequest.SetUrl(url);
            ctx.SetupProperty<IRequest>(c => c.Request, fakeRequest);
            return ctx;
        }

        class TestTemplateInitializer: TemplateInitializerBase
        {
            public TestTemplateInitializer(BreviteeServer server) : base(server) { }
            public bool InitializeCalled
            {
                get;
                set;
            }
            public override void Initialize()
            {
                InitializeCalled = true;
            }
        }

        [UnitTest]
        public void SettingConfigShouldReflectInServerSettings()
        {
            BreviteeServer server = CreateServer("4566", "{0}_Content"._Format(MethodBase.GetCurrentMethod().Name));
            BreviteeConf conf = new BreviteeConf();
            conf.GenerateDao = true;
            conf.InitializeTemplates = true;
            
            server.GenerateDao = false;
            server.InitializeTemplates = false;

            Expect.IsFalse(server.GenerateDao);
            Expect.IsFalse(server.InitializeTemplates);

            Expect.IsTrue(conf.GenerateDao);
            Expect.IsTrue(conf.InitializeTemplates);

            server.SetConf(conf);

            Expect.IsTrue(server.GenerateDao);
            Expect.IsTrue(server.InitializeTemplates);
        }

        [UnitTest]
        public void TemplateInitializerShouldBeCalled()
        {
            BreviteeServer server = CreateServer("3567", "{0}_Content"._Format(MethodBase.GetCurrentMethod().Name));
            BreviteeConf conf = new BreviteeConf();
            conf.InitializeTemplates = true;
            server.SetConf(conf);
            server.SaveConf(true);
            TestTemplateInitializer testInitializer = new TestTemplateInitializer(server);
            server.TemplateInitializer = testInitializer;
            Expect.IsFalse(testInitializer.InitializeCalled);
            server.Start();
            Expect.IsTrue(testInitializer.InitializeCalled);
            server.Stop();
        }


        [UnitTest]
        public void DaoResponderMayRespondToDaoProxies()
        {
            DaoResponder responder = new DaoResponder(BreviteeConf.Load());
            Mock<IContext> mockContext = CreateMockContext("http://blah.com/dao/proxies");
            Expect.IsTrue(responder.MayRespond(mockContext.Object));
        }

        class TestBam
        {
            public void GetPages()
            {
                if (Do != null)
                {
                    Do();
                }
            }

            public Action Do
            {
                get;
                set;
            }
        }
        [UnitTest]
        public void ExecutionRequestShouldBeValidForValidUrls()
        {
            ExecutionRequest req = new ExecutionRequest(null, null);
            req.ServiceProvider.Set<TestBam>(new TestBam());
            req.RequestUrl = new Uri("http://blah.com/get/TestBam/GetPages.json");
            req.ParseRequestUrl();

            ValidationResult result = req.Validate();

            Expect.IsTrue(result.Success);
            Expect.AreEqual("TestBam", req.ClassName);
            Expect.AreEqual("GetPages", req.MethodName);
            Expect.AreEqual("json", req.Ext);
        }

        [UnitTest]
        public void ShouldBeAbleToWrapDynamic()
        {
            string url = "http://blah.com";
            RequestWrapper req = new RequestWrapper(new { Url = new Uri(url) });
            Expect.AreEqual(url, req.Url.OriginalString);
        }

        [UnitTest]
        public void ProxyAliasesShouldNotBeNull()
        {
            string url = "http://blah.com/Monkey/GetPages.json";
            RequestWrapper req = new RequestWrapper(new { Url = new Uri(url), HttpMethod = "GET" });
            ResponseWrapper res = new ResponseWrapper(new object());
            ProxyAlias alias = new ProxyAlias("Monkey", typeof(TestBam));
            ExecutionRequest execRequest = new ExecutionRequest(req, res, new ProxyAlias[] { alias });
            Expect.IsNotNull(execRequest.ProxyAliases);
        }

        [UnitTest]
        public void ExecutionRequestShouldResolveClassAlias()
        {
            string url = "http://blah.com/Monkey/GetPages.json";
            RequestWrapper req = new RequestWrapper(new { Url = new Uri(url), HttpMethod = "GET" });
            ResponseWrapper res = new ResponseWrapper(new object());
            ProxyAlias alias = new ProxyAlias("Monkey", typeof(TestBam));
            ExecutionRequest execRequest = new ExecutionRequest(req, res, new ProxyAlias[] { alias });
            Expect.AreEqual("TestBam", execRequest.ClassName);
        }

        [UnitTest]
        public void ExecutionRequestShouldNotBeInitialized()
        {
            string url = "http://blah.com/Monkey/GetPages.json";
            RequestWrapper req = new RequestWrapper(new { Url = new Uri(url), HttpMethod = "GET" });
            ResponseWrapper res = new ResponseWrapper(new object());
            ProxyAlias alias = new ProxyAlias("Monkey", typeof(TestBam));
            ExecutionRequest execRequest = new ExecutionRequest(req, res, new ProxyAlias[] { alias });

            Expect.IsFalse(execRequest.IsInitialized);
        }

        [UnitTest]
        public void GetShouldReturnSameInstanceThatWasSet()
        {
            Incubator inc = new Incubator();
            TestBam test = new TestBam();
            inc.Set(typeof(TestBam), test);
            TestBam check = (TestBam)inc.Get(typeof(TestBam), new object[] { });
            Expect.AreSame(test, check);
            Expect.IsTrue(test == check);
        }

        [UnitTest]
        public void ExecutionRequestShouldBeValid()
        {
            string url = "http://blah.com/TestBam/GetPages.json";
            RequestWrapper req = new RequestWrapper(new { Url = new Uri(url), HttpMethod = "GET", ContentLength = 0, QueryString= new NameValueCollection() });
            ResponseWrapper res = new ResponseWrapper(new object());
            ExecutionRequest execRequest = new ExecutionRequest(req, res);
            execRequest.ServiceProvider.Set(typeof(TestBam), new TestBam());
            ValidationResult validation = execRequest.Validate();

            Expect.IsTrue(validation.Success);
        }

        [UnitTest]
        public void ShouldBeAbleToGetMethodCaseInsensitively()
        {
            Type bam = typeof(BreviteeApplicationManager);
            MethodInfo method = bam.GetMethod("getpages", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            Expect.IsNotNull(method);
        }
        class TestExecutor
        {
            public TestExecutor()
            {
                this.Value = "Test_".RandomLetters(6);
            }

            public string Value { get; set; }
            public string DoExecute()
            {
                return Value;
            }

            public string DoExecuteWithParameters(string input)
            {
                return input;
            }
        }
        [UnitTest]
        public void ExecutionRequestShouldExecute()
        {
            string url = "http://blah.com/TestExecutor/DoExecute.json";
            RequestWrapper req = new RequestWrapper(new { Url = new Uri(url), HttpMethod = "GET", ContentLength = 0, QueryString = new NameValueCollection() });
            ResponseWrapper res = new ResponseWrapper(new object());
            ExecutionRequest execRequest = new ExecutionRequest(req, res);
            TestExecutor execTarget = new TestExecutor();
            execRequest.ServiceProvider.Set(typeof(TestExecutor), execTarget);
            Expect.IsTrue(execRequest.Execute());
            Expect.AreEqual(execTarget.Value, execRequest.Result);
        }

        [UnitTest]
        public void ExecutionRequestShouldExecuteWithParameters()
        {
            string url = "/TestExecutor/DoExecuteWithParameters.json?input=bananas";
            ExecutionRequest execRequest = CreateExecutionRequest(url);
            TestExecutor execTarget = new TestExecutor();
            execRequest.ServiceProvider.Set(typeof(TestExecutor), execTarget);
            Expect.IsTrue(execRequest.Execute());
            Expect.AreEqual("bananas", execRequest.Result);
        }

        private static ExecutionRequest CreateExecutionRequest(string path)
        {
            Uri uri = new Uri("http://blah.com" + path);
            HttpArgs query = new HttpArgs(uri.Query);
            NameValueCollection nvc = new NameValueCollection();
            query.Keys.Each(k =>
            {
                nvc.Add(k, query[k]);
            });
            RequestWrapper req = new RequestWrapper(new { Url = uri, HttpMethod = "GET", ContentLength = 0, QueryString = nvc });
            ResponseWrapper res = new ResponseWrapper(new object());
            ExecutionRequest execRequest = new ExecutionRequest(req, res);
            return execRequest;
        }

        class SwitchTest
        {
            public string Do()
            {
                return "Yay it worked";
            }
        }
        [UnitTest]
        public void ShouldBeAbleToSwitchOutServiceProviderAndReExecute()
        {
            ExecutionRequest request = CreateExecutionRequest("/SwitchTest/Do.json");
            Incubator bad = new Incubator();
            request.ServiceProvider = bad;

            Expect.IsFalse(request.Execute());

            Incubator good = new Incubator();
            good.Set<SwitchTest>(new SwitchTest());
            request.ServiceProvider = good;

            Expect.IsTrue(request.Execute());
            Expect.AreEqual("Yay it worked", request.Result);
        }

        [UnitTest]
        public void ShouldBeAbleToSetProxyAliases()
        {
            BreviteeServer server = CreateServer("8990", "Test_"._Format(MethodBase.GetCurrentMethod().Name));
            BreviteeConf conf = new BreviteeConf();
            conf.AddProxyAlias("Test", typeof(TestResponder));
            Expect.AreEqual(1, conf.ProxyAliases.Length);
            server.SetConf(conf);
            Expect.IsTrue(server.ProxyAliases.Length == 1);

            Expect.AreEqual(server.ProxyAliases[0].ClassName, typeof(TestResponder).Name);
        }

        private static ContentResponder GetTestContentResponder(DirectoryInfo root)
        {
            BreviteeConf conf = new BreviteeConf();
            conf.ContentRoot = root.FullName;
            ContentResponder content = new ContentResponder(conf);
            return content;
        }

        internal static BreviteeServer CreateServer(string port, string rootDir = "")
        {
            BreviteeServer server = new BreviteeServer();
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.UseColors = true;
            server.Logger = logger;
            if (string.IsNullOrEmpty(rootDir))
            {
                rootDir = ".\\Test_".RandomLetters(5);
            }
            server.ContentRoot = rootDir;
            server.Port = port;
            server.SaveConf(true);
            return server;
        }

              

        private static DirectoryInfo CreateTestRoot(string directoryName = "")
        {
            if (string.IsNullOrEmpty(directoryName))
            {
                directoryName = ".\\".RandomLetters(6);
            }
            else if (!directoryName.StartsWith(".\\"))
            {
                directoryName = Path.Combine(".\\", directoryName);
            }
            DirectoryInfo test = new DirectoryInfo(directoryName);
            if (!test.Exists)
            {
                test.Create();
            }
            return test;
        }
        
        [UnitTest]
        public void ContentRootOfBreviteeServerShouldHaveDefault()
        {
            BreviteeServer server = new BreviteeServer();
            Expect.IsNotNull(server.ContentRoot);
            Expect.IsTrue(server.ContentRoot.Length > 0);
            OutLine(server.ContentRoot);
        }

        [UnitTest]
        public void TruncateFrontShouldDropLeadingCharacters()
        {
            string toTruncateFront = "12345ThisIsIt";
            Expect.AreEqual("ThisIsIt", toTruncateFront.TruncateFront(5));
        }


        [UnitTest]
        public void WhatDoesPathGetExtensionReturn()
        {
            string ext = Path.GetExtension("file.zip");
            Expect.AreEqual(".zip", ext);
            OutLine(ext);
        }

        [UnitTest]
        public void WhatDoesFileInfoExtensionReturn()
        {
            FileInfo file = new FileInfo("someFile.db.js");
            OutLine(file.Extension);
            FileInfo file2 = new FileInfo("somefile.db.json");
            OutLine(file2.Extension);
        }

        [UnitTest]
        public void WhatDoesUriEscapingReallyDo()
        {
            string fullUrl = "http://fake.cxm/monkey/doc.htm?gorilla=<div>baloney</div>&balls=tupac";
            
            OutLineFormat("Uri.EscapeDataString({0})", ConsoleColor.Cyan, fullUrl);
            OutLineFormat("Result: {0}", ConsoleColor.Yellow, Uri.EscapeDataString(fullUrl));
            Out();
            OutLineFormat("Uri.EscapeUriString({0})", ConsoleColor.Cyan, fullUrl);
            OutLineFormat("Result: {0}", ConsoleColor.Yellow, Uri.EscapeUriString(fullUrl));
            Out();
            string queryString = "?key1=value1&key2=value2&key4=#monkey&key5=me@you.com";
            OutLineFormat("Uri.EscapeDataString({0})", ConsoleColor.Blue, queryString);
            OutLineFormat("Result: {0}", ConsoleColor.Yellow, Uri.EscapeDataString(queryString));
            Out();
            OutLineFormat("Uri.EscapeUriString({0})", ConsoleColor.Blue, queryString);
            OutLineFormat("Result: {0}", ConsoleColor.Yellow, Uri.EscapeUriString(queryString));
        }

        [UnitTest]
        public void GetPagesShouldIncludeSubdirectories()
        {
            DirectoryInfo root = new DirectoryInfo(".\\Test_{0}"._Format(MethodBase.GetCurrentMethod().Name));
            if (root.Exists)
            {
                root.Delete(true);
            }

            if (!root.Exists)
            {
                root.Create();
            }

            DirectoryInfo appPages = new DirectoryInfo(Path.Combine(root.FullName, "apps", "test", "pages"));
            if (!appPages.Exists)
            {
                appPages.Create();
            }

            2.Times(i =>
            {
                CreateTestFile(appPages, i);
            });

            2.Times(i =>
            {
                DirectoryInfo subDir = CreateSubDir(appPages, i);
                CreateTestFile(subDir, 1);
                DirectoryInfo subSubDir = CreateSubDir(subDir, 1);
                CreateTestFile(subSubDir, 1);
            });

            BreviteeConf conf = new BreviteeConf();
            conf.ContentRoot = root.FullName;
            BreviteeApplicationManager mgr = new BreviteeApplicationManager(conf);
            string[] pageNames = mgr.GetPageNames("test");
            Expect.AreEqual(6, pageNames.Length);
            pageNames.Each(pn =>
            {
                OutFormat("{0}", ConsoleColor.Yellow, pn);
            });
        }

        private static DirectoryInfo CreateSubDir(DirectoryInfo dirToCreateSubDirIn, int i)
        {
            DirectoryInfo subDir = new DirectoryInfo(Path.Combine(dirToCreateSubDirIn.FullName, "Dir{0}"._Format(i)));
            if (!subDir.Exists)
            {
                subDir.Create();
            }

            return subDir;
        }

        private static void CreateTestFile(DirectoryInfo dirToCreateFileIn, int i)
        {
            string pageName = "page{0}.html"._Format(i);
            "Test{0}"._Format(i).SafeWriteToFile(Path.Combine(dirToCreateFileIn.FullName, pageName), true);
        }

        [UnitTest]
        public void FileExtensionShouldBeBlank()
        {
            string path = "banana\\republic";
            Expect.AreEqual("republic", Path.GetFileName(path));
            Expect.IsTrue(string.IsNullOrEmpty(Path.GetExtension(path)));
        }


        [UnitTest]
        public void SubscribeShouldIncrementSubscribers()
        {
            BreviteeServer server = CreateServer("8080", MethodBase.GetCurrentMethod().Name);
            ILogger logger = new TextFileLogger();
            Expect.AreEqual(0, server.Subscribers.Length);
            server.Subscribe(logger);
            Expect.AreEqual(1, server.Subscribers.Length);
            server.Subscribe(logger);
            Expect.AreEqual(1, server.Subscribers.Length); // should only get added once
            ILogger winLogger = new WindowsLogger();
            server.Subscribe(winLogger);
            Expect.AreEqual(2, server.Subscribers.Length);
            Expect.IsTrue(server.IsSubscribed(winLogger));
        }

        [UnitTest]
        public void SettingLoggerShouldCauseServerToReinitializeIfItsRunning()
        {
            bool? stopped = false;
            bool? initialized = false;
            BreviteeServer server = CreateServer("9578", MethodBase.GetCurrentMethod().Name);
            Expect.IsFalse(server.IsRunning);
            server.Stopped += (s) =>
            {
                stopped = true;
            };
            server.Start();
            server.Initialized += (s) =>
            {
                initialized = true;
            };
            Expect.IsFalse(initialized.Value, "Initialized should have been false");
            Expect.IsFalse(stopped.Value, "Stopped should have been false");
            server.Logger = new TextFileLogger();
            Expect.IsTrue(initialized.Value, "Initialized should have been true");
            Expect.IsTrue(stopped.Value, "Stopped should have been true");
        }

        [UnitTest]
        public void ServerShouldLoadConfOnInitialize()
        {
            BreviteeServer server = CreateServer("8990", MethodBase.GetCurrentMethod().Name);
            bool? ingCalled = false;
            bool? edCalled = false;
            server.LoadingConf += (s, c) =>
            {
                ingCalled = true;
            };

            server.LoadedConf += (s, c) =>
            {
                edCalled = true;
            };

            Expect.IsFalse(ingCalled.Value);
            Expect.IsFalse(edCalled.Value);

            server.Initialize();

            Expect.IsTrue(ingCalled.Value);
            Expect.IsTrue(edCalled.Value);
        }
    }
}
