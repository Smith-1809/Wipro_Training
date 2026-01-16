using BankAccountManagementSystem;
using BankAudit;

Console.WriteLine("Welcome to Bank account management System");

// 1. Create a SavingAccount instance
SavingAccount savingAcc = new SavingAccount("SA123", "1234", 5.0m, "BR001");
savingAcc.Deposit(1000);

// Accessible if Program.cs is in the same assembly as BankAccount
decimal interestRateValue = savingAcc.interestRate;

decimal interest = savingAcc.CalculateInterest();
Console.WriteLine($"Interest for Saving Account: {interest}");

// 2. Create a CorporateAccount instance from another assembly
CorporateAccount corpAcc = new CorporateAccount("CA123", "5678", 7.0m, "BR002");
corpAcc.Deposit(5000);
corpAcc.ApplyCorporateInterest(); // Updates the balance with interest

Console.WriteLine("Corporate Account interest applied successfully.");