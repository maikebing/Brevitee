using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Schema;
using Brevitee.Data;	 
using Brevitee.Data.Qi;

namespace Brevitee.Logging.Data
{
    [Brevitee.Data.Table("LogEvent", "DaoLogger")]
    public partial class LogEvent: Dao
    {
        public LogEvent():base()
		{
			this.KeyColumnName = "Id";
		}

        public LogEvent(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
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

	// property:Source, columnName:Source	
	[Brevitee.Data.Column(Name="Source", ExtractedType="NVarChar", MaxLength="4000", AllowNull=false)]
	public string Source
	{
		get
		{
			return GetStringValue("Source");
		}
		set
		{
			SetValue("Source", value);
		}
	}

	// property:Category, columnName:Category	
	[Brevitee.Data.Column(Name="Category", ExtractedType="NVarChar", MaxLength="255", AllowNull=false)]
	public string Category
	{
		get
		{
			return GetStringValue("Category");
		}
		set
		{
			SetValue("Category", value);
		}
	}

	// property:EventID, columnName:EventID	
	[Brevitee.Data.Column(Name="EventID", ExtractedType="Int", MaxLength="4", AllowNull=true)]
	public int? EventID
	{
		get
		{
			return GetIntValue("EventID");
		}
		set
		{
			SetValue("EventID", value);
		}
	}

	// property:User, columnName:User	
	[Brevitee.Data.Column(Name="User", ExtractedType="NVarChar", MaxLength="255", AllowNull=true)]
	public string User
	{
		get
		{
			return GetStringValue("User");
		}
		set
		{
			SetValue("User", value);
		}
	}

	// property:Time, columnName:Time	
	[Brevitee.Data.Column(Name="Time", ExtractedType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime Time
	{
		get
		{
			return GetDateTimeValue("Time");
		}
		set
		{
			SetValue("Time", value);
		}
	}

	// property:MessageSignature, columnName:MessageSignature	
	[Brevitee.Data.Column(Name="MessageSignature", ExtractedType="NVarChar", MaxLength="4000", AllowNull=false)]
	public string MessageSignature
	{
		get
		{
			return GetStringValue("MessageSignature");
		}
		set
		{
			SetValue("MessageSignature", value);
		}
	}

	// property:MessageVariableValues, columnName:MessageVariableValues	
	[Brevitee.Data.Column(Name="MessageVariableValues", ExtractedType="NVarChar", MaxLength="4000", AllowNull=false)]
	public string MessageVariableValues
	{
		get
		{
			return GetStringValue("MessageVariableValues");
		}
		set
		{
			SetValue("MessageVariableValues", value);
		}
	}

	// property:Message, columnName:Message	
	[Brevitee.Data.Column(Name="Message", ExtractedType="NVarChar", MaxLength="4000", AllowNull=false)]
	public string Message
	{
		get
		{
			return GetStringValue("Message");
		}
		set
		{
			SetValue("Message", value);
		}
	}

	// property:Computer, columnName:Computer	
	[Brevitee.Data.Column(Name="Computer", ExtractedType="NVarChar", MaxLength="255", AllowNull=false)]
	public string Computer
	{
		get
		{
			return GetStringValue("Computer");
		}
		set
		{
			SetValue("Computer", value);
		}
	}

	// property:Severity, columnName:Severity	
	[Brevitee.Data.Column(Name="Severity", ExtractedType="NVarChar", MaxLength="50", AllowNull=false)]
	public string Severity
	{
		get
		{
			return GetStringValue("Severity");
		}
		set
		{
			SetValue("Severity", value);
		}
	}


				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new LogEventColumns();
			return (colFilter.Id == IdValue);
		}

		public override void Delete(Database database = null)
		{
			Database db;
			SqlStringBuilder sql = GetSqlStringBuilder(out db);
			if(database != null)
			{
				sql = GetSqlStringBuilder(database);
				db = database;				
			}
			
			if(AutoDeleteChildren)
			{					   			
				WriteChildDeletes(sql);
			}
			WriteDelete(sql);
			sql.Execute(db);
		}

		public static LogEventCollection Where(Func<LogEventColumns, QueryFilter<LogEventColumns>> where, OrderBy<LogEventColumns> orderBy = null)
		{
			return new LogEventCollection(Select<LogEventColumns>.From<LogEvent>().Where(where, orderBy));
		}
		
		public static LogEventCollection Where(WhereDelegate<LogEventColumns> where, Database db = null)
		{
			return new LogEventCollection(Select<LogEventColumns>.From<LogEvent>().Where(where, db));
		}
		   
		public static LogEventCollection Where(WhereDelegate<LogEventColumns> where, OrderBy<LogEventColumns> orderBy = null, Database db = null)
		{
			return new LogEventCollection(Select<LogEventColumns>.From<LogEvent>().Where(where, orderBy, db));
		}

		public static LogEventCollection Where(QiQuery where, Database db = null)
		{
			return new LogEventCollection(Select<LogEventColumns>.From<LogEvent>().Where(where, db));
		}

		public static LogEvent OneWhere(WhereDelegate<LogEventColumns> where, Database db = null)
		{
			var results = new LogEventCollection(Select<LogEventColumns>.From<LogEvent>().Where(where, db));
			return OneOrThrow(results);
		}

		public static LogEvent OneWhere(QiQuery where, Database db = null)
		{
			var results = new LogEventCollection(Select<LogEventColumns>.From<LogEvent>().Where(where, db));
			return OneOrThrow(results);
		}

		private static LogEvent OneOrThrow(LogEventCollection c)
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

		public static LogEvent FirstOneWhere(WhereDelegate<LogEventColumns> where, Database db = null)
		{
			var results = new LogEventCollection(Select<LogEventColumns>.From<LogEvent>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static LogEventCollection Top(int count, WhereDelegate<LogEventColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static LogEventCollection Top(int count, WhereDelegate<LogEventColumns> where, OrderBy<LogEventColumns> orderBy, Database db = null)
        {
            LogEventColumns c = new LogEventColumns();
            IQueryFilter filter = where(c);         
            QuerySet query = new QuerySet();
            query.Top<LogEvent>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<LogEventColumns>(orderBy);
			}

            if (db == null)
            {
                db = _.Db.For<LogEvent>();
            }

            query.Execute(db);
            return query.Results.As<LogEventCollection>(0);
        }

		public static long Count(WhereDelegate<LogEventColumns> where, Database db = null)
		{
			LogEventColumns c = new LogEventColumns();
			IQueryFilter filter = where(c) ;
			QuerySet query = new QuerySet();
			query.Count<LogEvent>();
			query.Where(filter);

			if(db == null)
			{
				db = _.Db.For<LogEvent>();
			}
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
