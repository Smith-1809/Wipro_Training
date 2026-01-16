Console.WriteLine("Hello, World!");
ElectronicProduct laptop = new ElectronicProduct("Laptop", 999.99m, 24);
Console.WriteLine(laptop.GetProductDetails());
abstract class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public abstract string GetProductDetails();
}

class ElectronicProduct : Product
{
    public int WarrantyPeriod { get; set; } // in months

    public ElectronicProduct(string name, decimal price, int warrantyPeriod)
        : base(name, price)
    {
        WarrantyPeriod = warrantyPeriod;
    }

    public override string GetProductDetails()
    {
        return $"Electronic Product: {Name}, Price: {Price:C}, Warranty: {WarrantyPeriod} months";
    }
}