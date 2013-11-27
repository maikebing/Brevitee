using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Brevitee.Net
{
    public class DnsServerNotFoundException: Exception
    {
        public DnsServerNotFoundException()
		{
			// no implementation
		}

		public DnsServerNotFoundException(Exception innerException) :  base(null, innerException) 
		{
			// no implementation
		}

		public DnsServerNotFoundException(string message, Exception innerException) : base (message, innerException)
		{
			// no implementation
		}

        protected DnsServerNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
		{
			// no implementation
		}
    }
}
