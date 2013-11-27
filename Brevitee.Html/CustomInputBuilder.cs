using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Brevitee.Html
{
    public delegate TagBuilder CustomInputBuilder(PropertyInfo property);
}
