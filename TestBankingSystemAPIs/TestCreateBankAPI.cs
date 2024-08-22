using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace BankingSystem
{
    public class TestCreateBankAPI
    {
        private readonly CreateBankApi _createBank;
        
        public TestCreateBankAPI(){
            _createBank = new CreateBankApi();
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            Environment.SetEnvironmentVariable("ACCOUNTCOLLECTION","accounts");
            Environment.SetEnvironmentVariable("TRANSACTIONCOLLECTION","transactions");
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

        [Fact]
        public void CreateBank_YAllDetailsCorrect_ReturnsCreated(){
            ///Arrange
            var BankData = "{Name:'New Test Bank',Phone:'9999987999',AddressLine : 'Vijay Nagar',City : 'Indore',IFSCHead:'NTBI',State: 'Madhya Pradesh',PostalCode: '452010',Country:'India',Email:'NTBI@Exc.com',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act
            var Result = _createBank.CreateBank(createBankRequest);
            var CreatedResult = Result as ObjectResult;
            ///Assert
            Assert.NotNull(CreatedResult);
            Assert.Equal(201, CreatedResult.StatusCode);
        }

        [Fact]
        public void CreateBank_EverythingNull_ReturnsBadRequest(){
            ///Arrange
            var BankData = "";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            
            ///Act
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
            Assert.Equal(new { Error = "Please Fill All Details Of Your Bank." }, Actual.Value);
            }
        }

        ///
        /// 
        /// BankName Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///  

        [Fact]
        public void CreateBank_BankNameAsNull_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:null,Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1500}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
            Assert.Equal(new { Error = "Please Enter Name of Your Bank" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBank_InCorrectBankName_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'AAA&#@     ',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            /// We are trimming the name cause after coming back from the service the white spaces will be removed so that will be causing the error in validation
            createBankRequest.Name = createBankRequest.Name.Trim();
            if(Actual.StatusCode == 400){
            Assert.Equal(new { Error = createBankRequest.Name+" Name is INVALID!" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBank_ExistingBankName_ReturnsConflict(){
            ///Arrange
            var BankData = "{Name:'Nitin mo Bank',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 409){
            Assert.Equal(new { Error = "Here Bank's Name is already used by a bank." }, Actual.Value);
            }
        }

        ///
        /// 
        /// BankPhone Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        /// 

        [Fact]
        public void CreateBank_BankPhoneAsNull_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:null,AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
            Assert.Equal(new { Error = "Please Enter Phone Of Your Bank" }, Actual.Value);
            }
            
        }
        [Fact]
        public void CreateBank_InCorrectBankPhone_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'93800@#712',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            createBankRequest.Phone = createBankRequest.Phone.Trim();
            if(Actual.StatusCode == 400){
            Assert.Equal(new { Error = createBankRequest.Phone+" is Not a Valid Phone Number" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBank_ExistingBankPhone_ReturnsConflict(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9380071279',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 409){
                Assert.Equal(new { Error = "Here Bank's Phone is already used by a bank." }, Actual.Value);
            }
        }

        ///
        /// 
        /// BankEmail Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateBank_BankEmailAsNull_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:null,Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Email Of Your Bank" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBank_InCorrectBankEmail_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'GDNitu@.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            /// Act
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
             createBankRequest.Email = createBankRequest.Email.Trim();
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Email Is Not Valid : "+createBankRequest.Email }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBank_ExistingBankEmail_ReturnsConflict(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'SundNitu@pro.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 409){
                Assert.Equal(new { Error = "Here Bank's Email is already used by a bank." }, Actual.Value);
            }
            
        }

        ///
        /// 
        /// BankFund Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateBank_InCorrectBankFund_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:10}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Bank's Fund "+createBankRequest.Funds+" is Not Valid" }, Actual.Value);
            }
        }

        ///
        /// 
        /// BankIFSCHead Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateBank_BankIFSCHeadAsNull_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:null,State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter IFSC HEADER for branches IFSC codes." }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBank_InCorrectBankIFSCHead_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'HTT%',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
             createBankRequest.IFSCHead = createBankRequest.IFSCHead.Trim();
            
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = createBankRequest.IFSCHead+" is Not Valid Please Enter 4 Unique Characters of IFSCHead For Branch's IFSC Codes" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBank_ExistingBankIFSCHead_ReturnsConflict(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'MHTT',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;
            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 409){
                Assert.Equal(new { Error = "Here Bank's IFSCHead is already used by a bank." }, Actual.Value);
            }
        }

        ///
        /// 
        /// AddressLine Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateBank_BankAddressLineAsNull_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : null ,City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 409){
                Assert.Equal(new { Error = "Please Enter Address Line" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBank_InCorrectBankAddressLine_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : '@kll9-',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            createBankRequest.AddressLine = createBankRequest.AddressLine.Trim();
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address Line: "+createBankRequest.AddressLine+" is INVALID!" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBank_ExistingBankAddressLine_ReturnsConflict(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'Near So India',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            
            if(Actual.StatusCode == 409){
                Assert.Equal(new {Error = "This Address is Already used by a Bank"} , Actual.Value);
            }
        }
        [Fact]
        public void CreateBank_SameBankAddressLineWithOtherFields_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'ALALALAL',IFSCHead:'IIII',State: 'ALALALAL',PostalCode: '100000',Country:'ALALALAL',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
            Assert.Equal(new { Error = "Address Line Sould be Unique then other fields of Address" }, Actual.Value);
            }
        }

        ///
        /// 
        /// BankCity Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateBank_BankCityAsNull_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : null,IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
            Assert.Equal(new { Error = "Please Enter City Of Your Bank" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBank_InCorrectBankCity_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'C&$#',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            createBankRequest.City = createBankRequest.City.Trim();
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address City: "+createBankRequest.City+" is INVALID!" }, Actual.Value);
            }
        }
        
        [Fact]
        public void CreateBank_SameBankAddressCityWithOtherFields_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'AAAAA',City : 'CCCCC',IFSCHead:'IIII',State: 'CCCCC',PostalCode: '100000',Country:'CCCCC',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
            Assert.Equal(new { Error = "Address City Sould be Unique then other fields of Address" }, Actual.Value);
            }
        }

        ///
        /// 
        /// BankState Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateBank_BankStateAsNull_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: null,PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
            Assert.Equal(new { Error = "Please Enter State Of Your Bank" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBank_InCorrectBankState_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: '   S%$#SS',PostalCode: '100000',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            createBankRequest.State = createBankRequest.State.Trim();
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address State: "+createBankRequest.State+" is INVALID!" }, Actual.Value);
            }
        }
        
        [Fact]
        public void CreateBank_SameBankAddressStateWithOtherFields_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'SSSSSSS',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
            Assert.Equal(new { Error = "Address State Sould be Unique then other fields of Address" }, Actual.Value);
            }
        }

        ///
        /// 
        /// BankCountry Validation & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateBank_BankCountryAsNull_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:null,Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
            Assert.Equal(new { Error = "Please Enter Country Of Your Bank" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBank_InCorrectBankCountry_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100000',Country:'II@#$I',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
           ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            createBankRequest.Country = createBankRequest.Country.Trim();
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address Country: "+createBankRequest.Country+" is INVALID!" }, Actual.Value);
            }
        }

        ///
        /// 
        /// BankPostalCode Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateBank_BankPostalCodeAsNull_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: null,Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
            Assert.Equal(new { Error = "Please Enter PostalCode Of Your Bank" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBank_InCorrectBankPostalCode_ReturnsBadRequest(){
            ///Arrange
            var BankData = "{Name:'NNNNN',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',IFSCHead:'IIII',State: 'SSSSSSS',PostalCode: '100%$%  00',Country:'IIIII',Email:'example@test.co',Funds:1200}";
            CreateBankRequest createBankRequest = new CreateBankRequest();
            createBankRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBankRequest>(BankData);
            ///Act 
            var Result= _createBank.CreateBank(createBankRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            createBankRequest.PostalCode = createBankRequest.PostalCode.Trim();
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address PostalCode: "+createBankRequest.PostalCode+" is INVALID!" }, Actual.Value);
            }
        }
                
    }
}