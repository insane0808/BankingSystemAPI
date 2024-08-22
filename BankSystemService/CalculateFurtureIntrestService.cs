
namespace BankingSystem
{
    public class CalculateFurtureIntrestService
    {
        public CalculateFutureIntrestResponse CalculateIntrest(CalculateFutureIntrestRequest calculateFutureIntrestRequest)
        {

            if (calculateFutureIntrestRequest == null) throw new ArgumentException($"Please Enter Details to calculate Intrest");
            if ((calculateFutureIntrestRequest.months<0  ||calculateFutureIntrestRequest.months > 120) || (calculateFutureIntrestRequest.months>11 && calculateFutureIntrestRequest.years >= 1)) throw new ArgumentException($"Please Enter Valid Months B/W 1 - 120 or with year 1 - 11(Max 10 Year Tenture)");
            if (calculateFutureIntrestRequest.years< 0  ||calculateFutureIntrestRequest.years > 10 || (calculateFutureIntrestRequest.years>9 && calculateFutureIntrestRequest.months!= 0)) throw new ArgumentException($"Please Enter Valid Years B/W 1 - 10 or max 9 with months(Max 10 Year Tenture)");
            if (calculateFutureIntrestRequest.LoanAmmount < 2000 || calculateFutureIntrestRequest.LoanAmmount > 1000000)throw new ArgumentException($"Please Enter Valid Loan Ammount. We can Provide Loan From 2000 Up-to 10 Lacs.");
            if (calculateFutureIntrestRequest.RateOfIntrest < 0 || calculateFutureIntrestRequest.RateOfIntrest > 1000)throw new ArgumentException($"Please Ensure The Rate of Intrest, Max 1000% is Allowed");
            // Validating IFSC code
            calculateFutureIntrestRequest.RateOfIntrest = Math.Round(calculateFutureIntrestRequest.RateOfIntrest, 2);
            calculateFutureIntrestRequest.LoanAmmount = Math.Round(calculateFutureIntrestRequest.LoanAmmount, 2);
            var RateOfIntrest = (calculateFutureIntrestRequest.RateOfIntrest / (12 * 100)); // one month interest
            int Installmants = 1;
            //If We will not be getting the date Then We will be calculating the installmants based on months and years
            if(calculateFutureIntrestRequest.years > 0)
            {
                Installmants = calculateFutureIntrestRequest.years * 12;
            }
            else if(calculateFutureIntrestRequest.years == 0)
            {
                Installmants = calculateFutureIntrestRequest.months;
            }
            if(calculateFutureIntrestRequest.months > 0){
                Installmants += calculateFutureIntrestRequest.months;
            }
            double EMI;
            if(calculateFutureIntrestRequest.RateOfIntrest == 0){ //If we are giving loan to him on ZER0 INTREST.z
                EMI = calculateFutureIntrestRequest.LoanAmmount/Installmants;
            }
            else{
                //If any Error happens in the calcultaion then we will be returning the BAD request.
                try {
                    EMI = (calculateFutureIntrestRequest.LoanAmmount *RateOfIntrest * (double)Math.Pow(1 +RateOfIntrest, Installmants))/ (double)(Math.Pow(1 +RateOfIntrest, Installmants) - 1);
                } catch(Exception){
                    throw new ArgumentException($"Intrest Calculation Having Some Error Please Contact Admin.");
                }
            }
            //Sending Data to MongoDB
            //Making the EMI as the 2 digit value
            EMI = Math.Round(EMI,2);
            double LoanFundedAmmount = calculateFutureIntrestRequest.LoanAmmount - 1000;
            // Response
            CalculateFutureIntrestResponse calculateFutureIntrestResponse = new CalculateFutureIntrestResponse(calculateFutureIntrestRequest, Installmants, LoanFundedAmmount, EMI);
            return calculateFutureIntrestResponse;
        }
    }
}