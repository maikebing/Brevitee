using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Automation
{
    public class IfElseWork: Worker
    {
        public IfElseWork() : base() { }
        public IfElseWork(string name) : base(name) { }
        public IfElseWork(string name, Worker workIfTrue, Worker elseWork)
            : base(name)
        {
            this.WorkIfTrue = workIfTrue;
            this.ElseWork = elseWork;
        }

        public override string[] RequiredProperties
        {
            get { return new string[] { "Name" }; }
        }
        public bool Condition { get; set; }
        public Worker WorkIfTrue { get; set; }
        public Worker ElseWork { get; set; }

        protected override WorkState Do()
        {
            return Condition ? WorkIfTrue.Do(this.Job) : ElseWork.Do(this.Job);
        }
    }
}
