using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace BankingSystem
{
    public class TestGetAllTransactionsByDate
    {
        private readonly GetAllTransactionsByDateApi _getAllTransactionsByDate;
        public TestGetAllTransactionsByDate()
        {
            _getAllTransactionsByDate = new GetAllTransactionsByDateApi();
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            Environment.SetEnvironmentVariable("ACCOUNTCOLLECTION","accounts");
            Environment.SetEnvironmentVariable("TRANSACTIONCOLLECTION", "transactions");
            Environment.SetEnvironmentVariable("BRANCHCOLLECTION","branch");
            Environment.SetEnvironmentVariable("BANKCOLLECTION","bank");
            Environment.SetEnvironmentVariable("CLIENT","mongodb://localhost:27017");
            Environment.SetEnvironmentVariable("DATABASE","BankingSystemLF");
        }
        ///
        ///
        /// CommonCase Validation & Verification testcases
        ///
        ///

        [Fact]
        public void GetAllTransactoinsByDate_CorrectDetails_ReturnsOk(){

            ///Arrange
            string From = "2020-10-04";
            string To = "2022-10-04";
            string AccountNumber = "716447224623";
            GetAllTransactionsByDateRequest getAllTransactionsByDateRequest = new GetAllTransactionsByDateRequest();
            getAllTransactionsByDateRequest.From = From;
            getAllTransactionsByDateRequest.To = To;
            ///Act
            var Result = _getAllTransactionsByDate.GetAllTransactionsByDate(getAllTransactionsByDateRequest, AccountNumber);
            var GetResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(GetResult);
            Assert.Equal(200, GetResult.StatusCode);
        }
        [Fact]
        public void GetAllTransactoinsByDate_EverythingAsNull_ReturnsBadRequest(){

            
            ///Arrange
            string From = null!;
            string To = null!;
            string AccountNumber = null!;
            GetAllTransactionsByDateRequest getAllTransactionsByDateRequest = new GetAllTransactionsByDateRequest();
            getAllTransactionsByDateRequest.From = From;
            getAllTransactionsByDateRequest.To = To;
            ///Act
            var Result = _getAllTransactionsByDate.GetAllTransactionsByDate(getAllTransactionsByDateRequest, AccountNumber);
            var GetResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(GetResult);
            if(GetResult.StatusCode == 400)
            {
                Assert.Equal(new {Error = "Please Provide Your Account Number to get the Transactions"}, GetResult.Value);
            }

        }
        ///
        /// 
        /// AccountNumber Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void GetAllTransactoinsByDate_AccountNumberAsNull_ReturnsBadRequest(){

            
            ///Arrange
            string From = "2022-01-20";
            string To = "2022-01-20";
            string AccountNumber = null!;
            GetAllTransactionsByDateRequest getAllTransactionsByDateRequest = new GetAllTransactionsByDateRequest();
            getAllTransactionsByDateRequest.From = From;
            getAllTransactionsByDateRequest.To = To;
            ///Act
            var Result = _getAllTransactionsByDate.GetAllTransactionsByDate(getAllTransactionsByDateRequest, AccountNumber);
            var GetResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(GetResult);
            if(GetResult.StatusCode == 400)
            {
                Assert.Equal(new {Error = "Please Provide Your Account Number to get the Transactions"}, GetResult.Value);
            }
            

        }

        [Fact]
        public void GetAllTransactoinsByDate_IncorrectAccountNumber_ReturnsBadRequest(){

            ///Arrange
            string AccountNumber = "878374@#$%%g";
            GetAllTransactionsByDateRequest getAllTransactionsByDateRequest = new GetAllTransactionsByDateRequest();
            getAllTransactionsByDateRequest.From = "2022-01-20";
            getAllTransactionsByDateRequest.To = "2022-01-20";
            
            ///Act
            var Result = _getAllTransactionsByDate.GetAllTransactionsByDate(getAllTransactionsByDateRequest, AccountNumber);
            var GetResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(GetResult);
            if(GetResult.StatusCode == 400)
            {
                Assert.Equal(new {Error = "AccountNumber "+AccountNumber+" is Invalid!"}, GetResult.Value);
            }
        }

        [Fact]
        public void GetAllTransactoinsByDate_UnKnownAccountNumber_ReturnsNotFound(){

            ///Arrange
            /// Please Edit From Date and To date to pass This Case
            string From = "2020-10-04";
            string To = "2022-10-04";
            string AccountNumber = "23234343443";
            GetAllTransactionsByDateRequest getAllTransactionsByDateRequest = new GetAllTransactionsByDateRequest();
            getAllTransactionsByDateRequest.From = From;
            getAllTransactionsByDateRequest.To = To;
            ///Act
            var Result = _getAllTransactionsByDate.GetAllTransactionsByDate(getAllTransactionsByDateRequest, AccountNumber);
            var GetResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(GetResult);
            if(GetResult.StatusCode == 404)
            {
                Assert.Equal(new {Error = "Account Number: "+AccountNumber+" Doesn't exists. Please Check."}, GetResult.Value);
            }
        }
        ///
        /// 
        /// AccountNumber Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void GetAllTransactoinsByDate_FromDateAsNull_ReturnsBadRequest(){

            
            ///Arrange
            string From = null!;
            string To = "1-01-1000";
            string AccountNumber = "111111111";
            GetAllTransactionsByDateRequest getAllTransactionsByDateRequest = new GetAllTransactionsByDateRequest();
            getAllTransactionsByDateRequest.From = From;
            getAllTransactionsByDateRequest.To = To;
            ///Act
            var Result = _getAllTransactionsByDate.GetAllTransactionsByDate(getAllTransactionsByDateRequest, AccountNumber);
            var GetResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(GetResult);
            if(GetResult.StatusCode == 400)
            {
                Assert.Equal(new {Error = "Please Enter Date From"}, GetResult.Value);
            }

        }

        [Fact]
        public void GetAllTransactoinsByDate_IncorrectFromDate_ReturnsBadRequest(){

            ///Arrange
            string From = "27-98989";
            string To = "1-01-1000";
            string AccountNumber = "111111111";
            GetAllTransactionsByDateRequest getAllTransactionsByDateRequest = new GetAllTransactionsByDateRequest();
            getAllTransactionsByDateRequest.From = From;
            getAllTransactionsByDateRequest.To = To;
            
            ///Act
            var Result = _getAllTransactionsByDate.GetAllTransactionsByDate(getAllTransactionsByDateRequest, AccountNumber);
            var GetResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(GetResult);
            if(GetResult.StatusCode == 400)
            {
                Assert.Equal(new {Error = "String '"+getAllTransactionsByDateRequest.From+"' was not recognized as a valid DateTime."}, GetResult.Value);
            }
        }

        [Fact]
        public void GetAllTransactoinsByDate_UnRegisteredFromDate_ReturnsNotFound(){

            ///Arrange
            /// Please Edit From Date and To date to pass This Case
            string From = "1-01-1000";
            string To = "10-01-2022";
            string AccountNumber = "716447224623";
            GetAllTransactionsByDateRequest getAllTransactionsByDateRequest = new GetAllTransactionsByDateRequest();
            getAllTransactionsByDateRequest.From = From;
            getAllTransactionsByDateRequest.To = To;
            ///Act
            var Result = _getAllTransactionsByDate.GetAllTransactionsByDate(getAllTransactionsByDateRequest, AccountNumber);
            var GetResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(GetResult);
            if(GetResult.StatusCode == 400)
            {
                Assert.Equal(new {Error = "Please Enter a Valid 'From' Date"}, GetResult.Value);
            }
        }
        ///
        /// 
        /// AccountNumber Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void GetAllTransactoinsByDate_ToDateAsNull_ReturnsBadRequest(){

            
            ///Arrange
            string From = "1-01-1000";
            string To = null!;
            string AccountNumber = "111111111";
            GetAllTransactionsByDateRequest getAllTransactionsByDateRequest = new GetAllTransactionsByDateRequest();
            getAllTransactionsByDateRequest.From = From;
            getAllTransactionsByDateRequest.To = To;
            ///Act
            var Result = _getAllTransactionsByDate.GetAllTransactionsByDate(getAllTransactionsByDateRequest, AccountNumber);
            var GetResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(GetResult);
            if(GetResult.StatusCode == 400)
            {
                Assert.Equal(new {Error = "Please Enter Date To"}, GetResult.Value);
            }

        }

        [Fact]
        public void GetAllTransactoinsByDate_IncorrectToDate_ReturnsBadRequest(){

            ///Arrange
            string From = "2022-01-10";
            string To = "1-011000";
            string AccountNumber = "111111111";
            GetAllTransactionsByDateRequest getAllTransactionsByDateRequest = new GetAllTransactionsByDateRequest();
            getAllTransactionsByDateRequest.From = From;
            getAllTransactionsByDateRequest.To = To;
            
            ///Act
            var Result = _getAllTransactionsByDate.GetAllTransactionsByDate(getAllTransactionsByDateRequest, AccountNumber);
            var GetResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(GetResult);
            if(GetResult.StatusCode == 400)
            {
                Assert.Equal(new {Error = "String '"+getAllTransactionsByDateRequest.To+"' was not recognized as a valid DateTime."}, GetResult.Value);
            }
        }

        [Fact]
        public void GetAllTransactoinsByDate_UnKnownToDate_ReturnsNotFound(){

            ///Arrange
            string From = "12-01-2022";
            string To = "1-01-2023";
            string AccountNumber = "716447224623";
            GetAllTransactionsByDateRequest getAllTransactionsByDateRequest = new GetAllTransactionsByDateRequest();
            getAllTransactionsByDateRequest.From = From;
            getAllTransactionsByDateRequest.To = To;
            ///Act
            var Result = _getAllTransactionsByDate.GetAllTransactionsByDate(getAllTransactionsByDateRequest, AccountNumber);
            var GetResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(GetResult);
            if(GetResult.StatusCode == 400)
            {
                Assert.Equal(new {Error = "Please Enter a Valid 'To' Date "}, GetResult.Value);
            }
        }
        [Fact]
        public void GetAllTransactoinsByDate_FromDateGreaterThanTo_ReturnsBadRequest(){

            ///Arrange
            string From = "2022-10-04";
            string To = "2020-10-04";
            string AccountNumber = "716447224623";
            GetAllTransactionsByDateRequest getAllTransactionsByDateRequest = new GetAllTransactionsByDateRequest();
            getAllTransactionsByDateRequest.From = From;
            getAllTransactionsByDateRequest.To = To;
            ///Act
            var Result = _getAllTransactionsByDate.GetAllTransactionsByDate(getAllTransactionsByDateRequest, AccountNumber);
            var GetResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(GetResult);
            if(GetResult.StatusCode == 400)
            {
                Assert.Equal(new {Error = "Please Enter 'From' Date less than 'To Date"}, GetResult.Value);
            }
        }
    }
}