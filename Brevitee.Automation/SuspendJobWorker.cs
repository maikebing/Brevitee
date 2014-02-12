using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Automation
{
    public class SuspendJobWorker: Worker
    {
        public SuspendJobWorker() : base() { }
        public SuspendJobWorker(string name) : base(name) { }


        protected override WorkState Do()
        {
            SuspendedJob suspended = Foreman.Default.SuspendJob(Job); ;
            WorkState<SuspendedJob> result = new WorkState<SuspendedJob>(this, suspended);
            result.Status = Status.Suspended;
            return result;
        }

        public override string[] RequiredProperties
        {
            get { return new string[] { }; } // no required properties to suspend the job
        }
    }
}
