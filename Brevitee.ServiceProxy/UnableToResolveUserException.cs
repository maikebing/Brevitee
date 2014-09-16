using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.ServiceProxy
{
    public class UnableToResolveUserException: Exception
    {
        public UnableToResolveUserException(IUserResolver resolver)
            : base("Unable to resolve user with specified resolver: ({0})"._Format(resolver.GetType().FullName))
        {
            this.Resolver = resolver;
        }

        public IUserResolver Resolver
        {
            get;
            set;
        }
    }
}
