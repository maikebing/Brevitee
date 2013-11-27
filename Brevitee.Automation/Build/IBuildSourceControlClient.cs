using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Automation.Build
{
    public interface IBuildSourceControlClient
    {
        void RetrieveLatest(string source, string localDirectory);
        void RetrieveTag(string tagName, string localDirectory);
        void SetTag(string tagName, string message);
        void Notify(string notificationRecieverIdentifier, string buildIdentifier, BuildJobResult buildResult, string message);
    }
}
