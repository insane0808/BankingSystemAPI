using Microsoft.AspNetCore.Mvc;
namespace BankingSystem
{
    public class AddDepositTransactionApi : Controller
    {
        [HttpPost("/AddDepositTransaction")]
        public IActionResult Deposit([FromBody] AddDepositTransactionRequest addDepositTransactionRequest, string AccountNumber)
        {
            try
            {   //Checking for object nullablity
                if (addDepositTransactionRequest == null) return BadRequest(new { Error = "Please Enter Account Number, Account Type and Ammount" });
                if (AccountNumber == null) return BadRequest(new { Error = "Please Provide Account Number" });


                AddDepositTransactionService addDepositTransactionService = new AddDepositTransactionService();
                addDepositTransactionRequest.AccountNumber = AccountNumber; //Assigning the header account number to the model and sending for further processing

                AddDepositTransactionResponse addDepositTransactionResponse = addDepositTransactionService.AddDepositTransaction(addDepositTransactionRequest);
                if (addDepositTransactionResponse == null) return BadRequest(new { Error = "Transaction Not Processed Due to some technical error." });
                return Ok(Json(addDepositTransactionResponse).Value);
            }
            catch (Exception exception)
            {

                if(exception.Message.Contains("exists"))
                {
                    return NotFound(new {Error = exception.Message});
                }
                return BadRequest(new { Error = exception.Message });
            }
        }
    }
}