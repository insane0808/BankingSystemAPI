using System.Text.RegularExpressions;
namespace BankingSystem
{
    public class UpdatedAccountInformationService
    {
        public UpdatedAccountInformationResponse UpdatedAccountInformation(UpdatedAccountInformationRequest updatedAccountInformationRequest)
        {
            // Checking if the request object is null
            if (updatedAccountInformationRequest == null) throw new ArgumentException($"Please Provide Details");
            if (updatedAccountInformationRequest.AccountNumber == null) throw new ArgumentException($"Please Enter Account Number");
            if (updatedAccountInformationRequest.AccountNumber.Length < 9 || updatedAccountInformationRequest.AccountNumber.Length > 18 || !updatedAccountInformationRequest.AccountNumber.All(char.IsDigit)) throw new ArgumentException($"Account Number is Not Valid");
            if (updatedAccountInformationRequest.AccountType == null
              && updatedAccountInformationRequest.AddressLine == null
              && updatedAccountInformationRequest.City == null
              && updatedAccountInformationRequest.State == null
              && updatedAccountInformationRequest.PostalCode == null
              && updatedAccountInformationRequest.Phone == null
              ) throw new ArgumentException($"Please Enter Any Details To Update in Your Account");
            // General Validations with Database as well

            CrudAccount crudAccount = new CrudAccount();
            CrudBranch crudBranch = new CrudBranch();
            var AccountInformation = crudAccount.Get(updatedAccountInformationRequest.AccountNumber);
            bool AnythingUpdated = false;
            if (AccountInformation == null) throw new ArgumentException($" Account Number {updatedAccountInformationRequest.AccountNumber}'s User Doesn't Exist.");

            // If account Type is to be updated
            if (updatedAccountInformationRequest.AccountType != null && updatedAccountInformationRequest.AccountType != AccountInformation.AccountType)
            {
                updatedAccountInformationRequest.AccountType = updatedAccountInformationRequest.AccountType.Trim();
                if (updatedAccountInformationRequest.AccountType.Length < 6 || updatedAccountInformationRequest.AccountType.Length > 7 || !(Enum.IsDefined(typeof(AccountTypes), updatedAccountInformationRequest.AccountType.ToLower()))) throw new ArgumentException($" Account Type {updatedAccountInformationRequest.AccountType} is Not Valid. We Only Offer Savings, Current & Salary");
                if (!crudAccount.Update(updatedAccountInformationRequest.AccountNumber, "AccountType", updatedAccountInformationRequest.AccountType)) throw new ArgumentException($" Unwanted Error On Updating Account Type");
                AnythingUpdated = true;
            }

            // If Address is to be updated
            if (updatedAccountInformationRequest.AddressLine != null && updatedAccountInformationRequest.AddressLine != AccountInformation.Address.AddressLine)
            {
                updatedAccountInformationRequest.AddressLine = updatedAccountInformationRequest.AddressLine.Trim();
                if (updatedAccountInformationRequest.AddressLine == null || updatedAccountInformationRequest.AddressLine.Length <= 1 || updatedAccountInformationRequest.AddressLine.Length >= 30) throw new ArgumentException($" Address Line {updatedAccountInformationRequest.AddressLine} is Not Valid.");
                Address address = AccountInformation.Address;
                address.AddressLine = updatedAccountInformationRequest.AddressLine;
                if (!crudAccount.Update(updatedAccountInformationRequest.AccountNumber, address)) throw new ArgumentException($" Unwanted Error On Updating AddressLine");
                AnythingUpdated = true;
            }

            // If Address is to be updated
            if (updatedAccountInformationRequest.City != null && updatedAccountInformationRequest.City != AccountInformation.Address.City)
            {
                updatedAccountInformationRequest.City = updatedAccountInformationRequest.City.Trim();
                if (updatedAccountInformationRequest.City == null || updatedAccountInformationRequest.City.Length <= 2 || updatedAccountInformationRequest.City.Length >= 20 || updatedAccountInformationRequest.City.Any(char.IsDigit) || updatedAccountInformationRequest.City.Any(char.IsSymbol)) throw new ArgumentException($"Please Enter Valid City");
                Address address = AccountInformation.Address;
                address.City = updatedAccountInformationRequest.City;
                if (!crudAccount.Update(updatedAccountInformationRequest.AccountNumber, address)) throw new ArgumentException($" Unwanted Error On Updating City");
                AnythingUpdated = true;
            }

            // If Address is to be updated
            if (updatedAccountInformationRequest.State != null && updatedAccountInformationRequest.State != AccountInformation.Address.State)
            {
                updatedAccountInformationRequest.State = updatedAccountInformationRequest.State.Trim();
                if (updatedAccountInformationRequest.State == null || updatedAccountInformationRequest.State.Length <= 2 || updatedAccountInformationRequest.State.Length >= 28 || updatedAccountInformationRequest.State.Any(char.IsDigit) || updatedAccountInformationRequest.State.Any(char.IsSymbol)) throw new ArgumentException($"Please Enter Valid State");
                Address address = AccountInformation.Address;
                address.State = updatedAccountInformationRequest.State;
                if (!crudAccount.Update(updatedAccountInformationRequest.AccountNumber, address)) throw new ArgumentException($" Unwanted Error On Updating State");
                AnythingUpdated = true;
            }

            // If Address is to be updated
            if (updatedAccountInformationRequest.PostalCode != null && updatedAccountInformationRequest.PostalCode != AccountInformation.Address.PostalCode)
            {
                updatedAccountInformationRequest.PostalCode = updatedAccountInformationRequest.PostalCode.Trim();
                if (updatedAccountInformationRequest.PostalCode == null || !(new Regex("^[1-9]{1}[0-9]{2}\\s{0,1}[0-9]{3}$")).IsMatch(updatedAccountInformationRequest.PostalCode)) throw new ArgumentException($"Not A Valid Postal Code");
                Address address = AccountInformation.Address;
                address.PostalCode = updatedAccountInformationRequest.PostalCode;
                if (!crudAccount.Update(updatedAccountInformationRequest.AccountNumber, address)) throw new ArgumentException($" Unwanted Error On Updating Postal Code");
                AnythingUpdated = true;
            }

            // If Phone is to be updated
            if (updatedAccountInformationRequest.Phone != null && updatedAccountInformationRequest.Phone != AccountInformation.Phone)
            {
                updatedAccountInformationRequest.Phone = updatedAccountInformationRequest.Phone.Trim();
                if (!(new Regex(@"(\+91)?(-)?\s*?(91)?\s*?(\d{3})-?\s*?(\d{3})-?\s*?(\d{4})").IsMatch(updatedAccountInformationRequest.Phone))) throw new ArgumentException($" {updatedAccountInformationRequest.Phone} is Not a Valid Phone Number");
                if (!crudAccount.Update(updatedAccountInformationRequest.AccountNumber, "Phone", updatedAccountInformationRequest.Phone)) throw new ArgumentException($" Unwanted Error On Updating Phone Number");
                AnythingUpdated = true;
            }
            if (!AnythingUpdated) throw new ArgumentException($"Please Enter Something to Update, EveryThing looks Same");

            // Response validation
            AccountInformation = crudAccount.Get(updatedAccountInformationRequest.AccountNumber);
            var GetBranch = crudBranch.GetByIFSC(AccountInformation.IFSC);
            UpdatedAccountInformationResponse updatedAccountInformationResponse = new UpdatedAccountInformationResponse(AccountInformation, GetBranch.BankName, GetBranch.Name);
            return updatedAccountInformationResponse;
        }
    }
}