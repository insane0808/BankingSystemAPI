using MongoDB.Driver;
namespace BankingSystem
{
    public class CrudBranch
    {
        private IMongoCollection<BranchDetails> _branchCollection;

        public CrudBranch()
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
            if (Environment.GetEnvironmentVariable("BRANCHCOLLECTION") == null)
            {
                throw new ArgumentException($"No Branch Collection Path Found.");
            }


            var MongoClient = new MongoClient(Environment.GetEnvironmentVariable("CLIENT"));
            var MongoDatabase = MongoClient.GetDatabase(Environment.GetEnvironmentVariable("DATABASE"));
            _branchCollection = MongoDatabase.GetCollection<BranchDetails>(Environment.GetEnvironmentVariable("BRANCHCOLLECTION"));

        }
        public BranchDetails GetByIFSC(string IFSC) // Getting branch by IFSC
        {
            return _branchCollection.Find(Branch => Branch.IFSC == IFSC).FirstOrDefault();
        }
        public bool Get(Address address) // Checking if the addressline of a branch is same
        {
            if (_branchCollection.Find(Branch => Branch.Address.AddressLine == address.AddressLine).FirstOrDefault() == null) return false;
            return true;
        }
        public bool GetByName(string Name) // checking the branch name as to be the unique
        {
            if (_branchCollection.Find(Branch => (Branch.Name == Name)).FirstOrDefault() != null) return true;
            return false;
        }
        public bool GetByPhone(string Phone) // No branch phones can be same
        {
            if (_branchCollection.Find(Branch => (Branch.Phone == Phone)).FirstOrDefault() != null) return true;
            return false;
        }
        public List<BranchDetails> Get(int Code) // Getting all branches from the bank code
        {
            return _branchCollection.Find(branch => branch.BankCode == Code).ToList();
        }
        // Creating a branch in the collection
        public BranchDetails Create(BranchDetails branchDetails)
        {
            _branchCollection.InsertOne(branchDetails);
            return GetByIFSC(branchDetails.IFSC); // Sending back as the branch response
        }
    }
}