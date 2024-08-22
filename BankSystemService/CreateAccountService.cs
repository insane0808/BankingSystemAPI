using System.Text.RegularExpressions;
namespace BankingSystem
{
    public class CreateAccountService
    {
        public CreateAccountResponse CreateAccount(CreateAccountRequest createAccountRequest)
        {
            // Null Validations
            if (createAccountRequest == null) throw new ArgumentException($"Please Fill All Valid Details Of Your Account.");
            if (createAccountRequest.IFSC == null) throw new ArgumentException($"Please Enter IFSC Code Of Your Branch");
            if (createAccountRequest.Name == null) throw new ArgumentException($"Please Enter Your Name");
            if (createAccountRequest.FathersName == null) throw new ArgumentException($"Please Enter Your Father's Name");
            if (createAccountRequest.AccountType == null) throw new ArgumentException($"Please Enter Account Type of Your Account");
            if (createAccountRequest.PanNumber == null) throw new ArgumentException($"Please Enter Your Pan Number");
            if (createAccountRequest.AadharNumber == null) throw new ArgumentException($"Please Enter Your Aadhar Number");
            if (createAccountRequest.Gender == null) throw new ArgumentException($"Please Enter Your Gender");
            if (createAccountRequest.AddressLine == null) throw new ArgumentException($"Please Enter AddressLine Of Your Home");
            if (createAccountRequest.City == null) throw new ArgumentException($"Please Enter City Of Your Home");
            if (createAccountRequest.Country == null) throw new ArgumentException($"Please Enter Country Where you Live");
            if (createAccountRequest.State == null) throw new ArgumentException($"Please Enter State Of Your Address");
            if (createAccountRequest.PostalCode == null) throw new ArgumentException($"Please Enter Postal Code Of Your Area");
            if (createAccountRequest.Email == null) throw new ArgumentException($"Please Enter Your Email Address");
            if (createAccountRequest.Phone == null) throw new ArgumentException($"Please Enter Your Phone Number");
            if (createAccountRequest.Balance <= 99 || createAccountRequest.Balance > 100000) throw new ArgumentException($"Please Enter Minimum Banalnce 100 or Maximum Balance 1 Lac.");
            if (createAccountRequest.DOB == null) throw new ArgumentException($"Please Enter Your Date Of Birth(DOB).");

            // White Space validations And Trimming
            createAccountRequest.IFSC = createAccountRequest.IFSC.Trim();
            createAccountRequest.FathersName = createAccountRequest.FathersName.Trim();
            createAccountRequest.AccountType = createAccountRequest.AccountType.Trim();
            createAccountRequest.PanNumber = createAccountRequest.PanNumber.Trim();
            createAccountRequest.AadharNumber = createAccountRequest.AadharNumber.Trim();
            createAccountRequest.Gender = createAccountRequest.Gender.Trim();
            createAccountRequest.Email = createAccountRequest.Email.Trim();
            createAccountRequest.Name = createAccountRequest.Name.Trim();
            createAccountRequest.AddressLine = createAccountRequest.AddressLine.Trim();
            createAccountRequest.City = createAccountRequest.City.Trim();
            createAccountRequest.Country = createAccountRequest.Country.Trim();
            createAccountRequest.State = createAccountRequest.State.Trim();
            createAccountRequest.Phone = createAccountRequest.Phone.Trim();
            createAccountRequest.DOB = createAccountRequest.DOB.Trim();
            var DOB = DateTime.Parse(createAccountRequest.DOB);

            // General Validations
            if (createAccountRequest.IFSC.Length < 11 || createAccountRequest.IFSC.Length > 11 || createAccountRequest.IFSC.Any(char.IsSymbol)) throw new ArgumentException($"IFSC Code: {createAccountRequest.IFSC} is Not Valid Please Enter Your Branch's IFSC Code");
            if (createAccountRequest.Name.Length <= 5 || createAccountRequest.Name.Length >= 50 || createAccountRequest.Name.Any(char.IsSymbol) || createAccountRequest.Name.Any(char.IsDigit)) throw new ArgumentException($"Name : {createAccountRequest.Name} is Invalid!");
            if (createAccountRequest.Name.Contains(' ') && !createAccountRequest.Name.Split(' ').All(w => w.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' ')))throw new ArgumentException($"{createAccountRequest.Name} Name is INVALID!");
            else if(!createAccountRequest.Name.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' '))throw new ArgumentException($"{createAccountRequest.Name} Name is INVALID!");

