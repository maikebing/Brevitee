using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Automation
{
    public abstract class Worker: IWorker
    {
        public Worker(string name)
        {
            this.Name = name;
        }

        public Job Job { get; set; }
        public string Name { get; set; }
        public bool Busy { get; set; }
        object _state;

        /// <summary>
        /// Gets or sets the current WorkState of this Worker
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public WorkState<T> State<T>(WorkState<T> state)
        {
            if (state != null)
            {
                _state = state;
            }

            return (WorkState<T>)_state;
        }

        public WorkState Do(Job job)
        {
            this.Job = job;
            WorkState state = null;
            try
            {
                state = Do();
            }
            catch (Exception ex)
            {
                state = new WorkState(ex);
            }

            return state;
        }

        protected abstract WorkState Do();
    }
}
