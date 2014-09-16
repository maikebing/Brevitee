using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Configuration;

namespace Brevitee.ServiceProxy.Secure
{
    public class ApiKeyInfo
    {
        public ApiKeyInfo()
        {
            this.ApplicationNameProvider = new DefaultConfigurationApplicationNameProvider();
        }
        protected internal IApplicationNameProvider ApplicationNameProvider
        {
            get;
            set;
        }
        public string ApplicationName
        {
            get
            {
                return ApplicationNameProvider.GetApplicationName();
            }
        }

        public string ApplicationClientId
        {
            get;
            set;
        }

        public string ApiKey
        {
            get;
            set;
        }
    }
}
