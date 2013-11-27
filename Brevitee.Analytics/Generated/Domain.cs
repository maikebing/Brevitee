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
    [Brevitee.Data.Table("Domain", "Analytics")]
    public partial class Domain: Dao
    {
        public Domain():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Domain(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Domain(DataRow data)
        {
            return new Domain(data);
        }

		private void SetChildren()
		{

            this.ChildCollections.Add("Url_DomainId", new UrlCollection(new Query<UrlColumns, Url>((c) => c.DomainId == this.Id), this, "DomainId"));							
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
	public UrlCollection UrlsByDomainId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Url_DomainId"))
			{
				SetChildren();
			}

			var c = (UrlCollection)this.ChildCollections["Url_DomainId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new DomainColumns();
			return (colFilter.Id == IdValue);
		}

		public static DomainCollection Where(Func<DomainColumns, QueryFilter<DomainColumns>> where, OrderBy<DomainColumns> orderBy = null)
		{
			return new DomainCollection(new Query<DomainColumns, Domain>(where, orderBy), true);
		}
		
		public static DomainCollection Where(WhereDelegate<DomainColumns> where, Database db = null)
		{
			return new DomainCollection(new Query<DomainColumns, Domain>(where, db), true);
		}
		   
		public static DomainCollection Where(WhereDelegate<DomainColumns> where, OrderBy<DomainColumns> orderBy = null, Database db = null)
		{
			return new DomainCollection(new Query<DomainColumns, Domain>(where, orderBy, db), true);
		}

		public static DomainCollection Where(QiQuery where, Database db = null)
		{
			return new DomainCollection(Select<DomainColumns>.From<Domain>().Where(where, db));
		}

		public static Domain OneWhere(WhereDelegate<DomainColumns> where, Database db = null)
		{
			var results = new DomainCollection(Select<DomainColumns>.From<Domain>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Domain OneWhere(QiQuery where, Database db = null)
		{
			var results = new DomainCollection(Select<DomainColumns>.From<Domain>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Domain OneOrThrow(DomainCollection c)
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

		public static Domain FirstOneWhere(WhereDelegate<DomainColumns> where, Database db = null)
		{
			var results = new DomainCollection(Select<DomainColumns>.From<Domain>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static DomainCollection Top(int count, WhereDelegate<DomainColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static DomainCollection Top(int count, WhereDelegate<DomainColumns> where, OrderBy<DomainColumns> orderBy, Database database = null)
        {
            DomainColumns c = new DomainColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<Domain>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Domain>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<DomainColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<DomainCollection>(0);
        }

		public static long Count(WhereDelegate<DomainColumns> where, Database database = null)
		{
			DomainColumns c = new DomainColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Domain>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Domain>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
