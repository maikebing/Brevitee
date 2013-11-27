using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    /// <summary>
    /// Use to exclude a method from being proxied or a property from 
    /// being "editable" in a call to Html.InputsFor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class ExcludeAttribute : Attribute
    {
    }
}
