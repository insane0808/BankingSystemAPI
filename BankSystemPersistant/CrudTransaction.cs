using MongoDB.Driver;
namespace BankingSystem
{
    public class CrudTransaction
    {
        private IMongoCollection<TransactionDetails> _transactionCollection;

        public CrudTransaction()
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
            if (Environment.GetEnvironmentVariable("TRANSACTIONCOLLECTION") == null)
            {
                throw new ArgumentException($"No Transaction Collection Path Found.");
            }


            var MongoClient = new MongoClient(Environment.GetEnvironmentVariable("CLIENT"));
            var MongoDatabase = MongoClient.GetDatabase(Environment.GetEnvironmentVariable("DATABASE"));
            _transactionCollection = MongoDatabase.GetCollection<TransactionDetails>(Environment.GetEnvironmentVariable("TRANSACTIONCOLLECTION"));

        }
        // Getting transactiondetails from account number
        public TransactionDetails Get(string AccountNumber)
        {
            return _transactionCollection.Find(transaction => transaction.AccountNumber == AccountNumber).FirstOrDefault();
        }
        // Getting transaction details from transaction ID
        public TransactionDetails GetByTID(Int64 TransactionId)
        {
            return _transactionCollection.Find(transaction => transaction.TransactionId == TransactionId.ToString()).FirstOrDefault();
        }
        // Getting all transaction details 'from' a date 'to' a
        public List<TransactionDetails> Get(DateTime From, DateTime To, string AccountNumber)
        {
            return _transactionCollection.Find(transaction => ((transaction.AccountNumber == AccountNumber) && (transaction.DOT) >= From && transaction.DOT <= To.AddDays(1))).ToList();
        }
        // For checking if the transaction datetime is matching to any of the existing transaction
        public TransactionDetails Get(DateTime DOT)
        {
            return _transactionCollection.Find(transaction => transaction.DOT == DOT).FirstOrDefault();
        }
        // Getting all transactions of a account number
        public List<TransactionDetails> GetAll(string AccountNumber)
        {
            return _transactionCollection.Find(transaction => transaction.AccountNumber == AccountNumber).ToList();
        }
        // adding the deposit transaction to the collection
        public void Deposit(TransactionDetails transactionDetails)
        {
            _transactionCollection.InsertOne(transactionDetails);
        }
        // Adding withdrawl transaction to the service
        public void Withdrawl(TransactionDetails transactionDetails)
        {
            _transactionCollection.InsertOne(transactionDetails);
        }
    }
}