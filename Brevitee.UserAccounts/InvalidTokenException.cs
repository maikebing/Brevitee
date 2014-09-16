using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.UserAccounts
{
    public class InvalidTokenException: Exception
    {
        public InvalidTokenException()
            : base("The specified token was expired or invalid")
        { }
    }
}
