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
    [Brevitee.Data.Table("Port", "Analytics")]
    public partial class Port: Dao
    {
        public Port():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Port(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Port(DataRow data)
        {
            return new Port(data);
        }

		private void SetChildren()
		{

            this.ChildCollections.Add("Url_PortId", new UrlCollection(new Query<UrlColumns, Url>((c) => c.PortId == this.Id), this, "PortId"));							
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
	public int? Value
	{
		get
		{
			return GetIntValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



				
	[Exclude]	
	public UrlCollection UrlsByPortId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Url_PortId"))
			{
				SetChildren();
			}

			var c = (UrlCollection)this.ChildCollections["Url_PortId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new PortColumns();
			return (colFilter.Id == IdValue);
		}

		public static PortCollection Where(Func<PortColumns, QueryFilter<PortColumns>> where, OrderBy<PortColumns> orderBy = null)
		{
			return new PortCollection(new Query<PortColumns, Port>(where, orderBy), true);
		}
		
		public static PortCollection Where(WhereDelegate<PortColumns> where, Database db = null)
		{
			return new PortCollection(new Query<PortColumns, Port>(where, db), true);
		}
		   
		public static PortCollection Where(WhereDelegate<PortColumns> where, OrderBy<PortColumns> orderBy = null, Database db = null)
		{
			return new PortCollection(new Query<PortColumns, Port>(where, orderBy, db), true);
		}

		public static PortCollection Where(QiQuery where, Database db = null)
		{
			return new PortCollection(Select<PortColumns>.From<Port>().Where(where, db));
		}

		public static Port OneWhere(WhereDelegate<PortColumns> where, Database db = null)
		{
			var results = new PortCollection(Select<PortColumns>.From<Port>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Port OneWhere(QiQuery where, Database db = null)
		{
			var results = new PortCollection(Select<PortColumns>.From<Port>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Port OneOrThrow(PortCollection c)
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

		public static Port FirstOneWhere(WhereDelegate<PortColumns> where, Database db = null)
		{
			var results = new PortCollection(Select<PortColumns>.From<Port>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static PortCollection Top(int count, WhereDelegate<PortColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static PortCollection Top(int count, WhereDelegate<PortColumns> where, OrderBy<PortColumns> orderBy, Database database = null)
        {
            PortColumns c = new PortColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<Port>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Port>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PortColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<PortCollection>(0);
        }

		public static long Count(WhereDelegate<PortColumns> where, Database database = null)
		{
			PortColumns c = new PortColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Port>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Port>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
