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
using Brevitee.Drawing;
using Brevitee.Testing;
using Brevitee.Encryption;
using Brevitee.Yaml;
using Brevitee.Logging;
//using Brevitee.Hatagi.Data;
using Brevitee.Data;
using Brevitee.Data.MsSql;

using Moq.Proxy;
using Moq;
using Moq.Language;
using Moq.Linq;
using Moq.Matchers;
using Moq.Properties;
using Moq.Protected;

using Brevitee.Server;
using Brevitee.ServiceProxy;

namespace Bam.Tests
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

        [ConsoleAction("Ensure DaoRef Schema")]
        public void EnsureDaoRefSchema()
        {
            SqlClientRegistrar.Register<Brevitee.DaoRef.TestTable>();
            Db.EnsureSchema<Brevitee.DaoRef.TestTable>();
        }

        string _filePath = "C:\\files.txt";
        [ConsoleAction("Get File List")]
        public void GetFileList()
        {
            DirectoryInfo daoDir = new DirectoryInfo("C:\\Dao");
            FileInfo[] files = daoDir.GetFiles();
            
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];
                string.Format("{0}\r\n", file.Name).SafeAppendToFile(_filePath);
            }
        }

        [ConsoleAction("Copy Dao files")]
        public void CopyDaoFiles()
        {
            string[] files = null;
            using (StreamReader sr = new StreamReader(_filePath))
            {
                string text = sr.ReadToEnd();
                files = text.DelimitSplit("\r\n");
            }

            if(Directory.Exists("C:\\shared\\Dao"))
            {
                Directory.Delete("C:\\shared\\Dao", true);
            }

            Directory.CreateDirectory("C:\\shared\\Dao");
            Directory.CreateDirectory("C:\\shared\\Dao\\Qi");

            foreach (string file in files)
            {
                string src = string.Format("C:\\Dao\\{0}", file);
                string dst = string.Format("C:\\shared\\Dao\\{0}", file);
                File.Copy(src, dst);
            }
        }

        class FakeRequest : IRequest
        {
            public void SetUrl(string value)
            {
                this.Url = new Uri(value);
            }

            #region IRequest Members

            public string[] AcceptTypes
            {
                get;
                set;
            }

            public Encoding ContentEncoding
            {
                get;
                set;
            }

            public long ContentLength64
            {
                get;
                set;
            }

            public string ContentType
            {
                get;
                set;
            }

            public System.Net.CookieCollection Cookies
            {
                get;
                set;
            }

            public bool HasEntityBody
            {
                get;
                set;
            }

            public System.Collections.Specialized.NameValueCollection Headers
            {
                get;
                set;
            }

            public string HttpMethod
            {
                get;
                set;
            }

            public Stream InputStream
            {
                get;
                set;
            }

            public Uri Url
            {
                get;
                set;
            }

            public Uri UrlReferrer
            {
                get;
                set;
            }

            public string UserAgent
            {
                get;
                set;
            }

            public string UserHostAddress
            {
                get;
                set;
            }

            public string UserHostName
            {
                get;
                set;
            }

            public string[] UserLanguages
            {
                get;
                set;
            }

            public string RawUrl
            {
                get;
                set;
            }

            #endregion


            public int ContentLength
            {
                get { throw new NotImplementedException(); }
            }

            public System.Collections.Specialized.NameValueCollection QueryString
            {
                get { throw new NotImplementedException(); }
            }

            string[] IRequest.AcceptTypes
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            Encoding IRequest.ContentEncoding
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            long IRequest.ContentLength64
            {
                get { throw new NotImplementedException(); }
            }

            int IRequest.ContentLength
            {
                get { throw new NotImplementedException(); }
            }

            System.Collections.Specialized.NameValueCollection IRequest.QueryString
            {
                get { throw new NotImplementedException(); }
            }

            string IRequest.ContentType
            {
                get { throw new NotImplementedException(); }
            }

            System.Net.CookieCollection IRequest.Cookies
            {
                get { throw new NotImplementedException(); }
            }

            System.Collections.Specialized.NameValueCollection IRequest.Headers
            {
                get { throw new NotImplementedException(); }
            }

            string IRequest.HttpMethod
            {
                get { throw new NotImplementedException(); }
            }

            Stream IRequest.InputStream
            {
                get { throw new NotImplementedException(); }
            }

            Uri IRequest.Url
            {
                get { throw new NotImplementedException(); }
            }

            Uri IRequest.UrlReferrer
            {
                get { throw new NotImplementedException(); }
            }

            string IRequest.UserAgent
            {
                get { throw new NotImplementedException(); }
            }

            string IRequest.UserHostAddress
            {
                get { throw new NotImplementedException(); }
            }

            string IRequest.UserHostName
            {
                get { throw new NotImplementedException(); }
            }

            string[] IRequest.UserLanguages
            {
                get { throw new NotImplementedException(); }
            }

            string IRequest.RawUrl
            {
                get { throw new NotImplementedException(); }
            }
        }

        [UnitTest]
        public void ExecutionMayHandleAnyRequestNotSpecificallyAddressedToItself()
        {
            Mock<IHttpContext> ctx = new Mock<IHttpContext>();
            ctx.SetupProperty<IRequest>(c => c.Request, new FakeRequest());
            
            After.Setup((context) =>
            {
                context.Set<IHttpContext>(ctx.Object);
                SetupCtorParamsForExecutionClass(context);
            })
            .WhenA<ServiceProxyResponder>("execution MayHandle a request", (e) =>
            {
                FakeRequest request = (FakeRequest)ctx.Object.Request;
                request.SetUrl("http://blah.cxm/Execution");
                Expect.IsFalse(e.MayRespond(ctx.Object), "MayHandle should have been false");
                request.SetUrl("http://blah.cxm/AnythingElse");
                Expect.IsTrue(e.MayRespond(ctx.Object));
            })
            .TheTest
            .ShouldPass(because =>
            {
                because.ItsTrue("no exceptions were thrown", true, "Expect calls failed");
            })
            .SoBeHappy()
            .UnlessItFailed();
        }

        class TestType
        {

        }

        [UnitTest()]
        public void ExecutionDotAddShouldIncrementExecutors()
        {
            After.Setup((context) =>
            {
                SetupCtorParamsForExecutionClass(context);
            })
            .WhenA<ServiceProxyResponder>("has a type added", (e) =>
            {
                e.AddCommonService(typeof(TestType), new TestType());
            })
            .TheTest
            .ShouldPass(because =>
            {
                because.ItsTrue("the executors count was incremented", // success message
                    because.ObjectUnderTest<ServiceProxyResponder>().Services.Length == 1, // the assertion
                    "Executors count didn't go up"); // failure message
            })
            .SoBeHappy()
            .UnlessItFailed();
        }

        private static void SetupCtorParamsForExecutionClass(SetupContext context)
        {
            context.Set<ILogger>(new Mock<ILogger>().Object); // needed for the ctor of Execution
            context.Set<Fs>(new Mock<Fs>(".").Object); // needed for the ctor of Execution
        }

        [UnitTest]
        public void ExecutionDotRemoveShouldDecrementExecutors()
        {
            ServiceProxyResponder testObject = null;
            After.Setup((ctx) =>
            {
                SetupCtorParamsForExecutionClass(ctx);
                ctx.Get<ServiceProxyResponder>().AddCommonService(new TestType());
                testObject = ctx.Get<ServiceProxyResponder>();
                Expect.IsNotNull(testObject);
                Expect.AreEqual(1, testObject.Services.Length);
            })
            .WhenA<ServiceProxyResponder>("has its remove method called", (o) =>
            {
                o.RemoveCommonService<TestType>();
            })
            .TheTest
            .ShouldPass(because =>
            {
                because.ItsTrue("the Executors length was 0", 
                    testObject.Services.Length == 0, 
                    "the Executors length was not 0");                
            }).SoBeHappy()
            .UnlessItFailed();
        }
        
