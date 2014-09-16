// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.BattleStickers.Business.Data
{
	// schema = BattleStickers
	// connection Name = BattleStickers
	[Brevitee.Data.Table("PlayerTwoEquipment", "BattleStickers")]
	public partial class PlayerTwoEquipment: Dao
	{
		public PlayerTwoEquipment():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerTwoEquipment(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerTwoEquipment(DataRow data)
		{
			return new PlayerTwoEquipment(data);
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



	// start PlayerTwoId -> PlayerTwoId
	[Brevitee.Data.ForeignKey(
        Table="PlayerTwoEquipment",
		Name="PlayerTwoId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="PlayerTwo",
		Suffix="1")]
	public long? PlayerTwoId
	{
		get
		{
			return GetLongValue("PlayerTwoId");
		}
		set
		{
			SetValue("PlayerTwoId", value);
		}
	}

	PlayerTwo _playerTwoOfPlayerTwoId;
	public PlayerTwo PlayerTwoOfPlayerTwoId
	{
		get
		{
			if(_playerTwoOfPlayerTwoId == null)
			{
				_playerTwoOfPlayerTwoId = Brevitee.BattleStickers.Business.Data.PlayerTwo.OneWhere(f => f.Id == this.PlayerTwoId);
			}
			return _playerTwoOfPlayerTwoId;
		}
	}
	
	// start EquipmentId -> EquipmentId
	[Brevitee.Data.ForeignKey(
        Table="PlayerTwoEquipment",
		Name="EquipmentId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Equipment",
		Suffix="2")]
	public long? EquipmentId
	{
		get
		{
			return GetLongValue("EquipmentId");
		}
		set
		{
			SetValue("EquipmentId", value);
		}
	}

	Equipment _equipmentOfEquipmentId;
	public Equipment EquipmentOfEquipmentId
	{
		get
		{
			if(_equipmentOfEquipmentId == null)
			{
				_equipmentOfEquipmentId = Brevitee.BattleStickers.Business.Data.Equipment.OneWhere(f => f.Id == this.EquipmentId);
			}
			return _equipmentOfEquipmentId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new PlayerTwoEquipmentColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the PlayerTwoEquipment table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PlayerTwoEquipmentCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<PlayerTwoEquipment>();
			Database db = database == null ? Db.For<PlayerTwoEquipment>(): database;
			var results = new PlayerTwoEquipmentCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerTwoEquipmentColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerTwoEquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoEquipmentCollection Where(Func<PlayerTwoEquipmentColumns, QueryFilter<PlayerTwoEquipmentColumns>> where, OrderBy<PlayerTwoEquipmentColumns> orderBy = null)
		{
			return new PlayerTwoEquipmentCollection(new Query<PlayerTwoEquipmentColumns, PlayerTwoEquipment>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoEquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoEquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoEquipmentCollection Where(WhereDelegate<PlayerTwoEquipmentColumns> where, Database db = null)
		{
			var results = new PlayerTwoEquipmentCollection(db, new Query<PlayerTwoEquipmentColumns, PlayerTwoEquipment>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoEquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoEquipmentColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoEquipmentCollection Where(WhereDelegate<PlayerTwoEquipmentColumns> where, OrderBy<PlayerTwoEquipmentColumns> orderBy = null, Database db = null)
		{
			var results = new PlayerTwoEquipmentCollection(db, new Query<PlayerTwoEquipmentColumns, PlayerTwoEquipment>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerTwoEquipmentColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerTwoEquipmentCollection Where(QiQuery where, Database db = null)
		{
			var results = new PlayerTwoEquipmentCollection(db, Select<PlayerTwoEquipmentColumns>.From<PlayerTwoEquipment>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerTwoEquipment instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoEquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoEquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoEquipment OneWhere(WhereDelegate<PlayerTwoEquipmentColumns> where, Database db = null)
		{
			var results = new PlayerTwoEquipmentCollection(db, Select<PlayerTwoEquipmentColumns>.From<PlayerTwoEquipment>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerTwoEquipmentColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerTwoEquipment OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerTwoEquipmentCollection(db, Select<PlayerTwoEquipmentColumns>.From<PlayerTwoEquipment>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerTwoEquipment OneOrThrow(PlayerTwoEquipmentCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoEquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoEquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoEquipment FirstOneWhere(WhereDelegate<PlayerTwoEquipmentColumns> where, Database db = null)
		{
			var results = new PlayerTwoEquipmentCollection(db, Select<PlayerTwoEquipmentColumns>.From<PlayerTwoEquipment>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoEquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoEquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoEquipmentCollection Top(int count, WhereDelegate<PlayerTwoEquipmentColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoEquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoEquipmentColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoEquipmentCollection Top(int count, WhereDelegate<PlayerTwoEquipmentColumns> where, OrderBy<PlayerTwoEquipmentColumns> orderBy, Database database = null)
		{
			PlayerTwoEquipmentColumns c = new PlayerTwoEquipmentColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<PlayerTwoEquipment>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerTwoEquipment>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerTwoEquipmentColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PlayerTwoEquipmentCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoEquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoEquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerTwoEquipmentColumns> where, Database database = null)
		{
			PlayerTwoEquipmentColumns c = new PlayerTwoEquipmentColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<PlayerTwoEquipment>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerTwoEquipment>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
