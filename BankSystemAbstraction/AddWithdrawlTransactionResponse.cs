
namespace BankingSystem
{
    public class AddWithdrawlTransactionResponse
    {
        public string AccountNumber { get; set; } = null!;
        public string TransactionType { get; set; } = null!;
        public string Date { get; set; }
        public double Balance { get; set; }

        public AddWithdrawlTransactionResponse(TransactionDetails transactionDetails, double Balance)
        {
            this.AccountNumber = transactionDetails.AccountNumber;
            this.TransactionType = transactionDetails.TransactionType;
            this.Date = transactionDetails.DOT.ToString("G");
            this.Balance = Balance;

        }
    }
}