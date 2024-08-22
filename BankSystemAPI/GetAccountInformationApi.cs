using Microsoft.AspNetCore.Mvc;
namespace BankingSystem
{
    public class GetAccountInformationApi : Controller
    {
        [HttpGet("/GetAccountInformation")]
        public IActionResult GetBranches(GetAccountInformationRequest getAccountInformationRequest) //here we will be having the request and input from the body of the request
        {
            try
            {
                if (getAccountInformationRequest == null)
                    return BadRequest(new { Error = "Please Enter Account Number Attribute to get Information" });


                //Sending For Validation and further processing to buisness layer
                GetAccountInformationService getAccountInformationService = new GetAccountInformationService();
                GetAccountInformationResponse getAccountInformationResponse = getAccountInformationService.GetAccountInformation(getAccountInformationRequest);


                if (getAccountInformationResponse == null) return BadRequest(new { Error = "No Account Found" });
                return Ok(Json(getAccountInformationResponse).Value);
            }
            catch (Exception exception)
            {
                if(exception.Message.Contains("exists"))
                    return NotFound(new{Error=exception.Message});

                return BadRequest(new { Error = exception.Message });
            }
        }
    }
}