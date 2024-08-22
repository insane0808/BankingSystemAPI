
namespace BankingSystem
{
    public class GetLast3MonthsRecordsResponse
    {
        public string TransactionId { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public String TransactionType { get; set; } = null!;
        public string DOT { get; set; }
        public double Ammount { get; set; }

        public GetLast3MonthsRecordsResponse(TransactionDetails transactionDetails)
        {
            this.TransactionId = transactionDetails.TransactionId;
            this.AccountNumber = transactionDetails.AccountNumber;
            this.TransactionType = transactionDetails.TransactionType;
            this.DOT = transactionDetails.DOT.ToString("G");
            this.Ammount = transactionDetails.Ammount;
        }
    }
}