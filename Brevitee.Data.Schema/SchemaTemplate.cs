using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Data.Schema
{
    public abstract class SchemaTemplate : DaoRazorTemplate<SchemaDefinition>
    {

        public void WriteContextMethods(Table table)
        {
            RazorParser<TableTemplate> razorParser = new RazorParser<TableTemplate>();
            Write(razorParser.ExecuteResource("ContextMethods.tmpl", new { Model = table }));
        }
    }
}
