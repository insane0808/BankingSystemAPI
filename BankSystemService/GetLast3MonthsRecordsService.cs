
namespace BankingSystem
{
    public class GetLast3MonthsRecordsService
    {
        public List<GetLast3MonthsRecordsResponse> GetLast3MonthsRecords(string AccountNumber)
        {
            AccountNumber = AccountNumber.Trim();
            if (AccountNumber.Length < 9 || AccountNumber.Length > 18 || !(AccountNumber.All(char.IsDigit))) throw new ArgumentException($"AccountNumber: {AccountNumber} is Invalid!");


            CrudAccount crudAccount = new CrudAccount();
            CrudBranch crudBranch = new CrudBranch();
            CrudBank crudBank = new CrudBank();
            CrudTransaction crudTransaction = new CrudTransaction();
            var GetAccount = crudAccount.Get(AccountNumber);
            if (GetAccount == null) throw new ArgumentException($"Account Number: {AccountNumber} Doesn't exists. Please Check.");
            var GetBranch = crudBranch.GetByIFSC(GetAccount.IFSC);
            // if (GetBranch == null) throw new ArgumentException($"There is No Branch exists for AccountNumber {AccountNumber}");
            // var GetBank = crudBank.Get(GetBranch.BankCode);
            // if (GetBank == null) throw new ArgumentException($"There is No Bank exists for AccountNumber {AccountNumber}");


            // Validating the from and to dates
            var DateBefore3MonthsAgo = DateTime.Now.AddMonths(-3);
            var GetTransactions = crudTransaction.Get(DateBefore3MonthsAgo, System.DateTime.Now, AccountNumber);
            if (GetTransactions == null) throw new ArgumentException($"No Transactions Found.");

            // Generations of All responses
            List<GetLast3MonthsRecordsResponse> getLast3MonthsRecordsResponses = new List<GetLast3MonthsRecordsResponse>(); //For converting from branchDetails List to response List
            foreach (TransactionDetails transactionDetails in GetTransactions)
            {
                getLast3MonthsRecordsResponses.Add(new GetLast3MonthsRecordsResponse(transactionDetails));
            }
            return getLast3MonthsRecordsResponses;
        }
    }
}