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
    [Brevitee.Data.Table("UrlTag", "Analytics")]
    public partial class UrlTag: Dao
    {
        public UrlTag():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public UrlTag(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator UrlTag(DataRow data)
        {
            return new UrlTag(data);
        }

		private void SetChildren()
		{
						
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



	// start UrlId -> UrlId
	[Brevitee.Data.ForeignKey(
        Table="UrlTag",
		Name="UrlId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Url",
		Suffix="1")]
	public long? UrlId
	{
		get
		{
			return GetLongValue("UrlId");
		}
		set
		{
			SetValue("UrlId", value);
		}
	}

	Url _urlOfUrlId;
	public Url UrlOfUrlId
	{
		get
		{
			if(_urlOfUrlId == null)
			{
				_urlOfUrlId = Brevitee.Analytics.Data.Url.OneWhere(f => f.Id == this.UrlId);
			}
			return _urlOfUrlId;
		}
	}
	
	// start TagId -> TagId
	[Brevitee.Data.ForeignKey(
        Table="UrlTag",
		Name="TagId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Tag",
		Suffix="2")]
	public long? TagId
	{
		get
		{
			return GetLongValue("TagId");
		}
		set
		{
			SetValue("TagId", value);
		}
	}

	Tag _tagOfTagId;
	public Tag TagOfTagId
	{
		get
		{
			if(_tagOfTagId == null)
			{
				_tagOfTagId = Brevitee.Analytics.Data.Tag.OneWhere(f => f.Id == this.TagId);
			}
			return _tagOfTagId;
		}
	}
	
				
		


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new UrlTagColumns();
			return (colFilter.Id == IdValue);
		}

		public static UrlTagCollection Where(Func<UrlTagColumns, QueryFilter<UrlTagColumns>> where, OrderBy<UrlTagColumns> orderBy = null)
		{
			return new UrlTagCollection(new Query<UrlTagColumns, UrlTag>(where, orderBy), true);
		}
		
		public static UrlTagCollection Where(WhereDelegate<UrlTagColumns> where, Database db = null)
		{
			return new UrlTagCollection(new Query<UrlTagColumns, UrlTag>(where, db), true);
		}
		   
		public static UrlTagCollection Where(WhereDelegate<UrlTagColumns> where, OrderBy<UrlTagColumns> orderBy = null, Database db = null)
		{
			return new UrlTagCollection(new Query<UrlTagColumns, UrlTag>(where, orderBy, db), true);
		}

		public static UrlTagCollection Where(QiQuery where, Database db = null)
		{
			return new UrlTagCollection(Select<UrlTagColumns>.From<UrlTag>().Where(where, db));
		}

		public static UrlTag OneWhere(WhereDelegate<UrlTagColumns> where, Database db = null)
		{
			var results = new UrlTagCollection(Select<UrlTagColumns>.From<UrlTag>().Where(where, db));
			return OneOrThrow(results);
		}

		public static UrlTag OneWhere(QiQuery where, Database db = null)
		{
			var results = new UrlTagCollection(Select<UrlTagColumns>.From<UrlTag>().Where(where, db));
			return OneOrThrow(results);
		}

		private static UrlTag OneOrThrow(UrlTagCollection c)
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

		public static UrlTag FirstOneWhere(WhereDelegate<UrlTagColumns> where, Database db = null)
		{
			var results = new UrlTagCollection(Select<UrlTagColumns>.From<UrlTag>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static UrlTagCollection Top(int count, WhereDelegate<UrlTagColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static UrlTagCollection Top(int count, WhereDelegate<UrlTagColumns> where, OrderBy<UrlTagColumns> orderBy, Database database = null)
        {
            UrlTagColumns c = new UrlTagColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<UrlTag>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<UrlTag>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UrlTagColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<UrlTagCollection>(0);
        }

		public static long Count(WhereDelegate<UrlTagColumns> where, Database database = null)
		{
			UrlTagColumns c = new UrlTagColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<UrlTag>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<UrlTag>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
