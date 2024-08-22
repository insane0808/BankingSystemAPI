using Microsoft.AspNetCore.Mvc;
namespace BankingSystem
{
    public class GetAllTransactionsByDateApi : Controller
    {
        [HttpGet("/GetAllTransactionsByDate")]
        public IActionResult GetAllTransactionsByDate([FromBody] GetAllTransactionsByDateRequest getAllTransactionsByDateRequest, string AccountNumber) //here we will be having the request and input from the body of the request
        {
            try
            {
                if (AccountNumber == null) return BadRequest(new { Error = "Please Provide Your Account Number to get the Transactions" });
                if (getAllTransactionsByDateRequest == null) return BadRequest(new { Error = "Please Give Dates To get Transactions" });
                if (getAllTransactionsByDateRequest.From == null) return BadRequest(new { Error = "Please Enter Date From" });
                if (getAllTransactionsByDateRequest.To == null) return BadRequest(new { Error = "Please Enter Date To" });
                


                // For Further Validaitons and Responses
                GetAllTransactionsByDateService getAllTransactionsByDateService = new GetAllTransactionsByDateService();
                List<GetAllTransactionsByDateResponse> getAllTransactionsByDateResponses = getAllTransactionsByDateService.GetAllTransactions(DateTime.Parse(getAllTransactionsByDateRequest.From), DateTime.Parse(getAllTransactionsByDateRequest.To), AccountNumber);


                if (getAllTransactionsByDateResponses == null) return BadRequest(new { Error = "No Records Found." });
                return Ok(Json(getAllTransactionsByDateResponses).Value);
            }
            catch (Exception exception)
            {
                if(exception.Message.Contains("exists"))
                {
                    return NotFound(new{Error=exception.Message});
                }
                return BadRequest(new { Error = exception.Message });
            }
        }
    }
}