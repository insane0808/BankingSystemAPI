using MongoDB.Driver;
namespace BankingSystem
{
    public class CrudBank
    {
        private IMongoCollection<BankDetails> _bankCollection;

        public CrudBank()
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
            if (Environment.GetEnvironmentVariable("BANKCOLLECTION") == null)
            {
                throw new ArgumentException($"No Bank Collection Path Found.");
            }


            var MongoClient = new MongoClient(Environment.GetEnvironmentVariable("CLIENT"));
            var MongoDatabase = MongoClient.GetDatabase(Environment.GetEnvironmentVariable("DATABASE"));
            _bankCollection = MongoDatabase.GetCollection<BankDetails>(Environment.GetEnvironmentVariable("BANKCOLLECTION"));

        }
        public BankDetails Get(int Code) // Getting bank details from the bank code
        {
            return _bankCollection.Find(Bank => Bank.Code == Code).FirstOrDefault();
        }
        public BankDetails Get(string IFSCHead) // Checking if any IFSC head is used by any other bank
        {
            return _bankCollection.Find(Bank => Bank.IFSCHead == IFSCHead).FirstOrDefault();
        }
        public bool Get(Address address) //For checking any bank's addressline
        {
            if (_bankCollection.Find(Bank => Bank.Address.AddressLine == address.AddressLine).FirstOrDefault() == null) return false;
            return true;
        }
        public bool GetOthers(Address address, int Code) //For checking any bank's addressline
        {
            if (_bankCollection.Find(Bank => (Bank.Code != Code) && (Bank.Address.AddressLine == address.AddressLine)).FirstOrDefault() == null) return false;
            return true;
        }
        public BankDetails GetByName(string Name) //For getting any bank's name present or not
        {
            return _bankCollection.Find(Bank => (Bank.Name == Name)).FirstOrDefault();

        }
        public bool Update(int Code, double Balance) //Updating the bank fund whenever needeed
        {
            var updatefund = _bankCollection.UpdateOne(bank => bank.Code == Code, (Builders<BankDetails>.Update.Inc("Funds", Balance)));
            if (updatefund == null) return false;
            return true;
        }
        public bool GetByEmail(string Email) //Getting the bank associated with a email
        {
            if (_bankCollection.Find(Bank => (Bank.Email == Email)).FirstOrDefault() != null) return true;
            return false;
        }
        public bool GetByPhone(string Phone) //Getting bank associated with the phone
        {
            if (_bankCollection.Find(Bank => (Bank.Phone == Phone)).FirstOrDefault() != null) return true;
            return false;
        }
        public BankDetails Create(BankDetails bankDetails) //Creating the bank
        {
            _bankCollection.InsertOne(bankDetails);
            return Get(bankDetails.Code);
        }
    }
}