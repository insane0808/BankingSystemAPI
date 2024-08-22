using Microsoft.AspNetCore.Mvc;
namespace BankingSystem
{
    public class GetAllBranchesByBankNameApi : Controller
    {
        [HttpGet("/GetBranchesByBankName")]
        public IActionResult GetBranches(GetAllBranchesRequest getAllBranchesRequest) //here we will be having the request and input from the body of the request
        {
            try
            {
                if (getAllBranchesRequest == null)
                    return BadRequest(new { Error = "Please Provide BankName To Get All Branches Details." });


                //Sending For Validation and further processing to buisness layer
                GetAllBranchesService getAllBranchesService = new GetAllBranchesService();
                List<GetAllBranchesResponse> getAllBranchesResponse = getAllBranchesService.GetAllBranches(getAllBranchesRequest);


                if (getAllBranchesResponse == null) return BadRequest(new { Error = "No Branches Found" });
                return Ok(Json(getAllBranchesResponse).Value);
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