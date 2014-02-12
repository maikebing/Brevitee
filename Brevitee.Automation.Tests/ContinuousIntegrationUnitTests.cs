using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Testing;
using Brevitee.Automation;
using Brevitee.Automation.ContinuousIntegration;
using System.IO;
using System.Reflection;
using System.Threading;
using Brevitee.Configuration;
using Brevitee.Logging;
using Brevitee.Automation.ContinuousIntegration.Loggers;
using Microsoft.Build.Framework;
using M = Microsoft.Build.Logging;
using Microsoft.Build.Execution;

namespace Brevitee.Automation.Tests
{
    [Serializable]
    public class ContinuousIntegrationUnitTests: CommandLineTestInterface
    {
        [UnitTest]
        public void EmailJobShouldSendEmail()
        {
            ProjectBuildWorker worker = new ProjectBuildWorker();
            WorkState state = new WorkState(worker, "Testing EmailWorker");
            BreviteeContinuousIntegrationJobConf conf  = new BreviteeContinuousIntegrationJobConf();
            Job job = new Job(conf);
            job.CurrentWorkState = state;
            EmailWorker email = new EmailWorker();
            email.SmtpHost = "smtp.live.com";
            email.From = "ci@stickerize.me";
            email.EnableSsl = "true";
            email.Port = "587";
            email.UserName = "ci@stickerize.me";
            email.Password = "put password";
            email.Recipients = "bryan.apellanes@gmail.com";
            WorkState result = email.Do(job);

            Expect.AreEqual(Status.Succeeded, result.Status, result.Message);
        }

        [UnitTest]
        public void ContinuousIntegrationJobConfWorkersShouldntBeNull()
        {
            ContinuousIntegrationJobConf jobConf = new BreviteeContinuousIntegrationJobConf();
            Expect.IsNotNull(jobConf.GetSource, "GetSource was null");
            Expect.IsNotNull(jobConf.Build, "Build was null");
            Expect.IsNotNull(jobConf.RunTests, "RunTests was null");
            Expect.IsNotNull(jobConf.DeployToTest, "Deploy was null");
            Expect.IsNotNull(jobConf.SendNotification, "SendNotification was null");
            Expect.IsNotNull(jobConf.SuspendJob, "SuspendJob was null");
            Expect.IsNotNull(jobConf.ProductionTransform, "ProductionTransfomr was null");
            Expect.IsNotNull(jobConf.DeployToProduction, "DeployToProduction was null");
            Expect.IsNotNull(jobConf.Archive, "Archive was null");
        }
        
        [UnitTest]
        public void ContinuousIntegrationJobConfWorkerNameShouldGetSet()
        {
            string name = "Test_".RandomLetters(4);
            ContinuousIntegrationJobConf conf = new BreviteeContinuousIntegrationJobConf();
            conf.Name = name;
            Job job = new Job(conf);
            Expect.AreEqual(name, job.Name);
        }

        [UnitTest]
        public void GitCIConfShouldHaveWorkerFiles()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            BreviteeContinuousIntegrationJobConf conf = new BreviteeContinuousIntegrationJobConf(name);
            Directory.Delete(conf.JobDirectory, true);
            conf = new BreviteeContinuousIntegrationJobConf(name);

            Expect.IsGreaterThan(conf.WorkerFiles.Length, 0, "There were no workerfiles in the config");
        }

        [UnitTest]
        public void GitCIJobShouldHaveWorkers()
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            BreviteeContinuousIntegrationJobConf conf = GetGitCIConf(methodName);

