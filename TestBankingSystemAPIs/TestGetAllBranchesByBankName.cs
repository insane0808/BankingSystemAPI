using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace BankingSystem
{
    public class TestGetAllBranchesByBankName
    {
        private readonly GetAllBranchesByBankNameApi _getAllBranchesByBankName;
        public TestGetAllBranchesByBankName()
        {
            _getAllBranchesByBankName = new GetAllBranchesByBankNameApi();
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            Environment.SetEnvironmentVariable("ACCOUNTCOLLECTION","accounts");
            Environment.SetEnvironmentVariable("BRANCHCOLLECTION","branch");
            Environment.SetEnvironmentVariable("BANKCOLLECTION","bank");
            Environment.SetEnvironmentVariable("CLIENT","mongodb://localhost:27017");
            Environment.SetEnvironmentVariable("DATABASE","BankingSystemLF");
        }

        ///
        /// 
        /// BankName Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///  

        [Fact]
        public void GetAllBranchesByBankName_BankNameAsNull_ReturnsBadRequest(){
            ///Arrange
            string BankName = null!;
            GetAllBranchesRequest getAllBranchesRequest = new GetAllBranchesRequest();
            getAllBranchesRequest.BankName = BankName;
            ///Act
            var Result= _getAllBranchesByBankName.GetBranches(getAllBranchesRequest);
            var Actual = Result as ObjectResult;

            ///Assert 
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Provide BankName to get Details of all the branches related to it." }, Actual.Value);
            }
        }
        [Fact]
        public void GetAllBranchesByBankName_InCorrectBankName_ReturnsBadRequest(){
            ///Arrange
            string BankName = "vgvhgv212";
            GetAllBranchesRequest getAllBranchesRequest = new GetAllBranchesRequest();
            getAllBranchesRequest.BankName = BankName;
            ///Act 
            var Result= _getAllBranchesByBankName.GetBranches(getAllBranchesRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "BankName: "+getAllBranchesRequest.BankName+" Is Invalid." }, Actual.Value);
            }
        }

        [Fact]
        public void GetAllBranchesByBankName_UnknownBankName_ReturnsBadRequest(){
            ///Arrange
            var BankName = "My Bank";
            GetAllBranchesRequest getAllBranchesRequest = new GetAllBranchesRequest();
            getAllBranchesRequest.BankName = BankName;
            ///Act 
            var Result= _getAllBranchesByBankName.GetBranches(getAllBranchesRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 404){
                Assert.Equal(new { Error = "There is No Bank exists with "+getAllBranchesRequest.BankName+" Name" }, Actual.Value);
            }
        }

        [Fact]
        public void GetAllBranchesByBankName_CorrectBankName_ReturnsBadRequest(){
            ///Arrange
            string BankName = "Nitin mo Bank";
            GetAllBranchesRequest getAllBranchesRequest = new GetAllBranchesRequest();
            getAllBranchesRequest.BankName = BankName;
            ///Act 
            var Result= _getAllBranchesByBankName.GetBranches(getAllBranchesRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            Assert.Equal(200, Actual.StatusCode);
        }
    }
}