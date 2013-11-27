using System;
using System.Collections.Generic;
using System.Text;

namespace Brevitee.System
{
    public interface IRunAsCredentials
    {
        string DomainAndUserName { get; }
        string Domain { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
