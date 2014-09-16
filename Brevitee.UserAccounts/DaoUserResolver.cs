using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.ServiceProxy;
using Brevitee.Data;
using Brevitee.UserAccounts;
using Brevitee.UserAccounts.Data;

namespace Brevitee.UserAccounts
{
    public class DaoUserResolver: IUserResolver
    {
        public string GetCurrentUser()
        {
            Session session = Session.Get(HttpContext);
            return session.UserOfUserId.UserName;
        }

        public string GetUser(IHttpContext context)
        {
            Session session = Session.Get(context);
            return session.UserOfUserId.UserName;
        }

        public void SetUser(IHttpContext context, string userName, bool isAuthenticated)
        {
            User user = User.GetByUserNameOrDie(userName);
            SetUser(context, user, isAuthenticated);
        }

        public void SetUser(IHttpContext context, User user, bool isAuthenticated)
        {
            Session session = Session.Get(context);
            session.UserId = user.Id;
            session.Save();

            context.User = new DaoPrincipal(user, isAuthenticated);
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }
    }
}
