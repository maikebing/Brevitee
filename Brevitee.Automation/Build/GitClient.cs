using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace Brevitee.Automation.Build
{
    public class GitClient : IBuildSourceControlClient
    {

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
