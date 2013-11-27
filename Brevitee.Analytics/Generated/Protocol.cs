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
    [Brevitee.Data.Table("Protocol", "Analytics")]
    public partial class Protocol: Dao
    {
        public Protocol():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Protocol(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Protocol(DataRow data)
        {
            return new Protocol(data);
        }

		private void SetChildren()
		{

            this.ChildCollections.Add("Url_ProtocolId", new UrlCollection(new Query<UrlColumns, Url>((c) => c.ProtocolId == this.Id), this, "ProtocolId"));							
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
	public UrlCollection UrlsByProtocolId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Url_ProtocolId"))
			{
				SetChildren();
			}

			var c = (UrlCollection)this.ChildCollections["Url_ProtocolId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ProtocolColumns();
			return (colFilter.Id == IdValue);
		}

		public static ProtocolCollection Where(Func<ProtocolColumns, QueryFilter<ProtocolColumns>> where, OrderBy<ProtocolColumns> orderBy = null)
		{
			return new ProtocolCollection(new Query<ProtocolColumns, Protocol>(where, orderBy), true);
		}
		
		public static ProtocolCollection Where(WhereDelegate<ProtocolColumns> where, Database db = null)
		{
			return new ProtocolCollection(new Query<ProtocolColumns, Protocol>(where, db), true);
		}
		   
		public static ProtocolCollection Where(WhereDelegate<ProtocolColumns> where, OrderBy<ProtocolColumns> orderBy = null, Database db = null)
		{
			return new ProtocolCollection(new Query<ProtocolColumns, Protocol>(where, orderBy, db), true);
		}

		public static ProtocolCollection Where(QiQuery where, Database db = null)
		{
			return new ProtocolCollection(Select<ProtocolColumns>.From<Protocol>().Where(where, db));
		}

		public static Protocol OneWhere(WhereDelegate<ProtocolColumns> where, Database db = null)
		{
			var results = new ProtocolCollection(Select<ProtocolColumns>.From<Protocol>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Protocol OneWhere(QiQuery where, Database db = null)
		{
			var results = new ProtocolCollection(Select<ProtocolColumns>.From<Protocol>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Protocol OneOrThrow(ProtocolCollection c)
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

		public static Protocol FirstOneWhere(WhereDelegate<ProtocolColumns> where, Database db = null)
		{
			var results = new ProtocolCollection(Select<ProtocolColumns>.From<Protocol>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ProtocolCollection Top(int count, WhereDelegate<ProtocolColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ProtocolCollection Top(int count, WhereDelegate<ProtocolColumns> where, OrderBy<ProtocolColumns> orderBy, Database database = null)
        {
            ProtocolColumns c = new ProtocolColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<Protocol>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Protocol>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ProtocolColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<ProtocolCollection>(0);
        }

		public static long Count(WhereDelegate<ProtocolColumns> where, Database database = null)
		{
			ProtocolColumns c = new ProtocolColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Protocol>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Protocol>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
