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
        protected  string HandleApiException(ApiException ex)
        {
            if(ex.Headers != null)
            { 
                if(ex.Headers.Any(h => h.Value.Any(v => v.Contains("token expired"))))
                {
                    return "token expired";
                }
            }
            return ex.StatusCode.ToString();
        }
    }
}
