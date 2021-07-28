using System;
using System.Collections.Generic;

namespace Lab3_3DeliCounterMenuWithObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, MenuItem> menuItems = new Dictionary<string, MenuItem>();
            menuItems["Cuban"] = new MenuItem();
            menuItems["Cuban"].name = "Cuban";
            menuItems["Cuban"].price = 12.35m;
            menuItems["Cuban"].quantity = 25;
            menuItems["Club"] = new MenuItem();
            menuItems["Club"].name = "Club";
            menuItems["Club"].price = 14.75m;
            menuItems["Club"].quantity = 20;
            menuItems["California Club"] = new MenuItem();
            menuItems["California Club"].name = "California Club";
            menuItems["California Club"].price = 16.99m;
            menuItems["California Club"].quantity = 15;
            menuItems["Reuben"] = new MenuItem();
            menuItems["Reuben"].name = "Reuben";
            menuItems["Reuben"].price = 15.99m;
            menuItems["Reuben"].quantity = 30;
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
                    Console.WriteLine("Type 'Q' to quit\n");
                    userInput = Console.ReadLine().ToUpper();
                    validInput = IsValidString(userInput, "A", "R", "C", "Q");
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
                Console.WriteLine($"{item.Value.name}...........${item.Value.price} ({item.Value.quantity} remaining)");
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
                if (!existingMenu.ContainsKey(name))
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

            existingMenu[name] = new MenuItem();
            existingMenu[name].name = name;
            existingMenu[name].price = price;
            existingMenu[name].quantity = quantity;
        }

        static void RemoveMenuItem(Dictionary<string, MenuItem> existingMenu)
        {
            string name = "";
            bool itemExists = false;      
            while (!itemExists)
            {
                Console.Write("What is the name of the menu item you would like to remove? ");
                name = Console.ReadLine();

                if (existingMenu.ContainsKey(name))
                {
                    existingMenu.Remove(name);
                    itemExists = true;
                }
                else
                {
                    Console.WriteLine("That item does not exists on the menu, try again!\n");
                }
            }
            Console.WriteLine($"{name} has been removed from the menu");
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
                    decimal price = existingMenu[name].price;
                    int quantity = existingMenu[name].quantity;
                    existingMenu.Remove(name);
                    NameChange(existingMenu, price, quantity);
                    break;
                case "PRICE":
                    decimal newPrice;
                    Console.Write("What is the new price you would like to give? ");
                    newPrice = decimal.Parse(Console.ReadLine());
                    existingMenu[name].price = newPrice;
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

            existingMenu[name] = new MenuItem();
            existingMenu[name].name = name;
            existingMenu[name].price = existingPrice;
            existingMenu[name].quantity = existingQuantity;

            Console.WriteLine($"{name} has been updated on the menu");
        }

        static bool IsValidString(string userInput, string validInput1, string validInput2, string validInput3, string validInput4)
        {
            bool validString = false;
            while (!validString)
            {
                validString = false;
                for (int i = 0; i < userInput.Length; i++)
                {
                    char letter = char.ToUpper(userInput[i]);
                    if (letter != char.ToUpper(validInput1[i]) && letter != char.ToUpper(validInput2[i]) && letter != char.ToUpper(validInput3[i]) && letter != char.ToUpper(validInput4[i]))
                    {
                        validString = false;
                        break;
                    }
                    else
                    {
                        validString = true;
                    }
                }
                if (validString == false)
                {
                    break;
                }
            }
            return validString;
        }
    }

    class MenuItem
    {
        public string name;
        public decimal price;
        public int quantity;
        public void sell(int howMany)
        {

        }
    }
}
