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
using System.Net;
using System.CodeDom.Compiler;
using Brevitee.Configuration;
using Brevitee.Encryption;
using Brevitee.CommandLine;
using Brevitee.Incubation;
using Brevitee;
using Brevitee.Logging;
using Brevitee.Data;
using Brevitee.Data.Schema;
using Brevitee.Testing;
using Brevitee.Javascript;
using Brevitee.Server;
using Brevitee.ServiceProxy;
using Brevitee.ServiceProxy.Secure;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Engines;
using FakeItEasy;
using FakeItEasy.Creation;
using Brevitee.Web;

namespace Brevitee.ServiceProxy.Tests
{
    public partial class Program
    {
        [UnitTest]
        public void GetProxyiedMethodsShouldHaveResults()
        {
            MethodInfo[] methods = ServiceProxySystem.GetProxiedMethods(typeof(EncryptedEcho));
            Expect.IsGreaterThan(methods.Length, 0, "expected more than zero methods");
        }

        [UnitTest(AlwaysAfter = "StopServers")]
        public void ShouldBeAbleToDownloadAndCompileCSharpProxy()
        {
            BreviteeServer server;
            SecureServiceProxyClient<EncryptedEcho> sspc;
            StartSecureChannelTestServerGetEncryptedEchoClient(out server, out sspc);

            string value = Http.Get("http://localhost:8080/ServiceProxy/CSharpProxies?namespace=My.Test.Namespace&classes=Echo");
            FileInfo codeFile = new FileInfo(".\\Tmp\\code.cs");
            if (codeFile.Directory.Exists)
            {
                codeFile.Directory.Delete(true);
            }
            codeFile.Directory.Create();
            value.SafeWriteToFile(codeFile.FullName, true);

            List<string> referenceAssemblies = new List<string>(DaoGenerator.DefaultReferenceAssemblies);
            
            referenceAssemblies.Add(typeof(ServiceProxyClient).Assembly.GetFileInfo().FullName);  
            referenceAssemblies.Add(typeof(BreviteeServer).Assembly.GetFileInfo().FullName);

            CompilerResults results = AdHocCSharpCompiler.CompileDirectories(new DirectoryInfo[] { codeFile.Directory }, ".\\Tmp\\TestClients.dll", referenceAssemblies.ToArray(), false);

            if (results.Errors.Count > 0)
            {
                StringBuilder message = new StringBuilder();
                foreach (CompilerError err in results.Errors)
                {
                    message.AppendLine(string.Format("File=>{0}\r\n{1}:::Line {2}, Column {3}::{4}",
                                                    err.FileName, err.ErrorNumber, err.Line, err.Column, err.ErrorText));
                }
                OutLine(message.ToString(), ConsoleColor.Red);
            }
            
            Expect.AreEqual(0, results.Errors.Count);

            Expect.IsTrue(results.CompiledAssembly.GetFileInfo().Exists);

            server.Stop();
        }

        class TestApplicationNameProvider : IApplicationNameProvider
        {
            public TestApplicationNameProvider(string name)
            {
                ApplicationCreateResult result = Application.Create(name);
                Expect.AreEqual(ApplicationCreateStatus.Success, result.Status, result.Message);
                this.Name = name;
            }
            protected string Name
            {
                get;
                set;
            }
            public string GetApplicationName()
            {
                return Name;
            }
        }

        public static void ClearApps()
        {
            ApplicationCollection apps = Application.LoadAll();
            apps.Delete();
        }

        public static void ClientTestSetup()
        {
            RegisterDb();
            ApiKeyManager.Default.UserResolver = new TestUserResolver();
        }

        [UnitTest(Before = "ClientTestSetup", AlwaysAfter = "ClearApps")] // RegisterDb is defined in Program.cs
        public void ApiKey_ShouldCreateToken()
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ApiKeyResolver resolver = new ApiKeyResolver(new LocalApiKeyProvider(), new TestApplicationNameProvider(methodName));
            string data = "some random data";
            string token = resolver.CreateToken(data);
            Expect.IsNotNullOrEmpty(token, "Token was null or empty");
        }

        [UnitTest(Before = "ClientTestSetup", AlwaysAfter = "ClearApps")] // RegisterDb is defined in Program.cs
        public void ApiKey_ShouldSetToken()
        {
            string testName = MethodBase.GetCurrentMethod().Name;
            NameValueCollection nvc = new NameValueCollection();
            string data = "Some random data";

            IApiKeyProvider keyProvider = new LocalApiKeyProvider();
            ApiKeyResolver resolver = new ApiKeyResolver(keyProvider, new TestApplicationNameProvider(testName));
            resolver.SetToken(nvc, data);

            Expect.IsNotNullOrEmpty(nvc[ApiKeyResolver.KeyTokenName], "Key token was not set");
        }

        [UnitTest(Before = "ClientTestSetup", AlwaysAfter = "ClearApps")] // RegisterDb is defined in Program.cs
        public void ApiKey_ShouldSetValidToken()
        {
            string testName = MethodBase.GetCurrentMethod().Name;
            NameValueCollection nvc = new NameValueCollection();
            string data = "Some random data";
            IApiKeyProvider keyProvider = new LocalApiKeyProvider();
            TestApplicationNameProvider nameProvider = new TestApplicationNameProvider(testName);
            ApiKeyResolver resolver = new ApiKeyResolver(keyProvider, nameProvider);

            resolver.SetToken(nvc, data);

            string token = nvc[ApiKeyResolver.KeyTokenName];
            bool isValid = resolver.IsValidToken(data, token);
            Expect.IsTrue(isValid, "token was not valid");
        }
    }
}
