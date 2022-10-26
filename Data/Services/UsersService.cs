using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using OpenBankClient.Data.Providers;
using OpenBankClient.Data.Services.Base;

namespace OpenBankClient.Data.Services
{
    public class UsersService
    {
        private readonly IClient _httpClient;
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILogger<UsersService> _logger;
        public UsersService(IClient httpClient, ProtectedLocalStorage protectedLocalStorage, ILogger<UsersService> logger, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _protectedLocalStorage = protectedLocalStorage;
            _logger = logger;
            _authenticationStateProvider = authenticationStateProvider;
    }
        public async Task RegisterUser(CreateUserRequest user)
        {
            var response = await _httpClient.UsersAsync(user);
            Console.WriteLine(response.ToString());
            //await _httpClient.PostAsJsonAsync("/api/v1/users", user);
        }
        public async Task<(bool, LoginUserResponse?, int?)> LoginAsync(LoginUserRequest loginRequest)
        {
            try
            {
                var response = await _httpClient.LoginAsync(loginRequest);
                await _protectedLocalStorage.SetAsync("token", response.AccessToken);
                await _protectedLocalStorage.SetAsync("refreshToken", response.RefreshToken);
                ((CustomAuthStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(response.User.Username);
                return (true, response, null);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                return (false, null, ex.StatusCode);
            }
        }
        public async Task LogoutAsync()
        {
            await _protectedLocalStorage.DeleteAsync("token");
            ((CustomAuthStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        }
        public async Task RenewSessionAsync()
        {
            var refreshToken = await _protectedLocalStorage.GetAsync<string>("refreshToken");
            var renewRequest = new RenewRequest
            {
                RefreshToken = refreshToken.Value,
            };
            var auth = await _protectedLocalStorage.GetAsync<string>("token");
            var token = String.Join(" ", "Bearer", auth.Value);
            var response = await _httpClient.RenewAsync(token, renewRequest);
            await _protectedLocalStorage.SetAsync("token", response.AccessToken);
            await _protectedLocalStorage.SetAsync("refreshToken", response.RefreshToken);
        }
    }
}
