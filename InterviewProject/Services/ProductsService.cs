using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewProject.Model;
using InterviewProject.Persistence;

namespace InterviewProject.Services
{
    public class ProductsService : IProductsDatabase
    {
        public  List<Product> ListOfProducts=new();
        private readonly double EuroRate = 4.74;
        private readonly double USDRate = 4.44;
        public void Add(Product product)
        {
            ListOfProducts.Add(product);
        }

        public void AddNewProduct(string ProductName, string Description, string plnPrice,DateTime CreatedAt)
        {
            if (CheckValidation(ProductName, plnPrice))
            {
                if(double.TryParse(plnPrice, out double ConvertPrice))
                {
                    var MyProduct = new Product(ConvertPrice, ProductName, Description, CreatedAt);
                    Add(MyProduct); 
                    Console.WriteLine("\nNew Product added!");
                }
            }
        }
        public bool CheckValidation(string name, string price)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Invalid input. Field NAME can not be empty");
                return false;
            }
            else if (string.IsNullOrEmpty(price))
            {
                Console.WriteLine("Invalid input. Field PRICE can not be empty");
                return false;
            }
            else if (double.TryParse(price, out double checkPrice) && checkPrice > 0) 
                return true;
            else
            {
                Console.WriteLine($"The value '{price}' is not a valid number.");
                return false;
            }
        }
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        public  Product GetByName(string name,string OperationType)
        {
            var result = ListOfProducts.FirstOrDefault(p => p.Name == name);
            if (result != null)
            {
                Console.WriteLine($"Product found: {result.Name}\nPLN Price: {result.PlnPrice}zł\nDescription: {result.Description}\nModyfied time: {result.Created}\nID: {result.Id} ");
                if (OperationType == "update")
                    UpdateProduct(name);
                else if (OperationType == "exchange")
                    ExchangeRate(result.PlnPrice);
                return result;
            }
            else
            {
                Console.WriteLine($"Product with name '{name}' not found...");
                return result;

            }
        }
        public void ExchangeRate(double price)
        {
            Console.WriteLine("\nChoose, to which currency you'd like to change current price: ");
            Console.WriteLine($"1 - EUR({EuroRate})");
            Console.WriteLine($"2 - USD({USDRate})");
            string rate = Console.ReadLine() ?? "";
            if (int.TryParse(rate, out int num) && num >= 1 && num <= 2)
            {
                double Results;
                switch (num)
                {
                    case 1:
                        Results= CaluclateRate(price, EuroRate);
                        Console.WriteLine("Calculated price:" + Results + "EUR");
                        break;
                    case 2:
                        Results= CaluclateRate(price, USDRate);
                        Console.WriteLine("Calculated price:"+Results+"USD");
                        break;
                    default:
                        Console.WriteLine("Invalid data....");
                        break;
                }
                Thread.Sleep(1500);
            }
            else
                Console.WriteLine("Invalid input. Please enter a number from 1 to 2.");
        }
        public double CaluclateRate(double price, double rate)
        {
            if(price <= 0 || rate <= 0) 
                throw new ArgumentException("Value must be positive.");
            else    
                return Math.Round(price/rate,2);
        }
        public Product GetTheCheapest()
        {
            var CheapestProduct = ListOfProducts.OrderBy(p => p.PlnPrice).FirstOrDefault();
            if (CheapestProduct != null)
                Console.WriteLine($"The cheapest  product:  {CheapestProduct.Name}\nPLN Price: {CheapestProduct.PlnPrice}zł\nDescription: {CheapestProduct.Description}\nModyfied time: {CheapestProduct.Created}\nID: {CheapestProduct.Id} ");
            else
                Console.WriteLine("Products not found...");
            return CheapestProduct;
        }
        public Product GetTheMostExpensive()
        {
            var TheMostExpensiveProduct = ListOfProducts.OrderByDescending(p => p.PlnPrice).FirstOrDefault();
            if (TheMostExpensiveProduct != null) 
                Console.WriteLine($"he most expensive product:  {TheMostExpensiveProduct.Name}\nPLN Price: {TheMostExpensiveProduct.PlnPrice}zł\nDescription: {TheMostExpensiveProduct.Description}\nModyfied time: {TheMostExpensiveProduct.Created}\nID: {TheMostExpensiveProduct.Id} ");
            else
                Console.WriteLine("Products not found...");
            return TheMostExpensiveProduct;
        }
        public Product GetTheNewest()
        {
            var LastUpdatedProduct=ListOfProducts.OrderByDescending(p => p.Created).FirstOrDefault();
            if(LastUpdatedProduct != null) Console.WriteLine($"Product found: {LastUpdatedProduct.Name}\nPLN Price: {LastUpdatedProduct.PlnPrice}zł\nDescription: {LastUpdatedProduct.Description}\nModyfied time: {LastUpdatedProduct.Created}\nID: {LastUpdatedProduct.Id} ");
            else
                Console.WriteLine("Products not found...");
            return LastUpdatedProduct;
        }
        public void UpdateProduct(string name)
        {
            Console.WriteLine("Please update product: (in case of no changes, leave blank field and press enter to go to next step)");
            Console.Write("New name: ");
            string NewName = Console.ReadLine() ?? "";
            Console.Write("New price: ");
            string NewPriceString = Console.ReadLine() ?? "";
            Console.Write("New description: ");
            string NewDescription = Console.ReadLine() ?? "";
            Update(NewName, NewPriceString, NewDescription,name);
        }
        public void Update(string NewName,string NewPriceString,string NewDescription,string name)
        {
            var result = ListOfProducts.FirstOrDefault(p => p.Name == name);
            if (NewName != "") 
                result.Name = NewName;
            if (NewDescription != "") 
                result.Description = NewDescription;
            if (NewName != "" || NewDescription != "" || NewPriceString != "")
                result.Created = DateTime.Now;
            if (NewPriceString != "")
            {
                if (double.TryParse(NewPriceString, out double NewPrice) && NewPrice > 0)
                {
                    result.PlnPrice = NewPrice;
                    Console.WriteLine($"Product Updated to: {result.Name}\nPLN Price: {result.PlnPrice}zł\nDescription: {result.Description}\nModyfied time: {result.Created}\nID: {result.Id} ");
                }
                else
                    Console.WriteLine($"The value '{NewPriceString}' is not a valid number.");
            }
        }
    }
}
