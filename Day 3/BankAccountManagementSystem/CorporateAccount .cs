using BankAccountManagementSystem;

namespace BankAudit
{
    public class CorporateAccount : BankAccount
    {
        public CorporateAccount(string accNumber, string pin, decimal interest, string branchCode)
            : base(accNumber, pin, interest, branchCode)
        {
            // interestRate is accessible here via inheritance (protected internal)
        }

        // Added to support the call in Program.cs
        public void ApplyCorporateInterest()
        {
            // Accesses protected balance and protected internal interestRate
            decimal interest = balance * interestRate / 100;
            balance += interest;
        }
    }
}