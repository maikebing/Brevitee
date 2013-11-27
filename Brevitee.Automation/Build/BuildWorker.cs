using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Automation.Build
{
    public class BuildWorker: Worker
    {
        public BuildWorker(string name)
            : base(name)
        { }

        public IBuildSourceControlClient SourceControlClient
        {
            get;
            set;
        }

        protected override WorkState Do()
        {
            throw new NotImplementedException();
        }
    }
}
