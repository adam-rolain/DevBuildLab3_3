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
            decimal price = 0.00m;
            int quantity = 0;
            bool validPrice = false;
            bool validQuantity = false;

            while (alreadyExists)
            {
                Console.Write("What is the name of the new menu item you would like to add? Enter name of menu item or type 'CANCEL' to go back to the main menu: ");
                name = Console.ReadLine();
                if (!existingMenu.ContainsKey(name.ToUpper()) || name.ToUpper() == "CANCEL")
                {
                    alreadyExists = false;
                }
                else
                {
                    Console.WriteLine("That item already exists on the menu, try again!\n");
                }
            }

            if (name.ToUpper() == "CANCEL")
            {
                Console.WriteLine("Heading back to the main menu...");
                Thread.Sleep(1200);
                return;
            }

            Console.Write("What is the price? ");
            while (!validPrice)
            {
                validPrice = IsValidDecimal(Console.ReadLine(), 0.01m, 100.00m, out price);
            }
            

            Console.Write("How many will be available to sell? ");
            while (!validQuantity)
            {
                validQuantity = IsValidInteger(Console.ReadLine(), 1, 100, true, out quantity);
            }

            existingMenu[name.ToUpper()] = new MenuItem(name, price, quantity);
            Console.WriteLine($"{existingMenu[name.ToUpper()].Name} has been added to the menu");
            Thread.Sleep(1500);
        }

        static void RemoveMenuItem(Dictionary<string, MenuItem> existingMenu)
        {
            string name = "";
            string currentMenuName = "";
            bool itemExists = false;      
            while (!itemExists)
            {
                Console.Write("What is the name of the menu item you would like to remove? Enter name of menu item or type 'CANCEL' to go back to the main menu: ");
                name = Console.ReadLine();

                if (existingMenu.ContainsKey(name.ToUpper()))
                {
                    currentMenuName = existingMenu[name.ToUpper()].Name;
                    existingMenu.Remove(name.ToUpper());
                    itemExists = true;
                }
                else if (name.ToUpper() == "CANCEL")
                {
                    itemExists = true;
                }
                else
                {
                    Console.WriteLine("That item does not exists on the menu, try again!\n");
                }
            }

            if (name.ToUpper() == "CANCEL")
            {
                Console.WriteLine("Heading back to the main menu...");
                Thread.Sleep(1200);
                return;
            }

            Console.WriteLine($"{currentMenuName} has been removed from the menu");
            Thread.Sleep(1500);
        }

        static void ChangeMenuItem(Dictionary<string, MenuItem> existingMenu)
        {
            string name = "";
            bool itemExists = false;
            string currentMenuName = "";

            while (!itemExists)
            {
                Console.Write("What is the name of the menu item you would like to change? Enter name of menu item or type 'CANCEL' to go back to the main menu: ");
                name = Console.ReadLine();

                if (existingMenu.ContainsKey(name.ToUpper()))
                {
                    currentMenuName = existingMenu[name.ToUpper()].Name;
                    itemExists = true;
                }
                else if (name.ToUpper() == "CANCEL")
                {
                    itemExists = true;
                }
                else
                {
                    Console.WriteLine("That item does not exists on the menu, try again!\n");
                }
            }

            if (name.ToUpper() == "CANCEL")
            {
                Console.WriteLine("Heading back to the main menu...");
                Thread.Sleep(1200);
                return;
            }

            string selection;
            Console.Write($"Would you like to edit the name or price of {currentMenuName}? Type 'name' or 'price': ");
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
                    decimal newPrice = 0.00m;
                    bool validPrice = false;
                    Console.Write("What is the new price you would like to give? ");
                    while (!validPrice)
                    {
                        validPrice = IsValidDecimal(Console.ReadLine(), 0.01m, 100.00m, out newPrice);
                    }
                    existingMenu[name.ToUpper()].Price = newPrice;
                    Console.WriteLine($"{currentMenuName}'s price has been updated on the menu");
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
                Console.Write("What is the name of the menu item you sold? Enter name of menu item or type 'CANCEL' to go back to the main menu: ");
                name = Console.ReadLine();

                if (existingMenu.ContainsKey(name.ToUpper()))
                {
                    int quantitySold = 0;
                    bool validQuantity = false;
                    while (!validQuantity)
                    {
                        Console.Write($"\nHow many {existingMenu[name.ToUpper()].Name}s did you sell? ");
                        validQuantity = IsValidInteger(Console.ReadLine(), 1, existingMenu[name.ToUpper()].Quantity, false, out quantitySold);
                    }
                    existingMenu[name.ToUpper()].sell(quantitySold);
                    itemExists = true;
                }
                else if (name.ToUpper() == "CANCEL")
                {
                    itemExists = true;
                }
                else
                {
                    Console.WriteLine("\nThat item does not exists on the menu, try again!\n");
                }
            }

            if (name.ToUpper() == "CANCEL")
            {
                Console.WriteLine("Heading back to the main menu...");
                Thread.Sleep(1200);
                return;
            }

            Console.WriteLine($"\nThe quantity of {existingMenu[name.ToUpper()].Name}s has been updated on the menu");
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

        static bool IsValidInteger(string userInput, int min, int max, bool addingNewQuantity, out int parsedInput)
        {
            while (true)
            {
                // Using TryParse to attempt to get an integer from the string provided by the user.
                bool inputIsNumber = Int32.TryParse(userInput, out parsedInput);

                // If TryParse returns true and the parsed integer is greater than the minimum and less than or equal to max, then return true. If it is greater than or equal the maximum, then return false and direct the user to pick a smaller integer.
                if (inputIsNumber && parsedInput >= min)
                {
                    if (parsedInput > max)
                    {
                        if (!addingNewQuantity)
                        {
                            Console.Write($"\nThat is not a valid amount. Please input a quantity no larger than {max}, since that is all we have left! ");
                        }
                        else
                        {
                            Console.Write($"\nThat is not a valid amount. Please input a quantity no larger than {max}, since we can't hold any more inventory than that! ");
                        }
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                // If TryParse returns false, then tell the user to input a whole number.
                else if (!inputIsNumber)
                {
                    Console.Write($"Whoops, your input needs to be a whole number between {min} and {max}! Try again: ");
                    return false;
                }
                // If the parsed integer is less than 1, then direct the user to pick a number greater than 0.
                else if (parsedInput < min)
                {
                    Console.Write($"Whoops, your input needs to be at least {min}! Try again: ");
                    parsedInput = 0;
                    return false;
                }
                // Otherwise, return false.
                else
                {
                    return false;
                }
            }
        }

        static bool IsValidDecimal(string userInput, decimal min, decimal max, out decimal parsedInput)
        {
            while (true)
            {
                // Using TryParse to attempt to get an integer from the string provided by the user.
                bool inputIsNumber = Decimal.TryParse(userInput, out parsedInput);

                // If TryParse returns true and the parsed integer is greater than the minimum and less than or equal to max, then return true. If it is greater than or equal the maximum, then return false and direct the user to pick a smaller integer.
                if (inputIsNumber && parsedInput >= min)
                {
                    if (parsedInput > max)
                    {
                        Console.Write($"That is not a valid amount. Please input a quantity no larger than {max} because we should not be charging more than that for anything on our menu! Try again: ");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                // If TryParse returns false, then tell the user to input a whole number.
                else if (!inputIsNumber)
                {
                    Console.Write($"Whoops, your input needs to be a dollar amount between {min} and {max}! Try again: ");
                    return false;
                }
                // If the parsed integer is less than 1, then direct the user to pick a number greater than 0.
                else if (parsedInput < min)
                {
                    Console.Write($"Whoops, your input needs to be at least {min}! Try again: ");
                    parsedInput = 0;
                    return false;
                }
                // Otherwise, return false.
                else
                {
                    return false;
                }
            }
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
