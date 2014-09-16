// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.ServiceProxy.Secure
{
	// schema = SecureServiceProxy 
    public static class SecureServiceProxyContext
    {
		public static string ConnectionName
		{
			get
			{
				return "SecureServiceProxy";
			}
		}

		public static Database Db
		{
			get
			{
				return Brevitee.Data.Db.For(ConnectionName);
			}
		}


	public class ApplicationQueryContext
	{
			public ApplicationCollection Where(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Application.Where(where, db);
			}
		   
			public ApplicationCollection Where(WhereDelegate<ApplicationColumns> where, OrderBy<ApplicationColumns> orderBy = null, Database db = null)
			{
				return Application.Where(where, orderBy, db);
			}

			public Application OneWhere(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Application.OneWhere(where, db);
			}
		
			public Application FirstOneWhere(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Application.FirstOneWhere(where, db);
			}

			public ApplicationCollection Top(int count, WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Application.Top(count, where, db);
			}

			public ApplicationCollection Top(int count, WhereDelegate<ApplicationColumns> where, OrderBy<ApplicationColumns> orderBy, Database db = null)
			{
				return Application.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ApplicationColumns> where, Database db = null)
			{
				return Application.Count(where, db);
			}
	}

	static ApplicationQueryContext _applications;
	static object _applicationsLock = new object();
	public static ApplicationQueryContext Applications
	{
		get
		{
			return _applicationsLock.DoubleCheckLock<ApplicationQueryContext>(ref _applications, () => new ApplicationQueryContext());
		}
	}
	public class ApiKeyQueryContext
	{
			public ApiKeyCollection Where(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return ApiKey.Where(where, db);
			}
		   
			public ApiKeyCollection Where(WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy = null, Database db = null)
			{
				return ApiKey.Where(where, orderBy, db);
			}

			public ApiKey OneWhere(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return ApiKey.OneWhere(where, db);
			}
		
			public ApiKey FirstOneWhere(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return ApiKey.FirstOneWhere(where, db);
			}

			public ApiKeyCollection Top(int count, WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return ApiKey.Top(count, where, db);
			}

			public ApiKeyCollection Top(int count, WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy, Database db = null)
			{
				return ApiKey.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ApiKeyColumns> where, Database db = null)
			{
				return ApiKey.Count(where, db);
			}
	}

	static ApiKeyQueryContext _apiKeys;
	static object _apiKeysLock = new object();
	public static ApiKeyQueryContext ApiKeys
	{
		get
		{
			return _apiKeysLock.DoubleCheckLock<ApiKeyQueryContext>(ref _apiKeys, () => new ApiKeyQueryContext());
		}
	}
	public class SecureSessionQueryContext
	{
			public SecureSessionCollection Where(WhereDelegate<SecureSessionColumns> where, Database db = null)
			{
				return SecureSession.Where(where, db);
			}
		   
			public SecureSessionCollection Where(WhereDelegate<SecureSessionColumns> where, OrderBy<SecureSessionColumns> orderBy = null, Database db = null)
			{
				return SecureSession.Where(where, orderBy, db);
			}

			public SecureSession OneWhere(WhereDelegate<SecureSessionColumns> where, Database db = null)
			{
				return SecureSession.OneWhere(where, db);
			}
		
			public SecureSession FirstOneWhere(WhereDelegate<SecureSessionColumns> where, Database db = null)
			{
				return SecureSession.FirstOneWhere(where, db);
			}

			public SecureSessionCollection Top(int count, WhereDelegate<SecureSessionColumns> where, Database db = null)
			{
				return SecureSession.Top(count, where, db);
			}

			public SecureSessionCollection Top(int count, WhereDelegate<SecureSessionColumns> where, OrderBy<SecureSessionColumns> orderBy, Database db = null)
			{
				return SecureSession.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SecureSessionColumns> where, Database db = null)
			{
				return SecureSession.Count(where, db);
			}
	}

	static SecureSessionQueryContext _secureSessions;
	static object _secureSessionsLock = new object();
	public static SecureSessionQueryContext SecureSessions
	{
		get
		{
			return _secureSessionsLock.DoubleCheckLock<SecureSessionQueryContext>(ref _secureSessions, () => new SecureSessionQueryContext());
		}
	}    }
}																								
