using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using OpenBankClient.Data.Services.Base;

namespace OpenBankClient.Data.Services
{
    public class AccountsService
    {
        private IClient _httpClient;
        private ProtectedLocalStorage _localStorage;
        public AccountsService(IClient httpClient, ProtectedLocalStorage protectedLocalStorage)
        {
            _httpClient = httpClient;
            _localStorage = protectedLocalStorage;
        }
        public async Task<ICollection<AccountResponse>> GetAllAccounts()
        {
            var auth = await _localStorage.GetAsync<string>("token");
            var token = String.Join(" ", "Bearer", auth.Value);
            var response = await _httpClient.AccountsAllAsync(token);
            //await _httpClient.PostAsJsonAsync("/api/v1/users", user);
            return response;
        }
        public async Task<GetAccountResponse> GetAccountDetails(int id)
        {
            var auth = await _localStorage.GetAsync<string>("token");
            var token = String.Join(" ", "Bearer", auth.Value);
            var response = await _httpClient.AccountsGETAsync(id, token);
            return response;
        }

    }
}
