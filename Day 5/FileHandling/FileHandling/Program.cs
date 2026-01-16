//File handling in C# is done using the System.IO namespace.
//here we have all the classes and methods to work with files and directories.
//Steps for implementing file handling 
//Step 1: using System.IO;
//Step 2: Create a file using File.Create() method.
//Step 3: Write to the file using StreamWriter class.( we have sperate classes for reading different type pof data ie
// reading char can be done with StreamReader class, reading binary data can be done with BinaryReader class)
//Writing to a file can be done in two ways:
//1. Using StreamWriter class : 
//2. Using File.WriteAllText() method.
//Step 4: Read from the file using StreamReader class.
//Step 5: Delete the file using File.Delete() method.
//Step 6: Check if the file exists using File.Exists() method.
Console.WriteLine("Hello, World!");
string filePath = "demo.txt";
using (FileStream fs = File.Create(filePath))
{
    // File created successfully
    if (File.Exists(filePath))
    {
        Console.WriteLine("File created successfully: " + filePath);
    }
}

//Writing to the file using StreamWriter class
using (StreamWriter sw = new StreamWriter(filePath))
{
    sw.WriteLine("Hello, this is a demo file created today.");
    sw.WriteLine("This file is created to demonstrate file handling in C#.");
}