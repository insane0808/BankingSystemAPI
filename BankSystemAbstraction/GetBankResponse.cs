

namespace BankingSystem
{
    public class GetBankResponse
    {
        public string Name { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public GetBankResponse(BankDetails bankDetails)
        {
            this.Name = bankDetails.Name;
            if (bankDetails.Address != null)
            {
                this.AddressLine = bankDetails.Address.AddressLine;
                this.City = bankDetails.Address.City;
                this.State = bankDetails.Address.State;
                this.PostalCode = bankDetails.Address.PostalCode;
                this.Country = bankDetails.Address.Country;
            }
            this.Email = bankDetails.Email;
            this.Phone = bankDetails.Phone;
        }
    }
}