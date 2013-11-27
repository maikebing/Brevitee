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
    [Brevitee.Data.Table("GiveStatus", "Test")]
    public partial class GiveStatus: Dao
    {
        public GiveStatus():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public GiveStatus(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Give_GiveStatusId", new GiveCollection(new Query<GiveColumns, Give>((c) => c.GiveStatusId == this.Id), this, "GiveStatusId"));	
		}

	// property:Id, columnName:Id	
	[Brevitee.Data.KeyColumn(Name="Id", ExtractedType="BigInt", MaxLength="8")]
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
	[Brevitee.Data.Column(Name="Value", ExtractedType="Int", MaxLength="4", AllowNull=false)]
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

	// property:Status, columnName:Status	
	[Brevitee.Data.Column(Name="Status", ExtractedType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Status
	{
		get
		{
			return GetStringValue("Status");
		}
		set
		{
			SetValue("Status", value);
		}
	}


				
	
	public GiveCollection GiveCollectionByGiveStatusId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Give_GiveStatusId"))
			{
				SetChildren();
			}

			var c = (GiveCollection)this.ChildCollections["Give_GiveStatusId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new GiveStatusColumns();
			return (colFilter.Id == IdValue);
		}

		public static GiveStatusCollection Where(Func<GiveStatusColumns, QueryFilter<GiveStatusColumns>> where, OrderBy<GiveStatusColumns> orderBy = null)
		{
			return new GiveStatusCollection(new Query<GiveStatusColumns, GiveStatus>(where, orderBy), true);
		}
		
		public static GiveStatusCollection Where(WhereDelegate<GiveStatusColumns> where, Database db = null)
		{
			return new GiveStatusCollection(new Query<GiveStatusColumns, GiveStatus>(where, db), true);
		}
		   
		public static GiveStatusCollection Where(WhereDelegate<GiveStatusColumns> where, OrderBy<GiveStatusColumns> orderBy = null, Database db = null)
		{
			return new GiveStatusCollection(new Query<GiveStatusColumns, GiveStatus>(where, orderBy, db), true);
		}

		public static GiveStatusCollection Where(QiQuery where, Database db = null)
		{
			return new GiveStatusCollection(Select<GiveStatusColumns>.From<GiveStatus>().Where(where, db));
		}

		public static GiveStatus OneWhere(WhereDelegate<GiveStatusColumns> where, Database db = null)
		{
			var results = new GiveStatusCollection(Select<GiveStatusColumns>.From<GiveStatus>().Where(where, db));
			return OneOrThrow(results);
		}

		public static GiveStatus OneWhere(QiQuery where, Database db = null)
		{
			var results = new GiveStatusCollection(Select<GiveStatusColumns>.From<GiveStatus>().Where(where, db));
			return OneOrThrow(results);
		}

		private static GiveStatus OneOrThrow(GiveStatusCollection c)
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

		public static GiveStatus FirstOneWhere(WhereDelegate<GiveStatusColumns> where, Database db = null)
		{
			var results = new GiveStatusCollection(Select<GiveStatusColumns>.From<GiveStatus>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static GiveStatusCollection Top(int count, WhereDelegate<GiveStatusColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static GiveStatusCollection Top(int count, WhereDelegate<GiveStatusColumns> where, OrderBy<GiveStatusColumns> orderBy, Database database = null)
        {
            GiveStatusColumns c = new GiveStatusColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<GiveStatus>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<GiveStatus>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<GiveStatusColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<GiveStatusCollection>(0);
        }

		public static long Count(WhereDelegate<GiveStatusColumns> where, Database database = null)
		{
			GiveStatusColumns c = new GiveStatusColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<GiveStatus>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<GiveStatus>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
