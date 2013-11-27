using System;
using System.Collections.Generic;
using System.Text;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace SampleData.Qi
{
	[Brevitee.Proxy("accessRequest")]
    public class AccessRequest
    {	
		public object OneWhere(QiQuery query)
		{
			return SampleData.AccessRequest.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return SampleData.AccessRequest.Where(query).ToJsonSafe();
		}
	}
}