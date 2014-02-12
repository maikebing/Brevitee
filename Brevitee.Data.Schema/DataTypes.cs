using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data.Schema
{
    public enum DataTypes
    {
        SelectDataType,
        Boolean,
        Int,
        Long,
        Decimal,
        String,
        /// <summary>
        /// The field will be generated as a byte array (byte[])
        /// </summary>
        Byte,
        DateTime
    }
}
