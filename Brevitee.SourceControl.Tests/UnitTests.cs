using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using Brevitee.Testing;
using Brevitee.SourceControl;
using Brevitee.CommandLine;
using System.IO;

namespace Brevitee.SourceControl.Tests
{
    [Serializable]
    public class UnitTests: CommandLineTestInterface
    {
        [UnitTest]
        public void ShouldBeAbleToClone()
        {
            DirectoryInfo outputTo = new DirectoryInfo(".\\TestCore");
            if (outputTo.Exists)
            {
                outputTo.Delete();
            }

            ProcessOutput output = Git.Repository("https://github.com/BreviteeApellanes/Core.git")
                                        .UserName("bryanapellanes")
                                        .UserEmail("bryan.apellanes@gmail.com")
                                        .CloneTo(outputTo).LastOutput();
            OutLine("**** Output ****");
            OutLine(output.StandardOutput);
            OutLine("**** /Output ****");

            OutLine("**** Error Output if any ****");
            OutLine(output.StandardOutput);
            OutLine("**** /Error Output if any ****");
        }

        [UnitTest]
        public void ShowEnvrionmentVariables()
        {
            IDictionary vars = Environment.GetEnvironmentVariables();
            foreach (object k in vars.Keys)
            {
                OutLineFormat("{0}={1}", ConsoleColor.Cyan, k.ToString(), vars[k].ToString());
            }
        }

        [UnitTest]
        public void SetEnvironmentVariableTest()
        {
            //ProcessOutput output = "test".Run();
            //OutLineFormat("{0}", ConsoleColor.Cyan, output.PropertiesToString());
            string path = Environment.GetEnvironmentVariable("PATH");
            Environment.SetEnvironmentVariable("PATH", "{0};{1}"._Format(path, "c:\\tmp"));
            ProcessOutput output = "test.cmd".Run();
            OutLineFormat("{0}", ConsoleColor.Cyan, output.PropertiesToString());
        }
    }
}
