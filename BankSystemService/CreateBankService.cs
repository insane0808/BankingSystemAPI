using System.Text.RegularExpressions;
namespace BankingSystem
{
    public class CreateBankService
    {
        public CreateBankResponse CreateBank(CreateBankRequest createBankRequest)
        {
            // Null Validations
            if (createBankRequest == null) throw new ArgumentException($"Please Fill All Details Of Your Bank.");
            if (createBankRequest.IFSCHead == null) throw new ArgumentException($"Please Enter IFSC HEADER for branches IFSC codes.");
            if (createBankRequest.Name == null) throw new ArgumentException($"Please Enter Name of Your Bank");
            if (createBankRequest.Email == null) throw new ArgumentException($"Please Enter Email Of Your Bank");
            if (createBankRequest.AddressLine == null) throw new ArgumentException($"Please Enter AddressLine Of Your Bank");
            if (createBankRequest.City == null) throw new ArgumentException($"Please Enter City Of Your Bank");
            if (createBankRequest.Country == null) throw new ArgumentException($"Please Enter Country Of Your Bank");
            if (createBankRequest.State == null) throw new ArgumentException($"Please Enter State Of Your Bank");
            if (createBankRequest.PostalCode == null) throw new ArgumentException($"Please Enter PostalCode Of Your Bank");
            if (createBankRequest.Phone == null) throw new ArgumentException($"Please Enter Phone Of Your Bank");

            // White Space validations And Trimming
            createBankRequest.IFSCHead = createBankRequest.IFSCHead.Trim();
            createBankRequest.Name = createBankRequest.Name.Trim();
            createBankRequest.Email = createBankRequest.Email.Trim();
            createBankRequest.AddressLine = createBankRequest.AddressLine.Trim();
            createBankRequest.City = createBankRequest.City.Trim();
            createBankRequest.Country = createBankRequest.Country.Trim();
            createBankRequest.State = createBankRequest.State.Trim();
            createBankRequest.Phone = createBankRequest.Phone.Trim();

            // General Validations
            if (createBankRequest.Name.Length < 5 || createBankRequest.Name.Length >= 50 || createBankRequest.Name.Any(char.IsSymbol) || createBankRequest.Name.Any(char.IsDigit) ) throw new ArgumentException($"{createBankRequest.Name} Name is INVALID!");
            
            if (createBankRequest.Name.Contains(' ') && !createBankRequest.Name.Split(' ').All(w => w.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' ')))throw new ArgumentException($"{createBankRequest.Name} Name is INVALID!");
            else if(!createBankRequest.Name.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' '))throw new ArgumentException($"{createBankRequest.Name} Name is INVALID!");

