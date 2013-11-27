using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Automation
{
    public class MultiFileCopyWorker: Worker, IConfigurable, IEnumerable<string>
    {
        List<string> _filePaths;

        public MultiFileCopyWorker(string name)
            : base(name)
        {
            this._filePaths = new List<string>();
        }

        public void AddFile(string path)
        {
            _filePaths.Add(path);
        }

        public string[] FilePaths
        {
            get
            {
                return _filePaths.ToArray();
            }
        }

        public string Destination
        {
            get;
            set;
        }

        public void RemoveFile(string path)
        {
            _filePaths.Remove(path);
        }
        #region IConfigurable Members

        public void Configure(object configuration)
        {
            Foreman.Configure(this, configuration);
        }

        #endregion

        #region IEnumerable<string> Members

        public IEnumerator<string> GetEnumerator()
        {
            return _filePaths.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            return _filePaths.GetEnumerator();
        }

        #endregion

        protected override WorkState Do()
        {
            throw new NotImplementedException();
        }
    }
}
