using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using OpenBankClient.Data.Services.Base;

namespace OpenBankClient.Data.Services
{
    public class UsersService
    {
        private readonly IClient _httpClient;
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly ILogger<UsersService> _logger;
        public UsersService(IClient httpClient, ProtectedLocalStorage protectedLocalStorage, ILogger<UsersService> logger)
        {
            _httpClient = httpClient;
            _protectedLocalStorage = protectedLocalStorage;
            _logger = logger;
        }
        public async Task RegisterUser(CreateUserRequest user)
        {
            var response = await _httpClient.UsersAsync(user);
            Console.WriteLine(response.ToString());
            //await _httpClient.PostAsJsonAsync("/api/v1/users", user);
        }
        public async Task<LoginUserResponse> Login(LoginUserRequest loginRequest)
        {
            try
            {
            var response = await _httpClient.LoginAsync(loginRequest);
            await _protectedLocalStorage.SetAsync("token", response.AccessToken);
            return response;
            }
            catch(ApiException ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
