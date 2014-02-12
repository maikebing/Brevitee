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
	[Brevitee.Data.Table("Param", "DaoLogger2")]
	public partial class Param: Dao
	{
		public Param():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Param(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Param(DataRow data)
		{
			return new Param(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("EventParam_ParamId", new EventParamCollection(new Query<EventParamColumns, EventParam>((c) => c.ParamId == this.Id), this, "ParamId"));							
            this.ChildCollections.Add("Param_EventParam_Event",  new XrefDaoCollection<EventParam, Event>(this, false));
				
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

	// property:Position, columnName:Position	
	[Brevitee.Data.Column(Name="Position", ExtractedType="Int", MaxLength="4", AllowNull=true)]
	public int? Position
	{
		get
		{
			return GetIntValue("Position");
		}
		set
		{
			SetValue("Position", value);
		}
	}

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", ExtractedType="VarChar", MaxLength="4000", AllowNull=true)]
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
	public EventParamCollection EventParamsByParamId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("EventParam_ParamId"))
			{
				SetChildren();
			}

			var c = (EventParamCollection)this.ChildCollections["EventParam_ParamId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			


		// Xref       
        public XrefDaoCollection<EventParam, Event> Events
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Param_EventParam_Event"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<EventParam, Event>)this.ChildCollections["Param_EventParam_Event"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ParamColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the Param table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ParamCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Param>();
			Database db = database == null ? _.Db.For<Param>(): database;
			return new ParamCollection(sql.GetDataTable(db));
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ParamColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ParamCollection Where(Func<ParamColumns, QueryFilter<ParamColumns>> where, OrderBy<ParamColumns> orderBy = null)
		{
			return new ParamCollection(new Query<ParamColumns, Param>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ParamCollection Where(WhereDelegate<ParamColumns> where, Database db = null)
		{
			return new ParamCollection(new Query<ParamColumns, Param>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ParamCollection Where(WhereDelegate<ParamColumns> where, OrderBy<ParamColumns> orderBy = null, Database db = null)
		{
			return new ParamCollection(new Query<ParamColumns, Param>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ParamColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static ParamCollection Where(QiQuery where, Database db = null)
		{
			return new ParamCollection(Select<ParamColumns>.From<Param>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Param instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Param OneWhere(WhereDelegate<ParamColumns> where, Database db = null)
		{
			var results = new ParamCollection(Select<ParamColumns>.From<Param>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ParamColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Param OneWhere(QiQuery where, Database db = null)
		{
			var results = new ParamCollection(Select<ParamColumns>.From<Param>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Param OneOrThrow(ParamCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Param FirstOneWhere(WhereDelegate<ParamColumns> where, Database db = null)
		{
			var results = new ParamCollection(Select<ParamColumns>.From<Param>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ParamCollection Top(int count, WhereDelegate<ParamColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ParamCollection Top(int count, WhereDelegate<ParamColumns> where, OrderBy<ParamColumns> orderBy, Database database = null)
		{
			ParamColumns c = new ParamColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<Param>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Param>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ParamColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<ParamCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ParamColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ParamColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<ParamColumns> where, Database database = null)
		{
			ParamColumns c = new ParamColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Param>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Param>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
