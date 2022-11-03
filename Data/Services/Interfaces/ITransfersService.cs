using OpenBankClient.Data.Models;

namespace OpenBankClient.Data.Services.Interfaces
{
    public interface ITransfersService
    {
        Task<(bool, string?, string?)> Transfer(Transfer transfer);
    }
}