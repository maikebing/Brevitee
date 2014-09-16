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
using System.Collections;
using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Data;
using Brevitee.Testing;
using Brevitee.Encryption;
using Brevitee.Automation;
using Brevitee.Automation.Nuget;
using System.Collections.ObjectModel;
using Brevitee.SourceControl;
using Brevitee.Automation.ContinuousIntegration;
using Brevitee.ServiceProxy;
using Brevitee.Logging;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.VersionControl;
using Microsoft.TeamFoundation.VersionControl.Common;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.Build.BuildEngine;
using Microsoft.Build;
using Microsoft.Build.Framework;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Logging;
using Brevitee.Automation.ContinuousIntegration.Loggers;

namespace Brevitee.Automation.Tests
{
    [Serializable]
    public class UnitTests: CommandLineTestInterface
    {
        [UnitTest]
        public void ShouldBeAbleToSaveAndGetSecurelyWithDefaultForeman()
        {
            string key = "Key_".RandomLetters(4);
            string value = "Value_".RandomLetters(4);

            Foreman.Default.SecureSet(key, value);

            string validate = Foreman.Default.SecureGet(key);

            Expect.AreEqual(value, validate);
        }

        [UnitTest]
        public void CsvBuildLoggerActualLoggerShouldBeCorrectType()
        {
            CsvBuildLogger csv = new CsvBuildLogger();
            Expect.IsNotNull(csv.ActualLogger);
            Expect.AreEqual(typeof(CsvLogger), csv.ActualLogger.GetType());
        }

        [UnitTest]
        public void Dao2BuildLoggerActualLoggerShouldBeCorrectType()
        {
            Dao2BuildLogger csv = new Dao2BuildLogger();
            Expect.IsNotNull(csv.ActualLogger);
            Expect.AreEqual(typeof(DaoLogger2), csv.ActualLogger.GetType());
        }

        [UnitTest]
        public void DaoBuildLoggerActualLoggerShouldBeCorrectType()
        {
            DaoBuildLogger csv = new DaoBuildLogger();
            Expect.IsNotNull(csv.ActualLogger);
            Expect.AreEqual(typeof(DaoLogger), csv.ActualLogger.GetType());
        }

        [UnitTest]
        public void TextFileBuildLoggerActualLoggerShouldBeCorrectType()
        {
            TextFileBuildLogger csv = new TextFileBuildLogger();
            Expect.IsNotNull(csv.ActualLogger);
            Expect.AreEqual(typeof(TextFileLogger), csv.ActualLogger.GetType());
        }
        
        [UnitTest]
        public void WindowsBuildLoggerActualLoggerShouldBeCorrectType()
        {
            WindowsBuildLogger csv = new WindowsBuildLogger();
            Expect.IsNotNull(csv.ActualLogger);
            Expect.AreEqual(typeof(WindowsLogger), csv.ActualLogger.GetType());
        }

        [UnitTest]
        public void XmlBuildLoggerActualLoggerShouldBeCorrectType()
        {
            WindowsBuildLogger csv = new WindowsBuildLogger();
            Expect.IsNotNull(csv.ActualLogger);
            Expect.AreEqual(typeof(WindowsLogger), csv.ActualLogger.GetType());
        }

        [UnitTest]
        public void UnzipResourceTest()
        {
            string extractTo = ".\\Unzip";
            if (Directory.Exists(extractTo))
            {
                Directory.Delete(extractTo, true);
            }
            Expect.IsTrue(Assembly.GetExecutingAssembly().UnzipResource(typeof(UnitTests), "Test.zip", extractTo));
            FileInfo[] files = new DirectoryInfo(".\\UnzipTest").GetFiles();
            Expect.IsTrue(files.Length > 0);
        }

        [UnitTest]
        public void GetMemberTypeTest()
        {
            string name;
            MemberType type = DocInfo.GetMemberType("M:This.Is.A.Method", out name);
            Expect.IsTrue(type == MemberType.Method);
            Expect.AreEqual("This.Is.A.Method", name);
            type = DocInfo.GetMemberType("F:This.Is.A.Field", out name);
            Expect.IsTrue(type == MemberType.Field);
            Expect.AreEqual("This.Is.A.Field", name);
            type = DocInfo.GetMemberType("T:This.Is.A.Type", out name);
            Expect.IsTrue(type == MemberType.Type);
            Expect.AreEqual("This.Is.A.Type", name);
            type = DocInfo.GetMemberType("P:This.Is.A.Property", out name);
            Expect.IsTrue(type == MemberType.Property);
            Expect.AreEqual("This.Is.A.Property", name);
        }

