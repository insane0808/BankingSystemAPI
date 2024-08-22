
namespace BankingSystem
{
    public class CalculateFutureIntrestRequest
    {
        public int months {get; set;}
        public int years{get; set;}
        public double LoanAmmount { get; set; }
        public double RateOfIntrest { get; set; }
    }
}