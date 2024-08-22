
namespace BankingSystem
{
    public class GetAllBranchesResponse
    {
        public string BankName { get; set; }
        public string Name { get; set; } = null!;
        public string IFSC { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;

        public GetAllBranchesResponse(BranchDetails branchDetails)
        {
            this.BankName = branchDetails.BankName;
            this.Name = branchDetails.Name;
            this.AddressLine = branchDetails.Address.AddressLine;
            this.City = branchDetails.Address.City;
            this.State = branchDetails.Address.State;
            this.PostalCode = branchDetails.Address.PostalCode;
            this.Country = branchDetails.Address.Country;
            this.IFSC = branchDetails.IFSC;
            this.Phone = branchDetails.Phone;
        }
    }
}