// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.BattleStickers
{
	// schema = BattleStickers
	// connection Name = BattleStickers
	[Brevitee.Data.Table("Effect", "BattleStickers")]
	public partial class Effect: Dao
	{
		public Effect():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Effect(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Effect(DataRow data)
		{
			return new Effect(data);
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

	// property:Attribute, columnName:Attribute	
	[Brevitee.Data.Column(Name="Attribute", ExtractedType="", MaxLength="", AllowNull=false)]
	public string Attribute
	{
		get
		{
			return GetStringValue("Attribute");
		}
		set
		{
			SetValue("Attribute", value);
		}
	}

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", ExtractedType="", MaxLength="", AllowNull=false)]
	public int? Value
	{
		get
		{
			return GetIntValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



	// start EquipmentId -> EquipmentId
	[Brevitee.Data.ForeignKey(
        Table="Effect",
		Name="EquipmentId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Equipment",
		Suffix="1")]
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
				_equipmentOfEquipmentId = Brevitee.BattleStickers.Equipment.OneWhere(f => f.Id == this.EquipmentId);
			}
			return _equipmentOfEquipmentId;
		}
	}
	
				
		


		/// <summary>
		/// Gets a query filter that will should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new EffectColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a EffectColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between EffectColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EffectCollection Where(Func<EffectColumns, QueryFilter<EffectColumns>> where, OrderBy<EffectColumns> orderBy = null)
		{
			return new EffectCollection(new Query<EffectColumns, Effect>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EffectColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EffectCollection Where(WhereDelegate<EffectColumns> where, Database db = null)
		{
			return new EffectCollection(new Query<EffectColumns, Effect>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EffectColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static EffectCollection Where(WhereDelegate<EffectColumns> where, OrderBy<EffectColumns> orderBy = null, Database db = null)
		{
			return new EffectCollection(new Query<EffectColumns, Effect>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<EffectColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static EffectCollection Where(QiQuery where, Database db = null)
		{
			return new EffectCollection(Select<EffectColumns>.From<Effect>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Effect instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EffectColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Effect OneWhere(WhereDelegate<EffectColumns> where, Database db = null)
		{
			var results = new EffectCollection(Select<EffectColumns>.From<Effect>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<EffectColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Effect OneWhere(QiQuery where, Database db = null)
		{
			var results = new EffectCollection(Select<EffectColumns>.From<Effect>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Effect OneOrThrow(EffectCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a EffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EffectColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Effect FirstOneWhere(WhereDelegate<EffectColumns> where, Database db = null)
		{
			var results = new EffectCollection(Select<EffectColumns>.From<Effect>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a EffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EffectColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EffectCollection Top(int count, WhereDelegate<EffectColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a EffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EffectColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static EffectCollection Top(int count, WhereDelegate<EffectColumns> where, OrderBy<EffectColumns> orderBy, Database database = null)
		{
			EffectColumns c = new EffectColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<Effect>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Effect>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<EffectColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<EffectCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EffectColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<EffectColumns> where, Database database = null)
		{
			EffectColumns c = new EffectColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Effect>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Effect>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
