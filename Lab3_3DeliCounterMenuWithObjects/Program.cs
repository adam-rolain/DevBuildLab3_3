using System;
using System.Collections.Generic;

namespace Lab3_3DeliCounterMenuWithObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, decimal> menuItems = new Dictionary<string, decimal>();
            menuItems["Cuban"] = 12.35m;
            menuItems["Club"] = 14.75m;
            menuItems["Californian Club"] = 16.99m;
            menuItems["Reuben"] = 15.99m;
            string userInput = "";
            bool endProgram = false;

            while (!endProgram)
            {
                Console.WriteLine("Welcome to the Deli Counter. Here are our menu items: ");
                DisplayMenu(menuItems);

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

        static void DisplayMenu(Dictionary<string, decimal> existingMenu)
        {
            Console.WriteLine("\nDeli Counter Menu");
            foreach (var item in existingMenu)
            {
                Console.WriteLine($"{item.Key}...........${item.Value}");
            }
        }

        static void AddMenuItem(Dictionary<string, decimal> existingMenu)
        {
            bool alreadyExists = true;
            string name = "";
            decimal price;
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

            existingMenu[name] = price;
        }

        static void RemoveMenuItem(Dictionary<string, decimal> existingMenu)
        {
            string name;
            Console.Write("What is the name of the new menu item you would like to remove? ");
            name = Console.ReadLine();
            existingMenu.Remove(name);
            Console.WriteLine($"{name} has been removed from the menu");
        }

        static void RemoveMenuItem(Dictionary<string, decimal> existingMenu, string item)
        {
            existingMenu.Remove(item);
        }

        static void ChangeMenuItem(Dictionary<string, decimal> existingMenu)
        {
            string name;
            Console.Write("What is the name of the new menu item you would like to change? ");
            name = Console.ReadLine();

            string selection;
            Console.Write($"Would you like to edit the name or price of {name}? Type 'name' or 'price': ");
            selection = Console.ReadLine().ToUpper();
            switch (selection)
            {
                case "NAME":
                    decimal price = existingMenu[name];
                    RemoveMenuItem(existingMenu, name);
                    NameChange(existingMenu, price);
                    break;
                case "PRICE":
                    decimal newPrice;
                    Console.Write("What is the new price you would like to give? ");
                    newPrice = decimal.Parse(Console.ReadLine());
                    existingMenu[name] = newPrice;
                    break;
                default:
                    Console.WriteLine("Not valid input");
                    break;
            }
        }

        static void NameChange(Dictionary<string, decimal> existingMenu, decimal existingPrice)
        {
            string name;
            Console.Write("What is the new name you would like to give? ");
            name = Console.ReadLine();

            existingMenu[name] = existingPrice;
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
}
