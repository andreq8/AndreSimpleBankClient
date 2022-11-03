using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace OpenBankClient.Data.Services.Base
{
    public class BaseService
    {
        protected IClient _client;
        protected ProtectedLocalStorage _protectedLocalStorage;

        public BaseService(IClient client, ProtectedLocalStorage protectedLocalStorage)
        {
            _client = client;
            _protectedLocalStorage = protectedLocalStorage;
        }
        protected  (string, string) HandleApiException(ApiException ex)
        {
            if(ex.Headers != null)
            { 
                if(ex.Headers.Any(h => h.Value.Any(v => v.Contains("token expired"))))
                {
                    return (ex.Response, "token expired");
                }
            }
            return (ex.Response, ex.StatusCode.ToString());
        }
    }
}
