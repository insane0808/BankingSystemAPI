using Microsoft.AspNetCore.Mvc;
namespace BankingSystem
{
    public class CreateBranchApi : Controller
    {

        [HttpPost("/CreateBranch")]
        public IActionResult CreateBranch([FromBody] CreateBranchRequest createBranchRequest) //here we will be having the request and input from the body of the request
        {
            try
            {
                if (createBranchRequest == null)
                    throw new ArgumentException($"Please Enter All Valid Details Of Your Branch");


                //Sending For Validation and further processing to buisness layer
                CreateBranchService createBranchService = new CreateBranchService();
                CreateBranchResponse createBranchResponse = createBranchService.CreateBranch(createBranchRequest);


                if (createBranchResponse == null) return BadRequest(new { Error = "Branch Not Created, Due to some internal Error" });
                return Created("Database",Json(createBranchResponse).Value);
            }
            catch (Exception exception)
            {
                if(exception.Message.Contains("existing"))
                    return Conflict(new { Error = exception.Message });
                    
                return BadRequest(new { Error = exception.Message });
            }

        }
    }
}