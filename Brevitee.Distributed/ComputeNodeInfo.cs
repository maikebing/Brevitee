using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Distributed
{
    public class ComputeNodeInfo
    {
        public string HostName { get; set; }
        public string IPV4Address { get; set; }
        public string IPV6Address { get; set; }
        public string Port { get; set; }

        public static ComputeNodeInfo FromComputeNode(ComputeNode computeNode)
        {
            ComputeNodeInfo info = new ComputeNodeInfo();
            info.CopyProperties(computeNode.GetInfo());
            return info;
        }
    }
}
