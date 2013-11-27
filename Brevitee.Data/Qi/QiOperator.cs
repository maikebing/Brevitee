using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data.Qi
{
    public enum QiOperator
    {
        Invalid,
        Equals,
        NotEqualTo,
        GreaterThan,
        LessThan,
        StartsWith,
        DoesntStartWith,
        EndsWith,
        DoesntEndWith,
        Contains,
        DoesntContain,
        OpenParen,
        CloseParen,
        AND,
        OR
    }
}
