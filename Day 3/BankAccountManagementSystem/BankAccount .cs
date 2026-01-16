namespace BankAccountManagementSystem
{
    public class BankAccount
    {
        private string accountPIN; // Hidden: Only accessible within BankAccount
        public string accountNumber; // Accessible everywhere

        // Accessible within the same assembly OR derived classes in other assemblies
        protected internal decimal interestRate;

        // Accessible only within the same assembly (BankAccountManagementSystem)
        internal string bankBranchCode;

        // Accessible within BankAccount and all derived classes
        protected decimal balance;

        public BankAccount(string accNumber, string pin, decimal interest, string branchCode)
        {
            accountNumber = accNumber;
            accountPIN = pin;
            interestRate = interest;
            bankBranchCode = branchCode;
            balance = 0; // Initializing default balance
        }

        public void Deposit(decimal amount)
        {
            balance += amount; // Add amount to balance
        }
    }
}