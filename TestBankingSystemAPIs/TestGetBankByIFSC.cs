using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace BankingSystem
{
    public class TestGetBankByIFSC
    {
        private readonly GetBankByIFSCApi _getBankByIFSC;
        public TestGetBankByIFSC()
        {
            _getBankByIFSC = new GetBankByIFSCApi();
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            Environment.SetEnvironmentVariable("ACCOUNTCOLLECTION","accounts");
            Environment.SetEnvironmentVariable("BRANCHCOLLECTION","branch");
            Environment.SetEnvironmentVariable("BANKCOLLECTION","bank");
            Environment.SetEnvironmentVariable("CLIENT","mongodb://localhost:27017");
            Environment.SetEnvironmentVariable("DATABASE","BankingSystemLF");
        }


        ///
        /// 
        /// IFSC Code Validations & Verification tests
        /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        ///
        /// 

        [Fact]
        public void GetBankByIFSC_IFSCAsNull_ReturnsBadRequest(){
            ///Arrange
            string IFSC = null!;
            GetBankRequest getBankRequest = new GetBankRequest();
            getBankRequest.IFSC = IFSC;
            ///Act 
            var Result= _getBankByIFSC.GetBankByIFSC(getBankRequest);
            var Actual = Result as ObjectResult;
            ///Assert
            Assert.NotNull(Actual);
            Assert.Equal(400, Actual.StatusCode);
            if(Actual.StatusCode == 400)
            {
                Assert.Equal(new {Error = "Please Enter IFSC code of Your Branch to get bank."}, Actual.Value);
            }
        }

        [Fact]
        public void GetBankByIFSC_InCorrectIFSC_ReturnsBadRequest(){
            ///Arrange
            string IFSC = "GHDH987835@#";
            GetBankRequest getBankRequest = new GetBankRequest();
            getBankRequest.IFSC = IFSC;
            ///Act 
            var Result= _getBankByIFSC.GetBankByIFSC(getBankRequest);
            var Actual = Result as ObjectResult;
            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 400)
            {
                Assert.Equal(new {Error = "IFSC Code: "+getBankRequest.IFSC+" is Invalid. Please Check."}, Actual.Value);
            }
        }

        [Fact]
        public void GetBankByIFSC_UnKnownIFSC_ReturnsBadRequest(){
            ///Arrange
            string IFSC = "GHDH0878359";
            GetBankRequest getBankRequest = new GetBankRequest();
            getBankRequest.IFSC = IFSC;
            ///Act 
            var Result= _getBankByIFSC.GetBankByIFSC(getBankRequest);
            var Actual = Result as ObjectResult;
            ///Assert
            Assert.NotNull(Actual);
            if(Actual.StatusCode == 404)
            {
                Assert.Equal(new {Error = "There is No Branch exists with "+getBankRequest.IFSC}, Actual.Value);
            }
        }

        [Fact]
        public void GetBankByIFSC_CorrectIFSC_ReturnsBadRequest(){
            ///Arrange
            string IFSC = "MHTT0602718";
            GetBankRequest getBankRequest = new GetBankRequest();
            getBankRequest.IFSC = IFSC;
            ///Act 
            var Result= _getBankByIFSC.GetBankByIFSC(getBankRequest);
            var Actual = Result as ObjectResult;
            ///Assert
            Assert.NotNull(Actual);
            Assert.Equal(200, Actual.StatusCode);
        }
    }
}