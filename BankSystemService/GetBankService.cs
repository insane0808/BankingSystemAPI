using System.Text.RegularExpressions;
namespace BankingSystem
{
    public class GetBankService
    {
        public GetBankResponse GetBank(GetBankRequest getBankRequest)
        {

            if (getBankRequest.IFSC == null) throw new ArgumentException($"Please Enter IFSC code of Your Branch to get bank.");


            // Validating IFSC code
            getBankRequest.IFSC = getBankRequest.IFSC.Trim();
            if (getBankRequest.IFSC.Length < 11 || getBankRequest.IFSC.Length > 11 || getBankRequest.IFSC.Any(char.IsSymbol) || getBankRequest.IFSC[4] != '0' || !(new Regex("^[A-Z]{4}0[A-Z0-9]{6}$").IsMatch(getBankRequest.IFSC))) throw new ArgumentException($"IFSC Code: {getBankRequest.IFSC} is Invalid. Please Check.");

            // Getting Branch nd Bank
            CrudBranch crudBranch = new CrudBranch();
            CrudBank crudBank = new CrudBank();
            var GetBranch = crudBranch.GetByIFSC(getBankRequest.IFSC);
            if (GetBranch == null) throw new ArgumentException($"There is No Branch exists with {getBankRequest.IFSC}");
            var GetBank = crudBank.Get(GetBranch.BankCode);
            if (GetBank == null) throw new ArgumentException($"There is No bank exists with the branch. Please Contact Admin Of Your Bank.");

            // Response
            GetBankResponse getBankResponse = new GetBankResponse(GetBank);
            return getBankResponse;
        }
    }
}