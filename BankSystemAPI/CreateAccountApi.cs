using Microsoft.AspNetCore.Mvc;
namespace BankingSystem
{
    public class CreateAccountApi : Controller
    {
        [HttpPost("/CreateAccount")]
        public IActionResult CreateAccount([FromBody] CreateAccountRequest createAccountRequest)
        {
            try
            {
                if (createAccountRequest == null)
                    throw new ArgumentException($"Please Fill All Valid Details Of Your Account.");


                //Sending For Validation and further processing to buisness layer
                CreateAccountService createAccountService = new CreateAccountService();
                CreateAccountResponse createAccountResponse = createAccountService.CreateAccount(createAccountRequest);


                if (createAccountResponse == null) return BadRequest(new { Error = "Technical Error, Try Again" });
                return Created("Database",Json(createAccountResponse).Value);
            }
            catch (Exception exception)
            {
                if(exception.Message.Contains("existing"))
                {
                    return Conflict(new { Error = exception.Message });
                }
                return BadRequest(new { Error = exception.Message });
            }
        }
    }
}