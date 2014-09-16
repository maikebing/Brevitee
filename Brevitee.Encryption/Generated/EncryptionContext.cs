// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Encryption
{
	// schema = Encryption 
    public static class EncryptionContext
    {
		public static string ConnectionName
		{
			get
			{
				return "Encryption";
			}
		}

		public static Database Db
		{
			get
			{
				return Brevitee.Data.Db.For(ConnectionName);
			}
		}


	public class VaultQueryContext
	{
			public VaultCollection Where(WhereDelegate<VaultColumns> where, Database db = null)
			{
				return Vault.Where(where, db);
			}
		   
			public VaultCollection Where(WhereDelegate<VaultColumns> where, OrderBy<VaultColumns> orderBy = null, Database db = null)
			{
				return Vault.Where(where, orderBy, db);
			}

			public Vault OneWhere(WhereDelegate<VaultColumns> where, Database db = null)
			{
				return Vault.OneWhere(where, db);
			}
		
			public Vault FirstOneWhere(WhereDelegate<VaultColumns> where, Database db = null)
			{
				return Vault.FirstOneWhere(where, db);
			}

			public VaultCollection Top(int count, WhereDelegate<VaultColumns> where, Database db = null)
			{
				return Vault.Top(count, where, db);
			}

			public VaultCollection Top(int count, WhereDelegate<VaultColumns> where, OrderBy<VaultColumns> orderBy, Database db = null)
			{
				return Vault.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<VaultColumns> where, Database db = null)
			{
				return Vault.Count(where, db);
			}
	}

	static VaultQueryContext _vaults;
	static object _vaultsLock = new object();
	public static VaultQueryContext Vaults
	{
		get
		{
			return _vaultsLock.DoubleCheckLock<VaultQueryContext>(ref _vaults, () => new VaultQueryContext());
		}
	}
	public class VaultItemQueryContext
	{
			public VaultItemCollection Where(WhereDelegate<VaultItemColumns> where, Database db = null)
			{
				return VaultItem.Where(where, db);
			}
		   
			public VaultItemCollection Where(WhereDelegate<VaultItemColumns> where, OrderBy<VaultItemColumns> orderBy = null, Database db = null)
			{
				return VaultItem.Where(where, orderBy, db);
			}

			public VaultItem OneWhere(WhereDelegate<VaultItemColumns> where, Database db = null)
			{
				return VaultItem.OneWhere(where, db);
			}
		
			public VaultItem FirstOneWhere(WhereDelegate<VaultItemColumns> where, Database db = null)
			{
				return VaultItem.FirstOneWhere(where, db);
			}

			public VaultItemCollection Top(int count, WhereDelegate<VaultItemColumns> where, Database db = null)
			{
				return VaultItem.Top(count, where, db);
			}

			public VaultItemCollection Top(int count, WhereDelegate<VaultItemColumns> where, OrderBy<VaultItemColumns> orderBy, Database db = null)
			{
				return VaultItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<VaultItemColumns> where, Database db = null)
			{
				return VaultItem.Count(where, db);
			}
	}

	static VaultItemQueryContext _vaultItems;
	static object _vaultItemsLock = new object();
	public static VaultItemQueryContext VaultItems
	{
		get
		{
			return _vaultItemsLock.DoubleCheckLock<VaultItemQueryContext>(ref _vaultItems, () => new VaultItemQueryContext());
		}
	}
	public class VaultKeyQueryContext
	{
			public VaultKeyCollection Where(WhereDelegate<VaultKeyColumns> where, Database db = null)
			{
				return VaultKey.Where(where, db);
			}
		   
			public VaultKeyCollection Where(WhereDelegate<VaultKeyColumns> where, OrderBy<VaultKeyColumns> orderBy = null, Database db = null)
			{
				return VaultKey.Where(where, orderBy, db);
			}

			public VaultKey OneWhere(WhereDelegate<VaultKeyColumns> where, Database db = null)
			{
				return VaultKey.OneWhere(where, db);
			}
		
			public VaultKey FirstOneWhere(WhereDelegate<VaultKeyColumns> where, Database db = null)
			{
				return VaultKey.FirstOneWhere(where, db);
			}

			public VaultKeyCollection Top(int count, WhereDelegate<VaultKeyColumns> where, Database db = null)
			{
				return VaultKey.Top(count, where, db);
			}

			public VaultKeyCollection Top(int count, WhereDelegate<VaultKeyColumns> where, OrderBy<VaultKeyColumns> orderBy, Database db = null)
			{
				return VaultKey.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<VaultKeyColumns> where, Database db = null)
			{
				return VaultKey.Count(where, db);
			}
	}

	static VaultKeyQueryContext _vaultKeys;
	static object _vaultKeysLock = new object();
	public static VaultKeyQueryContext VaultKeys
	{
		get
		{
			return _vaultKeysLock.DoubleCheckLock<VaultKeyQueryContext>(ref _vaultKeys, () => new VaultKeyQueryContext());
		}
	}    }
}																								
