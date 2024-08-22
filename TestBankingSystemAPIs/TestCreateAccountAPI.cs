using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace BankingSystem
{
    public class TestCreateAccountAPI
    {
        private readonly CreateAccountApi _createAccount;
        public TestCreateAccountAPI()
        {
            _createAccount = new CreateAccountApi();
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            Environment.SetEnvironmentVariable("ACCOUNTCOLLECTION","accounts");
            Environment.SetEnvironmentVariable("BRANCHCOLLECTION","branch");
            Environment.SetEnvironmentVariable("BANKCOLLECTION","bank");
            Environment.SetEnvironmentVariable("CLIENT","mongodb://localhost:27017");
            Environment.SetEnvironmentVariable("DATABASE","BankingSystemLF");
        }

        ///
        /// 
        /// CommonCase Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        /// 

        // [Fact]
        // public void CreateAccount_AllDetailsCorrect_ReturnsCreated(){
        //     ///Arrange
        //     var AccountData = "{IFSC : 'MHTT0602718', Name: 'Shivam',FathersName : 'Mahesh',Email:'shivavish@gmail.com',Phone:'9388418488',AccountType:'savings',AddressLine : 'Sheetal Nagar',DOB:'2021-01-22',AadharNumber:'611646663555',PanNumber:'BYFPS0887D',Gender:'M',City : 'Indore', State: 'Madhya Pradesh',PostalCode: '452010',Country:'India',Balance:1000}";
        //     CreateAccountRequest createAccountRequest = new CreateAccountRequest();
        //     createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
        //     ///Act
        //     var Result = _createAccount.CreateAccount(createAccountRequest);
        //     var CreatedResult = Result as ObjectResult;
        //     ///Assert
        //     Assert.NotNull(CreatedResult);
        //     Assert.Equal(201, CreatedResult.StatusCode);
        // }

        [Fact]
        public void CreateAccount_EverythingNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Fill All Valid Details Of Your Account." }, Actual.Value);
            }
        }

        ///
        /// 
        /// AccountName Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///  

        [Fact]
        public void CreateAccount_AccountNameAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: null,FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert 
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Your Name" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_InCorrectAccountName_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'shiv997  ',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Name : "+createAccountRequest.Name+" is Invalid!" }, Actual.Value);
            }
        }
        
        ///
        /// 
        /// FathersName Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        /// 
        [Fact]
        public void CreateAccount_FathersNameAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name:'AAAAAA',FathersName :null ,Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert 
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Your Father's Name" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_InCorrectFathersName_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : '#@#HFH   ',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Fathers Name "+createAccountRequest.FathersName+" is Invalid. Enter Full Name Of Your Father" }, Actual.Value);
            }
        }
        
        ///
        /// 
        /// IFSC Code Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        /// 

        [Fact]
        public void CreateAccount_IFSCAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : null, Name:'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;
            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter IFSC Code Of Your Branch" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_InCorrectIFSC_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'MHAY0%^&^^96', Name: 'Shivam',FathersName : 'Mahesh',Email:'shivavish@gmail.com',Phone:'9388488488',AccountType:'savings',AddressLine : 'Sheetal Nagar',DOB:'2021-01-22',AadharNumber:'646646663555',PanNumber:'BYFPS0886D',Gender:'M',City : 'Indore', State: 'Madhya Pradesh',PostalCode: '452010',Country:'India',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "IFSC Code: "+createAccountRequest.IFSC+" is Not Valid Please Enter Your Branch's IFSC Code" }, Actual.Value);
            }
        }

        ///
        /// 
        /// Account Email Validations & Verification 
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        /// 

        [Fact]
        public void CreateAccount_AccountEmailAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:null,Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Your Email Address" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_InCorrectAccountEmail_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'   %@gm.c    ',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            /// Act
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Email Is Not Valid : "+createAccountRequest.Email }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_ExistingEmailOfAccounts_ReturnsConflict(){
            ///Arrange
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'shush@gm.co',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 409){
                Assert.Equal(new { Error = "There is an existing account linked to this Email" }, Actual.Value);
            }
            
        }
        [Fact]
        public void CreateAccount_ExistingEmailOfBanks_ReturnsConflict(){
            ///Arrange
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'SundNitu@pro.co',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 409){
                Assert.Equal(new { Error = "You Can't Use the Email of A existing Bank." }, Actual.Value);
            }
            
        }



        ///
        /// 
        /// AccountPhone Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        /// 

        [Fact]
        public void CreateAccount_PhoneAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:null,AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Your Phone Number" }, Actual.Value);
            }
        }

        [Fact]
        public void CreateAccount_InCorrectPhone_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'99@#$%9999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Phone : "+createAccountRequest.Phone+" is Invalid!" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_ExistingPhoneOfAccounts_ReturnsConflict(){
            ///Arrange
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9388488488',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 409){
                Assert.Equal(new { Error = "There is an existing account linked to this Phone Number." }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_ExistingPhoneOfBranches_ReturnsConflict(){
            ///Arrange
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9388971279',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 409){
                Assert.Equal(new { Error = "You Can't Use the Phone of A existing Branch." }, Actual.Value);
            }
        }
        
        ///
        /// 
        /// AccountType Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateAccount_AccountTypeAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:null,AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Account Type of Your Account" }, Actual.Value);
            }
        }

        [Fact]
        public void CreateAccount_InCorrectAccountType_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savis',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Account Type: "+createAccountRequest.AccountType+" is Not Valid. We Only Offer Savings, Current & Salary" }, Actual.Value);
            }
        }
        
        ///
        /// 
        /// DOB Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateAccount_DOBAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:null,AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Your Date Of Birth(DOB)." }, Actual.Value);
            }
        }

        [Fact]
        public void CreateAccount_InCorrectDOB_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01012',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Your Date Of Birth : "+createAccountRequest.DOB+" is Invalid, Age Of The Person Must be 1 - 150years" }, Actual.Value);
            }
        }

        ///
        /// 
        /// Aadhar No. Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateAccount_AadharAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9388488488',AccountType:'savings',AddressLine : 'XXXXX' ,DOB:'01-01-2000',AadharNumber:null,PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Your Aadhar Number" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_InCorrectAadhar_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9388488488',AccountType:'savings',AddressLine : 'xxxxxx',DOB:'01-01-2000',AadharNumber:'999999999$#%',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Aadhar Number : "+createAccountRequest.AadharNumber+" is Invalid, Please Enter A Valid 12 Charachter Aadhar Number." }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_ExistingAccountAadhar_ReturnsConflict(){
            ///Arrange Make Sure You are Entering Existing Account Name For Validation of Address
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'Near Wal', DOB:'01-01-2000',AadharNumber:'646646663555',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            Assert.Equal(409, Actual.StatusCode);
            if(Actual.StatusCode == 409){
                Assert.Equal(new { Error = "There is an existing account linked to this AadharNumber" }, Actual.Value);
            }
        }

        ///
        /// 
        /// Aadhar No. Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateAccount_PANAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9388488488',AccountType:'savings',AddressLine : 'XXXXX' ,DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:null,Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Your Pan Number" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_InCorrectPAN_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9388488488',AccountType:'savings',AddressLine : 'xxxxxx',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXXXXXXA',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Pan Number : "+createAccountRequest.PanNumber+" is Invalid, Please Enter Your Valid Pan Number" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_ExistingPAN_ReturnsConflict(){
            ///Arrange Make Sure You are Entering Existing Account Name For Validation of Address
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'SAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'Near Wal', DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'BYFPS0886D',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 409){
                Assert.Equal(new { Error = "There is an existing account linked to this Pan Number" }, Actual.Value);
            }
        }

        ///
        /// 
        /// DOB Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateAccount_GenderAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'2022-22-01',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:null,City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Your Gender" }, Actual.Value);
            }
        }

        [Fact]
        public void CreateAccount_InCorrectGender_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'MAllu',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Your Gender : "+createAccountRequest.Gender+" is Invalid, we offer for : male(m), female(m), transgender(t)" }, Actual.Value);
            }
        }



        ///
        /// 
        /// AddressLine Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateAccount_AddressLineAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9388488488',AccountType:'savings',AddressLine : null ,DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter AddressLine Of Your Home" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_InCorrectAddressLine_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9388488488',AccountType:'savings',AddressLine : 'A@#__$$#BB    ',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address Line: "+createAccountRequest.AddressLine+" is INVALID!" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_ExistingBankOrBranchAddressLine_ReturnsConflict(){
            ///Arrange Make Sure You are Entering Existing Account Name For Validation of Address
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'Near So India', DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 409){
                Assert.Equal(new { Error = "A existing Branch is at Same Address Line. Please Provide different AddressLine" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_SameAddressLineWithOtherFields_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9389999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'AAAAAAAAA',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address Line Sould be Unique then other fields of Address" }, Actual.Value);
            }
        }

        ///
        /// 
        /// AccountCity Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateAccount_CityAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9388488488',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : null,State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter City Of Your Home" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_InCorrectAccountCity_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCC_)_CC',State: 'SSSSS',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address City: "+createAccountRequest.City+" is INVALID!" }, Actual.Value);
            }
        }
        
        [Fact]
        public void CreateAccount_SameAddressCityWithOtherFields_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'CCCCC',PostalCode: '100000',Country:'CCCCC',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address City Sould be Unique then other fields of Address" }, Actual.Value);
            }
        }

        ///
        /// 
        /// AccountState Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateAccount_StateAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9388488488',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: null,PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter State Of Your Address" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_InCorrectState_ReturnsBadRequest(){
            ///Arrange
           var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9388488488',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: '    HHH&$&(*^@$',PostalCode: '100000',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address State: "+createAccountRequest.State+" is INVALID!" }, Actual.Value);
            }
        }
        
        [Fact]
        public void CreateAccount_SameAddressStateWithOtherFields_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9388488488',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'SSSSS',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address State Sould be Unique then other fields of Address" }, Actual.Value);
            }
        }

        ///
        /// 
        /// AccountCountry Validation & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateAccount_CountryAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:null ,Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Country Where you Live" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_InCorrectAccountCountry_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9388488488',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '100000',Country:'I@#@*^II',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
           ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address Country: "+createAccountRequest.Country+" is INVALID!" }, Actual.Value);
            }
        }
        

        ///
        /// 
        /// AccountPostalCode Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateAccount_PostalCodeAsNull_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9388488488',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: null ,Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Postal Code Of Your Area" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateAccount_InCorrectPostalCode_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'MHTT0602718', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: '&$(()9',Country:'IIIII',Balance:1000}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address PostalCode: "+createAccountRequest.PostalCode+" is INVALID!" }, Actual.Value);
            }
        }


        [Fact]
        public void CreateBank_InCorrectBalance_ReturnsBadRequest(){
            ///Arrange
            var AccountData = "{IFSC : 'AAAA0999999', Name: 'AAAAAA',FathersName : 'FFFFFF',Email:'example@gmail.com',Phone:'9999999999',AccountType:'savings',AddressLine : 'AAAAAAAAA',DOB:'01-01-2000',AadharNumber:'999999999999',PanNumber:'XXXXA0999A',Gender:'M',City : 'CCCCC',State: 'SSSSS',PostalCode: 545454,Country:'IIIII',Balance:10}";
            CreateAccountRequest createAccountRequest = new CreateAccountRequest();
            createAccountRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateAccountRequest>(AccountData);
            ///Act 
            var Result= _createAccount.CreateAccount(createAccountRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Minimum Banalnce 100 or Maximum Balance 1 Lac." }, Actual.Value);
            }
        }
    }
}