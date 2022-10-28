namespace OpenBankClient.Data.Models
{
    public class AccountDetails
    {
        public Account Account { get; set; }
        public IList<Movim> Movims { get; set; }
    }
}
