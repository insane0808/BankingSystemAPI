
namespace BankingSystem
{
    public class GetAllBranchesService
    {
        public List<GetAllBranchesResponse> GetAllBranches(GetAllBranchesRequest getAllBranchesRequest)
        {

            // Normal Validations
            if (getAllBranchesRequest.BankName == null) throw new ArgumentException($"Please Provide BankName to get Details of all the branches related to it.");
            getAllBranchesRequest.BankName = getAllBranchesRequest.BankName.Trim();
            if (getAllBranchesRequest.BankName.Length <= 5 || getAllBranchesRequest.BankName.Length >= 50 || getAllBranchesRequest.BankName.Any(char.IsSymbol) || getAllBranchesRequest.BankName.Any(char.IsDigit)) throw new ArgumentException($"BankName: {getAllBranchesRequest.BankName} Is Invalid.");
            if (getAllBranchesRequest.BankName.Contains(' ') && !getAllBranchesRequest.BankName.Split(' ').All(w => w.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' ')))throw new ArgumentException($"BankName: {getAllBranchesRequest.BankName} Is Invalid.");
            else if(!getAllBranchesRequest.BankName.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' '))throw new ArgumentException($"BankName: {getAllBranchesRequest.BankName} Is Invalid.");



            // Database Operations
            CrudBranch crudBranch = new CrudBranch();
            CrudBank crudBank = new CrudBank();
            var GetBank = crudBank.GetByName(getAllBranchesRequest.BankName); // Checking the bank
            if (GetBank == null) throw new ArgumentException($"There is No Bank exists with {getAllBranchesRequest.BankName} Name");
            List<BranchDetails> GetBranches = crudBranch.Get(GetBank.Code); //Getting all branch Details
            if (GetBranches == null) throw new ArgumentException($"There are No branches exists for the bank. Please Contact Admin Of Your Bank.");

            // Generating responses
            List<GetAllBranchesResponse> getAllBranchesResponses = new List<GetAllBranchesResponse>(); //For converting from branchDetails List to response List
            foreach (BranchDetails branchDetails in GetBranches)
            {
                getAllBranchesResponses.Add(new GetAllBranchesResponse(branchDetails));
            }
            return getAllBranchesResponses; // Returning the Response
        }
    }
}