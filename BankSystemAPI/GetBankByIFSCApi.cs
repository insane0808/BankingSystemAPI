using Microsoft.AspNetCore.Mvc;

namespace BankingSystem
{
    public class GetBankByIFSCApi : Controller
    {
        [HttpGet("/GetBankByIFSC")]
        public IActionResult GetBankByIFSC(GetBankRequest getBankRequest) //here we will be having the request and input from the body of the request
        {
            try
            {
                if (getBankRequest == null)
                    return BadRequest(new { Error = "Please Enter IFSC Code Of Your Branch" });


                //Sending For Validation and further processing to buisness layer
                GetBankService getBankService = new GetBankService();
                GetBankResponse getBankResponse = getBankService.GetBank(getBankRequest);


                if (getBankResponse == null) return BadRequest(new { Error = "No Bank Found" });
                return Ok(Json(getBankResponse).Value);
            }
            catch (Exception exception)
            {
                if(exception.Message.Contains("exists")){
                    return NotFound(new{Error=exception.Message});
                }
                return BadRequest(new { Error = exception.Message });
            }
        }
    }
}