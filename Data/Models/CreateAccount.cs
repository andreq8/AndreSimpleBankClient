using OpenBankClient.Data.Models.Enums;

namespace OpenBankClient.Data.Models
{
    public class CreateAccount
    {
        public int Amount { get; set; }
        public Currency Currency { get; set; }

    }
}
