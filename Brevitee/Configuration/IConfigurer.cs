using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Configuration
{
    public interface IConfigurer
    {
        void Configure(IConfigurable configurable);
    }
}
