using System;
using System.Collections.Generic;

class Program
{
    static Dictionary<string, double> products = new Dictionary<string, double>();
    static List<string> history = new List<string>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Enter a command: Insert, Consumption, Current Status, History, Exit");
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

        if (products.ContainsKey(productName))
            products[productName] += quantity;
        else
            products.Add(productName, quantity);

        string entry = $"Inserted {quantity} units of {productName}";
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

        if (quantity >= products[productName])
        {
            string entry = $"Consumed {products[productName]} units of {productName}";
            history.Add(entry);

            Console.WriteLine(entry);

            products.Remove(productName);
        }
        else
        {
            products[productName] -= quantity;
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
                Console.WriteLine($"{product.Key}: {product.Value} units");
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
}
