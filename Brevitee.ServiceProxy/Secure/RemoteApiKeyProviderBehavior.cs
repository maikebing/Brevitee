using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.ServiceProxy.Secure
{
    public enum RemoteApiKeyProviderBehavior
    {
        Invalid,
        AddNewKey,
        ReturnEmptyString,
        Throw
    }
}
