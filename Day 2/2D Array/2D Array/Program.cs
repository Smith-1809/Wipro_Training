namespace TwoDArray
{
    internal class Program
    {
        static void Main(String[] args)
        {
            int[,] arr = new int[3, 3]
            {
                {1,2,3},
                {4,5,6},
                {7,8,9}
            }
            ;

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }

            int[][] jaggedArray = new int[3][];
            jaggedArray[0] = new int[] { 1, 2 };
            jaggedArray[1] = new int[] { 3, 4, 5 };
            jaggedArray[2] = new int[] { 6, 7, 8, 9 };
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    System.Console.Write(jaggedArray[i][j] + " ");
                }
                System.Console.WriteLine();
            }

            // Case study : Using Arrays to Manage Student Grades
            // if i want to store marks of students in different subjects: "Subject wise marks for each student"
            // Step 1: Declare a 2D array to store marks of 3 students in 4 subjects
            // Step 2: Initialize the array with sample marks
            // Step 3: Calculate and print the average marks for each student
            // Step 4: Calculate and print the average marks for each subject
            // Step 5: Find and print the highest and lowest marks in the class
            // Syntax for declaraing  2D array as per above  sceanrio is 
            // dataType[,] arrayName = new dataType[rows, columns];
            int[,] marks = new int[3, 4]
            {
                {90,80,70,87},
                {75,65,65,87},
                {99,76,65,76}
            };
            for (int i = 0; i < marks.GetLength(0); i++)
            {
                int totalMarks = 0;
                int avgMarksStudent;
                for (int j = 0; j < marks.GetLength(1); j++)
                {
                    totalMarks += marks[i, j];
                }
                avgMarksStudent = totalMarks / 400;
                Console.WriteLine($"Total marks of student {i + 1} is: {totalMarks}");
                Console.WriteLine($"average marks of student {i + 1} is: {avgMarksStudent}");
            }

        }
    }
}