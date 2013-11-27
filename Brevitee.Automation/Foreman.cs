using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Configuration;

namespace Brevitee.Automation
{
    public static class Foreman
    {
        public static void Configure(IWorker worker, object configuration)
        {
            DefaultConfiguration.CopyProperties(configuration, worker);
            if (worker.ImplementsInterface<IHasRequiredProperties>())
            {
                DefaultConfiguration.CheckRequiredProperties((IHasRequiredProperties)worker);
            }
        }
    }
}
