using System;
using System.Collections.Generic;
using System.Text;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace SampleData.Qi
{
	[Brevitee.Proxy("activity")]
    public class Activity
    {	
		public object OneWhere(QiQuery query)
		{
			return SampleData.Activity.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return SampleData.Activity.Where(query).ToJsonSafe();
		}
	}
}