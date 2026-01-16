// See https://aka.ms/new-console-template for more information
Console.Write("Enter your age:");
int age;
bool isValidAge = int.TryParse(Console.ReadLine(), out age);
if (isValidAge)
{
    if (age >= 18)
    {
        Console.WriteLine("Eligible");
    }
    else
    {
        Console.WriteLine("Not Eligible");
    }
}
else
{
    Console.WriteLine("Input Error!!");
}