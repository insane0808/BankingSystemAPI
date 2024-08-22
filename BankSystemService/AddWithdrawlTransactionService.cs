
namespace BankingSystem
{
    public class AddWithdrawlTransactionService
    {
        public AddWithdrawlTransactionResponse AddWithdrawlTransaction(AddWithdrawlTransactionRequest addWithdrawlTransactionRequest)
        {
            if (addWithdrawlTransactionRequest.AccountNumber == null) throw new ArgumentException($"Please Enter Your Account Number");
            if (addWithdrawlTransactionRequest.AccountType == null) throw new ArgumentException($"Please Enter Your Account Type");
            if (addWithdrawlTransactionRequest.Ammount <= 0 || addWithdrawlTransactionRequest.Ammount > 100000000) throw new ArgumentException($"Please Enter Valid Withdrawl Ammount Between 1 - 10000000");

            // General Validations 
            if (addWithdrawlTransactionRequest.AccountNumber.Length < 9 || addWithdrawlTransactionRequest.AccountNumber.Length > 18 || !addWithdrawlTransactionRequest.AccountNumber.All(char.IsDigit)) throw new ArgumentException($"Account Number: {addWithdrawlTransactionRequest.AccountNumber} is Not Valid");
            if (addWithdrawlTransactionRequest.AccountType.Length < 6 || addWithdrawlTransactionRequest.AccountType.Length > 7 || !(Enum.IsDefined(typeof(AccountTypes), addWithdrawlTransactionRequest.AccountType.ToLower()))) throw new ArgumentException($" Account Type {addWithdrawlTransactionRequest.AccountType} is Not Valid. We Only Offer Savings, Current & Salary");

            // Database Validations
            CrudAccount crudAccount = new CrudAccount();
            CrudBank crudBank = new CrudBank();
            CrudBranch crudBranch = new CrudBranch();
            CrudTransaction crudTransaction = new CrudTransaction();


            // Validation of Account number with the bank
            var GetAccount = crudAccount.Get(addWithdrawlTransactionRequest.AccountNumber);
            if (GetAccount == null) throw new ArgumentException($"There is No User exists With This Account Number.");
            if (GetAccount.AccountType.ToLower() != addWithdrawlTransactionRequest.AccountType.ToLower()) throw new ArgumentException($"This Account is not {addWithdrawlTransactionRequest.AccountType} Type");
            if (GetAccount.Balance - addWithdrawlTransactionRequest.Ammount < 0) throw new ArgumentException($"Your Balance {GetAccount.Balance} is Insufficient");


            // Cheking branch and banks are present or not, May be possible that bank and branch have been removed
            var GetBranch = crudBranch.GetByIFSC(GetAccount.IFSC);
            if (GetBranch == null) throw new ArgumentException($"There is No branch Found With the Account Number");
            var GetBank = crudBank.Get(GetBranch.BankCode);
            if (GetBank == null) throw new ArgumentException($"There is No Bank Found For the Account Number");
            if (GetBank.Funds - addWithdrawlTransactionRequest.Ammount < 0) throw new ArgumentException($"The Withdrawl Ammount {addWithdrawlTransactionRequest.Ammount} is Larger Then Our Bank Fund. Please Contact Bank");


            // More Generations and Validations 
            // Gerating transaction id
            Int64 TransactionId = new Random().NextInt64(10000000000, 100000000000);
            Int64 CheckMatch = 10000000000;
            var SameTransactionId = crudTransaction.GetByTID(TransactionId);
            while (SameTransactionId != null && CheckMatch < 100000000000) // Validating the transaction ID whether its available or not
            {
                TransactionId = new Random().NextInt64(10000000000, 100000000000);
                SameTransactionId = crudTransaction.GetByTID(TransactionId);
                CheckMatch++;
            }
            if (CheckMatch >= 100000000000) throw new ArgumentException($"Some Transection Issue, Please try After sometime.");


            // Now generating transaction details for Proof purpose
            TransactionDetails transactionDetails = new TransactionDetails(addWithdrawlTransactionRequest, TransactionId);
            var GetTransactionTime = crudTransaction.Get(transactionDetails.DOT); // Checking the transaction time not to be same
            if (GetTransactionTime != null) throw new ArgumentException($"Two Transactions Can't be On Same Time");
            if (transactionDetails.DOT > DateTime.Now) throw new ArgumentException($"Your Time can't be in Future. Please Update Time Of Your System e.g.{DateTime.Now}");


            // Updation Part in bank and account
            if (!crudBank.Update(GetBank.Code, -addWithdrawlTransactionRequest.Ammount)) throw new ArgumentException($"The Bank Fund UPdation Causing Error");
            if (!crudAccount.Update(addWithdrawlTransactionRequest.AccountNumber, -addWithdrawlTransactionRequest.Ammount)) throw new ArgumentException($"User's Balance Updation Is Causing Error.");
            crudTransaction.Withdrawl(transactionDetails); // Finally making the transaction happen
            if (crudTransaction.GetByTID(TransactionId) == null) if (crudTransaction.GetByTID(TransactionId) == null)
                {
                    // Again changing the ammounts to default.
                    if (!crudBank.Update(GetBank.Code, -addWithdrawlTransactionRequest.Ammount)) throw new ArgumentException($"The Bank Fund Updation Causing Error");
                    if (!crudAccount.Update(addWithdrawlTransactionRequest.AccountNumber, -addWithdrawlTransactionRequest.Ammount)) throw new ArgumentException($"User's Balance Updation Is Causing Error.");
                    throw new ArgumentException($"The Transaction was Unsuccessfull. Please Try After Sometime");
                }


            GetAccount = crudAccount.Get(addWithdrawlTransactionRequest.AccountNumber);
            AddWithdrawlTransactionResponse addWithdrawlTransactionResponse = new AddWithdrawlTransactionResponse(transactionDetails, GetAccount.Balance);
            return addWithdrawlTransactionResponse;
        }
    }
}