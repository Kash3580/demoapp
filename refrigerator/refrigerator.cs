// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler
using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
 
    static List<string> history = new List<string>();
    static List<Product> shoppingList = new List<Product>();
    static List<Product> cartItems = new List<Product>();
   static List<Product> cartHistoryItems = new List<Product>();
    static List<Product> refrigeratorItems = new List<Product>();
    static void Main()
    {
         
          shoppingList.Add(new Product{ ProductId=101, Name="Milk", Quantity=1, ExpiryDate= DateTime.ParseExact(  "07/06/2023","dd/MM/yyyy", null)});
          shoppingList.Add(new Product{ ProductId=102, Name="Butter", Quantity=2, ExpiryDate= DateTime.ParseExact(  "02/05/2023","dd/MM/yyyy", null)});
           shoppingList.Add(new Product{ ProductId=103, Name="Curd", Quantity=1, ExpiryDate= DateTime.ParseExact(  "02/07/2023","dd/MM/yyyy", null)});
            shoppingList.Add(new Product{ ProductId=104, Name="Tomato", Quantity=10, ExpiryDate= DateTime.ParseExact(  "08/06/2023","dd/MM/yyyy", null)});
        
        while (true)
        {
         
            Console.WriteLine("\nEnter a command: \nshoppinglist | cart | insert | consume | status | history | check-expiry | exit");
            string command = Console.ReadLine().ToLower();
 Console.WriteLine(command);
            switch (command)
            {
                
                 case "shoppinglist": // display list of products and make order
                    ShowItems(shoppingList,true); 
                    InsertIntoCart();
                    break;
                    
                 case "cart": // display cart items
                     ShowItems(cartItems,false);
                    break;
                 case "insert":  //insert item into refrigerator
                    ShowItems(cartItems,false);
                    InsertIntoRefrigerator();
                    break;

                 case "consume":  //consume item from refrigerator
                    ConsumeProduct();
                    break;

                case "status":   //Display list of items in refrigerator
                    PrintCurrentStatus();
                    break;

                 case "history": // display history log
                    PrintHistory();
                    break;

                 case "check-expiry": //check and remove expired items from refrigerator
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

   
   
    static void ShowItems(List<Product> products, bool isShoppinglist)
    {
        if(cartHistoryItems.Count!=0 && isShoppinglist){
              Console.WriteLine("Recently purchased Item List:\n");
                foreach (var item in cartHistoryItems)
                {
                       Console.WriteLine($"{item.ProductId}:{item.Name}"  );
                }
        }
        if (products.Count == 0 )
        {
            Console.WriteLine("No product found.");
        }
        else{
               Console.WriteLine("Product Item List:\n");
                foreach (var item in products)
                {
                       Console.WriteLine($"{item.ProductId}:{item.Name}"  );
                }
        }
     
    }
    static void InsertIntoCart()
    {
        try
        {
           Console.WriteLine("What would you like add into cart(Enter ProductID):");
           int productId =  int.Parse(Console.ReadLine());
           Console.WriteLine("Product List:");
            bool hasProduct = false;
            foreach (var item in shoppingList)
            {
                if(item.ProductId==productId){
                      cartItems.Add(item);
                      string log=$"Item : {item.Name} has been added into the cart"  ;
                      Console.WriteLine(log);
                      history.Add(log);
                      hasProduct=true;
                }
            }
            if(!hasProduct){
                Console.WriteLine("No product found in shopping list");
            }
        }
        catch(Exception ex){
              Console.WriteLine("Wrong input");
            //  history.Add(ex.StackTrace.ToString());
        }
      
    }
    static void InsertIntoRefrigerator()
    {   
        try
        {
            if(cartItems.Count==0){
                return;
            }
            Console.WriteLine("What would you like to add into Refrigerator(Enter ProductId):");
            int productId =  int.Parse(Console.ReadLine());
            bool hasProduct = false;
            foreach (var item in cartItems)
            {
                if(item.ProductId==productId){
                    refrigeratorItems.Add(item);
                    string log=$"Item : {item.Name} has been added into the refrigerator"  ;
                      Console.WriteLine(log);
                      history.Add(log);
                      cartItems.Remove(item);
                      hasProduct = true;
                      break;
                }
            }
              if(!hasProduct){
                Console.WriteLine("No product found in cart. Please add some product in cart");
            }  
        }
        catch(Exception ex){
              Console.WriteLine("Wrong input");
              history.Add(ex.StackTrace.ToString());
        }
    }
    static void ConsumeProduct()
    {
        Console.WriteLine("Enter the name of the product:");
        string productName = Console.ReadLine();
        var item = refrigeratorItems.FirstOrDefault(x => x.Name == productName);
        if (item==null)
        {
            Console.WriteLine("Product not found in the refrigerator.");
            return;
        }

        Console.WriteLine("Enter the quantity consumed:");
        double quantity = double.Parse(Console.ReadLine());

     

        if (quantity >= item.Quantity && quantity>0)
        {
            string log = $"Date:{DateTime.Now.ToShortDateString()}: Consumed {item.Quantity} units of {productName}";
            history.Add(log);
            Console.WriteLine(log);
             var product = cartHistoryItems.FirstOrDefault(x => x.Name == productName);
                if (product==null)
                {
                    cartHistoryItems.Add(item);
                }
            refrigeratorItems.Remove(item);
        }
        else
        {
            item.Quantity -= quantity;
            string entry = $"Date:{DateTime.Now.ToShortDateString()}: Consumed {quantity} units of {productName}";
             history.Add(entry);

            Console.WriteLine(entry);
        }
    }
    static void PrintCurrentStatus()
    {
        Console.WriteLine("Refrigerator product details:");

        if (refrigeratorItems.Count == 0)
        {
            Console.WriteLine("No products in the refrigerator.");
        }
        else
        {
            foreach (var product in refrigeratorItems)
            {
                Console.WriteLine($"{product.Name}: {product.Quantity} units (Expiry Date: {product.ExpiryDate.ToShortDateString()})");
            }
        }
    }
   
   
    static void CheckExpiry()
    {
          List<Product> expiredProducts = new List<Product>();
        Console.WriteLine("Expired Products:");

        bool hasExpiredProducts = false;
        DateTime currentDate = DateTime.Now.Date;

        foreach (var product in refrigeratorItems)
        {
            if (product.ExpiryDate <= currentDate)
            {
                string log = $"Expired: {product.Name} (Expiry Date: {product.ExpiryDate.ToShortDateString()}) has been removed";
                history.Add(log);
                Console.WriteLine(log);
                expiredProducts.Add(product);
                var item = cartHistoryItems.FirstOrDefault(x => x.Name == product.Name);
                if (item==null)
                {
                    cartHistoryItems.Add(product);
                }
                hasExpiredProducts = true;
                break;
            }
        }

        if (!hasExpiredProducts)
        {
            Console.WriteLine("No expired products found.");
        }
        else
        { 
            foreach (var product in expiredProducts)
            {
              refrigeratorItems.Remove(product);
            }
        }
    }

   
    static void PrintHistory()
    {
        Console.WriteLine("History Log:");

        if (history.Count == 0)
        {
            Console.WriteLine("No history available.");
        }
        else
        {
            foreach (var record in history)
            {
                Console.WriteLine(record);
            }
        }
    }

}

class Product
{
       
      public int ProductId { get; set; }
      public string Name { get; set; }
    public double Quantity { get; set; }
    public DateTime ExpiryDate { get; set; }
} 
