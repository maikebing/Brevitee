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
using KLGates.Core.CommandLine;
using KLGates.Core;
using KLGates.Core.Testing;
using KLGates.Core.Encryption;

namespace TestBuildProject
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
