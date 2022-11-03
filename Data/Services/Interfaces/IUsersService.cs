using OpenBankClient.Data.Models;
using OpenBankClient.Data.Services.Base;

namespace OpenBankClient.Data.Services.Interfaces
{
    public interface IUsersService
    {
        Task<(bool, LoginUserResponse?, string?)> LoginAsync(User user);
        Task LogoutAsync();
        Task<(bool, string?)> RegisterUserAsync(CreateUser user);
        Task<(bool, string?)> RenewSessionAsync();
    }
}