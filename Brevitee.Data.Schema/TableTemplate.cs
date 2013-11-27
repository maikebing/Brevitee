using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Razor;
using System.Web.Mvc.Razor;
using Brevitee;
using System.Reflection;
using System.IO;

namespace Brevitee.Data.Schema
{
    public abstract class TableTemplate : DaoRazorTemplate<Table>
    {
        public SchemaDefinition Schema
        {
            get;
            set;
        }
    }
}
