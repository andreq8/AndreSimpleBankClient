using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using OpenBankClient.Data.Services.Base;

namespace OpenBankClient.Data.Services
{
    public class AccountsService : BaseService
    {
        private IClient _httpClient;
        private ProtectedLocalStorage _localStorage;
        private readonly ILogger<AccountsService> _logger;
        public AccountsService(IClient httpClient, ProtectedLocalStorage localStorage, ILogger<AccountsService> logger) 
            : base(httpClient, localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
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
                var response = HandleApiException(ex);
                return (false, null, response);
            }
        }
        public async Task<(bool, GetAccountResponse?, string?)> GetAccountDetails(int id)
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.AccountsGETAsync(id, token);
                return (true, response, null);
            }
            catch(ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response);
            }
        }
        public async Task<(bool, AccountResponse?, string?)>? CreateAccountAsync(AccountRequest accountRequest)
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.AccountsPOSTAsync(token, accountRequest);
                return (true, response, null);
            }
            catch(ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response);
            }
        }

    }
}
