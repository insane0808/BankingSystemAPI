using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace BankingSystem
{
    public class TestGetLast3MonthsRecords
    {
        private readonly GetLast3MonthsRecordsApi _getLast3MonthsRecords;
        public TestGetLast3MonthsRecords()
        {
            _getLast3MonthsRecords = new GetLast3MonthsRecordsApi();
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            Environment.SetEnvironmentVariable("ACCOUNTCOLLECTION","accounts");
            Environment.SetEnvironmentVariable("BRANCHCOLLECTION","branch");
            Environment.SetEnvironmentVariable("BANKCOLLECTION","bank");
            Environment.SetEnvironmentVariable("TRANSACTIONCOLLECTION", "transactions");
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
        public void GetLast3MonthsRecords_AccountNumberAsNull_ReturnsBadRequest(){

            ///Arrange
            string AccountNumber = null!;
            ///Act
            var Result = _getLast3MonthsRecords.GetLast3MonthsRecords(AccountNumber);
            var Actual = Result as ObjectResult;
            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400)
            {
                Assert.Equal(new{Error="Please Provide Your Account Number to Get Transaction Records"}, Actual.Value);
            }
            
        }

        [Fact]
        public void GetLast3MonthsRecords_IncorrectAccountNumber_ReturnsBadRequest(){

            ///Arrange
            string AccountNumber = "878374@#$%%g";
            ///Act
            var Result = _getLast3MonthsRecords.GetLast3MonthsRecords(AccountNumber);
            var Actual = Result as ObjectResult;
            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400)
            {
                Assert.Equal(new{Error="AccountNumber: "+AccountNumber+" is Invalid!"}, Actual.Value);
            }
        }
        [Fact]
        public void GetLast3MonthsRecords_UnKnownAccountNumber_ReturnsNotFound(){

            ///Arrange
            string AccountNumber = "23234343443";
            ///Act
            var Result = _getLast3MonthsRecords.GetLast3MonthsRecords(AccountNumber);
            var Actual = Result as ObjectResult;
            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 404)
            {
                Assert.Equal(new{Error="Account Number: "+AccountNumber+" Doesn't exists. Please Check."}, Actual.Value);
            }
        }
        [Fact]
        public void GetLast3MonthsRecords_CorrectAccountNumber_ReturnsOk(){

            ///Arrange
            string AccountNumber = "716447224623";
            ///Act
            var Result = _getLast3MonthsRecords.GetLast3MonthsRecords(AccountNumber);
            var Actual = Result as ObjectResult;
            ///Assert
            Assert.NotNull(Actual);
            Assert.Equal(200, Actual.StatusCode);
        }
    }
}