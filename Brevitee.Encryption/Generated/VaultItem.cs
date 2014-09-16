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
	[Brevitee.Data.Table("VaultItem", "Encryption")]
	public partial class VaultItem: Dao
	{
		public VaultItem():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public VaultItem(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator VaultItem(DataRow data)
		{
			return new VaultItem(data);
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

	// property:Key, columnName:Key	
	[Brevitee.Data.Column(Name="Key", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Key
	{
		get
		{
			return GetStringValue("Key");
		}
		set
		{
			SetValue("Key", value);
		}
	}

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
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



	// start VaultId -> VaultId
	[Brevitee.Data.ForeignKey(
        Table="VaultItem",
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
			var colFilter = new VaultItemColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the VaultItem table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static VaultItemCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<VaultItem>();
			Database db = database == null ? Db.For<VaultItem>(): database;
			var results = new VaultItemCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a VaultItemColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between VaultItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultItemCollection Where(Func<VaultItemColumns, QueryFilter<VaultItemColumns>> where, OrderBy<VaultItemColumns> orderBy = null)
		{
			return new VaultItemCollection(new Query<VaultItemColumns, VaultItem>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultItemCollection Where(WhereDelegate<VaultItemColumns> where, Database db = null)
		{
			var results = new VaultItemCollection(db, new Query<VaultItemColumns, VaultItem>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static VaultItemCollection Where(WhereDelegate<VaultItemColumns> where, OrderBy<VaultItemColumns> orderBy = null, Database db = null)
		{
			var results = new VaultItemCollection(db, new Query<VaultItemColumns, VaultItem>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<VaultItemColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static VaultItemCollection Where(QiQuery where, Database db = null)
		{
			var results = new VaultItemCollection(db, Select<VaultItemColumns>.From<VaultItem>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single VaultItem instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultItem OneWhere(WhereDelegate<VaultItemColumns> where, Database db = null)
		{
			var results = new VaultItemCollection(db, Select<VaultItemColumns>.From<VaultItem>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<VaultItemColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static VaultItem OneWhere(QiQuery where, Database db = null)
		{
			var results = new VaultItemCollection(db, Select<VaultItemColumns>.From<VaultItem>().Where(where, db));
			return OneOrThrow(results);
		}

		private static VaultItem OneOrThrow(VaultItemCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a VaultItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultItem FirstOneWhere(WhereDelegate<VaultItemColumns> where, Database db = null)
		{
			var results = new VaultItemCollection(db, Select<VaultItemColumns>.From<VaultItem>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a VaultItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultItemCollection Top(int count, WhereDelegate<VaultItemColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a VaultItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static VaultItemCollection Top(int count, WhereDelegate<VaultItemColumns> where, OrderBy<VaultItemColumns> orderBy, Database database = null)
		{
			VaultItemColumns c = new VaultItemColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<VaultItem>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<VaultItem>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<VaultItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<VaultItemCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<VaultItemColumns> where, Database database = null)
		{
			VaultItemColumns c = new VaultItemColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<VaultItem>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<VaultItem>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
