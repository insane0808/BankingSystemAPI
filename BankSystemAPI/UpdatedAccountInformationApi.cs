using Microsoft.AspNetCore.Mvc;
namespace BankingSystem
{
    public class UpdatedAccountInformationApi : Controller
    {
        [HttpPut("/UpdatedAccountInformation")]
        public IActionResult GetUpdatedAccountInformation([FromBody] UpdatedAccountInformationRequest updatedAccountInformationRequest, string AccountNumber) //here we will be having the request and input from the body of the request
        {
            try
            {
                if (updatedAccountInformationRequest == null)
                    return BadRequest(new { Error = "Please Enter Details To Update" });
                if (AccountNumber == null)
                    return BadRequest(new { Error = "Please Provide Your Account Number" });


                //Sending For Validation and further processing to buisness layer
                UpdatedAccountInformationService updatedAccountInformationService = new UpdatedAccountInformationService();
                updatedAccountInformationRequest.AccountNumber = AccountNumber;
                UpdatedAccountInformationResponse updatedAccountInformationResponse = updatedAccountInformationService.UpdatedAccountInformation(updatedAccountInformationRequest);


                if (updatedAccountInformationResponse == null) return BadRequest(new { Error = "Nothing Updated" });
                return Created("Database",Json(updatedAccountInformationResponse).Value);
            }
            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });
            }
        }
    }
}