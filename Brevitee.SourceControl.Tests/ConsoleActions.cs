using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Testing;
using Brevitee.Encryption;

namespace Brevitee.SourceControl.Tests
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        [ConsoleAction("Add your action here")]
        public void AddYourActionHere()
        {
        }
    }
}
