
namespace BankingSystem
{
    public class GetAllTransactionsByDateRequest
    {
        public string From { get; set; } = null!;
        public string To { get; set; } = null!;
    }
}