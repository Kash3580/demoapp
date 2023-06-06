using System;
using System.Collections.Generic;

class Program
{
    static Dictionary<string, Product> products = new Dictionary<string, Product>();
    static List<string> history = new List<string>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Enter a command: Insert, Consumption, Current Status, History, Check Expiry, Exit");
            string command = Console.ReadLine().ToLower();

            switch (command)
            {
                case "insert":
                    InsertProduct();
                    break;

                case "consumption":
                    ConsumeProduct();
                    break;

                case "current status":
                    PrintCurrentStatus();
                    break;

                case "history":
                    PrintHistory();
                    break;

                case "check expiry":
                    CheckExpiry();
                    break;

                case "exit":
                    return;

                default:
                    Console.WriteLine("Invalid command. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void InsertProduct()
    {
        Console.WriteLine("Enter the name of the product:");
        string productName = Console.ReadLine();

        Console.WriteLine("Enter the quantity:");
        double quantity = double.Parse(Console.ReadLine());

        Console.WriteLine("Enter the expiration date (dd/mm/yyyy):");
        DateTime expiryDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

        if (products.ContainsKey(productName))
            products[productName].Quantity += quantity;
        else
            products.Add(productName, new Product { Quantity = quantity, ExpiryDate = expiryDate });

        string entry = $"Inserted {quantity} units of {productName} (Expiry Date: {expiryDate.ToShortDateString()})";
        history.Add(entry);

        Console.WriteLine(entry);
    }

    static void ConsumeProduct()
    {
        Console.WriteLine("Enter the name of the product:");
        string productName = Console.ReadLine();

        if (!products.ContainsKey(productName))
        {
            Console.WriteLine("Product not found in the refrigerator.");
            return;
        }

        Console.WriteLine("Enter the quantity consumed:");
        double quantity = double.Parse(Console.ReadLine());

        Product product = products[productName];

        if (quantity >= product.Quantity)
        {
            string entry = $"Consumed {product.Quantity} units of {productName}";
            history.Add(entry);

            Console.WriteLine(entry);

            products.Remove(productName);
        }
        else
        {
            product.Quantity -= quantity;
            string entry = $"Consumed {quantity} units of {productName}";
            history.Add(entry);

            Console.WriteLine(entry);
        }
    }

    static void PrintCurrentStatus()
    {
        Console.WriteLine("Current Status:");

        if (products.Count == 0)
        {
            Console.WriteLine("No products in the refrigerator.");
        }
        else
        {
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Key}: {product.Value.Quantity} units (Expiry Date: {product.Value.ExpiryDate.ToShortDateString()})");
            }
        }
    }

    static void PrintHistory()
    {
        Console.WriteLine("History:");

        if (history.Count == 0)
        {
            Console.WriteLine("No history available.");
        }
        else
        {
            foreach (var entry in history)
            {
                Console.WriteLine(entry);
            }
        }
    }

    static void CheckExpiry()
    {
        Console.WriteLine("Expired Products:");

        bool hasExpiredProducts = false;
        DateTime currentDate = DateTime.Now.Date;

        foreach (var product in products)
        {
            if (product.Value.ExpiryDate <= currentDate)
            {
                string entry = $"Expired: {product.Key} (Expiry Date: {product.Value.ExpiryDate.ToShortDateString()})";
                history.Add(entry);

                Console.WriteLine(entry);

                products.Remove(product.Key);
                hasExpiredProducts = true;
            }
        }

        if (!hasExpiredProducts)
        {
            Console.WriteLine("No expired products found.");
        }
    }
}

class Product
{
    public double Quantity { get; set; }
    public DateTime ExpiryDate { get; set; }
}
