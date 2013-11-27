using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Brevitee.Incubation;
using System.Reflection;

namespace Brevitee.ServiceProxy
{
    public delegate ActionResult ExecutionResultDelegate(ExecutionRequest request);
}
