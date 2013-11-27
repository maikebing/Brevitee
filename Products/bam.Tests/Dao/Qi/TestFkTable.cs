using System;
using System.Collections.Generic;
using System.Text;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.DaoRef.Qi
{
	[Proxy("testFkTable")]
    public class TestFkTable
    {	
		public object Create(Brevitee.DaoRef.TestFkTable dao)
		{
			return Update(dao);
		}

		public object Retrieve(long id)
        {
            return Brevitee.DaoRef.TestFkTable.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
        }

		public object Update(Brevitee.DaoRef.TestFkTable dao)
        {
            dao.Save();
            return dao.ToJsonSafe();
        }

		public void Delete(Brevitee.DaoRef.TestFkTable dao)
		{
			dao.Delete();	
		}

		[Exclude]
		public static Type GetDaoType()
		{
			return typeof(Brevitee.DaoRef.TestFkTable);
		}

		public object OneWhere(QiQuery query)
		{
			return Brevitee.DaoRef.TestFkTable.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return Brevitee.DaoRef.TestFkTable.Where(query).ToJsonSafe();
		}
	}
}