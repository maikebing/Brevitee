using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Brevitee.Server
{
    public class TcpContext
    {
        public TcpContext()
            : this(8192)
        { }

        public TcpContext(int bufferSize)
        {
            this.ReadBufferSize = bufferSize;
            this.RequestData = new byte[bufferSize];
        }

        public int ReadBufferSize { get; set; }

        public byte[] RequestData { get; set; }

        public NetworkStream ResponseStream { get; set; }

        public Encoding Encoding { get; set; }
    }
}
