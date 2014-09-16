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
	[Brevitee.Data.Table("Event", "DaoLogger2")]
	public partial class Event: Dao
	{
		public Event():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Event(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Event(DataRow data)
		{
			return new Event(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("EventParam_EventId", new EventParamCollection(new Query<EventParamColumns, EventParam>((c) => c.EventId == this.Id), this, "EventId"));				
            this.ChildCollections.Add("Event_EventParam_Param",  new XrefDaoCollection<EventParam, Param>(this, false));
							
		}

	// property:Id, columnName:Id	
	[Exclude]
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

	// property:Occurred, columnName:Occurred	
	[Brevitee.Data.Column(Name="Occurred", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime? Occurred
	{
		get
		{
			return GetDateTimeValue("Occurred");
		}
		set
		{
			SetValue("Occurred", value);
		}
	}

	// property:Severity, columnName:Severity	
	[Brevitee.Data.Column(Name="Severity", DbDataType="Int", MaxLength="4", AllowNull=true)]
	public int? Severity
	{
		get
		{
			return GetIntValue("Severity");
		}
		set
		{
			SetValue("Severity", value);
		}
	}

	// property:EventId, columnName:EventId	
	[Brevitee.Data.Column(Name="EventId", DbDataType="Int", MaxLength="4", AllowNull=true)]
	public int? EventId
	{
		get
		{
			return GetIntValue("EventId");
		}
		set
		{
			SetValue("EventId", value);
		}
	}

	// property:ComputerId, columnName:ComputerId	
	[Brevitee.Data.Column(Name="ComputerId", DbDataType="BigInt", MaxLength="8", AllowNull=true)]
	public long? ComputerId
	{
		get
		{
			return GetLongValue("ComputerId");
		}
		set
		{
			SetValue("ComputerId", value);
		}
	}

	// property:CategoryId, columnName:CategoryId	
	[Brevitee.Data.Column(Name="CategoryId", DbDataType="BigInt", MaxLength="8", AllowNull=true)]
	public long? CategoryId
	{
		get
		{
			return GetLongValue("CategoryId");
		}
		set
		{
			SetValue("CategoryId", value);
		}
	}

	// property:SourceId, columnName:SourceId	
	[Brevitee.Data.Column(Name="SourceId", DbDataType="BigInt", MaxLength="8", AllowNull=true)]
	public long? SourceId
	{
		get
		{
			return GetLongValue("SourceId");
		}
		set
		{
			SetValue("SourceId", value);
		}
	}

	// property:UserId, columnName:UserId	
	[Brevitee.Data.Column(Name="UserId", DbDataType="BigInt", MaxLength="8", AllowNull=true)]
	public long? UserId
	{
		get
		{
			return GetLongValue("UserId");
		}
		set
		{
			SetValue("UserId", value);
		}
	}



	// start SignatureId -> SignatureId
	[Brevitee.Data.ForeignKey(
        Table="Event",
		Name="SignatureId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Signature",
		Suffix="1")]
	public long? SignatureId
	{
		get
		{
			return GetLongValue("SignatureId");
		}
		set
		{
			SetValue("SignatureId", value);
		}
	}

	Signature _signatureOfSignatureId;
	public Signature SignatureOfSignatureId
	{
		get
		{
			if(_signatureOfSignatureId == null)
			{
				_signatureOfSignatureId = Brevitee.Logging.Signature.OneWhere(f => f.Id == this.SignatureId);
			}
			return _signatureOfSignatureId;
		}
	}
	
				

	[Exclude]	
	public EventParamCollection EventParamsByEventId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("EventParam_EventId"))
			{
				SetChildren();
			}

			var c = (EventParamCollection)this.ChildCollections["EventParam_EventId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<EventParam, Param> Params
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Event_EventParam_Param"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<EventParam, Param>)this.ChildCollections["Event_EventParam_Param"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new EventColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the Event table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static EventCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Event>();
			Database db = database == null ? Db.For<Event>(): database;
			return new EventCollection(sql.GetDataTable(db));
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a EventColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between EventColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EventCollection Where(Func<EventColumns, QueryFilter<EventColumns>> where, OrderBy<EventColumns> orderBy = null)
		{
			return new EventCollection(new Query<EventColumns, Event>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EventColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EventCollection Where(WhereDelegate<EventColumns> where, Database db = null)
		{
			return new EventCollection(new Query<EventColumns, Event>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EventColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static EventCollection Where(WhereDelegate<EventColumns> where, OrderBy<EventColumns> orderBy = null, Database db = null)
		{
			return new EventCollection(new Query<EventColumns, Event>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<EventColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static EventCollection Where(QiQuery where, Database db = null)
		{
			return new EventCollection(Select<EventColumns>.From<Event>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Event instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EventColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Event OneWhere(WhereDelegate<EventColumns> where, Database db = null)
		{
			var results = new EventCollection(Select<EventColumns>.From<Event>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<EventColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Event OneWhere(QiQuery where, Database db = null)
		{
			var results = new EventCollection(Select<EventColumns>.From<Event>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Event OneOrThrow(EventCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a EventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EventColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Event FirstOneWhere(WhereDelegate<EventColumns> where, Database db = null)
		{
			var results = new EventCollection(Select<EventColumns>.From<Event>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a EventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EventColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EventCollection Top(int count, WhereDelegate<EventColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a EventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EventColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static EventCollection Top(int count, WhereDelegate<EventColumns> where, OrderBy<EventColumns> orderBy, Database database = null)
		{
			EventColumns c = new EventColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Event>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Event>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<EventColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<EventCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EventColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<EventColumns> where, Database database = null)
		{
			EventColumns c = new EventColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Event>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Event>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
