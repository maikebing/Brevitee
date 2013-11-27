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
    [Brevitee.Data.Table("Give", "Test")]
    public partial class Give: Dao
    {
        public Give():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Give(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

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

	// property:LastModified, columnName:LastModified	
	[Brevitee.Data.Column(Name="LastModified", ExtractedType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime LastModified
	{
		get
		{
			return GetDateTimeValue("LastModified");
		}
		set
		{
			SetValue("LastModified", value);
		}
	}


	// start HaveId -> HaveId
	[Brevitee.Data.ForeignKey(
        Table="Give",
		Name="HaveId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Have",
		Suffix="1")]
	public long? HaveId
	{
		get
		{
			return GetLongValue("HaveId");
		}
		set
		{
			SetValue("HaveId", value);
		}
	}

	Have _haveOfHaveId;
	public Have HaveOfHaveId
	{
		get
		{
			if(_haveOfHaveId == null)
			{
				_haveOfHaveId = SampleData.Have.OneWhere(f => f.Id == this.HaveId);
			}
			return _haveOfHaveId;
		}
	}
	
	// start WantId -> WantId
	[Brevitee.Data.ForeignKey(
        Table="Give",
		Name="WantId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Want",
		Suffix="2")]
	public long? WantId
	{
		get
		{
			return GetLongValue("WantId");
		}
		set
		{
			SetValue("WantId", value);
		}
	}

	Want _wantOfWantId;
	public Want WantOfWantId
	{
		get
		{
			if(_wantOfWantId == null)
			{
				_wantOfWantId = SampleData.Want.OneWhere(f => f.Id == this.WantId);
			}
			return _wantOfWantId;
		}
	}
	
	// start GiveStatusId -> GiveStatusId
	[Brevitee.Data.ForeignKey(
        Table="Give",
		Name="GiveStatusId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="GiveStatus",
		Suffix="3")]
	public long? GiveStatusId
	{
		get
		{
			return GetLongValue("GiveStatusId");
		}
		set
		{
			SetValue("GiveStatusId", value);
		}
	}

	GiveStatus _giveStatusOfGiveStatusId;
	public GiveStatus GiveStatusOfGiveStatusId
	{
		get
		{
			if(_giveStatusOfGiveStatusId == null)
			{
				_giveStatusOfGiveStatusId = SampleData.GiveStatus.OneWhere(f => f.Id == this.GiveStatusId);
			}
			return _giveStatusOfGiveStatusId;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new GiveColumns();
			return (colFilter.Id == IdValue);
		}

		public static GiveCollection Where(Func<GiveColumns, QueryFilter<GiveColumns>> where, OrderBy<GiveColumns> orderBy = null)
		{
			return new GiveCollection(new Query<GiveColumns, Give>(where, orderBy), true);
		}
		
		public static GiveCollection Where(WhereDelegate<GiveColumns> where, Database db = null)
		{
			return new GiveCollection(new Query<GiveColumns, Give>(where, db), true);
		}
		   
		public static GiveCollection Where(WhereDelegate<GiveColumns> where, OrderBy<GiveColumns> orderBy = null, Database db = null)
		{
			return new GiveCollection(new Query<GiveColumns, Give>(where, orderBy, db), true);
		}

		public static GiveCollection Where(QiQuery where, Database db = null)
		{
			return new GiveCollection(Select<GiveColumns>.From<Give>().Where(where, db));
		}

		public static Give OneWhere(WhereDelegate<GiveColumns> where, Database db = null)
		{
			var results = new GiveCollection(Select<GiveColumns>.From<Give>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Give OneWhere(QiQuery where, Database db = null)
		{
			var results = new GiveCollection(Select<GiveColumns>.From<Give>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Give OneOrThrow(GiveCollection c)
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

		public static Give FirstOneWhere(WhereDelegate<GiveColumns> where, Database db = null)
		{
			var results = new GiveCollection(Select<GiveColumns>.From<Give>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static GiveCollection Top(int count, WhereDelegate<GiveColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static GiveCollection Top(int count, WhereDelegate<GiveColumns> where, OrderBy<GiveColumns> orderBy, Database database = null)
        {
            GiveColumns c = new GiveColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<Give>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Give>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<GiveColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<GiveCollection>(0);
        }

		public static long Count(WhereDelegate<GiveColumns> where, Database database = null)
		{
			GiveColumns c = new GiveColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Give>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Give>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
