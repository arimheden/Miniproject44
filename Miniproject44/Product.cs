using System;
using System.Collections.Generic;
using System.Text;

namespace Miniproject44
{
    internal class Product
    {
        public int Id { get; set; }
        public string Category {  get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        static public int currentId = 1000;

        public Product (string Category, string Name, decimal Price) {
            this.Category = Category;
            this.Name = Name;
            this.Price = Price;
            currentId++;
            this.Id = currentId;
        }
    }
}
