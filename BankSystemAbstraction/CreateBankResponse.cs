using MongoDB.Bson.Serialization.Attributes;
namespace BankingSystem
{
    //Bank Data Schema
    public class CreateBankResponse
    {
        public int Code { get; set; }
        public string Name { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public double Funds { get; set; }

        public CreateBankResponse(BankDetails bankDetails)
        {
            this.Code = bankDetails.Code;
            this.Name = bankDetails.Name;
            // For removing null exception
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
            this.Funds = bankDetails.Funds;
        }
    }

}