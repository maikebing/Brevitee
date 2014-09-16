using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.ServiceProxy
{
    public class DefaultUserResolver: IUserResolver
    {
        static DefaultUserResolver()
        {
            Instance = new DefaultUserResolver();
        }

        public static IUserResolver Instance
        {
            get;
            set;
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }

        public string GetCurrentUser()
        {
            string userName = DefaultWebUserResolver.Instance.GetCurrentUser();
            if (string.IsNullOrEmpty(userName))
            {
                userName = UserUtil.GetCurrentWindowsUser(false);
            }

            return userName;
        }

        public string GetUser(IHttpContext context)
        {
            return DefaultWebUserResolver.GetUserFromContext(context);
        }
    }
}
