using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Brevitee.UserAccounts
{
    public class UserNameInUseException : MembershipCreateUserException
    {
        public UserNameInUseException()
            : base(MembershipCreateStatus.DuplicateUserName)
        {
        }

        public UserNameInUseException(string userName)
            : base("The specified userName ({0}) is already in use"._Format(userName))
        {
        }
    }
}
