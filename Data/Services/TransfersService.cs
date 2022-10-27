using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using OpenBankClient.Data.Services.Base;

namespace OpenBankClient.Data.Services
{
    public class TransfersService : BaseService
    {
        private readonly IClient _httpClient;
        private readonly ProtectedLocalStorage _localStorage;
        public TransfersService(IClient httpClient, ProtectedLocalStorage _localStorage) 
            : base(httpClient, _localStorage)
        {
            _httpClient = httpClient;
            this._localStorage = _localStorage;
        }
        public async Task<(bool, string?, string?)> Transfer(TransferRequest transferRequest)
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.TransfersAsync(token, transferRequest);
                return (true, response, null);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response);
            }
        }
        
    }
}
