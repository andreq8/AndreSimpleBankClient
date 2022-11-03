using OpenBankClient.Data.Models;
using OpenBankClient.Data.Services.Base;

namespace OpenBankClient.Data.Services.Interfaces
{
    public interface IAccountsService
    {
        Task<(bool, AccountResponse?, string?)>? CreateAccountAsync(CreateAccount account);
        Task<(bool, AccountDetails?, string?)> GetAccountDetails(int id);
        Task<(bool, IList<Account>?, string?)> GetAllAccounts();
    }
}