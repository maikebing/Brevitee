using System;
namespace Brevitee.ServiceProxy.Secure
{
    public interface IApiKeyProvider
    {
        ApiKeyInfo GetApiKeyInfo(IApplicationNameProvider nameProvider);
        string GetApplicationApiKey(string applicationClientId, int index);
        string GetApplicationClientId(IApplicationNameProvider nameProvider);
        string GetCurrentApiKey();
    }
}
