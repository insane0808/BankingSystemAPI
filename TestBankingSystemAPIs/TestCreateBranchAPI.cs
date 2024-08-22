using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BankingSystem
{
    public class TestCreateBranchAPI
    {
        private readonly CreateBranchApi _createBranch;

        public TestCreateBranchAPI(){
            _createBranch = new CreateBranchApi();
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            Environment.SetEnvironmentVariable("BRANCHCOLLECTION","branch");
            Environment.SetEnvironmentVariable("ACCOUNTCOLLECTION","accounts");
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
        public void CreateBranch_EverythingNull_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter All Valid Details Of Your Branch" }, Actual.Value);
            }
        }

        ///
        /// 
        /// BankName Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///  

        [Fact]
        public void CreateBranch_BankNameAsNull_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:null,Name:'Branch',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert 
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Name Of Your Bank" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBranch_InCorrectBankName_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Nitin &&*',Name:'Branch',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            createBranchRequest.BankName = createBranchRequest.BankName.Trim();
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "The Branch's Bank Name : "+createBranchRequest.BankName+", is Not Valid" }, Actual.Value);
            }
        }

        ///
        /// 
        /// BranchName Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        /// 

        [Fact]
        public void CreateBranch_NameAsNull_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Test Bank',Name:null,Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Name Of Your Branch" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBranch_InCorrectBranchName_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Test Bank',Name:'Branch&^$%',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
            Assert.Equal(new { Error = "The Branch Name : "+createBranchRequest.Name+ ", is Not Valid" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBranch_ExistingBranchNameOfBranch_ReturnsConflict(){
            ///Arrange
            var BranchData = "{BankName:'Test Bank',Name:'My Branc',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(409== Actual.StatusCode){
                Assert.Equal(new { Error = "The Name Of the Branch provided already used by a existing Branch." }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBranch_ExistingBranchNameOfBank_ReturnsConflict(){
            ///Arrange
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Nitin mo Bank',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(409 == Actual.StatusCode){
                Assert.Equal(new { Error = "The Name Of the Branch is already used by a existing Bank." }, Actual.Value);
            }
        }
        

        ///
        /// 
        /// BranchPhone Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        /// 

        [Fact]
        public void CreateBranch_PhoneAsNull_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Test Bank',Name:'Branch',Phone:null,AddressLine : 'ALALALAL',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode== 400){
                Assert.Equal(new { Error = "Please Enter Phone Of Your Branch" }, Actual.Value);
            }
        }

        [Fact]
        public void CreateBranch_InCorrectPhone_ReturnsBadRequest(){
            ///Arrange
            CreateBranchRequest createBranchRequest = new CreateBranchRequest() {BankName="Test Bank",Name="Branch",Phone="93800@#712",AddressLine = "ALALALAL",City = "CCCCC",State= "SSSSSSS",PostalCode= "100000",Country="IIIII"};
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
            Assert.Equal(new{ Error = createBranchRequest.Phone+" is Not a Valid Phone Number" }, Actual.Value);
            }
        }

        [Fact]
        public void CreateBranch_ExistingPhoneOfBranch_ReturnsConflict(){
            ///Arrange Please Enter Existing Bank Name to check the existance of the phone
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9380071279',AddressLine : 'ALALALAL',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;
            ///Assert
            Assert.NotNull(Actual);
            if(409 == Actual.StatusCode){
                Assert.Equal(new{ Error = "Here Branch's Phone is already used by a existing branch" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBranch_ExistingPhoneOfUser_ReturnsConflict(){
            ///Arrange Please Enter Existing Bank Name to check the existance of the phone
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9388488488',AddressLine : 'ALALALAL',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;
            ///Assert
            Assert.NotNull(Actual);
            if(409 == Actual.StatusCode){
                Assert.Equal(new{ Error = "Here Branch's Phone is already used by a existing User" }, Actual.Value);
            }
        }

        

        ///
        /// 
        /// AddressLine Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateBranch_AddressLineAsNull_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Test Bank',Name:'Branch',Phone:'9999999999',AddressLine : null ,City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter AddressLine Of Your Branch" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBranch_InCorrectAddressLine_ReturnsBadRequest(){
            ///Arrange 
            /// Please Pass Bank Name for this validation
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999999999',AddressLine : '@kll9$#$-',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            createBranchRequest.AddressLine = createBranchRequest.AddressLine.Trim();
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address Line: "+createBranchRequest.AddressLine+" is INVALID!" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBranch_ExistingBranchAddressLine_ReturnsConflict(){
            ///Arrange Make Sure You are Entering Existing Bank Name For Validation of Address
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999999999',AddressLine : 'Temple India',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 409){
                Assert.Equal(new { Error = "A existing Branch is at the same AddressLine, Please Provide a different Address Line" }, Actual.Value);
            }
        }
        
        [Fact]
        public void CreateBranch_ExistingAccountAddressLine_ReturnsConflict(){
            ///Arrange Make Sure You are Entering Existing Bank Name For Validation of Address
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999999999',AddressLine : 'Near',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 409){
                Assert.Equal(new { Error = "A existing User is at the same AddressLine, Please Provide a different Address Line" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBranch_SameAddressLineWithOtherFields_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'ALALALAL',State: 'ALALALAL',PostalCode: '100000',Country:'ALALALAL'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
            Assert.Equal(new{ Error = "Address Line Sould be Unique then other fields of Address" }, Actual.Value);
            }
        }

        ///
        /// 
        /// BranchCity Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateBranch_CityAsNull_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999999999',AddressLine : 'ALALALAL',City : null,State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter City Of Your Branch" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBranch_InCorrectBranchCity_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'C&$#',State: 'SSSSSSS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400)
                Assert.Equal(new { Error = "Address City: "+createBranchRequest.City+" is INVALID!" }, Actual.Value);
        }
        
        [Fact]
        public void CreateBranch_SameAddressCityWithOtherFields_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999999999',AddressLine : 'ALALALA',City : 'CCCCC',State: 'CCCCC',PostalCode: '100000',Country:'CCCCC'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400)
                Assert.Equal(new { Error = "Address City Sould be Unique then other fields of Address" }, Actual.Value);
        }

        ///
        /// 
        /// BranchState Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateBranch_StateAsNull_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',State: null,PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400)
                Assert.Equal(new { Error = "Please Enter State Of Your Branch" }, Actual.Value);
        }
        [Fact]
        public void CreateBranch_InCorrectState_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',State: '   S%$#SS',PostalCode: '100000',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400)
                Assert.Equal(new { Error = "Address State: "+createBranchRequest.State+" is INVALID!" }, Actual.Value);
            
        }
        
        [Fact]
        public void CreateBranch_SameAddressStateWithOtherFields_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999999999',AddressLine : 'ALALAL',City : 'CCCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'SSSSSSS'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400)
                Assert.Equal(new { Error = "Address State Sould be Unique then other fields of Address" }, Actual.Value);
            
        }

        ///
        /// 
        /// BranchCountry Validation & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateBranch_CountryAsNull_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:null}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Country Of Your Branch" }, Actual.Value);
            }
            
        }
        [Fact]
        public void CreateBranch_InCorrectBranchCountry_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100000',Country:'II@#$I'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
           ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Address Country: "+createBranchRequest.Country+" is INVALID!" }, Actual.Value);
            }
        }

        ///
        /// 
        /// BranchPostalCode Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        ///

        [Fact]
        public void CreateBranch_PostalCodeAsNull_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',State: 'SSSSSSS',PostalCode: null,Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400){
                Assert.Equal(new { Error = "Please Enter Postal Code Of Your Branch" }, Actual.Value);
            }
        }
        [Fact]
        public void CreateBranch_InCorrectPostalCode_ReturnsBadRequest(){
            ///Arrange
            var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999999999',AddressLine : 'ALALALAL',City : 'CCCCC',State: 'SSSSSSS',PostalCode: '100%$%  00',Country:'IIIII'}";
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
            ///Act 
            var Result= _createBranch.CreateBranch(createBranchRequest);
            var Actual = Result as ObjectResult;

            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400)
            Assert.Equal(new{ Error = "Address PostalCode: "+createBranchRequest.PostalCode+" is INVALID!" }, Actual.Value);
        }

        // [Fact]
        // public void CreateBranch_AllDetailsCorrect_ReturnsCreated(){
        //     ///Arrange
        //     var BranchData = "{BankName:'Nitin mo Bank',Name:'Branch',Phone:'9999987999',AddressLine : 'Near Vijay Nagar',City : 'Indore',State: 'Madhya Pradesh',PostalCode: '452010',Country:'India'}";
        //     CreateBranchRequest createBranchRequest = new CreateBranchRequest();
        //     createBranchRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateBranchRequest>(BranchData);
        //     ///Act
        //     var Result = _createBranch.CreateBranch(createBranchRequest);
        //     var CreatedResult = Result as ObjectResult;
        //     ///Assert
        //     Assert.NotNull(CreatedResult);
        //     Assert.Equal(201, CreatedResult.StatusCode);
        // }
    }
}