            if (!(new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))[a-zA-Z]{2,4}(\]?)$")).IsMatch(createBankRequest.Email)) throw new ArgumentException($"Email Is Not Valid : {createBankRequest.Email}");
            if (!(new Regex(@"(\+91)?(-)?\s*?(91)?\s*?(\d{3})-?\s*?(\d{3})-?\s*?(\d{4})").IsMatch(createBankRequest.Phone))) throw new ArgumentException($"{createBankRequest.Phone} is Not a Valid Phone Number");
            if (createBankRequest.Funds <= 999 || createBankRequest.Funds > 10000000000) throw new ArgumentException($"Bank's Fund {createBankRequest.Funds} is Not Valid");
            if (createBankRequest.IFSCHead.Length < 4 || createBankRequest.IFSCHead.Length > 4 || !createBankRequest.IFSCHead.All(char.IsLetter)) throw new ArgumentException($"{createBankRequest.IFSCHead} is Not Valid Please Enter 4 Unique Characters of IFSCHead For Branch's IFSC Codes");

            createBankRequest.IFSCHead = createBankRequest.IFSCHead.ToUpper();
            //Generations and More Validations at database end

            CrudBank crudBank = new CrudBank();
            CrudBranch crudBranch = new CrudBranch();
            CrudAccount crudAccount = new CrudAccount();

            // Code generation and Validation
            if (createBankRequest.Name.Any(char.IsWhiteSpace))
            {

                if (!createBankRequest.Name.ToLower().Split(' ').Any(w => w == "bank")) createBankRequest.Name += " Bank";
            }
            else
            {
                createBankRequest.Name += " Bank";
            }


            // Checking if the bank name or email or phone is used by any other existing
            if (crudBank.GetByName(createBankRequest.Name) != null) throw new ArgumentException($"Here Bank's Name is already used by a bank.");
            if (crudBank.GetByPhone(createBankRequest.Phone)) throw new ArgumentException($"Here Bank's Phone is already used by a bank.");
            if (crudBank.Get(createBankRequest.IFSCHead) != null) throw new ArgumentException($"Here Bank's IFSCHead is already used by a bank.");
            if (crudBank.GetByEmail(createBankRequest.Email)) throw new ArgumentException($"Here Bank's Email is already used by a bank.");


            // Generating the code
            int Code = new Random().Next(1000000, 9999999);
            var IsBankPresent = crudBank.Get(Code);
            int Check = 111111;
            while (IsBankPresent != null && Check < 1000000)
            {
                Code = new Random().Next(111111, 999999);
                IsBankPresent = crudBank.Get(Code);
                Check++;
            }

            // Sending in another Format
            BankDetails bankDetails = new BankDetails(createBankRequest, Code);
            // Some more Validations from local and from Database
            if (!ValidateAddress(bankDetails.Address)) throw new ArgumentException($"Please Check Your Address");
            if (crudBank.Get(bankDetails.Address)) throw new ArgumentException($"This Address is Already used by a Bank");
            if (crudBranch.Get(bankDetails.Address)) throw new ArgumentException($"This Address is Already Used by a Branch");
            if (crudAccount.Get(bankDetails.Address)) throw new ArgumentException($"This Address is Already Used by a Account");
            var Inserted = crudBank.Create(bankDetails);   //
            int CheckInserted = 1;                                     // Logic for checkig if:
            while (Inserted == null && CheckInserted < 3)
            {            //   The Bank is created or not It will try 3 times
                Inserted = crudBank.Create(bankDetails);   //
                CheckInserted++;                                       //
            }                                                  // Otherwise we will send exception message
            if (Check == 3) throw new ArgumentException($"Data Not Updated due to Internal Error");


            // Now Creating a Branch That will be associated to a Home Bank
            CreateBranchService createBranchService = new CreateBranchService();
            // Create Branch Request and data initiallizing
            CreateBranchRequest createBranchRequest = new CreateBranchRequest();
            createBranchRequest.BankName = createBankRequest.Name;
            createBranchRequest.Name = "Main " + createBankRequest.AddressLine;
            createBranchRequest.Phone = createBankRequest.Phone;
            createBranchRequest.AddressLine = createBankRequest.AddressLine;
            createBranchRequest.City = createBankRequest.City;
            createBranchRequest.State = createBankRequest.State;
            createBranchRequest.PostalCode = createBankRequest.PostalCode;
            createBranchRequest.Country = createBankRequest.Country;
            //Checking the insertion
            try
            {
                CreateBranchResponse createBranchResponse = createBranchService.CreateBranch(createBranchRequest);
            }
            catch
            {
                throw new ArgumentException($"Main Branch Is Not Created Please Try Again");
            }


            // Now sending data in another format
            CreateBankResponse createBankResponse = new CreateBankResponse(bankDetails);
            return createBankResponse; //Sending back result
        }

        // Validating Address
        bool ValidateAddress(Address address)
        {
            if (address.AddressLine.Length <= 1 || address.AddressLine.Length >= 30 || (address.AddressLine.Contains(' ') && !address.AddressLine.Split(' ').All(w => w.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' ' || c == '-' || c == ',' || c == '.')))) throw new ArgumentException($"Address Line: {address.AddressLine} is Invalid!");
            else if(!address.AddressLine.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' ' || c == '-' || c == ',' || c == '.'))throw new ArgumentException($"Address Line: {address.AddressLine} is INVALID!");
            if(address.AddressLine == address.City || address.AddressLine == address.State || address.AddressLine == address.Country) throw new ArgumentException($"Address Line Sould be Unique then other fields of Address");

            if (address.City.Length <= 2 || address.City.Length >= 10 || address.City.Any(char.IsDigit) || (address.City.Contains(' ') && !address.City.Split(' ').All(w => w.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' ' || c == '-' || c == ',' || c == '.')))) throw new ArgumentException($"Address City: {address.City} is Invalid!");
            else if(!address.City.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' ' || c == '-' || c == ',' || c == '.'))throw new ArgumentException($"Address City: {address.City} is INVALID!");
            if (address.City == address.State || address.City == address.Country) throw new ArgumentException($"Address City Sould be Unique then other fields of Address");;

            if (address.State.Length <= 2 || address.State.Length >= 28 || address.State.Any(char.IsDigit) || address.State.Any(char.IsSymbol) || (address.State.Contains(' ') && !address.State.Split(' ').All(w => w.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' ' || c == '-' || c == ',' || c == '.'))))throw new ArgumentException($"Address State: {address.State} is INVALID!");
            if(address.State == address.Country) throw new ArgumentException($"Address State Sould be Unique then other fields of Address");

            if (!(new Regex("^[1-9]{1}[0-9]{2}\\s{0,1}[0-9]{3}$")).IsMatch(address.PostalCode)) throw new ArgumentException($"Address PostalCode: {address.PostalCode} is INVALID!");

            if ( address.Country.Length <= 2 || address.Country.Length >= 57 || address.Country.Any(char.IsDigit) || address.Country.Any(char.IsSymbol) || (address.Country.Contains(' ') && !address.Country.Split(' ').All(w => w.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' ' || c == '-' || c == ',' || c == '.'))))throw new ArgumentException($"Address Country: {address.Country} is INVALID!");
            return true;
        }
    }
}