
namespace BankingSystem
{
    public class AddDepositTransactionService
    {
        public AddDepositTransactionResponse AddDepositTransaction(AddDepositTransactionRequest addDepositTransactionRequest)
        {

            if (addDepositTransactionRequest.AccountNumber == null) throw new ArgumentException($"Please Enter Account Number");
            if (addDepositTransactionRequest.AccountType == null) throw new ArgumentException($"Please Enter Your Account Type");
            if (addDepositTransactionRequest.Ammount <= 0 || addDepositTransactionRequest.Ammount > 100000000) throw new ArgumentException($"Please Enter Valid Deposit Ammount Between 1 - 10000000");

            // General Validations 
            if (addDepositTransactionRequest.AccountNumber.Length < 9 || addDepositTransactionRequest.AccountNumber.Length > 18 || !addDepositTransactionRequest.AccountNumber.All(char.IsDigit)) throw new ArgumentException($"Account Number: {addDepositTransactionRequest.AccountNumber} is Not Valid");
            if (addDepositTransactionRequest.AccountType.Length < 6 || addDepositTransactionRequest.AccountType.Length > 7 || !(Enum.IsDefined(typeof(AccountTypes), addDepositTransactionRequest.AccountType.ToLower()))) throw new ArgumentException($"Account Type {addDepositTransactionRequest.AccountType} is Not Valid. We Only Offer Savings, Current & Salary");


            // Database Validations            if (addDepositTransactionRequest.AccountType == null) throw new ArgumentException($"Please Enter Your Account Type");
            CrudAccount crudAccount = new CrudAccount();
            CrudBank crudBank = new CrudBank();
            CrudBranch crudBranch = new CrudBranch();
            CrudTransaction crudTransaction = new CrudTransaction();

            // Validating User's Account Number
            var GetAccount = crudAccount.Get(addDepositTransactionRequest.AccountNumber);
            if (GetAccount == null) throw new ArgumentException($"There is No User exists With This Account Number.");
            if (GetAccount.AccountType.ToLower() != addDepositTransactionRequest.AccountType.ToLower()) throw new ArgumentException($"This Account is not of {addDepositTransactionRequest.AccountType} Type");


            // For checking the transaction balance should not exceed the max limit of money of a bank
            if (GetAccount.Balance + addDepositTransactionRequest.Ammount > 10000000000) throw new ArgumentException($"The Deposit Ammount {addDepositTransactionRequest.Ammount} is Too Large To Deposit");
            var GetBranch = crudBranch.GetByIFSC(GetAccount.IFSC); //If No branch is present then there will be no account

            if (GetBranch == null) throw new ArgumentException($"There is No branch Found With the Account Number");
            var GetBank = crudBank.Get(GetBranch.BankCode); // Checking for the bank account to check whether there was any error at the bank end

            if (GetBank == null) throw new ArgumentException($"There is No Bank Found For the Account Number");
            if (GetBank.Funds + addDepositTransactionRequest.Ammount > 10000000000) throw new ArgumentException($"The Deposit Ammount {addDepositTransactionRequest.Ammount} is Too large For Our Bank");


            // More Generations and Validations 
            // Transaction ID generation for getting an unique transaction Id for every transaction
            Int64 TransactionId = new Random().NextInt64(10000000000, 100000000000);
            Int64 CheckMatch = 10000000000;
            var SameTransactionId = crudTransaction.GetByTID(TransactionId); //For checking if the transaction id is present or not

            while (SameTransactionId != null && CheckMatch < 100000000000)
            {
                TransactionId = new Random().NextInt64(10000000000, 100000000000);
                SameTransactionId = crudTransaction.GetByTID(TransactionId);
                CheckMatch++;
            } //If transaction ID's Limit have been reached
            if (CheckMatch >= 100000000000) throw new ArgumentException($"Some Technical error, Plese contact your branch");


            TransactionDetails transactionDetails = new TransactionDetails(addDepositTransactionRequest, TransactionId); // Transaction generation
            var GetTransactionTime = crudTransaction.Get(transactionDetails.DOT); // for checking any same time transaction issue
            if (GetTransactionTime != null) throw new ArgumentException($"Two Transactions Can't be On Same Time, Please Change Time e.g.(2022-06-22 18:20:13 pm)");
            if (transactionDetails.DOT > DateTime.Now) throw new ArgumentException($"Your Time can't be in Future. Please Enter a Valid Time e.g.{DateTime.Now}");


            // Updation Part to bank and account
            if (!crudBank.Update(GetBank.Code, addDepositTransactionRequest.Ammount)) throw new ArgumentException($"The Bank Fund Updation Causing Error");
            if (!crudAccount.Update(addDepositTransactionRequest.AccountNumber, addDepositTransactionRequest.Ammount)) throw new ArgumentException($"User's Balance Updation Is Causing Error.");
            crudTransaction.Deposit(transactionDetails); //Depositing money finally

            // Checking if the transaction is happened or not
            if (crudTransaction.GetByTID(TransactionId) == null)
            {
                // Again changing the ammounts to default.
                if (!crudBank.Update(GetBank.Code, -addDepositTransactionRequest.Ammount)) throw new ArgumentException($"The Bank Fund Updation Causing Error");
                if (!crudAccount.Update(addDepositTransactionRequest.AccountNumber, -addDepositTransactionRequest.Ammount)) throw new ArgumentException($"User's Balance Updation Is Causing Error.");

                throw new ArgumentException($"The Transaction was Unsuccessfull. Please Try After Sometime");
            }

            GetAccount = crudAccount.Get(addDepositTransactionRequest.AccountNumber); // Now getting the details to send as the response
            AddDepositTransactionResponse addDepositTransactionResponse = new AddDepositTransactionResponse(transactionDetails, GetAccount.Balance);
            return addDepositTransactionResponse;
        }
    }
}