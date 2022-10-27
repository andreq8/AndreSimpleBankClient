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

    }
}
