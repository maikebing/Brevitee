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
    [Brevitee.Data.Table("Path", "Analytics")]
    public partial class Path: Dao
    {
        public Path():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Path(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Path(DataRow data)
        {
            return new Path(data);
        }

		private void SetChildren()
		{

            this.ChildCollections.Add("Url_PathId", new UrlCollection(new Query<UrlColumns, Url>((c) => c.PathId == this.Id), this, "PathId"));							
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
	public UrlCollection UrlsByPathId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Url_PathId"))
			{
				SetChildren();
			}

			var c = (UrlCollection)this.ChildCollections["Url_PathId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new PathColumns();
			return (colFilter.Id == IdValue);
		}

		public static PathCollection Where(Func<PathColumns, QueryFilter<PathColumns>> where, OrderBy<PathColumns> orderBy = null)
		{
			return new PathCollection(new Query<PathColumns, Path>(where, orderBy), true);
		}
		
		public static PathCollection Where(WhereDelegate<PathColumns> where, Database db = null)
		{
			return new PathCollection(new Query<PathColumns, Path>(where, db), true);
		}
		   
		public static PathCollection Where(WhereDelegate<PathColumns> where, OrderBy<PathColumns> orderBy = null, Database db = null)
		{
			return new PathCollection(new Query<PathColumns, Path>(where, orderBy, db), true);
		}

		public static PathCollection Where(QiQuery where, Database db = null)
		{
			return new PathCollection(Select<PathColumns>.From<Path>().Where(where, db));
		}

		public static Path OneWhere(WhereDelegate<PathColumns> where, Database db = null)
		{
			var results = new PathCollection(Select<PathColumns>.From<Path>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Path OneWhere(QiQuery where, Database db = null)
		{
			var results = new PathCollection(Select<PathColumns>.From<Path>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Path OneOrThrow(PathCollection c)
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

		public static Path FirstOneWhere(WhereDelegate<PathColumns> where, Database db = null)
		{
			var results = new PathCollection(Select<PathColumns>.From<Path>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static PathCollection Top(int count, WhereDelegate<PathColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static PathCollection Top(int count, WhereDelegate<PathColumns> where, OrderBy<PathColumns> orderBy, Database database = null)
        {
            PathColumns c = new PathColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<Path>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Path>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PathColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<PathCollection>(0);
        }

		public static long Count(WhereDelegate<PathColumns> where, Database database = null)
		{
			PathColumns c = new PathColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Path>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Path>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
