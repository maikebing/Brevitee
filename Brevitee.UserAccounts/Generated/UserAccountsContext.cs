// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.UserAccounts.Data
{
	// schema = UserAccounts 
    public static class UserAccountsContext
    {
		public static string ConnectionName
		{
			get
			{
				return "UserAccounts";
			}
		}

		public static Database Db
		{
			get
			{
				return Brevitee.Data.Db.For(ConnectionName);
			}
		}


	public class UserQueryContext
	{
			public UserCollection Where(WhereDelegate<UserColumns> where, Database db = null)
			{
				return User.Where(where, db);
			}
		   
			public UserCollection Where(WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy = null, Database db = null)
			{
				return User.Where(where, orderBy, db);
			}

			public User OneWhere(WhereDelegate<UserColumns> where, Database db = null)
			{
				return User.OneWhere(where, db);
			}
		
			public User FirstOneWhere(WhereDelegate<UserColumns> where, Database db = null)
			{
				return User.FirstOneWhere(where, db);
			}

			public UserCollection Top(int count, WhereDelegate<UserColumns> where, Database db = null)
			{
				return User.Top(count, where, db);
			}

			public UserCollection Top(int count, WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy, Database db = null)
			{
				return User.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserColumns> where, Database db = null)
			{
				return User.Count(where, db);
			}
	}

	static UserQueryContext _users;
	static object _usersLock = new object();
	public static UserQueryContext Users
	{
		get
		{
			return _usersLock.DoubleCheckLock<UserQueryContext>(ref _users, () => new UserQueryContext());
		}
	}
	public class AccountQueryContext
	{
			public AccountCollection Where(WhereDelegate<AccountColumns> where, Database db = null)
			{
				return Account.Where(where, db);
			}
		   
			public AccountCollection Where(WhereDelegate<AccountColumns> where, OrderBy<AccountColumns> orderBy = null, Database db = null)
			{
				return Account.Where(where, orderBy, db);
			}

			public Account OneWhere(WhereDelegate<AccountColumns> where, Database db = null)
			{
				return Account.OneWhere(where, db);
			}
		
			public Account FirstOneWhere(WhereDelegate<AccountColumns> where, Database db = null)
			{
				return Account.FirstOneWhere(where, db);
			}

			public AccountCollection Top(int count, WhereDelegate<AccountColumns> where, Database db = null)
			{
				return Account.Top(count, where, db);
			}

			public AccountCollection Top(int count, WhereDelegate<AccountColumns> where, OrderBy<AccountColumns> orderBy, Database db = null)
			{
				return Account.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<AccountColumns> where, Database db = null)
			{
				return Account.Count(where, db);
			}
	}

	static AccountQueryContext _accounts;
	static object _accountsLock = new object();
	public static AccountQueryContext Accounts
	{
		get
		{
			return _accountsLock.DoubleCheckLock<AccountQueryContext>(ref _accounts, () => new AccountQueryContext());
		}
	}
	public class PasswordQueryContext
	{
			public PasswordCollection Where(WhereDelegate<PasswordColumns> where, Database db = null)
			{
				return Password.Where(where, db);
			}
		   
			public PasswordCollection Where(WhereDelegate<PasswordColumns> where, OrderBy<PasswordColumns> orderBy = null, Database db = null)
			{
				return Password.Where(where, orderBy, db);
			}

			public Password OneWhere(WhereDelegate<PasswordColumns> where, Database db = null)
			{
				return Password.OneWhere(where, db);
			}
		
			public Password FirstOneWhere(WhereDelegate<PasswordColumns> where, Database db = null)
			{
				return Password.FirstOneWhere(where, db);
			}

			public PasswordCollection Top(int count, WhereDelegate<PasswordColumns> where, Database db = null)
			{
				return Password.Top(count, where, db);
			}

			public PasswordCollection Top(int count, WhereDelegate<PasswordColumns> where, OrderBy<PasswordColumns> orderBy, Database db = null)
			{
				return Password.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PasswordColumns> where, Database db = null)
			{
				return Password.Count(where, db);
			}
	}

	static PasswordQueryContext _passwords;
	static object _passwordsLock = new object();
	public static PasswordQueryContext Passwords
	{
		get
		{
			return _passwordsLock.DoubleCheckLock<PasswordQueryContext>(ref _passwords, () => new PasswordQueryContext());
		}
	}
	public class PasswordResetQueryContext
	{
			public PasswordResetCollection Where(WhereDelegate<PasswordResetColumns> where, Database db = null)
			{
				return PasswordReset.Where(where, db);
			}
		   
			public PasswordResetCollection Where(WhereDelegate<PasswordResetColumns> where, OrderBy<PasswordResetColumns> orderBy = null, Database db = null)
			{
				return PasswordReset.Where(where, orderBy, db);
			}

			public PasswordReset OneWhere(WhereDelegate<PasswordResetColumns> where, Database db = null)
			{
				return PasswordReset.OneWhere(where, db);
			}
		
			public PasswordReset FirstOneWhere(WhereDelegate<PasswordResetColumns> where, Database db = null)
			{
				return PasswordReset.FirstOneWhere(where, db);
			}

			public PasswordResetCollection Top(int count, WhereDelegate<PasswordResetColumns> where, Database db = null)
			{
				return PasswordReset.Top(count, where, db);
			}

			public PasswordResetCollection Top(int count, WhereDelegate<PasswordResetColumns> where, OrderBy<PasswordResetColumns> orderBy, Database db = null)
			{
				return PasswordReset.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PasswordResetColumns> where, Database db = null)
			{
				return PasswordReset.Count(where, db);
			}
	}

	static PasswordResetQueryContext _passwordResets;
	static object _passwordResetsLock = new object();
	public static PasswordResetQueryContext PasswordResets
	{
		get
		{
			return _passwordResetsLock.DoubleCheckLock<PasswordResetQueryContext>(ref _passwordResets, () => new PasswordResetQueryContext());
		}
	}
	public class PasswordFailureQueryContext
	{
			public PasswordFailureCollection Where(WhereDelegate<PasswordFailureColumns> where, Database db = null)
			{
				return PasswordFailure.Where(where, db);
			}
		   
			public PasswordFailureCollection Where(WhereDelegate<PasswordFailureColumns> where, OrderBy<PasswordFailureColumns> orderBy = null, Database db = null)
			{
				return PasswordFailure.Where(where, orderBy, db);
			}

			public PasswordFailure OneWhere(WhereDelegate<PasswordFailureColumns> where, Database db = null)
			{
				return PasswordFailure.OneWhere(where, db);
			}
		
			public PasswordFailure FirstOneWhere(WhereDelegate<PasswordFailureColumns> where, Database db = null)
			{
				return PasswordFailure.FirstOneWhere(where, db);
			}

			public PasswordFailureCollection Top(int count, WhereDelegate<PasswordFailureColumns> where, Database db = null)
			{
				return PasswordFailure.Top(count, where, db);
			}

			public PasswordFailureCollection Top(int count, WhereDelegate<PasswordFailureColumns> where, OrderBy<PasswordFailureColumns> orderBy, Database db = null)
			{
				return PasswordFailure.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PasswordFailureColumns> where, Database db = null)
			{
				return PasswordFailure.Count(where, db);
			}
	}

	static PasswordFailureQueryContext _passwordFailures;
	static object _passwordFailuresLock = new object();
	public static PasswordFailureQueryContext PasswordFailures
	{
		get
		{
			return _passwordFailuresLock.DoubleCheckLock<PasswordFailureQueryContext>(ref _passwordFailures, () => new PasswordFailureQueryContext());
		}
	}
	public class LockOutQueryContext
	{
			public LockOutCollection Where(WhereDelegate<LockOutColumns> where, Database db = null)
			{
				return LockOut.Where(where, db);
			}
		   
			public LockOutCollection Where(WhereDelegate<LockOutColumns> where, OrderBy<LockOutColumns> orderBy = null, Database db = null)
			{
				return LockOut.Where(where, orderBy, db);
			}

			public LockOut OneWhere(WhereDelegate<LockOutColumns> where, Database db = null)
			{
				return LockOut.OneWhere(where, db);
			}
		
			public LockOut FirstOneWhere(WhereDelegate<LockOutColumns> where, Database db = null)
			{
				return LockOut.FirstOneWhere(where, db);
			}

			public LockOutCollection Top(int count, WhereDelegate<LockOutColumns> where, Database db = null)
			{
				return LockOut.Top(count, where, db);
			}

			public LockOutCollection Top(int count, WhereDelegate<LockOutColumns> where, OrderBy<LockOutColumns> orderBy, Database db = null)
			{
				return LockOut.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LockOutColumns> where, Database db = null)
			{
				return LockOut.Count(where, db);
			}
	}

	static LockOutQueryContext _lockOuts;
	static object _lockOutsLock = new object();
	public static LockOutQueryContext LockOuts
	{
		get
		{
			return _lockOutsLock.DoubleCheckLock<LockOutQueryContext>(ref _lockOuts, () => new LockOutQueryContext());
		}
	}
	public class LoginQueryContext
	{
			public LoginCollection Where(WhereDelegate<LoginColumns> where, Database db = null)
			{
				return Login.Where(where, db);
			}
		   
			public LoginCollection Where(WhereDelegate<LoginColumns> where, OrderBy<LoginColumns> orderBy = null, Database db = null)
			{
				return Login.Where(where, orderBy, db);
			}

			public Login OneWhere(WhereDelegate<LoginColumns> where, Database db = null)
			{
				return Login.OneWhere(where, db);
			}
		
			public Login FirstOneWhere(WhereDelegate<LoginColumns> where, Database db = null)
			{
				return Login.FirstOneWhere(where, db);
			}

			public LoginCollection Top(int count, WhereDelegate<LoginColumns> where, Database db = null)
			{
				return Login.Top(count, where, db);
			}

			public LoginCollection Top(int count, WhereDelegate<LoginColumns> where, OrderBy<LoginColumns> orderBy, Database db = null)
			{
				return Login.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LoginColumns> where, Database db = null)
			{
				return Login.Count(where, db);
			}
	}

	static LoginQueryContext _logins;
	static object _loginsLock = new object();
	public static LoginQueryContext Logins
	{
		get
		{
			return _loginsLock.DoubleCheckLock<LoginQueryContext>(ref _logins, () => new LoginQueryContext());
		}
	}
	public class PasswordQuestionQueryContext
	{
			public PasswordQuestionCollection Where(WhereDelegate<PasswordQuestionColumns> where, Database db = null)
			{
				return PasswordQuestion.Where(where, db);
			}
		   
			public PasswordQuestionCollection Where(WhereDelegate<PasswordQuestionColumns> where, OrderBy<PasswordQuestionColumns> orderBy = null, Database db = null)
			{
				return PasswordQuestion.Where(where, orderBy, db);
			}

			public PasswordQuestion OneWhere(WhereDelegate<PasswordQuestionColumns> where, Database db = null)
			{
				return PasswordQuestion.OneWhere(where, db);
			}
		
			public PasswordQuestion FirstOneWhere(WhereDelegate<PasswordQuestionColumns> where, Database db = null)
			{
				return PasswordQuestion.FirstOneWhere(where, db);
			}

			public PasswordQuestionCollection Top(int count, WhereDelegate<PasswordQuestionColumns> where, Database db = null)
			{
				return PasswordQuestion.Top(count, where, db);
			}

			public PasswordQuestionCollection Top(int count, WhereDelegate<PasswordQuestionColumns> where, OrderBy<PasswordQuestionColumns> orderBy, Database db = null)
			{
				return PasswordQuestion.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PasswordQuestionColumns> where, Database db = null)
			{
				return PasswordQuestion.Count(where, db);
			}
	}

	static PasswordQuestionQueryContext _passwordQuestions;
	static object _passwordQuestionsLock = new object();
	public static PasswordQuestionQueryContext PasswordQuestions
	{
		get
		{
			return _passwordQuestionsLock.DoubleCheckLock<PasswordQuestionQueryContext>(ref _passwordQuestions, () => new PasswordQuestionQueryContext());
		}
	}
	public class SettingQueryContext
	{
			public SettingCollection Where(WhereDelegate<SettingColumns> where, Database db = null)
			{
				return Setting.Where(where, db);
			}
		   
			public SettingCollection Where(WhereDelegate<SettingColumns> where, OrderBy<SettingColumns> orderBy = null, Database db = null)
			{
				return Setting.Where(where, orderBy, db);
			}

			public Setting OneWhere(WhereDelegate<SettingColumns> where, Database db = null)
			{
				return Setting.OneWhere(where, db);
			}
		
			public Setting FirstOneWhere(WhereDelegate<SettingColumns> where, Database db = null)
			{
				return Setting.FirstOneWhere(where, db);
			}

			public SettingCollection Top(int count, WhereDelegate<SettingColumns> where, Database db = null)
			{
				return Setting.Top(count, where, db);
			}

			public SettingCollection Top(int count, WhereDelegate<SettingColumns> where, OrderBy<SettingColumns> orderBy, Database db = null)
			{
				return Setting.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SettingColumns> where, Database db = null)
			{
				return Setting.Count(where, db);
			}
	}

	static SettingQueryContext _settings;
	static object _settingsLock = new object();
	public static SettingQueryContext Settings
	{
		get
		{
			return _settingsLock.DoubleCheckLock<SettingQueryContext>(ref _settings, () => new SettingQueryContext());
		}
	}
	public class SessionQueryContext
	{
			public SessionCollection Where(WhereDelegate<SessionColumns> where, Database db = null)
			{
				return Session.Where(where, db);
			}
		   
			public SessionCollection Where(WhereDelegate<SessionColumns> where, OrderBy<SessionColumns> orderBy = null, Database db = null)
			{
				return Session.Where(where, orderBy, db);
			}

			public Session OneWhere(WhereDelegate<SessionColumns> where, Database db = null)
			{
				return Session.OneWhere(where, db);
			}
		
			public Session FirstOneWhere(WhereDelegate<SessionColumns> where, Database db = null)
			{
				return Session.FirstOneWhere(where, db);
			}

			public SessionCollection Top(int count, WhereDelegate<SessionColumns> where, Database db = null)
			{
				return Session.Top(count, where, db);
			}

			public SessionCollection Top(int count, WhereDelegate<SessionColumns> where, OrderBy<SessionColumns> orderBy, Database db = null)
			{
				return Session.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SessionColumns> where, Database db = null)
			{
				return Session.Count(where, db);
			}
	}

	static SessionQueryContext _sessions;
	static object _sessionsLock = new object();
	public static SessionQueryContext Sessions
	{
		get
		{
			return _sessionsLock.DoubleCheckLock<SessionQueryContext>(ref _sessions, () => new SessionQueryContext());
		}
	}
	public class SessionStateQueryContext
	{
			public SessionStateCollection Where(WhereDelegate<SessionStateColumns> where, Database db = null)
			{
				return SessionState.Where(where, db);
			}
		   
			public SessionStateCollection Where(WhereDelegate<SessionStateColumns> where, OrderBy<SessionStateColumns> orderBy = null, Database db = null)
			{
				return SessionState.Where(where, orderBy, db);
			}

			public SessionState OneWhere(WhereDelegate<SessionStateColumns> where, Database db = null)
			{
				return SessionState.OneWhere(where, db);
			}
		
			public SessionState FirstOneWhere(WhereDelegate<SessionStateColumns> where, Database db = null)
			{
				return SessionState.FirstOneWhere(where, db);
			}

			public SessionStateCollection Top(int count, WhereDelegate<SessionStateColumns> where, Database db = null)
			{
				return SessionState.Top(count, where, db);
			}

			public SessionStateCollection Top(int count, WhereDelegate<SessionStateColumns> where, OrderBy<SessionStateColumns> orderBy, Database db = null)
			{
				return SessionState.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SessionStateColumns> where, Database db = null)
			{
				return SessionState.Count(where, db);
			}
	}

	static SessionStateQueryContext _sessionStates;
	static object _sessionStatesLock = new object();
	public static SessionStateQueryContext SessionStates
	{
		get
		{
			return _sessionStatesLock.DoubleCheckLock<SessionStateQueryContext>(ref _sessionStates, () => new SessionStateQueryContext());
		}
	}
	public class UserBehaviorQueryContext
	{
			public UserBehaviorCollection Where(WhereDelegate<UserBehaviorColumns> where, Database db = null)
			{
				return UserBehavior.Where(where, db);
			}
		   
			public UserBehaviorCollection Where(WhereDelegate<UserBehaviorColumns> where, OrderBy<UserBehaviorColumns> orderBy = null, Database db = null)
			{
				return UserBehavior.Where(where, orderBy, db);
			}

			public UserBehavior OneWhere(WhereDelegate<UserBehaviorColumns> where, Database db = null)
			{
				return UserBehavior.OneWhere(where, db);
			}
		
			public UserBehavior FirstOneWhere(WhereDelegate<UserBehaviorColumns> where, Database db = null)
			{
				return UserBehavior.FirstOneWhere(where, db);
			}

			public UserBehaviorCollection Top(int count, WhereDelegate<UserBehaviorColumns> where, Database db = null)
			{
				return UserBehavior.Top(count, where, db);
			}

			public UserBehaviorCollection Top(int count, WhereDelegate<UserBehaviorColumns> where, OrderBy<UserBehaviorColumns> orderBy, Database db = null)
			{
				return UserBehavior.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserBehaviorColumns> where, Database db = null)
			{
				return UserBehavior.Count(where, db);
			}
	}

	static UserBehaviorQueryContext _userBehaviors;
	static object _userBehaviorsLock = new object();
	public static UserBehaviorQueryContext UserBehaviors
	{
		get
		{
			return _userBehaviorsLock.DoubleCheckLock<UserBehaviorQueryContext>(ref _userBehaviors, () => new UserBehaviorQueryContext());
		}
	}
	public class RoleQueryContext
	{
			public RoleCollection Where(WhereDelegate<RoleColumns> where, Database db = null)
			{
				return Role.Where(where, db);
			}
		   
			public RoleCollection Where(WhereDelegate<RoleColumns> where, OrderBy<RoleColumns> orderBy = null, Database db = null)
			{
				return Role.Where(where, orderBy, db);
			}

			public Role OneWhere(WhereDelegate<RoleColumns> where, Database db = null)
			{
				return Role.OneWhere(where, db);
			}
		
			public Role FirstOneWhere(WhereDelegate<RoleColumns> where, Database db = null)
			{
				return Role.FirstOneWhere(where, db);
			}

			public RoleCollection Top(int count, WhereDelegate<RoleColumns> where, Database db = null)
			{
				return Role.Top(count, where, db);
			}

			public RoleCollection Top(int count, WhereDelegate<RoleColumns> where, OrderBy<RoleColumns> orderBy, Database db = null)
			{
				return Role.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<RoleColumns> where, Database db = null)
			{
				return Role.Count(where, db);
			}
	}

	static RoleQueryContext _roles;
	static object _rolesLock = new object();
	public static RoleQueryContext Roles
	{
		get
		{
			return _rolesLock.DoubleCheckLock<RoleQueryContext>(ref _roles, () => new RoleQueryContext());
		}
	}
	public class UserRoleQueryContext
	{
			public UserRoleCollection Where(WhereDelegate<UserRoleColumns> where, Database db = null)
			{
				return UserRole.Where(where, db);
			}
		   
			public UserRoleCollection Where(WhereDelegate<UserRoleColumns> where, OrderBy<UserRoleColumns> orderBy = null, Database db = null)
			{
				return UserRole.Where(where, orderBy, db);
			}

			public UserRole OneWhere(WhereDelegate<UserRoleColumns> where, Database db = null)
			{
				return UserRole.OneWhere(where, db);
			}
		
			public UserRole FirstOneWhere(WhereDelegate<UserRoleColumns> where, Database db = null)
			{
				return UserRole.FirstOneWhere(where, db);
			}

			public UserRoleCollection Top(int count, WhereDelegate<UserRoleColumns> where, Database db = null)
			{
				return UserRole.Top(count, where, db);
			}

			public UserRoleCollection Top(int count, WhereDelegate<UserRoleColumns> where, OrderBy<UserRoleColumns> orderBy, Database db = null)
			{
				return UserRole.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserRoleColumns> where, Database db = null)
			{
				return UserRole.Count(where, db);
			}
	}

	static UserRoleQueryContext _userRoles;
	static object _userRolesLock = new object();
	public static UserRoleQueryContext UserRoles
	{
		get
		{
			return _userRolesLock.DoubleCheckLock<UserRoleQueryContext>(ref _userRoles, () => new UserRoleQueryContext());
		}
	}    }
}																								
