using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewProject.Model;
using InterviewProject.Services;

namespace InterviewProject.Presentation
{
    public class ConsoleProductOperations
    {
        public void RenderOperations()
        {
            var MyService = new ProductsService();
            
            while (true)
            {
                Console.WriteLine("WAREHOUSE PROJECT");
                Console.WriteLine("\nSelect operation below:");
                Console.WriteLine("1.Get product (searches for product with given name)\n"
                                 + "2.Add new product\n"
                                 + "3.Update product\n"
                                 + "4.Get the cheapest product\n"
                                 + "5.Get the most expensive product\n"
                                 + "6.Get last modified product(returns last modified product, depending on its modification date)\n"
                                 + "7.Calculate product price in different currency(calculates product price depending on currency given by the user)\n"
                                 + "8.Exit\n"
                                 );
                string? input = Console.ReadLine();
                Console.Clear();

                if (int.TryParse(input, out int num) && num >= 1 && num <= 8)
                {
                    switch (num)
                    {
                        case 1:
                            Console.WriteLine("Please provide name of product: ");
                            Console.Write("Name: ");
                            string FindByName = Console.ReadLine() ?? "";
                            MyService.GetByName(FindByName,Operations.find.ToString());

                            break;
                        case 2:
                            Console.WriteLine("Please add new product: ");
                            Console.Write("Name: ");
                            //name nie moze byc null
                            string Name = Console.ReadLine() ?? "";
                            Console.Write("Price: ");
                            string price = Console.ReadLine()?? "";
                            Console.Write("Description: ");
                            string Description = Console.ReadLine() ?? "";
                            MyService.AddNewProduct(Name, Description, price, DateTime.Now);
                            
                           
                            break;
                        case 3:
                            Console.WriteLine("Please choose name of product to update: ");
                            Console.Write("Name: ");
                            string NameToFind = Console.ReadLine() ?? "";
                            MyService.GetByName(NameToFind, Operations.update.ToString());
                            
                            break;
                        case 4:
                            MyService.GetTheCheapest();
                            Thread.Sleep(1000);
                            break;
                        case 5:
                            MyService.GetTheMostExpensive();
                            Thread.Sleep(1000);
                            break;
                        case 6:
                            MyService.GetTheNewest();
                            Thread.Sleep(1000);
                            break;
                        case 7:
                            Console.WriteLine("Provide name of product:" );
                            Console.Write("Name: ");
                            string CurrencyExchangeItem = Console.ReadLine() ?? "";
                            MyService.GetByName(CurrencyExchangeItem, Operations.exchange.ToString()); ;

                            break;
                        case 8:
                            return;
                    }
                }
                else
                    Console.WriteLine("Invalid input. Please enter a number from 1 to 8.");
            }
        }
     }
}
