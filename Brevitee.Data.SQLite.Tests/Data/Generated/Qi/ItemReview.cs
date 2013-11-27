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
	[Brevitee.Proxy("itemReview")]
    public class ItemReview
    {	
		public object OneWhere(QiQuery query)
		{
			return SampleData.ItemReview.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return SampleData.ItemReview.Where(query).ToJsonSafe();
		}
	}
}