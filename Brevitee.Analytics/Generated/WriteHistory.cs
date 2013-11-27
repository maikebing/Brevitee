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
    [Brevitee.Data.Table("WriteHistory", "Analytics")]
    public partial class WriteHistory: Dao
    {
        public WriteHistory():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public WriteHistory(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator WriteHistory(DataRow data)
        {
            return new WriteHistory(data);
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

	// property:TableName, columnName:TableName	
	[Brevitee.Data.Column(Name="TableName", ExtractedType="", MaxLength="", AllowNull=false)]
	public string TableName
	{
		get
		{
			return GetStringValue("TableName");
		}
		set
		{
			SetValue("TableName", value);
		}
	}

	// property:ColumnName, columnName:ColumnName	
	[Brevitee.Data.Column(Name="ColumnName", ExtractedType="", MaxLength="", AllowNull=false)]
	public string ColumnName
	{
		get
		{
			return GetStringValue("ColumnName");
		}
		set
		{
			SetValue("ColumnName", value);
		}
	}

	// property:DateTime, columnName:DateTime	
	[Brevitee.Data.Column(Name="DateTime", ExtractedType="", MaxLength="", AllowNull=false)]
	public DateTime DateTime
	{
		get
		{
			return GetDateTimeValue("DateTime");
		}
		set
		{
			SetValue("DateTime", value);
		}
	}

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", ExtractedType="", MaxLength="", AllowNull=true)]
	public byte[] Value
	{
		get
		{
			return GetByteValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}

	// property:ValueType, columnName:ValueType	
	[Brevitee.Data.Column(Name="ValueType", ExtractedType="", MaxLength="", AllowNull=false)]
	public string ValueType
	{
		get
		{
			return GetStringValue("ValueType");
		}
		set
		{
			SetValue("ValueType", value);
		}
	}



				
		


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new WriteHistoryColumns();
			return (colFilter.Id == IdValue);
		}

		public static WriteHistoryCollection Where(Func<WriteHistoryColumns, QueryFilter<WriteHistoryColumns>> where, OrderBy<WriteHistoryColumns> orderBy = null)
		{
			return new WriteHistoryCollection(new Query<WriteHistoryColumns, WriteHistory>(where, orderBy), true);
		}
		
		public static WriteHistoryCollection Where(WhereDelegate<WriteHistoryColumns> where, Database db = null)
		{
			return new WriteHistoryCollection(new Query<WriteHistoryColumns, WriteHistory>(where, db), true);
		}
		   
		public static WriteHistoryCollection Where(WhereDelegate<WriteHistoryColumns> where, OrderBy<WriteHistoryColumns> orderBy = null, Database db = null)
		{
			return new WriteHistoryCollection(new Query<WriteHistoryColumns, WriteHistory>(where, orderBy, db), true);
		}

		public static WriteHistoryCollection Where(QiQuery where, Database db = null)
		{
			return new WriteHistoryCollection(Select<WriteHistoryColumns>.From<WriteHistory>().Where(where, db));
		}

		public static WriteHistory OneWhere(WhereDelegate<WriteHistoryColumns> where, Database db = null)
		{
			var results = new WriteHistoryCollection(Select<WriteHistoryColumns>.From<WriteHistory>().Where(where, db));
			return OneOrThrow(results);
		}

		public static WriteHistory OneWhere(QiQuery where, Database db = null)
		{
			var results = new WriteHistoryCollection(Select<WriteHistoryColumns>.From<WriteHistory>().Where(where, db));
			return OneOrThrow(results);
		}

		private static WriteHistory OneOrThrow(WriteHistoryCollection c)
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

		public static WriteHistory FirstOneWhere(WhereDelegate<WriteHistoryColumns> where, Database db = null)
		{
			var results = new WriteHistoryCollection(Select<WriteHistoryColumns>.From<WriteHistory>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static WriteHistoryCollection Top(int count, WhereDelegate<WriteHistoryColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static WriteHistoryCollection Top(int count, WhereDelegate<WriteHistoryColumns> where, OrderBy<WriteHistoryColumns> orderBy, Database database = null)
        {
            WriteHistoryColumns c = new WriteHistoryColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<WriteHistory>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<WriteHistory>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<WriteHistoryColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<WriteHistoryCollection>(0);
        }

		public static long Count(WhereDelegate<WriteHistoryColumns> where, Database database = null)
		{
			WriteHistoryColumns c = new WriteHistoryColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<WriteHistory>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<WriteHistory>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
