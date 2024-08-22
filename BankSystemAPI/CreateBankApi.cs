using Microsoft.AspNetCore.Mvc;
namespace BankingSystem
{
    [Route("controller")]
    public class CreateBankApi : Controller
    {
        [HttpPost("/CreateBank")]
        public IActionResult CreateBank([FromBody] CreateBankRequest createBankRequest) //here we will be having the request and input from the body of the request
        {
            try
            {
                if (createBankRequest == null)
                    throw new ArgumentException($"Please Fill All Details Of Your Bank.");

                //Sending For Validation and further processing to buisness layer
                CreateBankService createBankService = new CreateBankService();
                CreateBankResponse createBankResponse = createBankService.CreateBank(createBankRequest);


                if (createBankResponse == null) return BadRequest(new { Error = "Technical Error, Try Again" });
                return Created("Database",Json(createBankResponse).Value);
            }
            catch (Exception exception)
            {
                if(exception.Message.Contains("used"))
                    return Conflict(new { Error = exception.Message });
                    
                return BadRequest(new { Error = exception.Message });
            }
        }
    }
}