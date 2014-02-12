using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using Brevitee.Configuration;
using System.IO;

namespace Brevitee.Automation
{
    public class ZipFolderWorker: Worker
    {
        public ZipFolderWorker() : base() { }
        public ZipFolderWorker(string name) : base(name) { }

        public override string[] RequiredProperties
        {
            get
            {
                return new string[] { "Name", "SourceDirectory", "TargetPath" };
            }
        }

        /// <summary>
        /// The directory to zip
        /// </summary>
        public string SourceDirectory { get; set; }

        /// <summary>
        /// The full path or job relative path to the
        /// final zip file including the desired extension
        /// </summary>
        public string TargetPath { get; set; }
        protected override WorkState Do()
        {
            Validate.RequiredProperties(this);
            
            DirectoryInfo dir = new DirectoryInfo(SourceDirectory);
            dir.Zip(TargetPath);

            WorkState workstate = new WorkState(this, "Sucessfully zipped file to {0}"._Format(SourceDirectory));
            return workstate;
        }
    }
}
