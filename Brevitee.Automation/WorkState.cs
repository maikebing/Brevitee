using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Automation
{
    public class WorkState<T>: WorkState
    {
        public WorkState() : base() { }
        public WorkState(T data)
        {
            this.Data = data;
        }
        public WorkState(Exception ex) : base(ex) { }

        public T Data { get; set; }

        public override bool HasValue
        {
            get
            {
                return Data != null;
            }
        }
    }

    public class WorkState
    {
        public WorkState()
        {
            this.Sucess = true;
        }

        public WorkState(string message): this()
        {
            this.Message = message;
        }

        public WorkState(Worker work, string message)
            : this(message)
        {
            Args.ThrowIfNull(work, "work");

            this.WorkName = work.Name;

            if (work.Job != null)
            {
                this.JobName = work.Job.Name;
            }
        }

        public WorkState(Exception ex)
        {
            this.Sucess = false;
            this.Message = !string.IsNullOrEmpty(ex.StackTrace) ? string.Format("{0}:\r\n\r\n{1}", ex.Message, ex.StackTrace) : ex.Message;
        }

        public string JobName { get; set; }
        public string WorkName { get; set; }
        public string Message { get; set; }

        bool _error;
        public bool Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                _success = !value;
            }
        }

        bool _success;
        public bool Sucess
        {
            get
            {
                return _success;
            }
            set
            {
                _success = value;
                _error = !value;
            }
        }

        public bool Complete
        {
            get;
            internal set;
        }

        public virtual bool HasValue { get { return false; } }
    }
}
