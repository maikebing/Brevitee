using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Automation
{
    public class WorkStateEventArgs: EventArgs
    {
        public WorkStateEventArgs(WorkState state)
        {
            this.WorkState = state;
        }

        public WorkState WorkState
        {
            get;
            set;
        }
    }
}
