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
	[Brevitee.Data.Table("PlayerEquipment", "BattleStickers")]
	public partial class PlayerEquipment: Dao
	{
		public PlayerEquipment():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerEquipment(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerEquipment(DataRow data)
		{
			return new PlayerEquipment(data);
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



	// start PlayerId -> PlayerId
	[Brevitee.Data.ForeignKey(
        Table="PlayerEquipment",
		Name="PlayerId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Player",
		Suffix="1")]
	public long? PlayerId
	{
		get
		{
			return GetLongValue("PlayerId");
		}
		set
		{
			SetValue("PlayerId", value);
		}
	}

	Player _playerOfPlayerId;
	public Player PlayerOfPlayerId
	{
		get
		{
			if(_playerOfPlayerId == null)
			{
				_playerOfPlayerId = Brevitee.BattleStickers.Business.Data.Player.OneWhere(f => f.Id == this.PlayerId);
			}
			return _playerOfPlayerId;
		}
	}
	
	// start EquipmentId -> EquipmentId
	[Brevitee.Data.ForeignKey(
        Table="PlayerEquipment",
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
			var colFilter = new PlayerEquipmentColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the PlayerEquipment table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PlayerEquipmentCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<PlayerEquipment>();
			Database db = database == null ? Db.For<PlayerEquipment>(): database;
			var results = new PlayerEquipmentCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerEquipmentColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerEquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerEquipmentCollection Where(Func<PlayerEquipmentColumns, QueryFilter<PlayerEquipmentColumns>> where, OrderBy<PlayerEquipmentColumns> orderBy = null)
		{
			return new PlayerEquipmentCollection(new Query<PlayerEquipmentColumns, PlayerEquipment>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerEquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerEquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerEquipmentCollection Where(WhereDelegate<PlayerEquipmentColumns> where, Database db = null)
		{
			var results = new PlayerEquipmentCollection(db, new Query<PlayerEquipmentColumns, PlayerEquipment>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerEquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerEquipmentColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerEquipmentCollection Where(WhereDelegate<PlayerEquipmentColumns> where, OrderBy<PlayerEquipmentColumns> orderBy = null, Database db = null)
		{
			var results = new PlayerEquipmentCollection(db, new Query<PlayerEquipmentColumns, PlayerEquipment>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerEquipmentColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerEquipmentCollection Where(QiQuery where, Database db = null)
		{
			var results = new PlayerEquipmentCollection(db, Select<PlayerEquipmentColumns>.From<PlayerEquipment>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerEquipment instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerEquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerEquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerEquipment OneWhere(WhereDelegate<PlayerEquipmentColumns> where, Database db = null)
		{
			var results = new PlayerEquipmentCollection(db, Select<PlayerEquipmentColumns>.From<PlayerEquipment>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerEquipmentColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerEquipment OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerEquipmentCollection(db, Select<PlayerEquipmentColumns>.From<PlayerEquipment>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerEquipment OneOrThrow(PlayerEquipmentCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerEquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerEquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerEquipment FirstOneWhere(WhereDelegate<PlayerEquipmentColumns> where, Database db = null)
		{
			var results = new PlayerEquipmentCollection(db, Select<PlayerEquipmentColumns>.From<PlayerEquipment>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerEquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerEquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerEquipmentCollection Top(int count, WhereDelegate<PlayerEquipmentColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerEquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerEquipmentColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerEquipmentCollection Top(int count, WhereDelegate<PlayerEquipmentColumns> where, OrderBy<PlayerEquipmentColumns> orderBy, Database database = null)
		{
			PlayerEquipmentColumns c = new PlayerEquipmentColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<PlayerEquipment>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerEquipment>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerEquipmentColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PlayerEquipmentCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerEquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerEquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerEquipmentColumns> where, Database database = null)
		{
			PlayerEquipmentColumns c = new PlayerEquipmentColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<PlayerEquipment>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerEquipment>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
