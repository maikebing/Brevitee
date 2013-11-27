using System;

namespace Brevitee.Data
{
    public interface IFilterToken
    {
        string Operator { get; set; }
        string ToString();
    }
}
