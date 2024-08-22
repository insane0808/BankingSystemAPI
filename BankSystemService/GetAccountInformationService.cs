
namespace BankingSystem
{
    public class GetAccountInformationService
    {
        public GetAccountInformationResponse GetAccountInformation(GetAccountInformationRequest getAccountInformationRequest)
        {

            if (getAccountInformationRequest.AccountNumber == null) throw new ArgumentException($"Please Provide a AccountNumber To Get Details.");


            //Getting Account Number from Identifier value
            getAccountInformationRequest.AccountNumber = getAccountInformationRequest.AccountNumber.Trim();
            if (getAccountInformationRequest.AccountNumber.Length < 9 || getAccountInformationRequest.AccountNumber.Length > 18 || !(getAccountInformationRequest.AccountNumber.All(char.IsDigit))) throw new ArgumentException($"AccountNumber: {getAccountInformationRequest.AccountNumber} is Invalid. Please Enter Correct");


            CrudAccount crudAccount = new CrudAccount();
            CrudBranch crudBranch = new CrudBranch();
            CrudBank crudBank = new CrudBank();
            // Checking for the proof to validate that seriouslyan account a branch and a bank exists
            var GetAccount = crudAccount.Get(getAccountInformationRequest.AccountNumber);
            if (GetAccount == null) throw new ArgumentException($"Account Number :{getAccountInformationRequest.AccountNumber} Doesn't exists. Please Check.");
            var GetBranch = crudBranch.GetByIFSC(GetAccount.IFSC);
            if (GetBranch == null) throw new ArgumentException($"There is No Branch exists for AccountNumber: {getAccountInformationRequest.AccountNumber} Number");
            var GetBank = crudBank.Get(GetBranch.BankCode);
            if (GetBank == null) throw new ArgumentException($"There is No Bank exists for AccountNumber {getAccountInformationRequest.AccountNumber}");


            GetAccountInformationResponse getAccountInformationResponse = new GetAccountInformationResponse(GetAccount, GetBank.Name, GetBranch.Name);
            return getAccountInformationResponse;
        }
    }
}