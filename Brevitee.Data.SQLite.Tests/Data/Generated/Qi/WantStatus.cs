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
	[Brevitee.Proxy("wantStatus")]
    public class WantStatus
    {	
		public object OneWhere(QiQuery query)
		{
			return SampleData.WantStatus.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return SampleData.WantStatus.Where(query).ToJsonSafe();
		}
	}
}