using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Miniproject44;

/* 
 * Helper class for product list management
 * 
 */
internal class ProductManager
{
    // Request user input for product name
    public String userInput()
    {
        string userInput = Console.ReadLine();
        userInput = userInput.Trim();
        return userInput;
    }

    // Validate user input for product name
    public bool validateInput(string inputToValidate, string validateType)
    {
        if (String.IsNullOrEmpty(inputToValidate))
        {
            throw new ArgumentException("Input can not be empty!");
            return false;
        }

        // Validate price
        if (validateType == "price")
        {
            decimal price;
            if (Decimal.TryParse(inputToValidate, out price))
            {
                if (price > 0) return true;
                else
                {
                    throw new ArgumentException("Price can not be negative!");
                }
            }
            else
            {
                throw new ArgumentException("Price must be a decimal number!");
                return false;
            }

        }

        if (validateType == "id")
        {
            int id;
            if (Int32.TryParse(inputToValidate, out id))
            {
                if (id > 1000) return true;
                else
                {
                    throw new ArgumentException("Product id must be a number greater than 1000!");
                }
            }
            else
            {
                throw new ArgumentException("Product id must be a number!");
                return false;
            }
        }

        return true;
    }

    public void AddProducts(List<Product> products)
    {
        // Adding products
        Console.WriteLine("Type 'q' to finish.");

        Console.WriteLine();
        int inputLine = 1;
        string prodCategory = "";
        string prodName = "";
        decimal prodPrice = 0;
        while (true)
        {

            // Request user input
            switch (inputLine)
            {
                case 1:
                    Console.Write("Enter product category: ");
                    break;

                case 2:
                    Console.Write("Enter product name: ");
                    break;
                case 3:
                    Console.Write("Enter product price: ");
                    break;
            }

            string userInput = this.userInput();

            // Terminate execution on "q"
            if (userInput == "q")
            {
                break;
            }

            switch (inputLine)
            {
                case 1:
                    // Validate user input
                    try
                    {
                        this.validateInput(userInput, "category");
                        prodCategory = userInput;
                        inputLine++;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    break;

                case 2:
                    try
                    {
                        this.validateInput(userInput, "name");
                        prodName = userInput;
                        inputLine++;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    break;
                case 3:
                    try
                    {
                        this.validateInput(userInput, "price");
                        prodPrice = Convert.ToDecimal(userInput);
                        inputLine++;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    break;
            }

            if (inputLine > 3)
            {
                inputLine = 1;
                products.Add(new Product(prodCategory, prodName, prodPrice));
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Product has been added!");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
            }


        }

    }

    // Show statistics
    public void ProductStatistics(ref List<Product> products)
    {
        //int productCount = productList.Count;
        //Console.WriteLine("- Total Products: " + productCount);
        Console.WriteLine("========= STATISTICS =========");
        Console.WriteLine();

        Product expensiveProduct = products.OrderByDescending(a => a.Price).First();
        Product cheapProduct = products.OrderBy(a => a.Price).First();
        decimal average = products.Average(a => a.Price);

        Console.WriteLine("Most Expensive Product:");
        Console.WriteLine();

        Console.WriteLine(expensiveProduct.Name + " - " + expensiveProduct.Price);

        Console.WriteLine("Cheapest Product:");

        Console.WriteLine(cheapProduct.Name + " - " + cheapProduct.Price);
        Console.WriteLine();

        Console.WriteLine("Average Price:");

        Console.WriteLine(average + " kr");
        Console.WriteLine();

        Console.WriteLine("Products per Category:");
        var categories = products.DistinctBy(a => a.Category);
        foreach (var category in categories) {
            Console.Write(category.Category + ": ");
            decimal categorySum = products.Where(a => a.Category.Equals(category.Category)).Count();
            Console.WriteLine(categorySum);
        }

        //int average = 0;
        //average = sumNumbers / productCount;
        //Console.WriteLine("- Average: " + average);


    }

    // Sort and show products
    public void ShowProducts(List<Product> products)
    {
        Console.WriteLine();
        Console.WriteLine("===== PRODUCT LIST =====");
        Console.WriteLine();
        foreach (Product product in products.OrderBy(product => product.Price))
        {
            Console.WriteLine(product.Id.ToString().PadRight(12) + " | " + product.Category.PadRight(12) + " | " + product.Name.PadRight(12) + " | " + product.Price.ToString().PadRight(12));
        }
    }

    // Search products by category or name
    public void SearchProducts(List<Product> products)
    {
        Console.Write("Search Product: ");

        // Request user input
        string searchString = this.userInput();

        Console.WriteLine();
        Console.WriteLine("FOUND PRODUCTS:");
        Console.WriteLine();

        var prodQuery = from product in products where product.Category.ToLower().Contains(searchString.ToLower())
                        || product.Name.ToLower().Contains(searchString.ToLower()) select product;

        foreach (Product product in prodQuery) 
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(product.Category.PadRight(12) + " | " + product.Name.PadRight(12) + " | " + product.Price.ToString().PadRight(12));
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    // Delete product by Id
    public void DeleteProduct(ref List<Product> products)
    {

        Console.Write("Delete Product: ");

        // Request user input
        string deleteString = this.userInput();

        // Validate user input
        try
        {
            this.validateInput(deleteString, "id");
            if (products.Any(product => product.Id == Convert.ToInt32(deleteString)))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Product has been removed!");
                Console.ForegroundColor = ConsoleColor.Gray;
                products.RemoveAll(product => product.Id.Equals(Convert.ToInt32(deleteString)));
            }
            else throw new ArgumentException("Product does not exist!");

        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    // Edit product by Id
    public void EditProduct(ref List<Product> products)
    {

        Console.Write("Edit Product: ");

        // Request user input
        string editString = this.userInput();

        // Validate user input
        try
        {
            this.validateInput(editString, "id");
            if (products.Any(product => product.Id == Convert.ToInt32(editString)))
            {
                Console.Write("Enter new category (blank to keep existing): ");
                string newCategory = this.userInput();
                if (!String.IsNullOrEmpty(newCategory))
                {
                    this.validateInput(newCategory, "category");
                    try
                    {
                        foreach (Product product in products.Where(w => w.Id == Convert.ToInt32(editString)))
                        {
                            product.Category = newCategory;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }

                Console.Write("Enter new name (blank to keep existing): ");
                string newName = this.userInput();
                if (!String.IsNullOrEmpty(newName))
                {
                    this.validateInput(newName, "name");
                    try
                    {
                        foreach (Product product in products.Where(w => w.Id == Convert.ToInt32(editString)))
                        {
                            product.Name = newName;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                    
                Console.Write("Enter new price (blank to keep existing): ");
                string userInput = this.userInput();
                if (!String.IsNullOrEmpty(userInput))
                {
                    this.validateInput(userInput, "price");
                    try
                    {
                        decimal newPrice = Convert.ToDecimal(userInput);
                        foreach (Product product in products.Where(w => w.Id == Convert.ToInt32(editString)))
                        {
                            product.Price = newPrice;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
            }
            else throw new ArgumentException("Product does not exist!");

        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    // Save products to json file
    public void SaveProducts(List<Product> products)
    {
        StreamWriter sw = new StreamWriter("products.json");
        var jsonProductList = JsonSerializer.Serialize(products);
        sw.Write(jsonProductList.ToString());
        sw.Close();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Product list has been saved!");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    // Save products to json file
    public void LoadProducts(ref List<Product> products)
    {
        string json = File.ReadAllText("products.json");
        var jsonProductList = JsonSerializer.Deserialize<List<Product>>(json);
        products = jsonProductList;
        Product.currentId = products.Max(a => a.Id);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Product list has been loaded!");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    // Display menu and wait for key press
    public string ProductListMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. View Products");
        Console.WriteLine("3. Search Product");
        Console.WriteLine("4. Delete Product");
        Console.WriteLine("5. Edit Product");
        Console.WriteLine("6. Statistics");
        Console.WriteLine("7. Save Products");
        Console.WriteLine("8. Load Products");
        Console.WriteLine("9. Exit");
        Console.WriteLine();

        ConsoleKeyInfo keypressed;
        string numKey;
        do
        {
            keypressed = Console.ReadKey(true);
            numKey = keypressed.Key.ToString().Trim('D');

        } while (!Regex.IsMatch(numKey, @"^[1-9]+\z"));
        return numKey;
    }
}




