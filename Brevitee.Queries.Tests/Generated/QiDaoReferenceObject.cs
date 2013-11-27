using System;
using System.Collections.Generic;
using System.Text;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.DaoReferenceObjects.Data.Qi
{
	[Proxy("daoReferenceObject")]
    public class QiDaoReferenceObject
    {	
		public object OneWhere(QiQuery query)
		{
			return DaoReferenceObject.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return DaoReferenceObject.Where(query).ToJsonSafe();
		}
	}
}