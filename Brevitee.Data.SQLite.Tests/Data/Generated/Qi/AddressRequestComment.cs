using System;
using System.Collections.Generic;
using System.Text;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;
using Brevitee.ServiceProxy;
using Brevitee.ServiceProxy.Js;

namespace SampleData.Qi
{
	[Brevitee.Proxy("addressRequestComment")]
    public class AddressRequestComment
    {	
		public object OneWhere(QiQuery query)
		{
			return SampleData.AddressRequestComment.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return SampleData.AddressRequestComment.Where(query).ToJsonSafe();
		}
	}
}