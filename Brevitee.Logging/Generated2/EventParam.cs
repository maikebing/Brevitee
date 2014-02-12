// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Logging
{
	// schema = DaoLogger2
	// connection Name = DaoLogger2
	[Brevitee.Data.Table("EventParam", "DaoLogger2")]
	public partial class EventParam: Dao
	{
		public EventParam():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public EventParam(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator EventParam(DataRow data)
		{
			return new EventParam(data);
		}

		private void SetChildren()
		{
						
		}

	// property:Id, columnName:Id	
	[Exclude]
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



	// start EventId -> EventId
	[Brevitee.Data.ForeignKey(
        Table="EventParam",
		Name="EventId", 
		ExtractedType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Event",
		Suffix="1")]
	public long? EventId
	{
		get
		{
			return GetLongValue("EventId");
		}
		set
		{
			SetValue("EventId", value);
		}
	}

	Event _eventOfEventId;
	public Event EventOfEventId
	{
		get
		{
			if(_eventOfEventId == null)
			{
				_eventOfEventId = Brevitee.Logging.Event.OneWhere(f => f.Id == this.EventId);
			}
			return _eventOfEventId;
		}
	}
	
	// start ParamId -> ParamId
	[Brevitee.Data.ForeignKey(
        Table="EventParam",
		Name="ParamId", 
		ExtractedType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Param",
		Suffix="2")]
	public long? ParamId
	{
		get
		{
			return GetLongValue("ParamId");
		}
		set
		{
			SetValue("ParamId", value);
		}
	}

	Param _paramOfParamId;
	public Param ParamOfParamId
	{
		get
		{
			if(_paramOfParamId == null)
			{
				_paramOfParamId = Brevitee.Logging.Param.OneWhere(f => f.Id == this.ParamId);
			}
			return _paramOfParamId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new EventParamColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the EventParam table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static EventParamCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<EventParam>();
			Database db = database == null ? _.Db.For<EventParam>(): database;
			return new EventParamCollection(sql.GetDataTable(db));
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a EventParamColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between EventParamColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EventParamCollection Where(Func<EventParamColumns, QueryFilter<EventParamColumns>> where, OrderBy<EventParamColumns> orderBy = null)
		{
			return new EventParamCollection(new Query<EventParamColumns, EventParam>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EventParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EventParamColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EventParamCollection Where(WhereDelegate<EventParamColumns> where, Database db = null)
		{
			return new EventParamCollection(new Query<EventParamColumns, EventParam>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EventParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EventParamColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static EventParamCollection Where(WhereDelegate<EventParamColumns> where, OrderBy<EventParamColumns> orderBy = null, Database db = null)
		{
			return new EventParamCollection(new Query<EventParamColumns, EventParam>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<EventParamColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static EventParamCollection Where(QiQuery where, Database db = null)
		{
			return new EventParamCollection(Select<EventParamColumns>.From<EventParam>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single EventParam instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EventParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EventParamColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EventParam OneWhere(WhereDelegate<EventParamColumns> where, Database db = null)
		{
			var results = new EventParamCollection(Select<EventParamColumns>.From<EventParam>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<EventParamColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static EventParam OneWhere(QiQuery where, Database db = null)
		{
			var results = new EventParamCollection(Select<EventParamColumns>.From<EventParam>().Where(where, db));
			return OneOrThrow(results);
		}

		private static EventParam OneOrThrow(EventParamCollection c)
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

		/// <summary>
		/// Execute a query and return the first result
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EventParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EventParamColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EventParam FirstOneWhere(WhereDelegate<EventParamColumns> where, Database db = null)
		{
			var results = new EventParamCollection(Select<EventParamColumns>.From<EventParam>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Execute a query and return the specified number
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a EventParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EventParamColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EventParamCollection Top(int count, WhereDelegate<EventParamColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		/// <summary>
		/// Execute a query and return the specified count
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a EventParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EventParamColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static EventParamCollection Top(int count, WhereDelegate<EventParamColumns> where, OrderBy<EventParamColumns> orderBy, Database database = null)
		{
			EventParamColumns c = new EventParamColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<EventParam>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<EventParam>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<EventParamColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<EventParamCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EventParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EventParamColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<EventParamColumns> where, Database database = null)
		{
			EventParamColumns c = new EventParamColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<EventParam>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<EventParam>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
