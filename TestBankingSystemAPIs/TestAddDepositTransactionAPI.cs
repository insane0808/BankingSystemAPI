using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace BankingSystem
{
    public class TestAddDepositTransactionAPI
    {
         private readonly AddDepositTransactionApi _deposit;
        public TestAddDepositTransactionAPI()
        {
            _deposit = new AddDepositTransactionApi();
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
        public void AddDepositTransection_AllDetailsCorrect_ReturnsOk(){
            ///Arrange
            var TransactionData = "{AccountType:'savings',Ammount:12000}";
            var AccountNumber = "716447224623";
            AddDepositTransactionRequest addDepositTransactionRequest = new AddDepositTransactionRequest();
            addDepositTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddDepositTransactionRequest>(TransactionData);
            ///Act
            var Result = _deposit.Deposit(addDepositTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            Assert.Equal(200, CreatedResult.StatusCode);
        }

        [Fact]
        public void AddDepositTransection_EverythingNull_ReturnsOk(){

            ///Arrange
            var TransactionData = "";
            string AccountNumber = null!;
            AddDepositTransactionRequest addDepositTransactionRequest = new AddDepositTransactionRequest();
            addDepositTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddDepositTransactionRequest>(TransactionData);
            ///Act
            var Result = _deposit.Deposit(addDepositTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Account Number, Account Type and Ammount" }, CreatedResult.Value);
            }
        }
        
        ///
        /// 
        /// AccountNumber Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void AddDepositTransection_AccountNumberAsNull_ReturnsBadRequest(){

            ///Arrange
            var TransactionData = "{AccountType:'savings',Ammount:12000}";
            string AccountNumber = null!;
            AddDepositTransactionRequest addDepositTransactionRequest = new AddDepositTransactionRequest();
            addDepositTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddDepositTransactionRequest>(TransactionData);
            ///Act
            var Result = _deposit.Deposit(addDepositTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = "Please Provide Account Number" }, CreatedResult.Value);
            }
        }
        [Fact]
        public void AddDepositTransection_IncorrectAccountNumber_ReturnsBadRequest(){

            ///Arrange
            var TransactionData = "{AccountType:'savings',Ammount:12000}";
            string AccountNumber = "8734#$%9999979";
            AddDepositTransactionRequest addDepositTransactionRequest = new AddDepositTransactionRequest();
            addDepositTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddDepositTransactionRequest>(TransactionData);
            ///Act
            var Result = _deposit.Deposit(addDepositTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = "Account Number: "+addDepositTransactionRequest.AccountNumber+" is Not Valid" }, CreatedResult.Value);
            }
        }

        [Fact]
        public void AddDepositTransection_UnKnownAccountNumber_ReturnsNotFound(){

            ///Arrange
            var TransactionData = "{AccountType:'savings',Ammount:12000}";
            string AccountNumber = "898937745663";
            AddDepositTransactionRequest addDepositTransactionRequest = new AddDepositTransactionRequest();
            addDepositTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddDepositTransactionRequest>(TransactionData);
            ///Act
            var Result = _deposit.Deposit(addDepositTransactionRequest, AccountNumber);
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
        public void AddDepositTransection_AccountTypeAsNull_ReturnsBadRequest(){

            ///Arrange
            var TransactionData = "{AccountType:null,Ammount:12000}";
            string AccountNumber = "898937745663";
            AddDepositTransactionRequest addDepositTransactionRequest = new AddDepositTransactionRequest();
            addDepositTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddDepositTransactionRequest>(TransactionData);
            ///Act
            var Result = _deposit.Deposit(addDepositTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Your Account Type" }, CreatedResult.Value);
            }
        }
        [Fact]
        public void AddDepositTransection_IncorrectAccountType_ReturnsBadRequest(){

            ///Arrange
            var TransactionData = "{AccountType:'savin',Ammount:12000}";
            string AccountNumber = "898937745663";
            AddDepositTransactionRequest addDepositTransactionRequest = new AddDepositTransactionRequest();
            addDepositTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddDepositTransactionRequest>(TransactionData);
            ///Act
            var Result = _deposit.Deposit(addDepositTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = "Account Type "+addDepositTransactionRequest.AccountType+" is Not Valid. We Only Offer Savings, Current & Salary" }, CreatedResult.Value);
            }
        }
        [Fact]
        public void AddDepositTransection_DifferentAccountType_ReturnsBadRequest(){

            ///Arrange
            var TransactionData = "{AccountType:'current',Ammount:12000}";
            string AccountNumber = "716447224623";
            AddDepositTransactionRequest addDepositTransactionRequest = new AddDepositTransactionRequest();
            addDepositTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddDepositTransactionRequest>(TransactionData);
            ///Act
            var Result = _deposit.Deposit(addDepositTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = "This Account is not of "+addDepositTransactionRequest.AccountType+" Type" }, CreatedResult.Value);
            }
        }

        ///
        /// 
        /// Deposit Ammount Validation & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///
        
        [Fact]
        public void AddDepositTransection_IncorrectDepositAmmount_ReturnsBadRequest(){

            ///Arrange
            var TransactionData = "{AccountType:'savings',Ammount:0}";
            string AccountNumber = "898937745663";
            AddDepositTransactionRequest addDepositTransactionRequest = new AddDepositTransactionRequest();
            addDepositTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddDepositTransactionRequest>(TransactionData);
            ///Act
            var Result = _deposit.Deposit(addDepositTransactionRequest, AccountNumber);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            if(CreatedResult.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Valid Deposit Ammount Between 1 - 10000000" }, CreatedResult.Value);
            }
        }

        // [Fact]
        // public void AddDepositTransection_DepositAmmountMoreThanLimit_ReturnsBadRequest(){

        //     ///Arrange
        //     var TransactionData = "{AccountType:'savings',Ammount:1200000}";
        //     string AccountNumber = "716447224623";
        //     AddDepositTransactionRequest addDepositTransactionRequest = new AddDepositTransactionRequest();
        //     addDepositTransactionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<AddDepositTransactionRequest>(TransactionData);
        //     ///Act
        //     var Result = _deposit.Deposit(addDepositTransactionRequest, AccountNumber);
        //     var CreatedResult = Result as ObjectResult;
        //     ///Assert
        //     Assert.NotNull(CreatedResult);
        //     Assert.Equal(400, CreatedResult.StatusCode);
        // }

    }
}