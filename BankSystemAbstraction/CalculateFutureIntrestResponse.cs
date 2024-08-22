
namespace BankingSystem
{
    public class CalculateFutureIntrestResponse
    {
        public string Tenture {get; set;}
        public string LoanAmmount { get; set; }
        public string RateOfIntrest { get; set; }
        public string ProcessingFees { get; set; }
        public double EMIPerMonth { get; set; }
        public string IntrestAmmount { get; set; }
        public string LoanFundedAmmount{ get; set; }


        public CalculateFutureIntrestResponse(CalculateFutureIntrestRequest calculateFutureIntrestRequest, int Installments, double LoanFundedAmmount, double EMI)
        {
            
            this.Tenture = Installments.ToString() + " Installments";
            this.LoanAmmount =  "Rs. " + calculateFutureIntrestRequest.LoanAmmount.ToString();
            this.RateOfIntrest = calculateFutureIntrestRequest.RateOfIntrest.ToString() + "%";
            this.ProcessingFees = "Rs. " +1000.ToString();
            this.EMIPerMonth = EMI;
            if(calculateFutureIntrestRequest.RateOfIntrest != 0)
            this.IntrestAmmount = "Rs. "+(Math.Round((EMI*Installments - calculateFutureIntrestRequest.LoanAmmount), 2)).ToString();
            else this.IntrestAmmount = "Rs. 0";
            this.LoanFundedAmmount = "Rs. " +LoanFundedAmmount.ToString();
        }
    }   
}