using AutoMapper;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using OpenBankClient.Data.Models;
using OpenBankClient.Data.Services.Base;
using OpenBankClient.Data.Services.Interfaces;

namespace OpenBankClient.Data.Services
{
    public class AccountsService : BaseService, IAccountsService
    {
        private IClient _httpClient;
        private ProtectedLocalStorage _localStorage;
        private readonly ILogger<AccountsService> _logger;
        private IMapper _mapper;
        public AccountsService(IClient httpClient, ProtectedLocalStorage localStorage, ILogger<AccountsService> logger, IMapper mapper)
            : base(httpClient, localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<(bool, IList<Account>?, string?)> GetAllAccounts()
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.AccountsAllAsync(token);
                var accounts = _mapper.Map<IList<Account>>(response);

                return (true, accounts, null);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response.Item2);
            }
        }
        public async Task<(bool, AccountDetails?, string?)> GetAccountDetails(int id)
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var response = await _httpClient.AccountsGETAsync(id, token);
                var account = _mapper.Map<GetAccountResponse, AccountDetails>(response);
                return (true, account, null);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response.Item2);
            }
        }
        public async Task<(bool, AccountResponse?, string?)>? CreateAccountAsync(CreateAccount account)
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var accountRequest = _mapper.Map<AccountRequest>(account);
                var response = await _httpClient.AccountsPOSTAsync(token, accountRequest);
                return (true, response, null);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, null, response.Item2);
            }
        }

    }
}
