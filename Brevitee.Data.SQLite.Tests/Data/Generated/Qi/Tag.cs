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
	[Brevitee.Proxy("tag")]
    public class Tag
    {	
		public object OneWhere(QiQuery query)
		{
			return SampleData.Tag.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return SampleData.Tag.Where(query).ToJsonSafe();
		}
	}
}