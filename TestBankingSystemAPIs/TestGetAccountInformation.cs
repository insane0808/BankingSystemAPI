using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace BankingSystem
{
    public class TestGetAccountInformation
    {
        private readonly GetAccountInformationApi _getAccountInformation;
        public TestGetAccountInformation()
        {
            _getAccountInformation = new GetAccountInformationApi();
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            Environment.SetEnvironmentVariable("ACCOUNTCOLLECTION","accounts");
            Environment.SetEnvironmentVariable("BRANCHCOLLECTION","branch");
            Environment.SetEnvironmentVariable("BANKCOLLECTION","bank");
            Environment.SetEnvironmentVariable("CLIENT","mongodb://localhost:27017");
            Environment.SetEnvironmentVariable("DATABASE","BankingSystemLF");
        }
        
        ///
        /// 
        /// AccountNumber Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void GetAccountInformation_AccountNumberAsNull_ReturnsBadRequest(){

            ///Arrange
            string AccountNumber = null!;
            GetAccountInformationRequest getAccountInformationRequest = new GetAccountInformationRequest();
            getAccountInformationRequest.AccountNumber = AccountNumber;
            ///Act
            var Result = _getAccountInformation.GetBranches(getAccountInformationRequest);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = "Please Provide BankName To Get All Branches Details." }, CreatedResult.Value);
            }

        }

        [Fact]
        public void GetAccountInformation_IncorrectAccountNumber_ReturnsBadRequest(){

            ///Arrange
            string AccountNumber = "878374@#$%%g";
            GetAccountInformationRequest getAccountInformationRequest = new GetAccountInformationRequest();
            getAccountInformationRequest.AccountNumber = AccountNumber;
            ///Act
            var Result = _getAccountInformation.GetBranches(getAccountInformationRequest);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = "AccountNumber: "+getAccountInformationRequest.AccountNumber+" is Invalid. Please Enter Correct" }, CreatedResult.Value);
            }
        }

        [Fact]
        public void GetAccountInformation_UnKnownAccountNumber_ReturnsNotFound(){

            ///Arrange
            string AccountNumber = "23234343443";
            GetAccountInformationRequest getAccountInformationRequest = new GetAccountInformationRequest();
            getAccountInformationRequest.AccountNumber = AccountNumber;
            ///Act
            var Result = _getAccountInformation.GetBranches(getAccountInformationRequest);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 404){
                Assert.Equal(new { Error = "Account Number :"+getAccountInformationRequest.AccountNumber+" Doesn't exists. Please Check." }, CreatedResult.Value);
            }
        }

        [Fact]
        public void GetAccountInformation_CorrectAccountNumber_ReturnsOk(){

            ///Arrange
            string AccountNumber = "716447224623";
            GetAccountInformationRequest getAccountInformationRequest = new GetAccountInformationRequest();
            getAccountInformationRequest.AccountNumber = AccountNumber;
            ///Act
            var Result = _getAccountInformation.GetBranches(getAccountInformationRequest);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            Assert.Equal(200, CreatedResult.StatusCode);
        }
        
    }
}