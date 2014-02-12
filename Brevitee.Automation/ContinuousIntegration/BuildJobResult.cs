using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Automation.ContinuousIntegration
{
    public enum BuildJobResult
    {
        Invalid,
        BuildFailed,        
        TestsFailed,
        Success
    }
}
