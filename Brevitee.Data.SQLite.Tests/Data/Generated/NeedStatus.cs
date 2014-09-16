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
    [Brevitee.Data.Table("NeedStatus", "Test")]
    public partial class NeedStatus: Dao
    {
        public NeedStatus():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public NeedStatus(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Need_NeedStatusId", new NeedCollection(new Query<NeedColumns, Need>((c) => c.NeedStatusId == this.Id), this, "NeedStatusId"));	
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

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", DbDataType="Int", MaxLength="4", AllowNull=false)]
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
	[Brevitee.Data.Column(Name="Status", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
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


				
	
	public NeedCollection NeedCollectionByNeedStatusId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Need_NeedStatusId"))
			{
				SetChildren();
			}

			var c = (NeedCollection)this.ChildCollections["Need_NeedStatusId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new NeedStatusColumns();
			return (colFilter.Id == IdValue);
		}

		public static NeedStatusCollection Where(Func<NeedStatusColumns, QueryFilter<NeedStatusColumns>> where, OrderBy<NeedStatusColumns> orderBy = null)
		{
			return new NeedStatusCollection(new Query<NeedStatusColumns, NeedStatus>(where, orderBy), true);
		}
		
		public static NeedStatusCollection Where(WhereDelegate<NeedStatusColumns> where, Database db = null)
		{
			return new NeedStatusCollection(new Query<NeedStatusColumns, NeedStatus>(where, db), true);
		}
		   
		public static NeedStatusCollection Where(WhereDelegate<NeedStatusColumns> where, OrderBy<NeedStatusColumns> orderBy = null, Database db = null)
		{
			return new NeedStatusCollection(new Query<NeedStatusColumns, NeedStatus>(where, orderBy, db), true);
		}

		public static NeedStatusCollection Where(QiQuery where, Database db = null)
		{
			return new NeedStatusCollection(Select<NeedStatusColumns>.From<NeedStatus>().Where(where, db));
		}

		public static NeedStatus OneWhere(WhereDelegate<NeedStatusColumns> where, Database db = null)
		{
			var results = new NeedStatusCollection(Select<NeedStatusColumns>.From<NeedStatus>().Where(where, db));
			return OneOrThrow(results);
		}

		public static NeedStatus OneWhere(QiQuery where, Database db = null)
		{
			var results = new NeedStatusCollection(Select<NeedStatusColumns>.From<NeedStatus>().Where(where, db));
			return OneOrThrow(results);
		}

		private static NeedStatus OneOrThrow(NeedStatusCollection c)
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

		public static NeedStatus FirstOneWhere(WhereDelegate<NeedStatusColumns> where, Database db = null)
		{
			var results = new NeedStatusCollection(Select<NeedStatusColumns>.From<NeedStatus>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static NeedStatusCollection Top(int count, WhereDelegate<NeedStatusColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static NeedStatusCollection Top(int count, WhereDelegate<NeedStatusColumns> where, OrderBy<NeedStatusColumns> orderBy, Database database = null)
        {
            NeedStatusColumns c = new NeedStatusColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<NeedStatus>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<NeedStatus>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<NeedStatusColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<NeedStatusCollection>(0);
        }

		public static long Count(WhereDelegate<NeedStatusColumns> where, Database database = null)
		{
			NeedStatusColumns c = new NeedStatusColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<NeedStatus>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<NeedStatus>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
