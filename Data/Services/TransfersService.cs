using AutoMapper;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using OpenBankClient.Data.Models;
using OpenBankClient.Data.Services.Base;

namespace OpenBankClient.Data.Services
{
    public class TransfersService : BaseService
    {
        private readonly IClient _httpClient;
        private readonly ProtectedLocalStorage _localStorage;
        private IMapper _mapper;
        public TransfersService(IClient httpClient, ProtectedLocalStorage _localStorage, IMapper mapper) 
            : base(httpClient, _localStorage)
        {
            _httpClient = httpClient;
            this._localStorage = _localStorage;
            _mapper = mapper;
        }
        public async Task<(bool, string?, string?)> Transfer(Transfer transfer)
        {
            try
            {
                var auth = await _localStorage.GetAsync<string>("token");
                var token = String.Join(" ", "Bearer", auth.Value);
                var transferRequest = _mapper.Map<TransferRequest>(transfer);
                var response = await _httpClient.TransfersAsync(token, transferRequest);
                return (true, response, null);
            }
            catch (ApiException ex)
            {
                var response = HandleApiException(ex);
                return (false, response.Item1, response.Item2);
            }
        }
        
    }
}
