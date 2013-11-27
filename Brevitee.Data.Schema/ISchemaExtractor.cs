using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data.Schema
{
    public interface ISchemaExtractor
    {
        SchemaDefinition Extract();
    }
}