            Job job = new Job(conf);
            Expect.IsTrue(job.WorkerNames.Length > 0, "There were no WorkerNames");
        }

        private static BreviteeContinuousIntegrationJobConf GetGitCIConf(string methodName)
        {
            BreviteeContinuousIntegrationJobConf conf = new BreviteeContinuousIntegrationJobConf(methodName);
            Directory.Delete(conf.JobDirectory, true); // ensure we start fresh
            conf = new BreviteeContinuousIntegrationJobConf(methodName);
            return conf;
        }

        [UnitTest]
        public void ContinuousIntegrationJobConfWorkerNamesShouldBeInCorrectOrder()
        {
            BreviteeContinuousIntegrationJobConf conf = GetGitCIConf(MethodBase.GetCurrentMethod().Name);

            Job job = new Job(conf);
            OutLineFormat("Worker Names in Job Conf {0}", ConsoleColor.Cyan, conf.Name);
            string[] names = new string[] { "GetSource", "Build", "RunTests", "DeployToTest", "SendNotification", "SuspendJob", "ProductionTransform", "DeployToProduction", "Archive" };
            Expect.IsTrue(job.WorkerNames.Length > 0, "There were no workers");
            job.WorkerNames.Each((name, i) =>
            {
                OutLine(name);
                string compareTo = "{0}_{1}"._Format(conf.Name, names[i]);
                Expect.AreEqual(compareTo, name);
            });

            Directory.Delete(conf.JobDirectory, true);
        }

        [UnitTest]
        public void WorkerConfCreateShouldSetName()
        {
            string testName = "Name_".RandomLetters(4);
            WorkerConf conf = new WorkerConf();
            conf.WorkerType = typeof(ProjectBuildWorker);
            conf.Name = testName;

            Worker testResult = conf.CreateWorker();
            Expect.AreEqual(testName, testResult.Name);
        }

        class TestListener : ConsoleLogger
        {
            public TestListener()
            {

            }

            public bool ExceptionOccurred { get; set; }

            public override void CommitLogEvent(Logging.LogEvent logEvent)
            {
                if (logEvent.Severity == LogEventType.Error ||
                    logEvent.Severity == LogEventType.Fatal)
                {
                    ExceptionOccurred = true;
                }

                base.CommitLogEvent(logEvent);
            }
        }

      
        [UnitTest]
        public void GitCIShouldPassCheckRequiredProperties()
        {
            DirectoryInfo curDir = new DirectoryInfo(".");
            string curDirPath = curDir.FullName;
            BreviteeContinuousIntegrationJobConf conf = new BreviteeContinuousIntegrationJobConf(MethodBase.GetCurrentMethod().Name);

            conf.GetSourceLocalDirectory = Path.Combine(curDirPath, "Test_DownloadedSource");
            conf.GetSourceUserName = "bryanapellanes";
            conf.GetSourceUserEmail = "bryan.apellanes@gmail.com";
            conf.GetSourceServerSourcePath = "https://github.com/BreviteeApellanes/Core.git";

            conf.BuildSourceDirectory = conf.GetSource.LocalDirectory;
            conf.BuildOutputDirectory = Path.Combine(curDirPath, "Test_BuildOutput_{0}"._Format(conf.Name));

            //conf.ZipSourceDirectory = conf.Build.OutputDirectory;
            //conf.ZipTargetPath = Path.Combine(curDirPath, "Test_{0}"._Format(conf.Name));

            //conf.DeployUserName = "ftpuser";
            //conf.DeployPassword = "ftppassword";

            conf.CheckRequiredProperties();
        }
  
        [UnitTest]
        public void BuildLoggerShouldNotBeNull()
        {
            BuildWorker test = new AllProjectsBuildWorker();
            Expect.IsNotNull(test.Logger);
        }

        [UnitTest]
        public void ConfigureClientShouldSetClient()
        {
            GitGetSourceWorker test = new GitGetSourceWorker();
            test.ConfigureClient();
            Expect.IsNotNull(test.SourceControlClient);
        }

        [UnitTest]
        public void BuildWorkerShouldWork()
        {
            DirectoryInfo curDir = new DirectoryInfo(".");
            string curDirPath = curDir.FullName;
            string downloadedSource = Path.Combine(curDirPath, "Test_DownloadedSource");
            string outputDir = Path.Combine(curDirPath, MethodBase.GetCurrentMethod().Name + "BuildOutput");

            JobConf conf = new JobConf(MethodBase.GetCurrentMethod().Name);
            ProjectBuildWorker test = conf.GetWorker<ProjectBuildWorker>("Test_ProjectBuildWorker");
            test.Logger = new M.ConsoleLogger();
            test.Logger.Verbosity = LoggerVerbosity.Minimal;
            test.SourceDirectory = downloadedSource;
            test.OutputDirectory = outputDir;
            test.ProjectFileName = "Brevitee.Music";
            WorkState state = test.Do(conf.CreateJob());

            Expect.IsTrue(state.Status == Status.Succeeded, state.Message);
        }


        [UnitTest]
        public void GitGetShouldWork()
        {
            DirectoryInfo curDir = new DirectoryInfo(".");
            string curDirPath = curDir.FullName;
            string methodName = MethodBase.GetCurrentMethod().Name;
                        
            JobConf jobConf = new JobConf(methodName);
            GitGetSourceWorker getter = new GitGetSourceWorker("GitGetter");

            getter.LocalDirectory = Path.Combine(curDirPath, methodName + "_Test_DownloadedSource");
            getter.UserName = "bryanapellanes";
            getter.UserEmail = "bryan.apellanes@gmail.com";
            getter.ServerSourcePath = "https://github.com/BreviteeApellanes/Core.git";

            WorkState state = getter.Do(jobConf.CreateJob());

            Expect.IsTrue(state.Status == Status.Succeeded, state.Message);
            Expect.IsTrue(Directory.Exists(getter.LocalDirectory));
        }

        [UnitTest]
        public void AllBuildWorkerShouldWork()
        {
            DirectoryInfo curDir = new DirectoryInfo(".");
            string curDirPath = curDir.FullName;
            string downloadedSource = Path.Combine(curDirPath, "Test_DownloadedSource");
            string outputDir = Path.Combine(curDirPath, MethodBase.GetCurrentMethod().Name + "BuildOutput");

            JobConf conf = new JobConf(MethodBase.GetCurrentMethod().Name);
            AllProjectsBuildWorker test = conf.GetWorker<AllProjectsBuildWorker>("Test_ProjectBuildWorker");
            test.Logger = new M.ConsoleLogger();
            test.Logger.Verbosity = LoggerVerbosity.Quiet;
            test.SourceDirectory = downloadedSource;
            test.OutputDirectory = outputDir;

            test.ResultAdded += (o, a) =>
            {
                BuildResultEventArgs args = (BuildResultEventArgs)a;
                ConsoleColor color = args.ProjectBuildResult.BuildResult.OverallResult == BuildResultCode.Success ? ConsoleColor.Green: ConsoleColor.Red;
                string path = args.ProjectBuildResult.ProjectPath;                
                OutLineFormat("{0}: {1}", color, path, args.ProjectBuildResult.BuildResult.OverallResult.ToString());
            };

            WorkState<ProjectBuildResult[]> state = (WorkState<ProjectBuildResult[]>)test.Do(conf.CreateJob());

            state.Data.Each(pbr =>
            {
                if (pbr.BuildResult.OverallResult != BuildResultCode.Success)
                {
                    OutLineFormat("{0}: Failed", ConsoleColor.Red, pbr.ProjectPath);
                }
            });

            Expect.IsTrue(state.Status == Status.Succeeded, state.Message);

        }
        
        [UnitTest]
        public void GitCIJobConfShouldSetBuildLogger()
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            BreviteeContinuousIntegrationJobConf conf = new BreviteeContinuousIntegrationJobConf(methodName);

            conf.BuildLogger = new ConsoleBuildLogger();

            BreviteeContinuousIntegrationJobConf conf2 = new BreviteeContinuousIntegrationJobConf(methodName);

            Expect.AreEqual(conf2.BuildLogger.GetType(), typeof(ConsoleBuildLogger));
        }

        [UnitTest]
        public void GitCIJob()
        {
            DirectoryInfo curDir = new DirectoryInfo(".");
            string curDirPath = curDir.FullName;
            BreviteeContinuousIntegrationJobConf conf = new BreviteeContinuousIntegrationJobConf(MethodBase.GetCurrentMethod().Name);

            conf.GetSourceLocalDirectory = Path.Combine(curDirPath, "Test_DownloadedSource");
            conf.GetSourceUserName = "bryanapellanes";
            conf.GetSourceUserEmail = "bryan.apellanes@gmail.com";
            conf.GetSourceServerSourcePath = "https://github.com/BreviteeApellanes/Core.git";

            conf.BuildSourceDirectory = conf.GetSource.LocalDirectory;
            conf.BuildOutputDirectory = Path.Combine(curDirPath, "Test_BuildOutput_{0}"._Format(conf.Name));

            conf.BuildLogger = new M.ConsoleLogger();

            //conf.ZipSourceDirectory = conf.Build.OutputDirectory;
            //conf.ZipTargetPath = Path.Combine(curDirPath, "Test_{0}"._Format(conf.Name));

            //conf.DeployUserName = "ftpuser";
            //conf.DeployPassword = "ftppassword";

            TestListener logger = new TestListener();
            logger.AddDetails = false;
            logger.StartLoggingThread();
            Job job = new Job(conf);
            job.Subscribe(logger);

            job.Run();

            Thread.Sleep(2000); // give the logger a chance to output

            Expect.IsFalse(logger.ExceptionOccurred, "An exception occurred");
        }
    }
}
