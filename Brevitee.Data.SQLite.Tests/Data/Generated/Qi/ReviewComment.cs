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
	[Brevitee.Proxy("reviewComment")]
    public class ReviewComment
    {	
		public object OneWhere(QiQuery query)
		{
			return SampleData.ReviewComment.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return SampleData.ReviewComment.Where(query).ToJsonSafe();
		}
	}
}