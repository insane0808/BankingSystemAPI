
namespace BankingSystem
{
    public class UpdatedAccountInformationRequest
    {
        public string AccountNumber { get; set; } = null!;
        public string? AccountType { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Phone { get; set; }
    }
}