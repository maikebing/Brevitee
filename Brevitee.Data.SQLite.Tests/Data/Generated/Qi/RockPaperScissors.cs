using System;
using System.Collections.Generic;
using System.Text;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace SampleData.Qi
{
	[Brevitee.Proxy("rockPaperScissors")]
    public class RockPaperScissors
    {	
		public object OneWhere(QiQuery query)
		{
			return SampleData.RockPaperScissors.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return SampleData.RockPaperScissors.Where(query).ToJsonSafe();
		}
	}
}