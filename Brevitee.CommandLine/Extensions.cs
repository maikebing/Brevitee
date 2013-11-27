using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Brevitee.CommandLine
{
    public static class Extensions
    {
        /// <summary>
        /// Executes the current string on the command line
        /// and returns the output.
        /// </summary>
        /// <param name="command">a valid command line</param>
        /// <returns>ProcessOutput</returns>
        public static ProcessOutput Run(this string command)
        {
            Expect.IsFalse(string.IsNullOrEmpty(command), "command cannot be blank or null");
            Expect.IsFalse(command.Contains("\r"), "Multiple command lines not supported");
            Expect.IsFalse(command.Contains("\n"), "Multiple command lines not supported");

            string exe = string.Empty;
            string arguments = string.Empty;
            string[] split = command.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length > 1)
            {
                exe = split[0];
                for (int i = 1; i < split.Length; i++)
                {
                    arguments += split[i];
                    if (i != split.Length - 1)
                        arguments += " ";
                }
            }

            ProcessStartInfo startInfo = CreateStartInfo();


            startInfo.FileName = string.IsNullOrEmpty(exe) ? command : exe;
            startInfo.Arguments = arguments;

            string output = string.Empty;
            string error = string.Empty;

            int exitCode = -1;
            using (Process theProcess = new Process())
            {
                theProcess.StartInfo = startInfo;
                theProcess.Start();
                output = theProcess.StandardOutput.ReadToEnd();
                theProcess.StandardOutput.Close();
                error = theProcess.StandardError.ReadToEnd();
                theProcess.StandardError.Close();
                exitCode = theProcess.ExitCode;
                theProcess.Close();
            }

            return new ProcessOutput(output, error, exitCode);
        }

        private static ProcessStartInfo CreateStartInfo()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.ErrorDialog = false;
            startInfo.CreateNoWindow = true; ;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            return startInfo;
        }
    }
}
