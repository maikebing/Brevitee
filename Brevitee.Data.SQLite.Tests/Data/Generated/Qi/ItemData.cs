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
	[Brevitee.Proxy("itemData")]
    public class ItemData
    {	
		public object OneWhere(QiQuery query)
		{
			return SampleData.ItemData.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return SampleData.ItemData.Where(query).ToJsonSafe();
		}
	}
}