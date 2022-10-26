using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using OpenBankClient.Data.Services.Base;

namespace OpenBankClient.Data.Services
{
    public class AccountsService
    {
        private IClient _httpClient;
        private ProtectedLocalStorage _localStorage;
        private ILogger<AccountsService> _logger;
        public AccountsService(IClient httpClient, ProtectedLocalStorage protectedLocalStorage, ILogger<AccountsService> logger)
        {
            _httpClient = httpClient;
            _localStorage = protectedLocalStorage;
            _logger = logger;
        }
        public async Task<(bool, ICollection<AccountResponse>?, string?)> GetAllAccounts()
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.AccountsAllAsync(token);
                return (true, response, null);
            }
            catch(ApiException ex)
            {
                var authenticationHeader = ex.Headers.FirstOrDefault(h => h.Key == "WWW-Authenticate");
                if (authenticationHeader.Value.Any(s => s.Contains("token expired")))
                {
                    _logger.LogInformation("token expired");
                    return (false, null, "token expired");
                }
                return (false, null, ex.StatusCode.ToString());
            }
        }
        public async Task<GetAccountResponse> GetAccountDetails(int id)
        {
            var auth = await _localStorage.GetAsync<string>("token");
            var token = String.Join(" ", "Bearer", auth.Value);
            var response = await _httpClient.AccountsGETAsync(id, token);
            return response;
        }
        public async Task<AccountResponse>? CreateAccountAsync(AccountRequest accountRequest)
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.AccountsPOSTAsync(token, accountRequest);
                return response;
            }
            catch(ApiException)
            {
                return null;
            }
        }

    }
}
