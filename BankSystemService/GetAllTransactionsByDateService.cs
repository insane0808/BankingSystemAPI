
namespace BankingSystem
{
    public class GetAllTransactionsByDateService
    {
        public List<GetAllTransactionsByDateResponse> GetAllTransactions(DateTime From, DateTime To, string AccountNumber)
        {
            AccountNumber = AccountNumber.Trim();
            if (AccountNumber.Length < 9 || AccountNumber.Length > 18 || !(AccountNumber.All(char.IsDigit))) throw new ArgumentException($"AccountNumber {AccountNumber} is Invalid!");
            CrudAccount crudAccount = new CrudAccount();
            CrudBranch crudBranch = new CrudBranch();
            CrudBank crudBank = new CrudBank();

            var GetAccount = crudAccount.Get(AccountNumber);
            if (GetAccount == null) throw new ArgumentException($"Account Number: {AccountNumber} Doesn't exists. Please Check.");
            // var GetBranch = crudBranch.GetByIFSC(GetAccount.IFSC);
            // if (GetBranch == null) throw new ArgumentException($"There is No Branch exists for AccountNumber: {AccountNumber}");
            // var GetBank = crudBank.Get(GetBranch.BankCode);
            // if (GetBank == null) throw new ArgumentException($"There is No Bank exists for AccountNumber: {AccountNumber}");


            // Validating the from and to dates
            
            if (From.Day < 1 || From.Day > 31 || From.Month < 1 || From.Month > 12 || From.Year > DateTime.Now.Year || From.Year < DateTime.Now.AddYears(-100).Year || From >= DateTime.Now) throw new ArgumentException($"Please Enter a Valid 'From' Date");
            if (To.Day < 1 || To.Day > 31 || To.Month < 1 || To.Month > 12 || To.Year > DateTime.Now.Year || To.Year < DateTime.Now.AddYears(-100).Year || To >= DateTime.Now) throw new ArgumentException($"Please Enter a Valid 'To' Date ");
            if (From > To) throw new ArgumentException($"Please Enter 'From' Date less than 'To Date");


            // Getting the transaction list
            CrudTransaction crudTransaction = new CrudTransaction();
            var GetTransactions = crudTransaction.Get(From, To, AccountNumber);
            if (GetTransactions == null) throw new ArgumentException($"No Transactions Found.");


            // Generating Responses
            List<GetAllTransactionsByDateResponse> getAllTransactionsByDateResponses = new List<GetAllTransactionsByDateResponse>(); //For converting from Transaction List to response List
            foreach (TransactionDetails transactionDetails in GetTransactions)
            {
                getAllTransactionsByDateResponses.Add(new GetAllTransactionsByDateResponse(transactionDetails));
            }
            return getAllTransactionsByDateResponses;


        }
    }
}