// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Encryption
{
	// schema = Encryption
	// connection Name = Encryption
	[Brevitee.Data.Table("Vault", "Encryption")]
	public partial class Vault: Dao
	{
		public Vault():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Vault(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Vault(DataRow data)
		{
			return new Vault(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("VaultItem_VaultId", new VaultItemCollection(new Query<VaultItemColumns, VaultItem>((c) => c.VaultId == this.Id), this, "VaultId"));	
            this.ChildCollections.Add("VaultKey_VaultId", new VaultKeyCollection(new Query<VaultKeyColumns, VaultKey>((c) => c.VaultId == this.Id), this, "VaultId"));							
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

	// property:Uuid, columnName:Uuid	
	[Brevitee.Data.Column(Name="Uuid", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Uuid
	{
		get
		{
			return GetStringValue("Uuid");
		}
		set
		{
			SetValue("Uuid", value);
		}
	}

	// property:Name, columnName:Name	
	[Brevitee.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
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
	public VaultItemCollection VaultItemsByVaultId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("VaultItem_VaultId"))
			{
				SetChildren();
			}

			var c = (VaultItemCollection)this.ChildCollections["VaultItem_VaultId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public VaultKeyCollection VaultKeysByVaultId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("VaultKey_VaultId"))
			{
				SetChildren();
			}

			var c = (VaultKeyCollection)this.ChildCollections["VaultKey_VaultId"];
			if(!c.Loaded)
			{
				c.Load(Database);
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
			var colFilter = new VaultColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the Vault table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static VaultCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Vault>();
			Database db = database == null ? Db.For<Vault>(): database;
			var results = new VaultCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a VaultColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultCollection Where(Func<VaultColumns, QueryFilter<VaultColumns>> where, OrderBy<VaultColumns> orderBy = null)
		{
			return new VaultCollection(new Query<VaultColumns, Vault>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultCollection Where(WhereDelegate<VaultColumns> where, Database db = null)
		{
			var results = new VaultCollection(db, new Query<VaultColumns, Vault>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static VaultCollection Where(WhereDelegate<VaultColumns> where, OrderBy<VaultColumns> orderBy = null, Database db = null)
		{
			var results = new VaultCollection(db, new Query<VaultColumns, Vault>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<VaultColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static VaultCollection Where(QiQuery where, Database db = null)
		{
			var results = new VaultCollection(db, Select<VaultColumns>.From<Vault>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Vault instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Vault OneWhere(WhereDelegate<VaultColumns> where, Database db = null)
		{
			var results = new VaultCollection(db, Select<VaultColumns>.From<Vault>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<VaultColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Vault OneWhere(QiQuery where, Database db = null)
		{
			var results = new VaultCollection(db, Select<VaultColumns>.From<Vault>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Vault OneOrThrow(VaultCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Vault FirstOneWhere(WhereDelegate<VaultColumns> where, Database db = null)
		{
			var results = new VaultCollection(db, Select<VaultColumns>.From<Vault>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultCollection Top(int count, WhereDelegate<VaultColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static VaultCollection Top(int count, WhereDelegate<VaultColumns> where, OrderBy<VaultColumns> orderBy, Database database = null)
		{
			VaultColumns c = new VaultColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Vault>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Vault>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<VaultColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<VaultCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<VaultColumns> where, Database database = null)
		{
			VaultColumns c = new VaultColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Vault>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Vault>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
