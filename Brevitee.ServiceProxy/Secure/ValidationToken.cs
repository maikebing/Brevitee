using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.ServiceProxy.Secure
{
    public class ValidationToken
    {
        public string NonceCipher
        {
            get;
            set;
        }

        public string HashCipher
        {
            get;
            set;
        }
    }
}
