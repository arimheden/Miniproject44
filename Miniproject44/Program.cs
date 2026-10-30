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

        // Edit product
        case "5":
            prodManager.EditProduct(ref products);
            break;

        // Show statistics
        case "6":
            prodManager.ProductStatistics(products);
            break;

        // Save product list and exit program
        case "7":
            prodManager.SaveProducts(products);
            break;

        // Load product list and exit program
        case "8":
            prodManager.LoadProducts(ref products);
            break;

        // Exit program
        case "9":
            Environment.Exit(0);
            break;
    }
}


