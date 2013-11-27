using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Brevitee;
using Brevitee.CommandLine;
using Brevitee.Data.Model;

namespace _CommandLineMenuInterface
{
    class Program : CommandLineMenuInterface
    {
        static void Main(string[] args)
        {
            // Set AssemblyToAnalyze to interact with 
            // classes from that assembly
            Start(args);
        }
    }
}
