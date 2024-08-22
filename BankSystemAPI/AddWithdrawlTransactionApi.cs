using Microsoft.AspNetCore.Mvc;
namespace BankingSystem
{
    public class AddWithdrawlTransactionApi : Controller
    {
        [HttpPost("/AddWithdrawlTransaction")]
        public IActionResult Withdrawl([FromBody] AddWithdrawlTransactionRequest addWithdrawlTransactionRequest, string AccountNumber)
        {
            try
            {
                if (addWithdrawlTransactionRequest == null) return BadRequest(new { Error = "Please Enter Account Number, Account Type and Ammount" });
                if (AccountNumber == null) return BadRequest(new { Error = "Please Provide Account Number." });


                AddWithdrawlTransactionService addWithdrawlTransactionService = new AddWithdrawlTransactionService();
                addWithdrawlTransactionRequest.AccountNumber = AccountNumber;
                AddWithdrawlTransactionResponse addWithdrawlTransactionResponse = addWithdrawlTransactionService.AddWithdrawlTransaction(addWithdrawlTransactionRequest);


                if (addWithdrawlTransactionResponse == null) return BadRequest(new { Error = "Transaction Not Processed, Try Again Later." });
                return Ok(Json(addWithdrawlTransactionResponse).Value);
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