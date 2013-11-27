using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Brevitee;
using Brevitee.Configuration;

namespace Brevitee.Automation
{
    public class FileCopyWorker: Worker, IConfigurable, IHasRequiredProperties
    {
        public FileCopyWorker(string name) : base(name) { }

        public string Source { get; set; }
        public string Destination { get; set; }

        protected override WorkState Do()
        {        
            string jobName = this.Job != null ? this.Job.Name : "null";

            FileInfo src = new FileInfo(Source);
            ThrowIfFileNotFound(jobName, src);

            FileInfo dst = new FileInfo(Destination);
            //ThrowIfFileNotFound(jobName, dst);

            File.Copy(src.FullName, dst.FullName);

            return new WorkState("File {0} copied successfully to {1}"._Format(src.FullName, dst.FullName));
        }

        private void ThrowIfFileNotFound(string jobName, FileInfo src)
        {
            if (!src.Exists)
            {
                throw new FileNotFoundException("Job:{0},FileCopyWork:{1}::File {2} not found."._Format(jobName, this.Name, src.FullName));
            }
        }

        #region IConfigurable Members

        public void Configure(object configuration)
        {
            Foreman.Configure(this, configuration);
        }

        #endregion

        #region IHasRequiredProperties Members

        public string[] RequiredProperties
        {
            get { return new string[] { "Source", "Destination"}; }
        }

        #endregion
    }
}
