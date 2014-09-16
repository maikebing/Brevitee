using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using Brevitee.Web;
using Brevitee.Testing;
using Brevitee.Configuration;
using Brevitee.CommandLine;
using System.IO;
using System.Net.FtpClient;
using Brevitee.Logging;
using System.Reflection;

namespace Brevitee.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTestInterface
    {
        class TestLoggable: Loggable
        {
            [Verbosity(LogEventType.Custom, MessageFormat="Name={Name}")]
            public event EventHandler TestEvent;

            public string Name
            {
                get { return "Test"; }
            }

            public void OnTestEvent()
            {
                if (TestEvent != null)
                {
                    TestEvent(this, null);
                }
            }

            public void Fire()
            {
                OnTestEvent();
            }
        }

        class TestLogger: Logger
        {
            public bool AddCalled { get; set; }

            public override void AddEntry(string messageSignature, LogEventType type, params string[] variableMessageValues)
            {
                AddCalled = true;
                OutLineFormat(messageSignature, ConsoleColor.Cyan, variableMessageValues);
            }

            public override void CommitLogEvent(LogEvent logEvent)
            {
                throw new NotImplementedException(); // no implementation necessary for this test
            }
        }

        [UnitTest]
        public void ShouldBeAbleToLoadFromFile()
        {
            string name = "Brevitee.Stickerize.Services.Data.StickerizeContext, Brevitee.Stickerize.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            string path = "C:\\BreviteeContentRoot\\apps\\stickerize.me\\services\\Brevitee.Stickerize.Services.dll";
            Assembly assembly = Assembly.LoadFrom(path);
            Expect.IsNotNull(assembly);
            assembly.GetTypes().Each(type =>
            {
                OutLine(type.AssemblyQualifiedName);
            });
        }

        [UnitTest]
        public void ShouldSubscribe()
        {
            TestLoggable loggable = new TestLoggable();
            TestLogger logger = new TestLogger();
            Expect.IsFalse(logger.AddCalled);
            loggable.Subscribe(logger);
            loggable.Fire();
            Expect.IsTrue(logger.AddCalled);
        }

        [UnitTest]
        public void ShouldSetFtpServer()
        {
            Ftp ftp = Ftp.Server("localhost");
            Expect.AreEqual(ftp.Config.ServerHost, "localhost");
        }

        [UnitTest]
        public void ShouldSetFtpUserName()
        {       
            string un = "userTest";
            Ftp ftp = Ftp.Server("localhost").UserName(un);
            Expect.AreEqual(ftp.Config.UserName, un, "UserName was not set properly");
        }

        [UnitTest]
        public void ShouldSetPassword()
        {
            string p = "password";
            Ftp ftp = Ftp.Server("localhost").Password(p);
            Expect.AreEqual(ftp.Config.Password, p, "Password was not set properly");
        }

        [UnitTest]
        public void ShouldUpload()
        {
            string testPath= "C:\\inetpub\\ftproot\\subfolder";
            if (Directory.Exists(testPath))
            {
                Directory.Delete(testPath);
            };

            Expect.IsFalse(Directory.Exists(testPath));
            Ftp ftp = Ftp.Server("localhost");
            ftp.UserName("ftptest").Password("53cr3tP455w0rd1!").Upload(".\\Test");
            Expect.IsTrue(Directory.Exists(testPath));            
        }
        
        [UnitTest]
        public void SettingUserNameAndPasswordShouldSetNetworkCredential()
        {
            Ftp ftp = Ftp.Server("localhost");
            Expect.IsNull(ftp.Config.Credentials);

            ftp.UserName("ftptest").Password("53cr3tP455w0rd1!");

            Expect.IsNotNull(ftp.Config.Credentials);
        }

        [UnitTest]
        public void SetAndRemoveAttribute()
        {
            FileInfo file = new FileInfo(".\\test.txt");
            if (!file.Exists)
            {
                "test text _".RandomLetters(16).SafeWriteToFile(file.FullName);
            }

            Expect.IsFalse(file.Is(FileAttributes.ReadOnly));
            file.SetAttribute(FileAttributes.ReadOnly);
            Expect.IsTrue(file.Is(FileAttributes.ReadOnly));
            file.RemoveAttribute(FileAttributes.ReadOnly);
            Expect.IsFalse(file.Is(FileAttributes.ReadOnly));

            bool thrown = false;
            try
            {
                file.RemoveAttribute(FileAttributes.ReadOnly);
                Expect.IsFalse(file.Is(FileAttributes.ReadOnly));
            }
            catch (Exception ex)
            {
                thrown = true;
            }

            Expect.IsFalse(thrown, "Remove attribute threw exception");
        }
    }
}
