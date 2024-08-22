using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem
{
    public class CreateAccountRequest
    {
        public string IFSC { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string FathersName { get; set; } = null!;
        public string AccountType { get; set; } = null!;
        public string AadharNumber { get; set; } = null!;
        public string? PanNumber { get; set; }
        public string Gender { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string DOB { get; set; } = null!;
        public double Balance { get; set; }
        public string Phone { get; set; } = null!;
    }
}