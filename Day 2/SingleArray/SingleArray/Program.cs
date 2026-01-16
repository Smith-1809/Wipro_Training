int[] arr = new int[5];
arr[0] = 1;
arr[1] = 2;
arr[2] = 3;
arr[3] = 4;
arr[4] = 5;
for (int i = 0; i < arr.Length; i++)
{
    Console.WriteLine($"Element of index {i} is {arr[i]}");
}

Array.Reverse(arr);
for (int i = 0; i < arr.Length; i++)
{
    Console.WriteLine($"Element of index {i} is {arr[i]}");
}
Array.Sort(arr);
for (int i = 0; i < arr.Length; i++)
{
    Console.WriteLine($"Element of index {i} is {arr[i]}");
}
Console.WriteLine(Array.IndexOf(arr, 3));
Console.WriteLine(arr.Length);

string[] strArr = new string[2];
strArr[0] = "Ayan";
strArr[1] = "Aman";
for (int j = 0; j < strArr.Length; j++)
{
    Console.WriteLine($"Element of index {j} is {strArr[j]}");
}