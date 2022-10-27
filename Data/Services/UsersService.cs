using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using OpenBankClient.Data.Providers;
using OpenBankClient.Data.Services.Base;

namespace OpenBankClient.Data.Services
{
    public class UsersService : BaseService
    {
        private readonly IClient _httpClient;
        private readonly ProtectedLocalStorage _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILogger<UsersService> _logger;
        public UsersService(IClient httpClient, ProtectedLocalStorage localStorage, ILogger<UsersService> logger, AuthenticationStateProvider authenticationStateProvider) 
            : base(httpClient, localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _logger = logger;
            _authenticationStateProvider = authenticationStateProvider;
    }
        public async Task<(bool, string?)> RegisterUserAsync(CreateUserRequest user)
        {
            try
            {
                var response = await _httpClient.UsersAsync(user);
                return (true, "User registered");
            }
            catch(ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, response);
            }
        }
        public async Task<(bool, LoginUserResponse?, string?)> LoginAsync(LoginUserRequest loginRequest)
        {
            try
            {
                var response = await _httpClient.LoginAsync(loginRequest);
                await _localStorage.SetAsync("token", response.AccessToken);
                await _localStorage.SetAsync("refreshToken", response.RefreshToken);
                ((CustomAuthStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(response.User.Username);
                return (true, response, null);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response);
            }
        }
        public async Task LogoutAsync()
        {
            await _localStorage.DeleteAsync("token");
            ((CustomAuthStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        }
        public async Task<(bool, string?)> RenewSessionAsync()
        {
            try
            {
                var refreshToken = await _localStorage.GetAsync<string>("refreshToken");
                var renewRequest = new RenewRequest
                {
                    RefreshToken = refreshToken.Value,
                };
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.RenewAsync(token, renewRequest);
                await _localStorage.SetAsync("token", response.AccessToken);
                await _localStorage.SetAsync("refreshToken", response.RefreshToken);
                return (true, null);
            }
            catch(ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, response);
            }

        }
    }
}
