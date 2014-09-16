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
using System.Net;
using Brevitee.Configuration;
using Brevitee.Encryption;
using Brevitee.CommandLine;
using Brevitee.Incubation;
using Brevitee;
using Brevitee.Data;
using Brevitee.Testing;
using Brevitee.Javascript;
using Brevitee.Server;
using Brevitee.ServiceProxy;
using Brevitee.ServiceProxy.Secure;
using Brevitee.Logging;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using FakeItEasy;
using FakeItEasy.Creation;
using System.Reflection;

namespace Brevitee.ServiceProxy.Tests
{
    public partial class Program
    {
        static Program()
        {
            FilesToDelete = new List<string>();
        }

        public static void CleanUp()
        {
            FilesToDelete.Each(file =>
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            });

            Prepare();
            ApiKeyCollection keys = ApiKey.LoadAll();
            keys.Delete();
            SecureSessionCollection sessions = SecureSession.LoadAll();
            sessions.Delete();

            ApplicationCollection all = Application.LoadAll();
            all.Delete();
        }

        public void EnsureRepository()
        {
            ConsoleLogger logger = new ConsoleLogger();
            SecureChannel.EnsureRepository(logger);
        }

        [UnitTest(Before="EnsureRepository", AlwaysAfter="CleanUp")]
        public void SecureExecutionRequest_ShouldExecute()
        {
            IHttpContext context = CreateFakeContext(MethodInfo.GetCurrentMethod().Name);

            string input = "monkey";
            string jsonParams = ApiParameters.ParametersToJsonParamsArray(new object[] { input }).ToJson();

            Incubator testIncubator = new Incubator();
            testIncubator.Set<Echo>(new Echo());
            SecureExecutionRequest request = new SecureExecutionRequest(context, "Echo", "Send", jsonParams);
            request.ServiceProvider = testIncubator;

            AesKeyVectorPair kvp = new AesKeyVectorPair();
            // ensure the symettric key is set
            request.Session.SetSymmetricKey(kvp.Key, kvp.IV);
            // 

            request.Execute();

            Expect.IsTrue(request.Result.GetType() == typeof(string)); // should be base64 cipher of json result

            string result = request.GetResultAs<string>();
            Expect.AreEqual(input, result);
        }

        private static List<string> FilesToDelete
        {
            get;
            set;
        }

        private static IHttpContext CreateFakeContext(string sessionName)
        {
            IHttpContext context = A.Fake<IHttpContext>();
            IResponse response = A.Fake<IResponse>();
            response.Headers = new WebHeaderCollection();
            response.Headers[ServiceProxySystem.SecureSessionName] = sessionName;
            context.Request = A.Fake<IRequest>();
            context.Response = A.Fake<IResponse>();
            string fileName = MethodInfo.GetCurrentMethod().Name.RandomLetters(6);
            
            FileInfo testFile = new FileInfo(fileName);
            FilesToDelete.Add(testFile.FullName);
            "some junk".SafeWriteToFile(testFile.FullName, true);

            A.CallTo(() => context.Request.InputStream).Returns(new FileStream(testFile.FullName, FileMode.OpenOrCreate, FileAccess.Read));
            A.CallTo(() => context.Request.Url).Returns(new Uri("http://localhost:8080/POST/SecureChannel/Invoke.json?"));
            return context;
        }
    }
}
