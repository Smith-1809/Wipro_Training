using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Please enter your age:");

        string ageInput = Console.ReadLine(); // Read user input
        int age;

        bool isValidAge = int.TryParse(ageInput, out age);

        Console.WriteLine(); // Blank line

        if (isValidAge)
        {
            if (age >= 18)
            {
                Console.WriteLine("You are eligible to vote.");
            }
            else
            {
                Console.WriteLine("You are not eligible to vote.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid age.");
        }
    }
}

