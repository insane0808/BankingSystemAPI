using MongoDB.Bson.Serialization.Attributes;
namespace BankingSystem
{
    //Bank Data Schema
    public class CreateBankRequest
    {
        // For Branch Ifsc Calculations
        public string IFSCHead { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public double Funds { get; set; }
    }

}