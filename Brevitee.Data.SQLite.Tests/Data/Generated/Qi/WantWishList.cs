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
	[Brevitee.Proxy("wantWishList")]
    public class WantWishList
    {	
		public object OneWhere(QiQuery query)
		{
			return SampleData.WantWishList.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return SampleData.WantWishList.Where(query).ToJsonSafe();
		}
	}
}