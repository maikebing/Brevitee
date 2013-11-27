using System;
using System.Collections.Generic;
using System.Text;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace SampleData.Qi
{
	[Brevitee.Proxy("activityComment")]
    public class ActivityComment
    {	
		public object OneWhere(QiQuery query)
		{
			return SampleData.ActivityComment.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return SampleData.ActivityComment.Where(query).ToJsonSafe();
		}
	}
}