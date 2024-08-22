using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BankingSystem
{
    public class TestAddWithdrawlTransactionAPI
    {
        private readonly AddWithdrawlTransactionApi _withdrawl;
        public TestAddWithdrawlTransactionAPI()
        {
            _withdrawl = new AddWithdrawlTransactionApi();
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            Environment.SetEnvironmentVariable("ACCOUNTCOLLECTION","accounts");
            Environment.SetEnvironmentVariable("BRANCHCOLLECTION","branch");
            Environment.SetEnvironmentVariable("BANKCOLLECTION","bank");
            Environment.SetEnvironmentVariable("TRANSACTIONCOLLECTION","transactions");
            Environment.SetEnvironmentVariable("CLIENT","mongodb://localhost:27017");
            Environment.SetEnvironmentVariable("DATABASE","BankingSystemLF");
        }

        ///
        /// 
        /// CommonCase Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        /// 

        [Fact]
        public void AddWithdrawlTransection_AllDetailsCorrect_ReturnsOk(){
            ///Arrange
            var TransactionData = "{AccountType:'savings',Ammount:12000}";
            var AccountNumber = "716447224623";
            AddWithdrawlTransactionRequest addWithdrawlTransactionRequest = new AddWithdrawlTransactionRequest();
            addWithdrawlTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddWithdrawlTransactionRequest>(TransactionData);
            ///Act
            var Result = _withdrawl.Withdrawl(addWithdrawlTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            Assert.Equal(200, CreatedResult.StatusCode);
        }

        [Fact]
        public void AddWithdrawlTransection_EverythingNull_ReturnsOk(){

            ///Arrange
            var TransactionData = "";
            string AccountNumber = null!;
            AddWithdrawlTransactionRequest addWithdrawlTransactionRequest = new AddWithdrawlTransactionRequest();
            addWithdrawlTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddWithdrawlTransactionRequest>(TransactionData);
            ///Act
            var Result = _withdrawl.Withdrawl(addWithdrawlTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400)
            {
                Assert.Equal(new{Error="Please Enter Account Number, Account Type and Ammount"}, CreatedResult.Value);
            }
        }

        ///
        /// 
        /// AccountNumber Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        /// 

        [Fact]
        public void AddWithdrawlTransection_AccountNumberAsNull_ReturnsBadRequest(){

            ///Arrange
            var TransactionData = "{AccountType:'savings',Ammount:12000}";
            string AccountNumber = null!;
            AddWithdrawlTransactionRequest addWithdrawlTransactionRequest = new AddWithdrawlTransactionRequest();
            addWithdrawlTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddWithdrawlTransactionRequest>(TransactionData);
            ///Act
            var Result = _withdrawl.Withdrawl(addWithdrawlTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = "Please Provide Account Number." }, CreatedResult.Value);
            }
        }
        [Fact]
        public void AddWithdrawlTransection_IncorrectAccountNumber_ReturnsBadRequest(){

            ///Arrange
            var TransactionData = "{AccountType:'savings',Ammount:12000}";
            string AccountNumber = "8734#$%9999979";
            AddWithdrawlTransactionRequest addWithdrawlTransactionRequest = new AddWithdrawlTransactionRequest();
            addWithdrawlTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddWithdrawlTransactionRequest>(TransactionData);
            ///Act
            var Result = _withdrawl.Withdrawl(addWithdrawlTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = "Account Number: "+addWithdrawlTransactionRequest.AccountNumber+" is Not Valid" }, CreatedResult.Value);
            }
        }

        [Fact]
        public void AddWithdrawlTransection_UnKnownAccountNumber_ReturnsNotFound(){

            ///Arrange
            var TransactionData = "{AccountType:'savings',Ammount:12000}";
            string AccountNumber = "898937745663";
            AddWithdrawlTransactionRequest addWithdrawlTransactionRequest = new AddWithdrawlTransactionRequest();
            addWithdrawlTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddWithdrawlTransactionRequest>(TransactionData);
            ///Act
            var Result = _withdrawl.Withdrawl(addWithdrawlTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 404){
                Assert.Equal(new { Error = "There is No User exists With This Account Number." }, CreatedResult.Value);
            }
        }

        ///
        /// 
        /// AccountType Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        /// 

        [Fact]
        public void AddWithdrawlTransection_AccountTypeAsNull_ReturnsBadRequest(){

            ///Arrange
            var TransactionData = "{AccountType:null,Ammount:12000}";
            string AccountNumber = "898937745663";
            AddWithdrawlTransactionRequest addWithdrawlTransactionRequest = new AddWithdrawlTransactionRequest();
            addWithdrawlTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddWithdrawlTransactionRequest>(TransactionData);
            ///Act
            var Result = _withdrawl.Withdrawl(addWithdrawlTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Your Account Type" }, CreatedResult.Value);
            }
        }
        [Fact]
        public void AddWithdrawlTransection_IncorrectAccountType_ReturnsBadRequest(){

            ///Arrange
            var TransactionData = "{AccountType:'savin',Ammount:12000}";
            string AccountNumber = "898937745663";
            AddWithdrawlTransactionRequest addWithdrawlTransactionRequest = new AddWithdrawlTransactionRequest();
            addWithdrawlTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddWithdrawlTransactionRequest>(TransactionData);
            ///Act
            var Result = _withdrawl.Withdrawl(addWithdrawlTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = " Account Type "+addWithdrawlTransactionRequest.AccountType+" is Not Valid. We Only Offer Savings, Current & Salary" }, CreatedResult.Value);
            }
        }
        [Fact]
        public void AddWithdrawlTransection_DifferentAccountType_ReturnsBadRequest(){

            ///Arrange
            var TransactionData = "{AccountType:'current',Ammount:12000}";
            string AccountNumber = "716447224623";
            AddWithdrawlTransactionRequest addWithdrawlTransactionRequest = new AddWithdrawlTransactionRequest();
            addWithdrawlTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddWithdrawlTransactionRequest>(TransactionData);
            ///Act
            var Result = _withdrawl.Withdrawl(addWithdrawlTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = "This Account is not "+addWithdrawlTransactionRequest.AccountType+" Type" }, CreatedResult.Value);
            }
        }

        ///
        /// 
        /// Withdrawl Ammount Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        /// 

        [Fact]
        public void AddWithdrawlTransection_IncorrectWithdrawlAmmount_ReturnsBadRequest(){

            ///Arrange
            var TransactionData = "{AccountType:'savings',Ammount:0}";
            string AccountNumber = "898937745663";
            AddWithdrawlTransactionRequest addWithdrawlTransactionRequest = new AddWithdrawlTransactionRequest();
            addWithdrawlTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddWithdrawlTransactionRequest>(TransactionData);
            ///Act
            var Result = _withdrawl.Withdrawl(addWithdrawlTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Valid Withdrawl Ammount Between 1 - 10000000" }, CreatedResult.Value);
            }
        }

        [Fact]
        public void AddWithdrawlTransection_WithdrawlAmmountMoreThanBalance_ReturnsBadRequest(){

            ///Arrange
            var TransactionData = "{AccountType:'savings',Ammount:1200000000}";
            string AccountNumber = "716447224623";
            AddWithdrawlTransactionRequest addWithdrawlTransactionRequest = new AddWithdrawlTransactionRequest();
            addWithdrawlTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddWithdrawlTransactionRequest>(TransactionData);
            ///Act
            var Result = _withdrawl.Withdrawl(addWithdrawlTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            Assert.Equal(400, CreatedResult.StatusCode);
        }

    }
}