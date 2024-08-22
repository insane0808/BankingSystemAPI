using Microsoft.AspNetCore.Mvc;
namespace BankingSystem
{
    public class CalculateFutureIntrestApi : Controller
    {
        [HttpGet("/GetEMI")]
        public IActionResult getEMI([FromBody] CalculateFutureIntrestRequest calculateFutureIntrestRequest)
        {    
            
            try
            {
               if(calculateFutureIntrestRequest == null) return BadRequest(new {Error="Please Provide All Details"});
                CalculateFurtureIntrestService calculateFurtureIntrestService = new CalculateFurtureIntrestService();
                CalculateFutureIntrestResponse calculateFutureIntrestResponse = calculateFurtureIntrestService.CalculateIntrest(calculateFutureIntrestRequest);


                if (calculateFutureIntrestResponse == null) return BadRequest(new { Error = "EMI Not calculated, Due to some Internal Error" });
                return Ok(Json(calculateFutureIntrestResponse).Value);
            }
            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });
            }

        }
    }
}