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
	[Brevitee.Data.Table("Role", "Users")]
	public partial class Role: Dao
	{
		public Role():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Role(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Role(DataRow data)
		{
			return new Role(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("UserRole_RoleId", new UserRoleCollection(new Query<UserRoleColumns, UserRole>((c) => c.RoleId == this.Id), this, "RoleId"));							
            this.ChildCollections.Add("Role_UserRole_User",  new XrefDaoCollection<UserRole, User>(this, false));
				
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

	// property:Name, columnName:Name	
	[Brevitee.Data.Column(Name="Name", ExtractedType="", MaxLength="255", AllowNull=false)]
	public string Name
	{
		get
		{
			return GetStringValue("Name");
		}
		set
		{
			SetValue("Name", value);
		}
	}



				

	[Exclude]	
	public UserRoleCollection UserRolesByRoleId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserRole_RoleId"))
			{
				SetChildren();
			}

			var c = (UserRoleCollection)this.ChildCollections["UserRole_RoleId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			


		// Xref       
        public XrefDaoCollection<UserRole, User> Users
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Role_UserRole_User"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<UserRole, User>)this.ChildCollections["Role_UserRole_User"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }		/// <summary>
		/// Gets a query filter that will should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new RoleColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a RoleColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between RoleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RoleCollection Where(Func<RoleColumns, QueryFilter<RoleColumns>> where, OrderBy<RoleColumns> orderBy = null)
		{
			return new RoleCollection(new Query<RoleColumns, Role>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RoleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RoleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RoleCollection Where(WhereDelegate<RoleColumns> where, Database db = null)
		{
			return new RoleCollection(new Query<RoleColumns, Role>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RoleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RoleColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static RoleCollection Where(WhereDelegate<RoleColumns> where, OrderBy<RoleColumns> orderBy = null, Database db = null)
		{
			return new RoleCollection(new Query<RoleColumns, Role>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<RoleColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static RoleCollection Where(QiQuery where, Database db = null)
		{
			return new RoleCollection(Select<RoleColumns>.From<Role>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Role instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RoleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RoleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Role OneWhere(WhereDelegate<RoleColumns> where, Database db = null)
		{
			var results = new RoleCollection(Select<RoleColumns>.From<Role>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<RoleColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Role OneWhere(QiQuery where, Database db = null)
		{
			var results = new RoleCollection(Select<RoleColumns>.From<Role>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Role OneOrThrow(RoleCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a RoleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RoleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Role FirstOneWhere(WhereDelegate<RoleColumns> where, Database db = null)
		{
			var results = new RoleCollection(Select<RoleColumns>.From<Role>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a RoleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RoleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RoleCollection Top(int count, WhereDelegate<RoleColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a RoleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RoleColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static RoleCollection Top(int count, WhereDelegate<RoleColumns> where, OrderBy<RoleColumns> orderBy, Database database = null)
		{
			RoleColumns c = new RoleColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<Role>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Role>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<RoleColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<RoleCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RoleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RoleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<RoleColumns> where, Database database = null)
		{
			RoleColumns c = new RoleColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Role>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Role>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
