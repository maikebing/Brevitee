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
    [Brevitee.Data.Table("Password", "Test")]
    public partial class Password: Dao
    {
        public Password():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Password(DataRow data): base(data)
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

	// property:SHA256, columnName:SHA256	
	[Brevitee.Data.Column(Name="SHA256", DbDataType="Binary", MaxLength="32", AllowNull=false)]
	public byte[] SHA256
	{
		get
		{
			return GetByteValue("SHA256");
		}
		set
		{
			SetValue("SHA256", value);
		}
	}

	// property:Modified, columnName:Modified	
	[Brevitee.Data.Column(Name="Modified", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime Modified
	{
		get
		{
			return GetDateTimeValue("Modified");
		}
		set
		{
			SetValue("Modified", value);
		}
	}

	// property:Verified, columnName:Verified	
	[Brevitee.Data.Column(Name="Verified", DbDataType="Bit", MaxLength="1", AllowNull=false)]
	public bool? Verified
	{
		get
		{
			return GetBooleanValue("Verified");
		}
		set
		{
			SetValue("Verified", value);
		}
	}


	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="Password",
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
			var colFilter = new PasswordColumns();
			return (colFilter.Id == IdValue);
		}

		public static PasswordCollection Where(Func<PasswordColumns, QueryFilter<PasswordColumns>> where, OrderBy<PasswordColumns> orderBy = null)
		{
			return new PasswordCollection(new Query<PasswordColumns, Password>(where, orderBy), true);
		}
		
		public static PasswordCollection Where(WhereDelegate<PasswordColumns> where, Database db = null)
		{
			return new PasswordCollection(new Query<PasswordColumns, Password>(where, db), true);
		}
		   
		public static PasswordCollection Where(WhereDelegate<PasswordColumns> where, OrderBy<PasswordColumns> orderBy = null, Database db = null)
		{
			return new PasswordCollection(new Query<PasswordColumns, Password>(where, orderBy, db), true);
		}

		public static PasswordCollection Where(QiQuery where, Database db = null)
		{
			return new PasswordCollection(Select<PasswordColumns>.From<Password>().Where(where, db));
		}

		public static Password OneWhere(WhereDelegate<PasswordColumns> where, Database db = null)
		{
			var results = new PasswordCollection(Select<PasswordColumns>.From<Password>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Password OneWhere(QiQuery where, Database db = null)
		{
			var results = new PasswordCollection(Select<PasswordColumns>.From<Password>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Password OneOrThrow(PasswordCollection c)
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

		public static Password FirstOneWhere(WhereDelegate<PasswordColumns> where, Database db = null)
		{
			var results = new PasswordCollection(Select<PasswordColumns>.From<Password>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static PasswordCollection Top(int count, WhereDelegate<PasswordColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static PasswordCollection Top(int count, WhereDelegate<PasswordColumns> where, OrderBy<PasswordColumns> orderBy, Database database = null)
        {
            PasswordColumns c = new PasswordColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<Password>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Password>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PasswordColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<PasswordCollection>(0);
        }

		public static long Count(WhereDelegate<PasswordColumns> where, Database database = null)
		{
			PasswordColumns c = new PasswordColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Password>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Password>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
