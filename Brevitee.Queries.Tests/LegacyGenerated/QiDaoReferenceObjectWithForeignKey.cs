using System;
using System.Collections.Generic;
using System.Text;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.DaoReferenceObjects.Data.Qi
{
	[Proxy("daoReferenceObjectWithForeignKey")]
    public class QiDaoReferenceObjectWithForeignKey
    {	
		public object OneWhere(QiQuery query)
		{
			return DaoReferenceObjectWithForeignKey.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return DaoReferenceObjectWithForeignKey.Where(query).ToJsonSafe();
		}
	}
}