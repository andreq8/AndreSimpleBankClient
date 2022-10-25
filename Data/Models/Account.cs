using OpenBankClient.Data.Models.Enums;

namespace OpenBankClient.Data.Models
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public Currency Currency { get; set; }

    }
}