        [UnitTest]
        public void ReadXmlDocs()
        {
            doc doc = new FileInfo("./TestDoc.xml").FromXmlFile<doc>();
            OutFormat("Assembly Name: {0}", doc.assembly.name);
            if (doc.members == null)
            {
                Out("doc.members == null", ConsoleColor.Cyan);
            }
            else if (doc.members.Items == null)
            {
                Out("doc.members.Items == null", ConsoleColor.Yellow);
            }
            else
            {                
                OutFormat("Iterating on {0}", ConsoleColor.Cyan, "doc.members.Items");
                doc.members.Items.Each(member =>
                {
                    OutFormat("member.name={0}", ConsoleColor.Yellow, member.name);
                    if (member.Items == null)
                    {
                        OutFormat("member.Items == null");
                    }
                    else
                    {
                        OutFormat("Iterating on {0}", ConsoleColor.Cyan, "member.Items");
                        member.Items.Each(item =>
                        {
                            Type itemType = item.GetType();
                            OutFormat("\tItem type = {0}", ConsoleColor.Yellow, itemType.FullName);
                            summary summary = item as summary;
                            if (summary != null)
                            {
                                HandleSummary(summary);
                            }
                        });
                    }
                });
            }
        }

        private void HandleSummary(summary summary)
        {
            Out("Summary line by line");
            summary.Text.Each(text =>
            {
                OutFormat("{0}", text);
            });
            if (summary.Items != null)
            {
                Out("Iterating over summary.Items");
                summary.Items.Each(o =>
                {
                    Type itemType = o.GetType();
                    OutFormat("\tItem type = {0}", ConsoleColor.Yellow, itemType.FullName);
                });
            }
            else
            {
                Out("summary.Items was null");
            }

            if (summary.Items1 != null)
            {
                Out("Iterating over summary.Items1");
                summary.Items1.Each(o =>
                {
                    Type itemType = o.GetType();
                    OutFormat("\tItem type = {0}", ConsoleColor.Blue, itemType.FullName);
                });
            }
            else
            {
                Out("summary.Items1 was null");
            }
        }

        private void OutFormat(int tabCount, string format, ConsoleColor color, params string[] args)
        {
            StringBuilder tabs = new StringBuilder();
            tabCount.Times(i => tabs.Append("\t"));
            OutLineFormat("{0}{1}", color, tabs, format._Format(args));
        }

        [UnitTest]
        public void DocInfoFromXmlFileShouldHaveDeclaringTypeName()
        {
            Dictionary<string, List<DocInfo>> infos = DocInfo.FromXmlFile("./TestBuildProject.xml");
            infos.Keys.Each(s =>
            {
                OutLine(s, ConsoleColor.Cyan);

                infos[s].Each(info =>
                {
                    OutputInfo(info);
                });
            });
        }

