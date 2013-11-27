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
    [Brevitee.Data.Table("QueryString", "Analytics")]
    public partial class QueryString: Dao
    {
        public QueryString():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public QueryString(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator QueryString(DataRow data)
        {
            return new QueryString(data);
        }

		private void SetChildren()
		{

            this.ChildCollections.Add("Url_QueryStringId", new UrlCollection(new Query<UrlColumns, Url>((c) => c.QueryStringId == this.Id), this, "QueryStringId"));							
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

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", ExtractedType="", MaxLength="", AllowNull=true)]
	public string Value
	{
		get
		{
			return GetStringValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



				
	[Exclude]	
	public UrlCollection UrlsByQueryStringId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Url_QueryStringId"))
			{
				SetChildren();
			}

			var c = (UrlCollection)this.ChildCollections["Url_QueryStringId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new QueryStringColumns();
			return (colFilter.Id == IdValue);
		}

		public static QueryStringCollection Where(Func<QueryStringColumns, QueryFilter<QueryStringColumns>> where, OrderBy<QueryStringColumns> orderBy = null)
		{
			return new QueryStringCollection(new Query<QueryStringColumns, QueryString>(where, orderBy), true);
		}
		
		public static QueryStringCollection Where(WhereDelegate<QueryStringColumns> where, Database db = null)
		{
			return new QueryStringCollection(new Query<QueryStringColumns, QueryString>(where, db), true);
		}
		   
		public static QueryStringCollection Where(WhereDelegate<QueryStringColumns> where, OrderBy<QueryStringColumns> orderBy = null, Database db = null)
		{
			return new QueryStringCollection(new Query<QueryStringColumns, QueryString>(where, orderBy, db), true);
		}

		public static QueryStringCollection Where(QiQuery where, Database db = null)
		{
			return new QueryStringCollection(Select<QueryStringColumns>.From<QueryString>().Where(where, db));
		}

		public static QueryString OneWhere(WhereDelegate<QueryStringColumns> where, Database db = null)
		{
			var results = new QueryStringCollection(Select<QueryStringColumns>.From<QueryString>().Where(where, db));
			return OneOrThrow(results);
		}

		public static QueryString OneWhere(QiQuery where, Database db = null)
		{
			var results = new QueryStringCollection(Select<QueryStringColumns>.From<QueryString>().Where(where, db));
			return OneOrThrow(results);
		}

		private static QueryString OneOrThrow(QueryStringCollection c)
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

		public static QueryString FirstOneWhere(WhereDelegate<QueryStringColumns> where, Database db = null)
		{
			var results = new QueryStringCollection(Select<QueryStringColumns>.From<QueryString>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static QueryStringCollection Top(int count, WhereDelegate<QueryStringColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static QueryStringCollection Top(int count, WhereDelegate<QueryStringColumns> where, OrderBy<QueryStringColumns> orderBy, Database database = null)
        {
            QueryStringColumns c = new QueryStringColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<QueryString>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<QueryString>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<QueryStringColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<QueryStringCollection>(0);
        }

		public static long Count(WhereDelegate<QueryStringColumns> where, Database database = null)
		{
			QueryStringColumns c = new QueryStringColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<QueryString>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<QueryString>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
