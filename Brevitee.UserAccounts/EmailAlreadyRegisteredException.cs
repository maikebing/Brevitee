using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.UserAccounts
{
    public class EmailAlreadyRegisteredException : Exception
    {
        public EmailAlreadyRegisteredException(string email)
            : base("The specified email ({0}) has already been registered"._Format(email))
        {
        }
    }
}
