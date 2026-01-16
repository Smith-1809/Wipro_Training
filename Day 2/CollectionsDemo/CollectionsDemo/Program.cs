using System;
using System.Collections;
using System.Collections.Generic;

namespace CollectionsDemo
{
    // STEP 1: Define the Orders class so the List<Orders> has a template
    public class Orders
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        // Overriding ToString to display order details clearly
        public override string ToString()
        {
            return $"ID: {OrderId} | Product: {ProductName} | Qty: {Quantity} | Price: ${Price}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting started with arrays and collections in C# ...!!!");

            // --- NON-GENERIC COLLECTION (ArrayList) ---
            Console.WriteLine("\n--- Non-Generic Collection Implementation ---");
            ArrayList orderCollection = new ArrayList();

            orderCollection.Add("laptop"); // String
            orderCollection.Add(12345);    // Integer (Boxed to Object)
            orderCollection.Add(99.99);    // Double (Boxed to Object)
            orderCollection.Add(new DateTime(2024, 6, 1));
            orderCollection.Add(true);
            orderCollection.Add('A');

            Console.WriteLine($"Total elements: {orderCollection.Count}");
            Console.WriteLine($"Current Capacity: {orderCollection.Capacity}");

            foreach (var item in orderCollection)
            {
                Console.WriteLine($"{item} - Type: {item.GetType()}");
            }

            // --- GENERIC COLLECTION (List<string>) ---
            Console.WriteLine("\n--- Generic Collection Implementation (String Only) ---");
            List<string> genericOrderCollection = new List<string>();
            genericOrderCollection.Add("laptop");
            genericOrderCollection.Add("mobile");
            genericOrderCollection.Add("tablet");

            // FIX: genericOrderCollection.Add(12345); 
            // The line above would cause a COMPILE-TIME ERROR because 12345 is not a string.
            // We must convert it if we want it in a string list:
            genericOrderCollection.Add(12345.ToString());

            foreach (var item in genericOrderCollection)
            {
                Console.WriteLine(item);
            }

            // --- BEST PRACTICE: GENERIC LIST OF OBJECTS ---
            Console.WriteLine("\n--- Order Management System (Generic List of Orders) ---");
            List<Orders> orderList = new List<Orders>();

            orderList.Add(new Orders { OrderId = 1, ProductName = "Laptop", Quantity = 2, Price = 1500.00 });
            orderList.Add(new Orders { OrderId = 2, ProductName = "Mobile", Quantity = 5, Price = 800.00 });

            foreach (var order in orderList)
            {
                Console.WriteLine(order);
            }

            Console.ReadLine();
        }
    }
}