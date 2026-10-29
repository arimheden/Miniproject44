using Miniproject44;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using System.Text.Json.Serialization;


// Display instructions
Console.WriteLine("--------------------------------");
Console.WriteLine("   PRODUCT MANAGER - LEVEL 4");
Console.WriteLine("--------------------------------");
Console.WriteLine();

// Initialize list
List<Product> products = new List<Product>();

// Initialize helper class'
ProductManager prodManager = new();

while (true)
{

    string menuOption = prodManager.ProductListMenu();

    switch (menuOption)
    {
        case "1":
            prodManager.AddProducts(products);
            break;
                
        // Perform product sort and display
        case "2":
            prodManager.ShowProducts(products);
            break;

        // Perform product search
        case "3":
            prodManager.SearchProducts(products);
            break;

        // Delete product from list
        case "4":
            prodManager.DeleteProduct(ref products);
            break;

        // Show statistics
        case "5":
            prodManager.ProductStatistics(products);
            break;

        // Save product list and exit program
        case "6":
            StreamWriter sw = new StreamWriter("products.json");
            var jsonProductList = JsonSerializer.Serialize(products);
            sw.Write(jsonProductList.ToString());
            sw.Close();
            Environment.Exit(0);
            break;
    }
}


