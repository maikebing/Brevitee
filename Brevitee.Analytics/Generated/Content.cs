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
    [Brevitee.Data.Table("Content", "Analytics")]
    public partial class Content: Dao
    {
        public Content():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Content(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Content(DataRow data)
        {
            return new Content(data);
        }

		private void SetChildren()
		{

            this.ChildCollections.Add("HtmlElement_ContentId", new HtmlElementCollection(new Query<HtmlElementColumns, HtmlElement>((c) => c.ContentId == this.Id), this, "ContentId"));							
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

	// property:Hash, columnName:Hash	
	[Brevitee.Data.Column(Name="Hash", ExtractedType="", MaxLength="", AllowNull=true)]
	public string Hash
	{
		get
		{
			return GetStringValue("Hash");
		}
		set
		{
			SetValue("Hash", value);
		}
	}

	// property:HashAlgorithm, columnName:HashAlgorithm	
	[Brevitee.Data.Column(Name="HashAlgorithm", ExtractedType="", MaxLength="", AllowNull=true)]
	public string HashAlgorithm
	{
		get
		{
			return GetStringValue("HashAlgorithm");
		}
		set
		{
			SetValue("HashAlgorithm", value);
		}
	}

	// property:Date, columnName:Date	
	[Brevitee.Data.Column(Name="Date", ExtractedType="", MaxLength="", AllowNull=false)]
	public DateTime Date
	{
		get
		{
			return GetDateTimeValue("Date");
		}
		set
		{
			SetValue("Date", value);
		}
	}

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", ExtractedType="", MaxLength="", AllowNull=false)]
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
	public HtmlElementCollection HtmlElementsByContentId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("HtmlElement_ContentId"))
			{
				SetChildren();
			}

			var c = (HtmlElementCollection)this.ChildCollections["HtmlElement_ContentId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ContentColumns();
			return (colFilter.Id == IdValue);
		}

		public static ContentCollection Where(Func<ContentColumns, QueryFilter<ContentColumns>> where, OrderBy<ContentColumns> orderBy = null)
		{
			return new ContentCollection(new Query<ContentColumns, Content>(where, orderBy), true);
		}
		
		public static ContentCollection Where(WhereDelegate<ContentColumns> where, Database db = null)
		{
			return new ContentCollection(new Query<ContentColumns, Content>(where, db), true);
		}
		   
		public static ContentCollection Where(WhereDelegate<ContentColumns> where, OrderBy<ContentColumns> orderBy = null, Database db = null)
		{
			return new ContentCollection(new Query<ContentColumns, Content>(where, orderBy, db), true);
		}

		public static ContentCollection Where(QiQuery where, Database db = null)
		{
			return new ContentCollection(Select<ContentColumns>.From<Content>().Where(where, db));
		}

		public static Content OneWhere(WhereDelegate<ContentColumns> where, Database db = null)
		{
			var results = new ContentCollection(Select<ContentColumns>.From<Content>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Content OneWhere(QiQuery where, Database db = null)
		{
			var results = new ContentCollection(Select<ContentColumns>.From<Content>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Content OneOrThrow(ContentCollection c)
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

		public static Content FirstOneWhere(WhereDelegate<ContentColumns> where, Database db = null)
		{
			var results = new ContentCollection(Select<ContentColumns>.From<Content>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ContentCollection Top(int count, WhereDelegate<ContentColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ContentCollection Top(int count, WhereDelegate<ContentColumns> where, OrderBy<ContentColumns> orderBy, Database database = null)
        {
            ContentColumns c = new ContentColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<Content>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Content>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ContentColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<ContentCollection>(0);
        }

		public static long Count(WhereDelegate<ContentColumns> where, Database database = null)
		{
			ContentColumns c = new ContentColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Content>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Content>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
