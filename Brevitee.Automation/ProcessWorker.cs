using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.CommandLine;

namespace Brevitee.Automation
{
    /// <summary>
    /// Work done as a command line process
    /// </summary>
    public class ProcessWorker: Worker, IConfigurable
    {
        public ProcessWorker(string name) : base(name) { }
        public ProcessWorker(string name, string commandLine)
            : base(name)
        {
            this.CommandLine = commandLine;
        }

        public string CommandLine { get; set; }

        protected override WorkState Do()
        {
            Args.ThrowIfNullOrEmpty(CommandLine, "CommandLine");

            ProcessOutput output = CommandLine.Run();
            WorkState<ProcessOutput> result = new WorkState<ProcessOutput>(output);
            result.Message = "{0} exited with code {1}"._Format(CommandLine, output.ExitCode);

            return result;
        }

        #region IConfigurable Members

        public void Configure(object configuration)
        {
            Foreman.Configure(this, configuration);
        }

        #endregion
    }
}
