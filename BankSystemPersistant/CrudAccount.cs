using MongoDB.Driver;
namespace BankingSystem
{
    public class CrudAccount
    {
        private IMongoCollection<AccountDetails> _accountCollection;

        public CrudAccount()
        {
            // Checking if Environment Variables are null
            if (Environment.GetEnvironmentVariable("CLIENT") == null)
            {
                throw new ArgumentException($"No Database Client Path Found.");
            }
            if (Environment.GetEnvironmentVariable("DATABASE") == null)
            {
                throw new ArgumentException($"No Database Path Found.");
            }
            if (Environment.GetEnvironmentVariable("ACCOUNTCOLLECTION") == null)
            {
                throw new ArgumentException($"No Account Collection Path Found.");
            }


            var MongoClient = new MongoClient(Environment.GetEnvironmentVariable("CLIENT"));
            var MongoDatabase = MongoClient.GetDatabase(Environment.GetEnvironmentVariable("DATABASE"));
            _accountCollection = MongoDatabase.GetCollection<AccountDetails>(Environment.GetEnvironmentVariable("ACCOUNTCOLLECTION"));

        }
        public AccountDetails Get(string AccountNumber) //Getting the  associated with a email
        {
            return _accountCollection.Find(account => account.AccountNumber == AccountNumber).FirstOrDefault();
        }

        public bool Get(Address address) //Getting the account associated with any other account
        {
            if (_accountCollection.Find(account => account.Address.AddressLine == address.AddressLine).FirstOrDefault() == null) return false;
            return true;
        }
        public bool GetByPhone(string Phone) //Getting the account associated with a phone
        {
            if (_accountCollection.Find(account => (account.Phone == Phone)).FirstOrDefault() != null) return true;
            return false;
        }
        public bool GetByPan(string Pan) //Getting the account associated with a Pan
        {
            if (_accountCollection.Find(account => (account.PanNumber == Pan)).FirstOrDefault() != null) return true;
            return false;
        }
        public bool GetByAadhar(string AadharNumber) //Getting the account associated with a phone
        {
            if (_accountCollection.Find(account => (account.AadharNumber == AadharNumber)).FirstOrDefault() != null) return true;
            return false;
        }
        // ForChecking the email
        public bool GetByEmail(string Email) //Getting the account associated with a phone
        {
            if (_accountCollection.Find(account => (account.Email == Email)).FirstOrDefault() != null) return true;
            return false;
        }

        public bool Update(string AccountNumber, double Balance) // Updating balance of the user
        {
            var UpdateUser = _accountCollection.UpdateOne(account => account.AccountNumber == AccountNumber, (Builders<AccountDetails>.Update.Inc("Balance", Balance)));
            if (UpdateUser == null) return false;
            return true;
        }
        public bool Update(string AccountNumber, string Key, string Value) // Updating key and values specific
        {
            var UpdateUser = _accountCollection.UpdateOne(account => account.AccountNumber == AccountNumber, (Builders<AccountDetails>.Update.Set(Key, Value)));
            if (UpdateUser == null) return false;
            return true;
        }
        public bool Update(string AccountNumber, Address address) //Updting address object
        {
            var UpdateAddress = _accountCollection.UpdateOne(account => account.AccountNumber == AccountNumber, ((Builders<AccountDetails>.Update.Set("Address", address))));
            if (UpdateAddress == null) return false;
            return true;
        }
        public AccountDetails Create(AccountDetails accountDetails) //Creating account number
        {
            _accountCollection.InsertOne(accountDetails);
            return Get(accountDetails.AccountNumber);
        }
    }
}