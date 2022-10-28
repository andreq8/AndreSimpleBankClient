using OpenBankClient.Data.Services.Base;

namespace OpenBankClient.Data.Models
{
    public class Account
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public Currency Currency { get; set; }
    }
}
