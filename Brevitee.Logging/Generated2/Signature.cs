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
	[Brevitee.Data.Table("Signature", "DaoLogger2")]
	public partial class Signature: Dao
	{
		public Signature():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Signature(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Signature(DataRow data)
		{
			return new Signature(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Event_SignatureId", new EventCollection(new Query<EventColumns, Event>((c) => c.SignatureId == this.Id), this, "SignatureId"));							
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

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", ExtractedType="VarChar", MaxLength="4000", AllowNull=false)]
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
	public EventCollection EventsBySignatureId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Event_SignatureId"))
			{
				SetChildren();
			}

			var c = (EventCollection)this.ChildCollections["Event_SignatureId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new SignatureColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the Signature table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static SignatureCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Signature>();
			Database db = database == null ? _.Db.For<Signature>(): database;
			return new SignatureCollection(sql.GetDataTable(db));
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SignatureColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SignatureColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SignatureCollection Where(Func<SignatureColumns, QueryFilter<SignatureColumns>> where, OrderBy<SignatureColumns> orderBy = null)
		{
			return new SignatureCollection(new Query<SignatureColumns, Signature>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SignatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SignatureColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SignatureCollection Where(WhereDelegate<SignatureColumns> where, Database db = null)
		{
			return new SignatureCollection(new Query<SignatureColumns, Signature>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SignatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SignatureColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SignatureCollection Where(WhereDelegate<SignatureColumns> where, OrderBy<SignatureColumns> orderBy = null, Database db = null)
		{
			return new SignatureCollection(new Query<SignatureColumns, Signature>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SignatureColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static SignatureCollection Where(QiQuery where, Database db = null)
		{
			return new SignatureCollection(Select<SignatureColumns>.From<Signature>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Signature instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SignatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SignatureColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Signature OneWhere(WhereDelegate<SignatureColumns> where, Database db = null)
		{
			var results = new SignatureCollection(Select<SignatureColumns>.From<Signature>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SignatureColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Signature OneWhere(QiQuery where, Database db = null)
		{
			var results = new SignatureCollection(Select<SignatureColumns>.From<Signature>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Signature OneOrThrow(SignatureCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a SignatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SignatureColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Signature FirstOneWhere(WhereDelegate<SignatureColumns> where, Database db = null)
		{
			var results = new SignatureCollection(Select<SignatureColumns>.From<Signature>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a SignatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SignatureColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SignatureCollection Top(int count, WhereDelegate<SignatureColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a SignatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SignatureColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SignatureCollection Top(int count, WhereDelegate<SignatureColumns> where, OrderBy<SignatureColumns> orderBy, Database database = null)
		{
			SignatureColumns c = new SignatureColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<Signature>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Signature>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SignatureColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<SignatureCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SignatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SignatureColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<SignatureColumns> where, Database database = null)
		{
			SignatureColumns c = new SignatureColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Signature>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Signature>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
