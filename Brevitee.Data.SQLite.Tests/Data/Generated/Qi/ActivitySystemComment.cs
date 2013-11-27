using System;
using System.Collections.Generic;
using System.Text;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace SampleData.Qi
{
	[Brevitee.Proxy("activitySystemComment")]
    public class ActivitySystemComment
    {	
		public object OneWhere(QiQuery query)
		{
			return SampleData.ActivitySystemComment.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return SampleData.ActivitySystemComment.Where(query).ToJsonSafe();
		}
	}
}