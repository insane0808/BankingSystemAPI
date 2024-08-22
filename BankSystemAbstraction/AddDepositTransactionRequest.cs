namespace BankingSystem
{
    public class AddDepositTransactionRequest
    {

        public string AccountNumber { get; set; } = null!;
        public string AccountType { get; set; } = null!;
        public double Ammount { get; set; }
    }
}