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
    [Brevitee.Data.Table("RockPaperScissors", "Test")]
    public partial class RockPaperScissors: Dao
    {
        public RockPaperScissors():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public RockPaperScissors(DataRow data): base(data)
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

	// property:UserOneOption, columnName:UserOneOption	
	[Brevitee.Data.Column(Name="UserOneOption", DbDataType="VarChar", MaxLength="50", AllowNull=true)]
	public string UserOneOption
	{
		get
		{
			return GetStringValue("UserOneOption");
		}
		set
		{
			SetValue("UserOneOption", value);
		}
	}

	// property:UserTwoOption, columnName:UserTwoOption	
	[Brevitee.Data.Column(Name="UserTwoOption", DbDataType="VarChar", MaxLength="50", AllowNull=true)]
	public string UserTwoOption
	{
		get
		{
			return GetStringValue("UserTwoOption");
		}
		set
		{
			SetValue("UserTwoOption", value);
		}
	}

	// property:LastModifiedBy, columnName:LastModifiedBy	
	[Brevitee.Data.Column(Name="LastModifiedBy", DbDataType="BigInt", MaxLength="8", AllowNull=false)]
	public long? LastModifiedBy
	{
		get
		{
			return GetLongValue("LastModifiedBy");
		}
		set
		{
			SetValue("LastModifiedBy", value);
		}
	}

	// property:LastModifiedDate, columnName:LastModifiedDate	
	[Brevitee.Data.Column(Name="LastModifiedDate", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime LastModifiedDate
	{
		get
		{
			return GetDateTimeValue("LastModifiedDate");
		}
		set
		{
			SetValue("LastModifiedDate", value);
		}
	}


	// start UserIdOne -> UserIdOne
	[Brevitee.Data.ForeignKey(
        Table="RockPaperScissors",
		Name="UserIdOne", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="1")]
	public long? UserIdOne
	{
		get
		{
			return GetLongValue("UserIdOne");
		}
		set
		{
			SetValue("UserIdOne", value);
		}
	}

	User _userOfUserIdOne;
	public User UserOfUserIdOne
	{
		get
		{
			if(_userOfUserIdOne == null)
			{
				_userOfUserIdOne = SampleData.User.OneWhere(f => f.Id == this.UserIdOne);
			}
			return _userOfUserIdOne;
		}
	}
	
	// start UserIdTwo -> UserIdTwo
	[Brevitee.Data.ForeignKey(
        Table="RockPaperScissors",
		Name="UserIdTwo", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="2")]
	public long? UserIdTwo
	{
		get
		{
			return GetLongValue("UserIdTwo");
		}
		set
		{
			SetValue("UserIdTwo", value);
		}
	}

	User _userOfUserIdTwo;
	public User UserOfUserIdTwo
	{
		get
		{
			if(_userOfUserIdTwo == null)
			{
				_userOfUserIdTwo = SampleData.User.OneWhere(f => f.Id == this.UserIdTwo);
			}
			return _userOfUserIdTwo;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new RockPaperScissorsColumns();
			return (colFilter.Id == IdValue);
		}

		public static RockPaperScissorsCollection Where(Func<RockPaperScissorsColumns, QueryFilter<RockPaperScissorsColumns>> where, OrderBy<RockPaperScissorsColumns> orderBy = null)
		{
			return new RockPaperScissorsCollection(new Query<RockPaperScissorsColumns, RockPaperScissors>(where, orderBy), true);
		}
		
		public static RockPaperScissorsCollection Where(WhereDelegate<RockPaperScissorsColumns> where, Database db = null)
		{
			return new RockPaperScissorsCollection(new Query<RockPaperScissorsColumns, RockPaperScissors>(where, db), true);
		}
		   
		public static RockPaperScissorsCollection Where(WhereDelegate<RockPaperScissorsColumns> where, OrderBy<RockPaperScissorsColumns> orderBy = null, Database db = null)
		{
			return new RockPaperScissorsCollection(new Query<RockPaperScissorsColumns, RockPaperScissors>(where, orderBy, db), true);
		}

		public static RockPaperScissorsCollection Where(QiQuery where, Database db = null)
		{
			return new RockPaperScissorsCollection(Select<RockPaperScissorsColumns>.From<RockPaperScissors>().Where(where, db));
		}

		public static RockPaperScissors OneWhere(WhereDelegate<RockPaperScissorsColumns> where, Database db = null)
		{
			var results = new RockPaperScissorsCollection(Select<RockPaperScissorsColumns>.From<RockPaperScissors>().Where(where, db));
			return OneOrThrow(results);
		}

		public static RockPaperScissors OneWhere(QiQuery where, Database db = null)
		{
			var results = new RockPaperScissorsCollection(Select<RockPaperScissorsColumns>.From<RockPaperScissors>().Where(where, db));
			return OneOrThrow(results);
		}

		private static RockPaperScissors OneOrThrow(RockPaperScissorsCollection c)
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

		public static RockPaperScissors FirstOneWhere(WhereDelegate<RockPaperScissorsColumns> where, Database db = null)
		{
			var results = new RockPaperScissorsCollection(Select<RockPaperScissorsColumns>.From<RockPaperScissors>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static RockPaperScissorsCollection Top(int count, WhereDelegate<RockPaperScissorsColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static RockPaperScissorsCollection Top(int count, WhereDelegate<RockPaperScissorsColumns> where, OrderBy<RockPaperScissorsColumns> orderBy, Database database = null)
        {
            RockPaperScissorsColumns c = new RockPaperScissorsColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<RockPaperScissors>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<RockPaperScissors>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<RockPaperScissorsColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<RockPaperScissorsCollection>(0);
        }

		public static long Count(WhereDelegate<RockPaperScissorsColumns> where, Database database = null)
		{
			RockPaperScissorsColumns c = new RockPaperScissorsColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<RockPaperScissors>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<RockPaperScissors>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
