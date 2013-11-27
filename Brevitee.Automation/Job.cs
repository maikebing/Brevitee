using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Automation
{
    /// <summary>
    /// A set of workers that are run in sequence
    /// </summary>
    public class Job
    {
        List<string> _workNames;
        public Job(string name)
        {
            this.Name = name;
            this.WorkQueue = new Queue<IWorker>();
            this._workNames = new List<string>();
        }

        public void AddWork(IWorker work)
        {
            if (_workNames.Contains(work.Name))
            {
                throw new InvalidOperationException("Worker with the name {0} has already been added"._Format(work.Name));
            }

            WorkQueue.Enqueue(work);
        }

        public string Name { get; set; }
        protected Queue<IWorker> WorkQueue { get; private set; }

        public void Do()
        {
            while (WorkQueue.Count > 0)
            {
                IWorker work = WorkQueue.Dequeue();                
                work.Do(this);
            }
        }
    }
}
