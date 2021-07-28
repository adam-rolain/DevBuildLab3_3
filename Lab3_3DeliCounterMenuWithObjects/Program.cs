using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab3_3DeliCounterMenuWithObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, MenuItem> menuItems = new Dictionary<string, MenuItem>();
            menuItems["CUBAN"] = new MenuItem("Cuban", 12.35m, 25);
            menuItems["CLUB"] = new MenuItem("Club", 14.75m, 20);
            menuItems["CALIFORNIA CLUB"] = new MenuItem("California Club", 16.99m, 15);
            menuItems["REUBEN"] = new MenuItem("Reuben", 15.99m, 30);
            string userInput = "";
            bool endProgram = false;

            Console.WriteLine("Welcome to the Deli Counter. Here are our menu items: ");
            DisplayMenu(menuItems);

            while (!endProgram)
            {
                bool validInput = false;
                while (!validInput)
                {
                    Console.WriteLine("\nPlease choose from the following options:");
                    Console.WriteLine("Type 'A' to add a new item to the menu");
                    Console.WriteLine("Type 'R' to remove an item from the menu");
                    Console.WriteLine("Type 'C' to change an item on the menu");
                    Console.WriteLine("Type 'S' to sell an item on the menu");
                    Console.WriteLine("Type 'Q' to quit\n");
                    userInput = Console.ReadLine().ToUpper();
                    List<string> validInputOptions = new List<string> { "A", "R", "C", "S", "Q" };
                    validInput = IsValidString(userInput, validInputOptions);
                }

                switch (userInput)
                {
                    case "A":
                        AddMenuItem(menuItems);
                        DisplayMenu(menuItems);
                        break;
                    case "R":
                        RemoveMenuItem(menuItems);
                        DisplayMenu(menuItems);
                        break;
                    case "C":
                        ChangeMenuItem(menuItems);
                        DisplayMenu(menuItems);
                        break;
                    case "S":
                        SellMenuItem(menuItems);
                        DisplayMenu(menuItems);
                        break;
                    case "Q":
                        endProgram = true;
                        break;
                    default:
                        Console.WriteLine("Not valid input");
                        break;
                }
            }
        }

        static void DisplayMenu(Dictionary<string, MenuItem> existingMenu)
        {
            Console.WriteLine("\nDeli Counter Menu");
            foreach (var item in existingMenu)
            {
                Console.WriteLine($"{item.Value.Name} ${item.Value.Price} ({item.Value.Quantity} remaining)");
            }
        }

        static void AddMenuItem(Dictionary<string, MenuItem> existingMenu)
        {
            bool alreadyExists = true;
            string name = "";
            decimal price;
            int quantity;
            while (alreadyExists)
            {
                Console.Write("What is the name of the new menu item you would like to add? ");
                name = Console.ReadLine();
                if (!existingMenu.ContainsKey(name.ToUpper()))
                {
                    alreadyExists = false;
                }
                else
                {
                    Console.WriteLine("That item already exists on the menu, try again!\n");
                }
            }
            Console.Write("What is the price? ");
            price = decimal.Parse(Console.ReadLine());

            Console.Write("How many will be available to sell? ");
            quantity = Int32.Parse(Console.ReadLine());

            existingMenu[name.ToUpper()] = new MenuItem(name, price, quantity);
            Console.WriteLine($"{name} has been added to the menu");
            Thread.Sleep(1500);
        }

        static void RemoveMenuItem(Dictionary<string, MenuItem> existingMenu)
        {
            string name = "";
            bool itemExists = false;      
            while (!itemExists)
            {
                Console.Write("What is the name of the menu item you would like to remove? ");
                name = Console.ReadLine();

                if (existingMenu.ContainsKey(name.ToUpper()))
                {
                    existingMenu.Remove(name.ToUpper());
                    itemExists = true;
                }
                else
                {
                    Console.WriteLine("That item does not exists on the menu, try again!\n");
                }
            }
            Console.WriteLine($"{name} has been removed from the menu");
            Thread.Sleep(1500);
        }

        static void ChangeMenuItem(Dictionary<string, MenuItem> existingMenu)
        {
            string name;
            Console.Write("What is the name of the menu item you would like to change? ");
            name = Console.ReadLine();

            string selection;
            Console.Write($"Would you like to edit the name or price of {name}? Type 'name' or 'price': ");
            selection = Console.ReadLine().ToUpper();

            switch (selection)
            {
                case "NAME":
                    decimal price = existingMenu[name.ToUpper()].Price;
                    int quantity = existingMenu[name.ToUpper()].Quantity;
                    existingMenu.Remove(name.ToUpper());
                    NameChange(existingMenu, price, quantity);
                    break;
                case "PRICE":
                    decimal newPrice;
                    Console.Write("What is the new price you would like to give? ");
                    newPrice = decimal.Parse(Console.ReadLine());
                    existingMenu[name.ToUpper()].Price = newPrice;
                    Console.WriteLine($"{name}'s price has been updated on the menu");
                    Thread.Sleep(1500);
                    break;
                default:
                    Console.WriteLine("Not valid input");
                    break;
            }
        }

        static void NameChange(Dictionary<string, MenuItem> existingMenu, decimal existingPrice, int existingQuantity)
        {
            string name;
            Console.Write("What is the new name you would like to give? ");
            name = Console.ReadLine();

            existingMenu[name.ToUpper()] = new MenuItem(name, existingPrice, existingQuantity);

            Console.WriteLine($"{name} has been updated on the menu");
            Thread.Sleep(1500);
        }

        static void SellMenuItem(Dictionary<string, MenuItem> existingMenu)
        {
            string name = "";
            bool itemExists = false;
            while (!itemExists)
            {
                Console.Write("What is the name of the menu item you sold? ");
                name = Console.ReadLine();

                if (existingMenu.ContainsKey(name.ToUpper()))
                {
                    int quantitySold = 0;
                    bool validQuantity = false;
                    while (!validQuantity)
                    {
                        Console.Write($"\nHow many {name}s did you sell? ");
                        Int32.TryParse(Console.ReadLine(), out quantitySold);
                        if (quantitySold <= existingMenu[name.ToUpper()].Quantity && quantitySold > 0)
                        {
                            validQuantity = true;
                        }
                        else
                        {
                            Console.WriteLine($"\nThat is not a valid amount. Please input a quantity no larger than {existingMenu[name.ToUpper()].Quantity}, since that is all we have left!");
                        }
                    }
                    existingMenu[name.ToUpper()].sell(quantitySold);
                    itemExists = true;
                }
                else
                {
                    Console.WriteLine("\nThat item does not exists on the menu, try again!\n");
                }
            }
            Console.WriteLine($"\nThe quantity of {name}s has been updated on the menu");
            Thread.Sleep(1500);
        }

        static bool IsValidString(string userInput, List<string> validInputs)
        {
            bool validString = false;
            while (!validString)
            {
                validString = false;
                foreach (string validInput in validInputs)
                {
                    if (userInput.ToUpper() == validInput.ToUpper())
                    {
                        validString = true;
                    }
                }
                if (validString == false)
                {
                    Console.WriteLine("\nThat not a valid input, try again!");
                    break;
                }
            }
            return validString;
        }
    }

    class MenuItem
    {
        public MenuItem(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public string Name;
        public decimal Price;
        public int Quantity;
        public void sell(int howMany)
        {
            Quantity -= howMany;
        }
    }
}
