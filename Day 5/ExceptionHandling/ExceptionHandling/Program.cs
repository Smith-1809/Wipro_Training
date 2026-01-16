//In C#.NET We use Exception Handling for handling errors and exceptions in a controlled manner.
//With E.H we can gracefully handle runtime errors, maintain program stability, and provide meaningful feedback to users.
//hence Below staeps are used to implement Exception Handling in C#.NET
//in .NET There is gloabl exception class called System.Exception from where all exception classes are derived.
//hence .NET follows herirachal exception handling mechanism. Where base class are used to catch general exceptions and derived classes are used to catch specific exceptions.
//ex : System.IO.IOException is derived from System.Exception class.
//ex: System.DivideByZeroException is derived from System.ArithmeticException which is derived from System.Exception class.

//Common sceanrio where exception handling is used in C#.NET
//1. File I/O Operations: Handling exceptions when reading from or writing to files.
//2. Database Operations: Handling exceptions during database connections and queries.
//3. Network Communications: Handling exceptions during network requests and responses.
//4. User Input Validation: Handling exceptions when parsing and validating user inputs.
//5. Resource Management: Ensuring proper release of resources like file handles, database connections etc. using finally block or using statement.
//6. API Calls: Handling exceptions when calling external APIs or services.
//7. Multithreading: Handling exceptions in multi-threaded applications to prevent crashes and ensure thread safety.
//8. Configuration Management: Handling exceptions when reading configuration settings from files or environment variables.
//9. Data Serialization/Deserialization: Handling exceptions when converting data to/from formats like JSON, XML etc.
//10. Custom Exception Handling: Creating and handling custom exception classes for specific application scenarios.



using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

public class DailyLimitExceededException : Exception
{
    //Here I can define properties and methods specific to this exception. as well as constructors.
    //using ultimate base class System.Exception to derive my custom exception class.
    public DailyLimitExceededException(string message) : base(message)
    //calling base class constructor inside derived class constructor.
    {

    }
}

public class DailyNumberOfTransactionsExceededException : DailyLimitExceededException
{
    public DailyNumberOfTransactionsExceededException(string message) : base(message)
    {

    }
}

//Creating a account class that act as a busineslogic class for implementing user define exception 
class BankAccount //business logic class
{
    private decimal dailyLimit = 10000; //daily limit for transactions
    private decimal totalTransactionsToday = 0; //to keep track of total transactions made today
    private int numberOfTransactionsToday = 0; //to keep track of number of transactions made today
    public void MakeTransaction(decimal amount)
    {
        if (totalTransactionsToday + amount > dailyLimit)
        {
            //throwing user define exception when daily limit is exceeded.
            throw new DailyLimitExceededException("Daily transaction limit exceeded.");
        }
        else if (numberOfTransactionsToday > 5)
        {
            throw new DailyNumberOfTransactionsExceededException("Daily number of transactions limit exceeded.");
        }
        Console.WriteLine($"Number of transactions is {numberOfTransactionsToday}.");
        totalTransactionsToday += amount;
        numberOfTransactionsToday++;
        Console.WriteLine($"Transaction of {amount} completed successfully.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        BankAccount account = new BankAccount();
        try
        {
            account.MakeTransaction(500);
            account.MakeTransaction(600); //This will exceed the daily limit and throw exception
            account.MakeTransaction(200);
            account.MakeTransaction(30000);
            account.MakeTransaction(400);
            account.MakeTransaction(500);
            account.MakeTransaction(600); //This will exceed the number of transactions limit and throw exception
        }
        catch (DailyLimitExceededException ex)
        {
            Console.WriteLine($"Exception caught: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Argument Exception caught: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception caught: {ex.Message}");
        }
    }
}