using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.CommandLine;
using Brevitee.Testing;
using Brevitee.Net;
using Brevitee.Net.Dns;

namespace Brevitee.Net.Tests
{
    [Serializable]
    public class UnitTests: CommandLineTestInterface
    {
        [UnitTest]
        public void LookupMxRecordTest()
        {
            MXRecord[] mxRecords = DnsClient.LookupMXRecord("google.com");
            mxRecords.Each(mx =>
            {
                OutLine(mx.HostName);
            });
        }
    }
}
