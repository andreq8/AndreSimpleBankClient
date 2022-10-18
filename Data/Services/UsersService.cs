using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using OpenBankClient.Data.Services.Base;

namespace OpenBankClient.Data.Services
{
    public class UsersService
    {
        private readonly IClient _httpClient;
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        public UsersService(IClient httpClient, ProtectedLocalStorage protectedLocalStorage)
        {
            _httpClient = httpClient;
            _protectedLocalStorage = protectedLocalStorage;
        }
        public async Task RegisterUser(CreateUserRequest user)
        {
            var response = await _httpClient.UsersAsync(user);
            Console.WriteLine(response.ToString());
            //await _httpClient.PostAsJsonAsync("/api/v1/users", user);
        }
        public async Task<LoginUserResponse> Login(LoginUserRequest loginRequest)
        {
            var response = await _httpClient.LoginAsync(loginRequest);
            await _protectedLocalStorage.SetAsync("token", response.AccessToken);
            return response;
        }
    }
}
