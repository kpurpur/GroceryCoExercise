using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GroceryCo.Service;
using GroceryCo.Service.Utilities;
using GroceryCo.DTO;
using GroceryCo.ConsoleApp.Utilities;

namespace GroceryCo.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the GroceryCo Shopping App:");

            ShowOptions();

            string selection = Console.ReadLine().ToUpper();

            while(!selection.Equals("Q"))
            {
                switch (selection)
                {
                    case "L":
                        IProductReport productReport = new ProductReport();
                        productReport.DisplayProducts();
                        break;

                    case "C":
                        IShoppingCartHandler shoppingCartHandler = new ShoppingCartHandler();
                        shoppingCartHandler.CheckoutShoppingCart();
                        break;

                    case "Q":
                        break;

                    default:
                        Console.WriteLine("Invalid option selected");
                        break;

                }

                ShowOptions();

                selection = Console.ReadLine().ToUpper();
            }
        }

        static void ShowOptions()
        {
            Console.WriteLine("");
            Console.WriteLine("Enter 'L' to list all products");
            Console.WriteLine("Enter 'C' to process a shopping cart");
            Console.WriteLine("Enter 'Q' to exit");
        }


    }
}
