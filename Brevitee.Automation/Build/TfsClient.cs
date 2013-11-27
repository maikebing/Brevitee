using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Automation.Build
{
    public class TfsClient: IBuildSourceControlClient
    {
        public string Server { get; set; }
        #region ISourceControlClient Members

        public void RetrieveLatest(string source, string localDirectory)
        {
            throw new NotImplementedException();
        }

        public void RetrieveTag(string tagName, string localDirectory)
        {
            throw new NotImplementedException();
        }

        public void SetTag(string tagName, string message)
        {
            throw new NotImplementedException();
        }

        public void Notify(string notificationRecieverIdentifier, string buildIdentifier, BuildJobResult buildResult, string message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