//        [UnitTest]
//        public void FromYamlArray()
//        {
//            string yaml = @"%YAML 1.2
//---
//!Tbl
//Name: Monkey
//Cols: 
//  - Empty: True
//    Type: String
//    Name: first
//  - Empty: True
//    Type: String
//    Name: last
//Fks: []
//...
//%YAML 1.2
//---
//!Tbl
//Name: Tree
//Cols:
//  - Name: Height
//    Type: Long
//Fks:
//  - Name: monkeyId
//    Ref: Monkey
//...";
//            Tbl[] tbls = yaml.ArrayFromYaml<Tbl>();
//            Expect.AreEqual(2, tbls.Length);
//            foreach (Tbl tbl in tbls)
//            {
//                Out(tbl.Name, ConsoleColor.Cyan);
//            }
//        }

        //[UnitTest("ExecutionRequest should return empty string array for chunks when the path is /")]
        //public void ExecutionRequestShouldReturnEmptyStringArray()
        //{
        //    Mock<IContext> ctx = GetMockContext();

        //    When.A<ExecutionRequest>("calls GetChunksAndValidate", (r) =>
        //    {
        //        return r.GetChunksAndValidate(ctx.Object);
        //    })
        //    .TheTest
        //    .ShouldPass(because =>
        //    {
        //        string[] result = because.Result as string[];
        //        because.ItsTrue("The result was a string array", result != null, "The result was not a string array");
        //        because.ItsTrue("The results length was 0", result.Length == 0, string.Format("The length was ({0}) and not 0", result.Length));
        //    })
        //    .SoBeHappy()
        //    .UnlessItFailed();
        //}

        private static Mock<IHttpContext> GetMockContext(FakeRequest request = null)
        {
            Mock<IHttpContext> ctx = new Mock<IHttpContext>();
            if (request == null)
            {
                request = GetFakeRequest();
            }
            ctx.SetupProperty<IRequest>(c => c.Request, request);
            return ctx;
        }

        private static FakeRequest GetFakeRequest(string url = "http://localhost:8888")
        {
            FakeRequest request = new FakeRequest();
            request.SetUrl(url);
            return request;
        }

        class TestColorScheme : ColorScheme
        {
            bool _init = false;
            public bool InitWasCalled
            {
                get { return _init; }
                set { _init = value; }
            }
            internal override void Init()
            {
                InitWasCalled = true;
            }
        }

        [UnitTest("Instantiating a new ColorScheme should call Init()")]
        public void InstantiatingColorSchemeShouldCallInit()
        {
            TestColorScheme testObj = new TestColorScheme();
            Expect.IsTrue(testObj.InitWasCalled);
        }

        [UnitTest("ColorScheme.LoadDefault should set Fs")]
        public void ColorSchemLoadShouldSetFs()
        {
            string root = ".\\Tests\\".RandomLetters(4);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(string.Format("{0}\\content\\images", root));                
            }

            Fs fs = new Fs(root);            

            ColorScheme temp = new ColorScheme("Test", fs);
            temp.SaveDefault();

            When.A<ColorScheme>("is loaded using LoadDefault", (cs) =>
            {
                ColorScheme sche = ColorScheme.LoadDefault(fs);
                Expect.AreEqual(fs, sche.Fs);
                return sche;
            })
            .TheTest
            .ShouldPass((because) =>
            {                
                because.ResultIs<ColorScheme>();
                ColorScheme o = (ColorScheme)because.Result;
                because.ItsTrue("the Fs property was not null", o.Fs != null, "Fs property was null");
            })
            .SoBeHappy((scripts) =>
            {
                Directory.Delete(root, true);
            })
            .UnlessItFailed();
        }

        class FakeClass
        {
            public void Method()
            {
            }
        }

        //class FakeExecutionRequest : ExecutionRequest
        //{
        //    public void TestSetContext(IContext context)
        //    {
        //        base.SetContext(context);
        //    }
        //}

        //[UnitTest("Set context should keep extension chain")]
        //public void CallingGetChunksAndValidateShouldCallSetMethodAndExt()
        //{
        //    FakeRequest fr = GetFakeRequest("http://host/FakeClass/Method.json.html");
        //    Mock<IContext> mockCtx = GetMockContext(fr);
        //    FakeExecutionRequest testObject = new FakeExecutionRequest();
        //    testObject.TestSetContext(mockCtx.Object);


        //}

    }

}
