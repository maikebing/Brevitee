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
using Brevitee.Data;
using Brevitee.Testing;
using Brevitee.Encryption;

namespace Brevitee.Automation.Tests
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            Initialize(args);
        }
    }
}
