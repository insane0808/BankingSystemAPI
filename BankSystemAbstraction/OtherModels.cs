using MongoDB.Bson.Serialization.Attributes;
namespace BankingSystem
{
    // Account Type Enum Definition
    public enum AccountTypes
    {
        savings,
        salary,
        current
    }
    public enum Gender
    {
        male,
        m,
        f,
        female,
        t,
        transgender
    }

    //Bank Data Schema
    public class BankDetails
    {
        [BsonId]
        public int Code { get; set; }
        public string Name { get; set; } = null!;
        public Address Address { get; set; }
        public string Email { get; set; } = null!;
        public string IFSCHead { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public double Funds { get; set; }

        public BankDetails(CreateBankRequest createBankRequest, int Code)
        {
            this.Name = createBankRequest.Name;
            this.Code = Code;
            Address = new Address(); // For removing exception of Object null instance Reference
            this.Address.AddressLine = createBankRequest.AddressLine;
            this.Address.City = createBankRequest.City;
            this.Address.State = createBankRequest.State;
            this.IFSCHead = createBankRequest.IFSCHead;
            this.Address.PostalCode = createBankRequest.PostalCode;
            this.Address.Country = createBankRequest.Country;
            this.Email = createBankRequest.Email;
            this.Phone = createBankRequest.Phone;
            this.Funds = createBankRequest.Funds;
        }
    }

    //Address Schema
    public class Address
    {
        public string AddressLine { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
    }

    //Branch Schema
    public class BranchDetails
    {
        //For linking to the respected bank
        public int BankCode { get; set; }
        public string BankName { get; set; }
        public string Name { get; set; } = null!;
        // Making IFSC as ID to find any branch
        [BsonId]
        public string IFSC { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public Address Address { get; set; } = null!;

        public BranchDetails(CreateBranchRequest createBranchRequest, string IFSC, int Code)
        {
            this.BankCode = Code;
            this.BankName = createBranchRequest.BankName;
            this.Name = createBranchRequest.Name;
            Address = new Address(); // For removing exception of Object null instance Reference
            this.Address.AddressLine = createBranchRequest.AddressLine;
            this.Address.City = createBranchRequest.City;
            this.Address.State = createBranchRequest.State;
            this.Address.PostalCode = createBranchRequest.PostalCode;
            this.Address.Country = createBranchRequest.Country;
            this.IFSC = IFSC;
            this.Phone = createBranchRequest.Phone;
        }
    }

    public class AccountDetails
    {
        [BsonId]
        public string AccountNumber { get; set; } = null!;
        public string IFSC { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string FathersName { get; set; } = null!;
        public string AccountType { get; set; } = null!;
        public string AadharNumber { get; set; } = null!;
        public string? PanNumber { get; set; }
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public DateTime DOB { get; set; }
        public double Balance { get; set; }
        public string Phone { get; set; } = null!;

        public AccountDetails(CreateAccountRequest createAccountRequest, Int64 AccountNumber)
        {
            this.AccountNumber = AccountNumber.ToString();
            this.IFSC = createAccountRequest.IFSC;
            this.Name = createAccountRequest.Name;
            this.FathersName = createAccountRequest.FathersName;
            this.AccountType = createAccountRequest.AccountType;
            this.AadharNumber = createAccountRequest.AadharNumber;
            this.PanNumber = createAccountRequest.PanNumber;
            this.Email = createAccountRequest.Email;
            this.Gender = createAccountRequest.Gender;
            Address = new Address(); // For removing object null exception
            this.Address.AddressLine = createAccountRequest.AddressLine;
            this.Address.City = createAccountRequest.City;
            this.Address.State = createAccountRequest.State;
            this.Address.PostalCode = createAccountRequest.PostalCode;
            this.Address.Country = createAccountRequest.Country;
            this.Phone = createAccountRequest.Phone;
            this.DOB = DateTime.Parse(createAccountRequest.DOB);
            this.Balance = createAccountRequest.Balance;
        }

    }
    public class TransactionDetails
    {
        [BsonId]
        public string TransactionId { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public String TransactionType { get; set; } = null!;
        public DateTime DOT { get; set; }
        public double Ammount { get; set; }

        public TransactionDetails(AddDepositTransactionRequest addDepositTransactionRequest, Int64 TransactionId)
        {
            this.TransactionId = TransactionId.ToString();
            this.AccountNumber = addDepositTransactionRequest.AccountNumber;
            this.TransactionType = "Deposit";
            this.DOT = DateTime.Now;
            this.Ammount = addDepositTransactionRequest.Ammount;
        }
        public TransactionDetails(AddWithdrawlTransactionRequest addDepositTransactionRequest, Int64 TransactionId)
        {
            this.TransactionId = TransactionId.ToString();
            this.AccountNumber = addDepositTransactionRequest.AccountNumber;
            this.TransactionType = "Withdrawl";
            this.DOT = DateTime.Now;
            this.Ammount = -addDepositTransactionRequest.Ammount;
        }
    }

}