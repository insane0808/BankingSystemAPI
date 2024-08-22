using System.Text.RegularExpressions;
namespace BankingSystem
{
    public class CreateBranchService
    {
        public CreateBranchResponse CreateBranch(CreateBranchRequest createBranchRequest)
        {
            // Null Validations
            if (createBranchRequest == null) throw new ArgumentException($"Please Enter All Valid Details Of Your Branch");
            if (createBranchRequest.BankName == null) throw new ArgumentException($"Please Enter Name Of Your Bank");
            if (createBranchRequest.Name == null) throw new ArgumentException($"Please Enter Name Of Your Branch");
            if (createBranchRequest.AddressLine == null) throw new ArgumentException($"Please Enter AddressLine Of Your Branch");
            if (createBranchRequest.City == null) throw new ArgumentException($"Please Enter City Of Your Branch");
            if (createBranchRequest.State == null) throw new ArgumentException($"Please Enter State Of Your Branch");
            if (createBranchRequest.PostalCode == null) throw new ArgumentException($"Please Enter Postal Code Of Your Branch");
            if (createBranchRequest.Country == null) throw new ArgumentException($"Please Enter Country Of Your Branch");
            if (createBranchRequest.Phone == null) throw new ArgumentException($"Please Enter Phone Of Your Branch");


            // White Space validations And Trimming
            createBranchRequest.BankName = createBranchRequest.BankName.Trim();
            createBranchRequest.Name = createBranchRequest.Name.Trim();
            createBranchRequest.AddressLine = createBranchRequest.AddressLine.Trim();
            createBranchRequest.City = createBranchRequest.City.Trim();
            createBranchRequest.Country = createBranchRequest.Country.Trim();
            createBranchRequest.State = createBranchRequest.State.Trim();
            createBranchRequest.Phone = createBranchRequest.Phone.Trim();


            // General Validations
            if (createBranchRequest.BankName.Length <= 5 || createBranchRequest.BankName.Length >= 50 || createBranchRequest.BankName.Any(char.IsSymbol) || createBranchRequest.BankName.Any(char.IsDigit)) throw new ArgumentException($"The Branch's Bank Name : {createBranchRequest.BankName}, is Not Valid");
            if (createBranchRequest.BankName.Contains(' ') && !createBranchRequest.BankName.Split(' ').All(w => w.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' ')))throw new ArgumentException($"The Branch's Bank Name : {createBranchRequest.BankName}, is Not Valid");
            else if(!createBranchRequest.BankName.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' '))throw new ArgumentException($"The Branch's Bank Name : {createBranchRequest.BankName}, is Not Valid");


            if (createBranchRequest.Name.Length <= 5 || createBranchRequest.Name.Length >= 50 || createBranchRequest.Name.Any(char.IsSymbol) || createBranchRequest.Name.Any(char.IsDigit)) throw new ArgumentException($"The Branch Name : {createBranchRequest.Name}, is Not Valid");
            if (createBranchRequest.Name.Contains(' ') && !createBranchRequest.Name.Split(' ').All(w => w.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' ')))throw new ArgumentException($"The Branch Name : {createBranchRequest.Name}, is Not Valid");
            else if(!createBranchRequest.Name.All(c=>Char.IsLetterOrDigit(c) || c=='_' || c == ' '))throw new ArgumentException($"The Branch Name : {createBranchRequest.Name}, is Not Valid");


            if (!(new Regex(@"(\+91)?(-)?\s*?(91)?\s*?(\d{3})-?\s*?(\d{3})-?\s*?(\d{4})").IsMatch(createBranchRequest.Phone))) throw new ArgumentException($"{createBranchRequest.Phone} is Not a Valid Phone Number");


            //Generations and More Validations at database end
            CrudBranch crudBranch = new CrudBranch();
            CrudBank crudBank = new CrudBank(); //For Bank Validation
            CrudAccount crudAccount = new CrudAccount();


            // Checking if the bank name or email or phone is used by any other existing
            if (crudBank.GetByName(createBranchRequest.Name) != null) throw new ArgumentException($"The Name Of the Branch is already used by a existing Bank.");
            if (crudBranch.GetByName(createBranchRequest.Name)) throw new ArgumentException($"The Name Of the Branch provided already used by a existing Branch.");
            var BankPresent = crudBank.GetByName(createBranchRequest.BankName);
            if (BankPresent == null) throw new ArgumentException($"No Bank Present With The Bank Name");
            if (crudBranch.GetByPhone(createBranchRequest.Phone)) throw new ArgumentException($"Here Branch's Phone is already used by a existing branch");
            if (crudAccount.GetByPhone(createBranchRequest.Phone)) throw new ArgumentException($"Here Branch's Phone is already used by a existing User");
            if (crudAccount.Get(createBranchRequest.Name) != null) throw new ArgumentException($"Here Your Branch Name is already used by a existing User");
            
            // generating IFSC
            string IFSC = BankPresent.IFSCHead + '0';        // Making 5th number as 0 according to rule
            int Code = new Random().Next(111111, 999999);    // Generating next 6 numbers to make a valid IFSC
            string tempIFSC = IFSC + Code.ToString();        // Generating a temporary code to check whether the code is accepted nby the branch
            var GetIFSC = crudBranch.GetByIFSC(tempIFSC);    // Checking whether the IFSC is used by any previous branch
            int CheckIFSCTimes = 999999;
            while (GetIFSC != null && CheckIFSCTimes > 111111)                           // Untill we get null
            {                                                //
                Code = new Random().Next(100000, 999999);    // Again trying to achieve untill we get an IFSC for our branch
                tempIFSC = IFSC + Code.ToString();           // Assigning temp to check
                GetIFSC = crudBranch.GetByIFSC(tempIFSC);    // Again Checking with the Bank
                CheckIFSCTimes--;
            }
            if (GetIFSC == null) IFSC = tempIFSC;             // Now assigning the IFSC
            else throw new ArgumentException($"No IFSC code is Available");


            // Another Format

            BranchDetails branchDetails = new BranchDetails(createBranchRequest, IFSC, BankPresent.Code);
            // Some more Validations from local and from Database
            if (!ValidateAddress(branchDetails.Address)) throw new ArgumentException($"Please Check Your Address");
            if (crudBranch.Get(branchDetails.Address)) throw new ArgumentException($"A existing Branch is at the same AddressLine, Please Provide a different Address Line");
            if(crudBank.GetOthers(branchDetails.Address, branchDetails.BankCode)) throw new ArgumentException($"A existing Bank is already at the same address Line, Please provide different Address Line");
            if (crudAccount.Get(branchDetails.Address)) throw new ArgumentException($"A existing User is at the same AddressLine, Please Provide a different Address Line");
            var Inserted = crudBranch.Create(branchDetails);   //
            int CheckBranchInserted = 1;                                     // Logic for checkig if:
            while (Inserted == null && CheckBranchInserted < 3)
            {            //   The Branch is created or not It will try 3 times
                Inserted = crudBranch.Create(branchDetails);   //
                CheckBranchInserted++;                                       //
            }                                                  // Otherwise we will send exception message
            if (CheckBranchInserted == 3) throw new ArgumentException($"Data Not Updated due to Internal Error");


            // Now sending data in another format
            CreateBranchResponse createBranchResponse = new CreateBranchResponse(branchDetails);
            return createBranchResponse; //Sending back result
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