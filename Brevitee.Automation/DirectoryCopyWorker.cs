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
    public class DirectoryCopyWorker: Worker, IConfigurable, IHasRequiredProperties
    {
        public DirectoryCopyWorker(string name) : base(name) { }

        public string Source { get; set; }
        public string Destination { get; set; }

        protected override WorkState Do()
        {        
            string jobName = this.Job != null ? this.Job.Name : "null";

            DirectoryInfo src = new DirectoryInfo(Source);
            ThrowIfDirectoryNotFound(jobName, src);

            DirectoryInfo dst = new DirectoryInfo(Destination);
            ThrowIfDirectoryNotFound(jobName, dst);

            src.Copy(dst);

            return new WorkState("Directory {0} copied successfully to {1}"._Format(src.FullName, dst.FullName));
        }

        private void ThrowIfDirectoryNotFound(string jobName, DirectoryInfo src)
        {
            if (!src.Exists)
            {
                throw new DirectoryNotFoundException("Job:{0},DirectoryCopyWork:{1}::Directory {2} not found."._Format(jobName, this.Name, src.FullName));
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
