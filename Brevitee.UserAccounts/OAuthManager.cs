using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.ServiceProxy;
using Brevitee.UserAccounts.Data;

namespace Brevitee.UserAccounts
{
    [Proxy("oauth")]
    public class OAuthManager: IRequiresHttpContext
    {
        public Session Session
        {
            get
            {
                return Session.Get(HttpContext);
            }
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }

        public PageResult SetToken(string accessToken)
        {
            Session["accessToken"] = accessToken;
            return new PageResult("TokenSet");
        }
    }
}
