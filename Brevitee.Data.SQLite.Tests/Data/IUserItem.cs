using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleData
{
    public interface IUserItem
    {
        IItem Item { get; }
        User User { get; }
    }
}
