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
	[Brevitee.Data.Table("VaultKey", "Encryption")]
	public partial class VaultKey: Dao
	{
		public VaultKey():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public VaultKey(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator VaultKey(DataRow data)
		{
			return new VaultKey(data);
		}

		private void SetChildren()
		{
						
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

	// property:RsaKey, columnName:RsaKey	
	[Brevitee.Data.Column(Name="RsaKey", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string RsaKey
	{
		get
		{
			return GetStringValue("RsaKey");
		}
		set
		{
			SetValue("RsaKey", value);
		}
	}

	// property:Password, columnName:Password	
	[Brevitee.Data.Column(Name="Password", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Password
	{
		get
		{
			return GetStringValue("Password");
		}
		set
		{
			SetValue("Password", value);
		}
	}



	// start VaultId -> VaultId
	[Brevitee.Data.ForeignKey(
        Table="VaultKey",
		Name="VaultId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Vault",
		Suffix="1")]
	public long? VaultId
	{
		get
		{
			return GetLongValue("VaultId");
		}
		set
		{
			SetValue("VaultId", value);
		}
	}

	Vault _vaultOfVaultId;
	public Vault VaultOfVaultId
	{
		get
		{
			if(_vaultOfVaultId == null)
			{
				_vaultOfVaultId = Brevitee.Encryption.Vault.OneWhere(f => f.Id == this.VaultId);
			}
			return _vaultOfVaultId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new VaultKeyColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the VaultKey table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static VaultKeyCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<VaultKey>();
			Database db = database == null ? Db.For<VaultKey>(): database;
			var results = new VaultKeyCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a VaultKeyColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between VaultKeyColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultKeyCollection Where(Func<VaultKeyColumns, QueryFilter<VaultKeyColumns>> where, OrderBy<VaultKeyColumns> orderBy = null)
		{
			return new VaultKeyCollection(new Query<VaultKeyColumns, VaultKey>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultKeyColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultKeyCollection Where(WhereDelegate<VaultKeyColumns> where, Database db = null)
		{
			var results = new VaultKeyCollection(db, new Query<VaultKeyColumns, VaultKey>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultKeyColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static VaultKeyCollection Where(WhereDelegate<VaultKeyColumns> where, OrderBy<VaultKeyColumns> orderBy = null, Database db = null)
		{
			var results = new VaultKeyCollection(db, new Query<VaultKeyColumns, VaultKey>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<VaultKeyColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static VaultKeyCollection Where(QiQuery where, Database db = null)
		{
			var results = new VaultKeyCollection(db, Select<VaultKeyColumns>.From<VaultKey>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single VaultKey instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultKeyColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultKey OneWhere(WhereDelegate<VaultKeyColumns> where, Database db = null)
		{
			var results = new VaultKeyCollection(db, Select<VaultKeyColumns>.From<VaultKey>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<VaultKeyColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static VaultKey OneWhere(QiQuery where, Database db = null)
		{
			var results = new VaultKeyCollection(db, Select<VaultKeyColumns>.From<VaultKey>().Where(where, db));
			return OneOrThrow(results);
		}

		private static VaultKey OneOrThrow(VaultKeyCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a VaultKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultKeyColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultKey FirstOneWhere(WhereDelegate<VaultKeyColumns> where, Database db = null)
		{
			var results = new VaultKeyCollection(db, Select<VaultKeyColumns>.From<VaultKey>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a VaultKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultKeyColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultKeyCollection Top(int count, WhereDelegate<VaultKeyColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a VaultKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultKeyColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static VaultKeyCollection Top(int count, WhereDelegate<VaultKeyColumns> where, OrderBy<VaultKeyColumns> orderBy, Database database = null)
		{
			VaultKeyColumns c = new VaultKeyColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<VaultKey>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<VaultKey>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<VaultKeyColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<VaultKeyCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultKeyColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<VaultKeyColumns> where, Database database = null)
		{
			VaultKeyColumns c = new VaultKeyColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<VaultKey>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<VaultKey>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
