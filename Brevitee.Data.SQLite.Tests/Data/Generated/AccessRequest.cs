using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Schema; 
using Brevitee.Data.Qi;
using Brevitee.ServiceProxy;
using Brevitee.ServiceProxy.Js;

namespace SampleData
{
    [Brevitee.Data.Table("AccessRequest", "Test")]
    public partial class AccessRequest: Dao
    {
        public AccessRequest():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public AccessRequest(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

		}

	// property:Id, columnName:Id	
	[Brevitee.Data.KeyColumn(Name="Id", DbDataType="BigInt", MaxLength="8")]
	public long? Id
	{
		get
		{
			return GetLongValue("Id");
		}
		set
		{
			SetValue("Id", value);
		}
	}

	// property:Email, columnName:Email	
	[Brevitee.Data.Column(Name="Email", DbDataType="VarChar", MaxLength="255", AllowNull=false)]
	public string Email
	{
		get
		{
			return GetStringValue("Email");
		}
		set
		{
			SetValue("Email", value);
		}
	}


				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new AccessRequestColumns();
			return (colFilter.Id == IdValue);
		}

		public static AccessRequestCollection Where(Func<AccessRequestColumns, QueryFilter<AccessRequestColumns>> where, OrderBy<AccessRequestColumns> orderBy = null)
		{
			return new AccessRequestCollection(new Query<AccessRequestColumns, AccessRequest>(where, orderBy), true);
		}
		
		public static AccessRequestCollection Where(WhereDelegate<AccessRequestColumns> where, Database db = null)
		{
			return new AccessRequestCollection(new Query<AccessRequestColumns, AccessRequest>(where, db), true);
		}
		   
		public static AccessRequestCollection Where(WhereDelegate<AccessRequestColumns> where, OrderBy<AccessRequestColumns> orderBy = null, Database db = null)
		{
			return new AccessRequestCollection(new Query<AccessRequestColumns, AccessRequest>(where, orderBy, db), true);
		}

		public static AccessRequestCollection Where(QiQuery where, Database db = null)
		{
			return new AccessRequestCollection(Select<AccessRequestColumns>.From<AccessRequest>().Where(where, db));
		}

		public static AccessRequest OneWhere(WhereDelegate<AccessRequestColumns> where, Database db = null)
		{
			var results = new AccessRequestCollection(Select<AccessRequestColumns>.From<AccessRequest>().Where(where, db));
			return OneOrThrow(results);
		}

		public static AccessRequest OneWhere(QiQuery where, Database db = null)
		{
			var results = new AccessRequestCollection(Select<AccessRequestColumns>.From<AccessRequest>().Where(where, db));
			return OneOrThrow(results);
		}

		private static AccessRequest OneOrThrow(AccessRequestCollection c)
		{
			if(c.Count == 1)
			{
				return c[0];
			}
			else if(c.Count > 1)
			{
				throw new MultipleEntriesFoundException();
			}

			return null;
		}

		public static AccessRequest FirstOneWhere(WhereDelegate<AccessRequestColumns> where, Database db = null)
		{
			var results = new AccessRequestCollection(Select<AccessRequestColumns>.From<AccessRequest>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static AccessRequestCollection Top(int count, WhereDelegate<AccessRequestColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static AccessRequestCollection Top(int count, WhereDelegate<AccessRequestColumns> where, OrderBy<AccessRequestColumns> orderBy, Database database = null)
        {
            AccessRequestColumns c = new AccessRequestColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<AccessRequest>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<AccessRequest>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AccessRequestColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<AccessRequestCollection>(0);
        }

		public static long Count(WhereDelegate<AccessRequestColumns> where, Database database = null)
		{
			AccessRequestColumns c = new AccessRequestColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<AccessRequest>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<AccessRequest>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
