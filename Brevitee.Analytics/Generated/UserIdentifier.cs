// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Analytics.Data
{
	// schema = Analytics
	// connection Name = Analytics
    [Brevitee.Data.Table("UserIdentifier", "Analytics")]
    public partial class UserIdentifier: Dao
    {
        public UserIdentifier():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public UserIdentifier(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator UserIdentifier(DataRow data)
        {
            return new UserIdentifier(data);
        }

		private void SetChildren()
		{

            this.ChildCollections.Add("ImageRating_UserIdentifierId", new ImageRatingCollection(new Query<ImageRatingColumns, ImageRating>((c) => c.UserIdentifierId == this.Id), this, "UserIdentifierId"));							
		}

	// property:Id, columnName:Id	
	[Exclude]
	[Brevitee.Data.KeyColumn(Name="Id", ExtractedType="", MaxLength="")]
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

	// property:UserName, columnName:UserName	
	[Brevitee.Data.Column(Name="UserName", ExtractedType="", MaxLength="", AllowNull=false)]
	public string UserName
	{
		get
		{
			return GetStringValue("UserName");
		}
		set
		{
			SetValue("UserName", value);
		}
	}

	// property:Source, columnName:Source	
	[Brevitee.Data.Column(Name="Source", ExtractedType="", MaxLength="", AllowNull=false)]
	public string Source
	{
		get
		{
			return GetStringValue("Source");
		}
		set
		{
			SetValue("Source", value);
		}
	}



				
	[Exclude]	
	public ImageRatingCollection ImageRatingsByUserIdentifierId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ImageRating_UserIdentifierId"))
			{
				SetChildren();
			}

			var c = (ImageRatingCollection)this.ChildCollections["ImageRating_UserIdentifierId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new UserIdentifierColumns();
			return (colFilter.Id == IdValue);
		}

		public static UserIdentifierCollection Where(Func<UserIdentifierColumns, QueryFilter<UserIdentifierColumns>> where, OrderBy<UserIdentifierColumns> orderBy = null)
		{
			return new UserIdentifierCollection(new Query<UserIdentifierColumns, UserIdentifier>(where, orderBy), true);
		}
		
		public static UserIdentifierCollection Where(WhereDelegate<UserIdentifierColumns> where, Database db = null)
		{
			return new UserIdentifierCollection(new Query<UserIdentifierColumns, UserIdentifier>(where, db), true);
		}
		   
		public static UserIdentifierCollection Where(WhereDelegate<UserIdentifierColumns> where, OrderBy<UserIdentifierColumns> orderBy = null, Database db = null)
		{
			return new UserIdentifierCollection(new Query<UserIdentifierColumns, UserIdentifier>(where, orderBy, db), true);
		}

		public static UserIdentifierCollection Where(QiQuery where, Database db = null)
		{
			return new UserIdentifierCollection(Select<UserIdentifierColumns>.From<UserIdentifier>().Where(where, db));
		}

		public static UserIdentifier OneWhere(WhereDelegate<UserIdentifierColumns> where, Database db = null)
		{
			var results = new UserIdentifierCollection(Select<UserIdentifierColumns>.From<UserIdentifier>().Where(where, db));
			return OneOrThrow(results);
		}

		public static UserIdentifier OneWhere(QiQuery where, Database db = null)
		{
			var results = new UserIdentifierCollection(Select<UserIdentifierColumns>.From<UserIdentifier>().Where(where, db));
			return OneOrThrow(results);
		}

		private static UserIdentifier OneOrThrow(UserIdentifierCollection c)
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

		public static UserIdentifier FirstOneWhere(WhereDelegate<UserIdentifierColumns> where, Database db = null)
		{
			var results = new UserIdentifierCollection(Select<UserIdentifierColumns>.From<UserIdentifier>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static UserIdentifierCollection Top(int count, WhereDelegate<UserIdentifierColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static UserIdentifierCollection Top(int count, WhereDelegate<UserIdentifierColumns> where, OrderBy<UserIdentifierColumns> orderBy, Database database = null)
        {
            UserIdentifierColumns c = new UserIdentifierColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<UserIdentifier>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<UserIdentifier>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserIdentifierColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<UserIdentifierCollection>(0);
        }

		public static long Count(WhereDelegate<UserIdentifierColumns> where, Database database = null)
		{
			UserIdentifierColumns c = new UserIdentifierColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<UserIdentifier>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<UserIdentifier>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
