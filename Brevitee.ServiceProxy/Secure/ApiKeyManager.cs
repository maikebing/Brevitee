using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;

namespace Brevitee.ServiceProxy.Secure
{
    [Encrypt]
    [Proxy("apiKeyMgr")]
    public class ApiKeyManager : IRequiresHttpContext
    {
        public ApiKeyManager()
        {
            this.UserResolver = DefaultUserResolver.Instance;
        }

        static ApiKeyManager _default;
        static object _defaultLock = new object();
        public static ApiKeyManager Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () =>
                {
                    return new ApiKeyManager();
                });
            }
            set
            {
                _default = value;
            }
        }
        
        /// <summary>
        /// The component used to resolve the current user
        /// or the user of a specified IHttpContext based
        /// on the session cookie therein
        /// </summary>
        public IUserResolver UserResolver
        {
            get;
            protected internal set;
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }

        public string GetClientId(IApplicationNameProvider nameProvider)
        {
            return GetClientId(nameProvider.GetApplicationName());
        }

        public static string GetClientId(string applicationName)
        {
            return "{0}:{1}"._Format(applicationName, applicationName.Sha1());
        }

        public string GetApplicationApiKey(string applicationClientId, int index)
        {
            ApiKeyCollection keys = ApiKey.Where(c => c.ClientId == applicationClientId, Order.By<ApiKeyColumns>(c => c.CreatedAt, SortOrder.Descending));
            return keys[index].SharedSecret;
        }
        public ApplicationCreateResult CreateApplication(string applicationName)
        {
            return CreateApplication(HttpContext, UserResolver, applicationName);
        }

        public static ApplicationCreateResult CreateApplication(IHttpContext context, IUserResolver userResolver, string applicationName)
        {
            ApplicationCreateResult result = new ApplicationCreateResult();
            try
            {
                Application app = Application.OneWhere(c => c.Name == applicationName);
                if (app != null)
                {
                    result.Status = ApplicationCreateStatus.NameInUse;
                }
                else
                {
                    string createdBy = userResolver.GetUser(context);
                    if (string.IsNullOrEmpty(createdBy))
                    {
                        createdBy = userResolver.GetCurrentUser();
                        if (string.IsNullOrEmpty(createdBy))
                        {
                            throw new UnableToResolveUserException(userResolver);
                        }
                    }

                    app = new Application();
                    app.Name = applicationName;
                    app.Save();
                    AddKey(app, userResolver, context);

                    result.Application = app;
                    result.Status = ApplicationCreateStatus.Success;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Status = ApplicationCreateStatus.Error;
            }

            return result;
        }

        public Application GetApplication(string applicationClientId)
        {
            string[] split = applicationClientId.DelimitSplit(":");
            if (split.Length != 2)
            {
                throw new ArgumentException("The specified applicationClientId is not valid: {0}"._Format(applicationClientId));
            }

            return Application.OneWhere(c => c.Name == split[0]);
        }

        public ApiKey AddKey(string applicationClientId)
        {
            return AddKey(GetApplication(applicationClientId), UserResolver, HttpContext);
        }

        public static ApiKey AddKey(Application app, IUserResolver userResolver, IHttpContext context )
        {
            ApiKey key = app.ApiKeysByApplicationId.AddNew();
            key.ClientId = GetClientId(app.Name);
            key.Disabled = false;
            key.SharedSecret = ServiceProxySystem.GenerateId();
            key.CreatedBy = userResolver.GetUser(context);
            key.CreatedAt = new Instant();
            key.Save();
            return key;
        }
    }
}