            if (createAccountRequest.FathersName.Length <= 5 || createAccountRequest.FathersName.Length >= 50 || createAccountRequest.FathersName.Any(char.IsSymbol) || createAccountRequest.FathersName.Any(char.IsDigit) || createAccountRequest.FathersName == createAccountRequest.Name) throw new ArgumentException($"Fathers Name {createAccountRequest.FathersName} is Invalid. Enter Full Name Of Your Father");
            if (createAccountRequest.FathersName.Contains(' ') && !createAccountRequest.FathersName.Split(' ').All(w => w.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' ')))throw new ArgumentException($"Fathers Name {createAccountRequest.FathersName} is Invalid. Enter Full Name Of Your Father");
            else if(!createAccountRequest.FathersName.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' '))throw new ArgumentException($"Fathers Name {createAccountRequest.FathersName} is Invalid. Enter Full Name Of Your Father");

            if (!(new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))[a-zA-Z]{2,4}(\]?)$")).IsMatch(createAccountRequest.Email)) throw new ArgumentException($"Email Is Not Valid : {createAccountRequest.Email}");
            if (!(new Regex(@"(\+91)?(-)?\s*?(91)?\s*?(\d{3})-?\s*?(\d{3})-?\s*?(\d{4})").IsMatch(createAccountRequest.Phone))) throw new ArgumentException($"Phone : {createAccountRequest.Phone} is Invalid!");
            if (createAccountRequest.AccountType.Length < 6 || createAccountRequest.AccountType.Length > 7 || !(Enum.IsDefined(typeof(AccountTypes), createAccountRequest.AccountType.ToLower()))) throw new ArgumentException($"Account Type: {createAccountRequest.AccountType} is Not Valid. We Only Offer Savings, Current & Salary");
            if (createAccountRequest.PanNumber.Length < 10 || createAccountRequest.PanNumber.Length > 10 || createAccountRequest.PanNumber.Any(char.IsSymbol) || !createAccountRequest.Name.ToLower().Split(' ').Any(word => word.ToLower()[0] == createAccountRequest.PanNumber.ToLower()[4]) || !(new Regex("^[A-Z]{5}[0-9]{4}[A-Z]{1}$").IsMatch(createAccountRequest.PanNumber))) throw new ArgumentException($"Pan Number : {createAccountRequest.PanNumber} is Invalid, Please Enter Your Valid Pan Number");
            if (createAccountRequest.AadharNumber.Length < 12 || createAccountRequest.AadharNumber.Length > 12 || !createAccountRequest.AadharNumber.All(char.IsDigit)) throw new ArgumentException($"Aadhar Number : {createAccountRequest.AadharNumber} is Invalid, Please Enter A Valid 12 Charachter Aadhar Number.");
            if (!(Enum.IsDefined(typeof(Gender), createAccountRequest.Gender.ToLower()))) throw new ArgumentException($"Your Gender : {createAccountRequest.Gender} is Invalid, we offer for : male(m), female(m), transgender(t)");
            if (DOB.Month < 1 || DOB.Day < 1 || DOB.Day > 31 || (DateTime.Today.Date - DOB.Date).TotalDays <= 365 || DOB.Month > 12 || DOB.Year < DateTime.Today.Year - 150 || DOB.Year > DateTime.Today.Year) throw new ArgumentException($"Your Date Of Birth : {createAccountRequest.DOB} is Invalid, Age Of The Person Must be 1 - 150years");


            //Generations and More Validations at database end
            CrudBank crudBank = new CrudBank(); //For Bank Validation
            CrudBranch crudBranch = new CrudBranch();
            CrudAccount crudAccount = new CrudAccount();


            // Checking for proof
            var GetBranch = crudBranch.GetByIFSC(createAccountRequest.IFSC);
            if (GetBranch == null) throw new ArgumentException($"There is No Branch Found With This IFSC code");
            var GetBank = crudBank.Get(GetBranch.BankCode);
            if (GetBank == null) throw new ArgumentException($"There is No Bank Associated With The Branch Contact Administrator");

            // Advanced Validations
            if (crudBranch.GetByName(createAccountRequest.Name)) throw new ArgumentException($"There is an existing Branch Using to this Name");
            if (crudBank.GetByName(createAccountRequest.Name) != null) throw new ArgumentException($"There is an existing Bank Using to this Name");
            if (crudAccount.GetByPhone(createAccountRequest.Phone)) throw new ArgumentException($"There is an existing account linked to this Phone Number.");
            if (crudAccount.GetByAadhar(createAccountRequest.AadharNumber)) throw new ArgumentException($"There is an existing account linked to this AadharNumber");
            if (crudAccount.GetByPan(createAccountRequest.PanNumber)) throw new ArgumentException($"There is an existing account linked to this Pan Number");
            if (crudAccount.GetByEmail(createAccountRequest.Email)) throw new ArgumentException($"There is an existing account linked to this Email");
            if (crudBank.GetByEmail(createAccountRequest.Email)) throw new ArgumentException($"You Can't Use the Email of A existing Bank.");
            if (crudBranch.GetByPhone(createAccountRequest.Phone)) throw new ArgumentException($"You Can't Use the Phone of A existing Branch.");
            // Code generation and Validation
            Int64 AccountNumber = new Random().NextInt64(222222222222, 999999999999);
            var AccountPresent = crudAccount.Get(AccountNumber.ToString());
            Int64 Check = 222222222222;
            while (AccountPresent != null && Check < 999999999999)
            {
                AccountNumber = new Random().NextInt64(222222222222, 999999999999);
                AccountPresent = crudAccount.Get(AccountNumber.ToString());
                Check++;
            }

            // Sending in another Format
            AccountDetails accountDetails = new AccountDetails(createAccountRequest, AccountNumber);
            // Some more Validations from local and from Database
            if (!ValidateAddress(accountDetails.Address)) throw new ArgumentException($"Please Check All Fields Of Your Address");
            // Now Getting Bank To Update Fund
            if(crudBranch.Get(accountDetails.Address)) throw new ArgumentException($"A existing Branch is at Same Address Line. Please Provide different AddressLine");
            if (!crudBank.Update(GetBank.Code, accountDetails.Balance)) throw new ArgumentException($"The Bank Fund Is Not Updated.");
            var Inserted = crudAccount.Create(accountDetails);   //
            int CheckInserted = 1;                                     // Logic for checkig if:
            while (Inserted == null && CheckInserted < 3)
            {            //   The Account is created or not It will try 3 times
                Inserted = crudAccount.Create(accountDetails);   //
                CheckInserted++;                                       //
            }                                                  // Otherwise we will send exception message
            if (Check == 3)
            {
                if (!crudBank.Update(GetBank.Code, -accountDetails.Balance)) throw new ArgumentException($"The Bank Fund Is Not Updated.");
                throw new ArgumentException($"Data Not Updated due to Internal Error");
            }
            
            // Now sending data in another format
            CreateAccountResponse createAccountResponse = new CreateAccountResponse(accountDetails, GetBank.Name, GetBranch.Name);
            return createAccountResponse; //Sending back results
        }
        // Validating Address
        bool ValidateAddress(Address address)
        {
            if (address == null)throw new ArgumentException($"Please Enter Full Address");
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