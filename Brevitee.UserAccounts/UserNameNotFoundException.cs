using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.UserAccounts
{
    public class UserNameNotFoundException: Exception
    {
        public UserNameNotFoundException(string userName)
            : base("The specified user ({0}) was not found"._Format(userName))
        { }    
    }
}
