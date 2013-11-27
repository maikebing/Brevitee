using System;
using System.Collections.Generic;
using System.Text;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.DaoRef.Qi
{
	[Proxy("testTable")]
    public class TestTable
    {	
		public object Create(Brevitee.DaoRef.TestTable dao)
		{
			return Update(dao);
		}

		public object Retrieve(long id)
        {
            return Brevitee.DaoRef.TestTable.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
        }

		public object Update(Brevitee.DaoRef.TestTable dao)
        {
            dao.Save();
            return dao.ToJsonSafe();
        }

		public void Delete(Brevitee.DaoRef.TestTable dao)
		{
			dao.Delete();	
		}

		[Exclude]
		public static Type GetDaoType()
		{
			return typeof(Brevitee.DaoRef.TestTable);
		}

		public object OneWhere(QiQuery query)
		{
			return Brevitee.DaoRef.TestTable.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return Brevitee.DaoRef.TestTable.Where(query).ToJsonSafe();
		}
	}
}