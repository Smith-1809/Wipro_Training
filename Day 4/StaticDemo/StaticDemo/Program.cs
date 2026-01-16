namespace StaticDemo
{
    public static class MathUtilities
    {
        public static int Add(int a, int b)
        {
            return a + b;
        }

        public static int Subtract(int a, int b)
        {
            return a - b;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Static Demo");
            int sum = MathUtilities.Add(5, 3);
            int difference = MathUtilities.Subtract(10, 4);
            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Difference: {difference}");
        }
    }
}