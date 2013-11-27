// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Users.Data
{
	// schema = Users
	// connection Name = Users
	[Brevitee.Data.Table("PasswordQuestion", "Users")]
	public partial class PasswordQuestion: Dao
	{
		public PasswordQuestion():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PasswordQuestion(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PasswordQuestion(DataRow data)
		{
			return new PasswordQuestion(data);
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

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", ExtractedType="", MaxLength="", AllowNull=false)]
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

	// property:Answer, columnName:Answer	
	[Brevitee.Data.Column(Name="Answer", ExtractedType="", MaxLength="", AllowNull=false)]
	public string Answer
	{
		get
		{
			return GetStringValue("Answer");
		}
		set
		{
			SetValue("Answer", value);
		}
	}



	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="PasswordQuestion",
		Name="UserId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="1")]
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

	User _userOfUserId;
	public User UserOfUserId
	{
		get
		{
			if(_userOfUserId == null)
			{
				_userOfUserId = Brevitee.Users.Data.User.OneWhere(f => f.Id == this.UserId);
			}
			return _userOfUserId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that will should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new PasswordQuestionColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PasswordQuestionColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PasswordQuestionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordQuestionCollection Where(Func<PasswordQuestionColumns, QueryFilter<PasswordQuestionColumns>> where, OrderBy<PasswordQuestionColumns> orderBy = null)
		{
			return new PasswordQuestionCollection(new Query<PasswordQuestionColumns, PasswordQuestion>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordQuestionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordQuestionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordQuestionCollection Where(WhereDelegate<PasswordQuestionColumns> where, Database db = null)
		{
			return new PasswordQuestionCollection(new Query<PasswordQuestionColumns, PasswordQuestion>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordQuestionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordQuestionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PasswordQuestionCollection Where(WhereDelegate<PasswordQuestionColumns> where, OrderBy<PasswordQuestionColumns> orderBy = null, Database db = null)
		{
			return new PasswordQuestionCollection(new Query<PasswordQuestionColumns, PasswordQuestion>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PasswordQuestionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PasswordQuestionCollection Where(QiQuery where, Database db = null)
		{
			return new PasswordQuestionCollection(Select<PasswordQuestionColumns>.From<PasswordQuestion>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PasswordQuestion instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordQuestionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordQuestionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordQuestion OneWhere(WhereDelegate<PasswordQuestionColumns> where, Database db = null)
		{
			var results = new PasswordQuestionCollection(Select<PasswordQuestionColumns>.From<PasswordQuestion>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PasswordQuestionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PasswordQuestion OneWhere(QiQuery where, Database db = null)
		{
			var results = new PasswordQuestionCollection(Select<PasswordQuestionColumns>.From<PasswordQuestion>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PasswordQuestion OneOrThrow(PasswordQuestionCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PasswordQuestionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordQuestionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordQuestion FirstOneWhere(WhereDelegate<PasswordQuestionColumns> where, Database db = null)
		{
			var results = new PasswordQuestionCollection(Select<PasswordQuestionColumns>.From<PasswordQuestion>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PasswordQuestionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordQuestionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordQuestionCollection Top(int count, WhereDelegate<PasswordQuestionColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PasswordQuestionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordQuestionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PasswordQuestionCollection Top(int count, WhereDelegate<PasswordQuestionColumns> where, OrderBy<PasswordQuestionColumns> orderBy, Database database = null)
		{
			PasswordQuestionColumns c = new PasswordQuestionColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<PasswordQuestion>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PasswordQuestion>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PasswordQuestionColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<PasswordQuestionCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordQuestionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordQuestionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PasswordQuestionColumns> where, Database database = null)
		{
			PasswordQuestionColumns c = new PasswordQuestionColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<PasswordQuestion>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PasswordQuestion>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