        /// <summary>
        /// This is the xml summary
        /// </summary>
        [Doc(@"This class is for testing documentation
and whatever")]
        class DocumentedClassTest
        {
            [Doc(@"This is a method that takes no 
arguments")]
            public void TestMethod()
            {

            }

            [Doc(@"This method returns 
an empty string")]
            public string ReturnsStringMethod()
            {
                return string.Empty;
            }

            [Doc(@"This method takes arguments", "the reason this is funny")]
            public string ReturnFunnyString(string reason)
            {
                return reason + " funny";
            }
        }

        [UnitTest]
        public void DocInfoFromAttributeShouldHaveDeclaringTypeName()
        {
            Dictionary<string, List<DocInfo>> infos = DocInfo.FromDocAttributes(typeof(DocumentedClassTest));
            Expect.IsGreaterThan(infos.Count, 0);

            infos.Keys.Each(type =>
            {
                infos[type].Each(info =>
                {
                    Out("From: ");
                    OutLine(info.From.ToString(), ConsoleColor.Cyan);

                    if (info.From == DocFrom.Reflection)
                    {
                        Expect.IsFalse(string.IsNullOrEmpty(info.DeclaringTypeName));
                    }
                    OutputInfo(info);
                });
            });
        }

        [UnitTest]
        public void ShouldBeAbleToInferDocs()
        {
            Dictionary<string, List<DocInfo>> infos = DocInfo.Infer(Assembly.GetExecutingAssembly());
            Expect.IsGreaterThan(infos.Count, 0);

            infos.Keys.Each(type =>
            {
                infos[type].Each(info =>
                {
                    Out("From: ", ConsoleColor.Cyan);
                    ConsoleColor fromColor = info.From == DocFrom.Reflection ? ConsoleColor.Blue : ConsoleColor.Yellow;
                    OutLine(info.From.ToString(), fromColor);

                    if (info.From == DocFrom.Reflection)
                    {
                        Expect.IsFalse(string.IsNullOrEmpty(info.DeclaringTypeName));
                    }

                    OutputInfo(info);
                });
            });
        }

        private static void OutputInfo(DocInfo info)
        {
            OutLineFormat("Summary:\r\n{0}", ConsoleColor.Blue, info.Summary);
            OutLineFormat("DeclaringTypName: {0}", ConsoleColor.Yellow, info.DeclaringTypeName);
            OutLineFormat("MemberType: {0}", ConsoleColor.Magenta, info.MemberType.ToString());
            OutLineFormat("MemberName: {0}", ConsoleColor.Yellow, info.MemberName);

            if (info.MemberType == MemberType.Method)
            {
                info.ParamInfos.Each(p =>
                {
                    OutLineFormat("Parameter: {0}, Description: {1}", p.Name, p.Description);
                });
            }
            if (info.HasExtraItems)
            {
                OutLine("Extra Items", ConsoleColor.Yellow);
                info.Items().Each(o =>
                {
                    if (o != null)
                    {
                        OutLineFormat("\tType: {0}", ConsoleColor.Yellow, o.GetType());
                        OutLineFormat("\t{0}", o.ValuePropertiesToDynamicInstance().PropertiesToString());
                    }
                });
            }
        }

        //[UnitTest]
        //public void TeamProjectFluently()
        //{
        //    Tfs.Server("http://tfs.klgates.com:8080/tfs")
        //        .TeamProjectCatalogNode("ISDEV", "Brevitee", (catalogNode) =>
        //        {
        //            OutFormat("catalogNode.Resource.DisplayName: {0}", ConsoleColor.Cyan, catalogNode.Resource.DisplayName);
        //        });
        //}

        //Tfs _tfs;
        //public void DeleteWorkspace()
        //{
        //    if (_tfs != null)
        //    {
        //        _tfs.DeleteWorkspace();
        //    }

        //    DirectoryInfo _workingDir = new DirectoryInfo(_workspaceName);
        //    if (_workingDir.Exists)
        //    {
        //        FileInfo[] allFiles = _workingDir.GetFiles("*", SearchOption.AllDirectories);
        //        allFiles.Each(fi =>
        //        {
        //            fi.RemoveAttribute(FileAttributes.ReadOnly);
        //        });
        //        _workingDir.Delete(true);
        //    }
        //}

        //string _tfsServerUrl = "";
        
        //string _comment = "Test workspace for custom dev test suite";
        //string _workspaceName = "TEST_WORKSPACE";
        //string _teamProject = "GlobalServiceDesk"; // just for testing

        //[UnitTest("", "DeleteWorkspace", "")]
        //public void WorkspaceShouldNotBeNullAfterSpecifying()
        //{
        //    DirectoryInfo workingDir = new DirectoryInfo(_workspaceName);
        //    Tfs tfs = Tfs.Server(_tfsServerUrl)
        //                    .TeamProjectCollection("ISDEV")
        //                    .TeamProject(_teamProject)
        //                    .Workspace(_workspaceName, workingDir, _comment);

        //    _tfs = tfs;
                
        //    Expect.IsNotNull(tfs, "tfs was null");
        //    Expect.IsNotNull(tfs.Config, "tfs.Config was null");
        //    Expect.IsNotNull(tfs.Config.Workspace, "tfs.Config.Workspace was null");
        //    Expect.AreEqual(_comment, tfs.Config.Workspace.Comment);
        //}

        //[UnitTest("", "DeleteWorkspace", "")]
        //public void ShouldBeAbleToGetLatest()
        //{
        //    DirectoryInfo workingDir = new DirectoryInfo(_workspaceName);
        //    _tfs = Tfs.Server(_tfsServerUrl)
        //        .TeamProjectCollection("ISDEV")
        //        .TeamProject(_teamProject)
        //        .Workspace(_workspaceName, workingDir, _comment)
        //        .GetLatest();

        //    workingDir.Refresh();
        //    Expect.IsTrue(workingDir.Exists);
        //    FileInfo[] files = workingDir.GetFiles();
        //    DirectoryInfo[] directories = workingDir.GetDirectories();
        //    Expect.IsTrue(files.Length > 0 || directories.Length > 0);
        //}

        //[UnitTest(AlwaysPostInvokeMethodName="DeleteWorkspace")]
        //public void ShouldBeAbleToDeleteWorkspace()
        //{
        //    DirectoryInfo workingDir = new DirectoryInfo(_workspaceName);
        //    bool thrown = false;
        //    string msg = "";
        //    try
        //    {
        //        _tfs = Tfs.Server(_tfsServerUrl)
        //                .TeamProjectCollection("ISDEV")
        //                .TeamProject(_teamProject)
        //                .Workspace(_workspaceName, workingDir, _comment)
        //                .DeleteWorkspace();
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //        thrown = true;
        //    }

        //    Expect.IsFalse(thrown, msg);
        //}

        class TestCsvLogger: CsvLogger
        {
            public void Run()
            {
                LogEvent evt = CreateInfoEvent("this is a test");
                string msg = GetLogText(evt);
                Out(msg);
            }
        }

        [UnitTest]
        public void CsvLoggerTest()
        {
            TestCsvLogger test = new TestCsvLogger();
            test.Run();
        }

        public void Test()
        {
            OutLine("It worked!", ConsoleColor.Green);
        }

        [UnitTest(Before="Test")]
        public void BuildConfShouldSave()
        {
            WorkerConf conf = CreateTestConf();

            conf.Save();
            string fileName = ".\\{0}_WorkerConf.json"._Format(conf.Name);
            Expect.IsTrue(File.Exists(fileName));
            File.Delete(fileName);
        }

        private static WorkerConf CreateTestConf()
        {
            WorkerConf conf = new WorkerConf();
            conf.Name = "Test_".RandomLetters(4);
            return conf;
        }

        [UnitTest]
        public void WhenWorkerConfSavesShouldSetWorkerTypeName()
        {
            string filePath = "{0}.json"._Format(MethodBase.GetCurrentMethod().Name);
            AllProjectsBuildWorker worker = new AllProjectsBuildWorker("monkey");
            worker.SaveConf(filePath);
            WorkerConf conf = WorkerConf.Load(filePath);
            Expect.IsNotNull(conf.WorkerTypeName);
            Expect.AreEqual(typeof(AllProjectsBuildWorker).AssemblyQualifiedName, conf.WorkerTypeName);
        }

        [UnitTest]
        public void JobConfShouldCreateValidJob()
        {
            DirectoryInfo dir = new DirectoryInfo(".\\{0}"._Format(MethodBase.GetCurrentMethod().Name));
            if (dir.Exists)
            {
                dir.Delete(true);
            }

            JobConf jobConf = new JobConf();
            jobConf.JobDirectory = dir.FullName;

            GitGetSourceWorker worker = new GitGetSourceWorker("monkey");
            jobConf.AddWorker(worker);
            string filePath = jobConf.Save();

            JobConf check = JobConf.Load(filePath);
            Job job = check.CreateJob();
            IWorker checkWork = job["monkey"];
            Expect.IsNotNull(checkWork);
            Expect.AreEqual(typeof(GitGetSourceWorker), checkWork.GetType());

            GitGetSourceWorker checkWorker = job.GetWorker<GitGetSourceWorker>("monkey");
            Expect.AreEqual("Git", checkWorker.SourceControlType);
        }

        [UnitTest]
        public void ShouldBeAbleToCompile()
        {
            FileInfo proj = new FileInfo("../../TestSolution/TestBuildProject.sln");
            OutFormat("{0}", ConsoleColor.Cyan, proj.FullName);
            DirectoryInfo output = new DirectoryInfo(".\\output");
            if (output.Exists)
            {
                FileInfo[] files = output.GetFiles("*");
                files.Each((f, i) =>
                {
                    f.RemoveAttribute(FileAttributes.ReadOnly);
                });
                output.Delete(true);
            }
            if (!output.Exists)
            {
                output.Create();
            }
            Expect.IsTrue(proj.Exists);

            var logger = new ConsoleBuildLogger();//new CsvBuildLogger(".\\LOGS");

            BuildResult result = proj.Compile(output.FullName, logger);

            OutFormat("Overall: {0}", ConsoleColor.Cyan, result.OverallResult.ToString());
            if (result.OverallResult == BuildResultCode.Failure)
            {
                OutFormat("{0}:\r\n", ConsoleColor.Red, result.Exception == null ? "": result.Exception.Message);
            }
            
            Expect.IsFalse(result.OverallResult == BuildResultCode.Failure);
            Expect.IsTrue(result.OverallResult == BuildResultCode.Success);            
            
            output.Refresh();
            FileInfo[] fs = output.GetFiles("*");
            DirectoryInfo[] dirs = output.GetDirectories();
            Expect.IsTrue(fs.Length > 0 || dirs.Length > 0);
        }

        [ConsoleAction]
        public void ResultCodeToString()
        {
            Out(Microsoft.Build.Execution.TargetResultCode.Skipped.ToString());
        }
        //[UnitTest]
        //public void ListProjectCollectionsFluently()
        //{
        //    Tfs.Server("http://tfs.klgates.com:8080/tfs").TeamProjectCollections((tfs, tpcArray) =>
        //    {
        //        tpcArray.Each(tpc =>
        //        {
        //            OutFormat("Team Project Collection: {0}", ConsoleColor.Blue, tpc.Name);
        //            tfs.TeamProjectCatalogNodes(tpc, (cnArr) =>
        //            {
        //                cnArr.Each(cn =>
        //                {
        //                    OutFormat("\t{0}", ConsoleColor.Cyan, cn.Resource.DisplayName);
        //                });
        //            });
        //        });
        //    });
        //}

        //[UnitTest]
        //public void ListProjectCollections()
        //{
        //    Uri url = new Uri("http://tfs.klgates.com:8080/tfs");
        //    TfsConfigurationServer tfs = TfsConfigurationServerFactory.GetConfigurationServer(url);
            
        //    ReadOnlyCollection<CatalogNode> collectionNodes = tfs.CatalogNode.QueryChildren(
        //        new[] { CatalogResourceTypes.ProjectCollection },
        //        false, CatalogQueryOptions.None);

        //    // List the team project collections
        //    foreach (CatalogNode collectionNode in collectionNodes)
        //    {
        //        OutFormat("collectionNode.Resource.DisplayName: {0}", ConsoleColor.Yellow, collectionNode.Resource.DisplayName);

        //        // Use the InstanceId property to get the team project collection
        //        Guid collectionId = new Guid(collectionNode.Resource.Properties["InstanceId"]);
        //        TfsTeamProjectCollection teamProjectCollection = tfs.GetTeamProjectCollection(collectionId);

        //        // Print the name of the team project collection
        //        Console.WriteLine("Collection: " + teamProjectCollection.Name);

        //        // Get a catalog of team projects for the collection
        //        ReadOnlyCollection<CatalogNode> projectNodes = collectionNode.QueryChildren(
        //            new[] { CatalogResourceTypes.TeamProject },
        //            false, CatalogQueryOptions.None);

        //        // List the team projects in the collection
        //        foreach (CatalogNode projectNode in projectNodes)
        //        {
        //            Console.WriteLine(" Team Project: " + projectNode.Resource.DisplayName);
        //        }
        //    }
        //}

        [UnitTest]
        public void NuspecFileShouldHaveValuesAfterInstantiation()
        {
            NuspecFile file = new NuspecFile("test1.nuspec");
            Expect.IsNotNullOrEmpty(file.Version.Value);
            Expect.AreEqual("1", file.Version.Major);
            Expect.AreEqual("0", file.Version.Minor);
            Expect.AreEqual("0", file.Version.Patch);
            Expect.IsNotNullOrEmpty(file.Title);
            Expect.IsNotNullOrEmpty(file.Id);
            Expect.IsNotNullOrEmpty(file.Authors);
            Expect.IsNotNullOrEmpty(file.Owners);
            Expect.IsNotNullOrEmpty(file.ReleaseNotes);
            Expect.IsFalse(file.RequireLicenseAcceptance);
            Expect.IsNotNullOrEmpty(file.Copyright);
            Expect.IsNotNullOrEmpty(file.Description);
            file.AddDependency("monkey", "1.0.0");
            file.AddDependency("test", "[2]");
            file.Version.IncrementPatch();
            file.Save();
            string content = file.Path.SafeReadFile();
            Out(content, ConsoleColor.Cyan);
            File.Delete(file.Path);
        }

        [UnitTest]
        public void GitCloneTest()
        {
            //using (Repository repo = new Repository("git@github.com:BreviteeApellanes/Core.git"))
            //{
            //    repo.Branches.Keys.Each(key =>
            //    {
            //        Branch branch = repo.Branches[key];
                    
            //        OutLineFormat("FullName: {0}", branch);                    
            //    });
            //}
        }
    }
}
