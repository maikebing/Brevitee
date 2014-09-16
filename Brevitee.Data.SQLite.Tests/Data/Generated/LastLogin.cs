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
    [Brevitee.Data.Table("LastLogin", "Test")]
    public partial class LastLogin: Dao
    {
        public LastLogin():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public LastLogin(DataRow data): base(data)
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

	// property:DateTime, columnName:DateTime	
	[Brevitee.Data.Column(Name="DateTime", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime DateTime
	{
		get
		{
			return GetDateTimeValue("DateTime");
		}
		set
		{
			SetValue("DateTime", value);
		}
	}


	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="LastLogin",
		Name="UserId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="1")]
	public long? UserId
	{
		get
		{
			return GetLongValue("UserId");
		}
		set
		{
			SetValue("UserId", value);
		}
	}

	User _userOfUserId;
	public User UserOfUserId
	{
		get
		{
			if(_userOfUserId == null)
			{
				_userOfUserId = SampleData.User.OneWhere(f => f.Id == this.UserId);
			}
			return _userOfUserId;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new LastLoginColumns();
			return (colFilter.Id == IdValue);
		}

		public static LastLoginCollection Where(Func<LastLoginColumns, QueryFilter<LastLoginColumns>> where, OrderBy<LastLoginColumns> orderBy = null)
		{
			return new LastLoginCollection(new Query<LastLoginColumns, LastLogin>(where, orderBy), true);
		}
		
		public static LastLoginCollection Where(WhereDelegate<LastLoginColumns> where, Database db = null)
		{
			return new LastLoginCollection(new Query<LastLoginColumns, LastLogin>(where, db), true);
		}
		   
		public static LastLoginCollection Where(WhereDelegate<LastLoginColumns> where, OrderBy<LastLoginColumns> orderBy = null, Database db = null)
		{
			return new LastLoginCollection(new Query<LastLoginColumns, LastLogin>(where, orderBy, db), true);
		}

		public static LastLoginCollection Where(QiQuery where, Database db = null)
		{
			return new LastLoginCollection(Select<LastLoginColumns>.From<LastLogin>().Where(where, db));
		}

		public static LastLogin OneWhere(WhereDelegate<LastLoginColumns> where, Database db = null)
		{
			var results = new LastLoginCollection(Select<LastLoginColumns>.From<LastLogin>().Where(where, db));
			return OneOrThrow(results);
		}

		public static LastLogin OneWhere(QiQuery where, Database db = null)
		{
			var results = new LastLoginCollection(Select<LastLoginColumns>.From<LastLogin>().Where(where, db));
			return OneOrThrow(results);
		}

		private static LastLogin OneOrThrow(LastLoginCollection c)
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

		public static LastLogin FirstOneWhere(WhereDelegate<LastLoginColumns> where, Database db = null)
		{
			var results = new LastLoginCollection(Select<LastLoginColumns>.From<LastLogin>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static LastLoginCollection Top(int count, WhereDelegate<LastLoginColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static LastLoginCollection Top(int count, WhereDelegate<LastLoginColumns> where, OrderBy<LastLoginColumns> orderBy, Database database = null)
        {
            LastLoginColumns c = new LastLoginColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<LastLogin>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<LastLogin>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<LastLoginColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<LastLoginCollection>(0);
        }

		public static long Count(WhereDelegate<LastLoginColumns> where, Database database = null)
		{
			LastLoginColumns c = new LastLoginColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<LastLogin>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<LastLogin>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
