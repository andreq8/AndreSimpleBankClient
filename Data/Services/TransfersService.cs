using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using OpenBankClient.Data.Services.Base;

namespace OpenBankClient.Data.Services
{
    public class TransfersService
    {
        private IClient _httpClient;
        private ProtectedLocalStorage _localStorage;
        public TransfersService(IClient httpClient, ProtectedLocalStorage protectedLocalStorage)
        {
            _httpClient = httpClient;
            _localStorage = protectedLocalStorage;
        }
        public async Task<(bool, string, int?)> Transfer(TransferRequest transferRequest)
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
                return (false, ex.Response, ex.StatusCode);
            }
        }
        
    }
}
