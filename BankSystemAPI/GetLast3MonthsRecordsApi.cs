using Microsoft.AspNetCore.Mvc;
namespace BankingSystem
{
    public class GetLast3MonthsRecordsApi : Controller
    {
        [HttpGet("/GetLast3MonthsRecords")]
        public IActionResult GetLast3MonthsRecords(string AccountNumber) //here we will be having the request and input from the body of the request
        {
            try
            {
                if (AccountNumber == null)
                    return BadRequest(new { Error = "Please Provide Your Account Number to Get Transaction Records" });

                GetLast3MonthsRecordsService getLast3MonthsRecordsService = new GetLast3MonthsRecordsService();
                List<GetLast3MonthsRecordsResponse> getlast3MonthsRecordsResponses = getLast3MonthsRecordsService.GetLast3MonthsRecords(AccountNumber);


                if (getlast3MonthsRecordsResponses == null) return BadRequest(new { Error = "No Records Found." });
                return Ok(Json(getlast3MonthsRecordsResponses).Value);
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