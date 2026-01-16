namespace BankAccountManagementSystem
{
    public class SavingAccount : BankAccount
    {
        public SavingAccount(string accNumber, string pin, decimal interest, string branchCode)
            : base(accNumber, pin, interest, branchCode) { }

        public decimal CalculateInterest()
        {
            // Accesses balance (protected) and interestRate (protected internal)
            return balance * interestRate / 100;
        }
    }
}