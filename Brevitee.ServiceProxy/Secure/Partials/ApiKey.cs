// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.ServiceProxy.Secure
{
	public partial class ApiKey
	{
        public static ApiKey Blank
        {
            get
            {
                return new ApiKey { SharedSecret = string.Empty };
            }
        }
		  // Your code here
	}
}																								
