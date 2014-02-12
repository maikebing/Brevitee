using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Automation.ContinuousIntegration
{
    public interface ITestWorker
    {
        string SearchPattern { get; set; }
        string Filter { get; set; }
        string TestDirectory { get; set; }

        string CoverageOutputFileName { get; set; }
    }
}
