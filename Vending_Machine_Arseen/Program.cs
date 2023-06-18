using System;
using System.Runtime.Intrinsics.Arm;

namespace Vending_Machine_Arseen
{
    public class Program
    {
        static public List<string> menu = new List<string>(); //This fields are used for the entire program, they are outside of main so can reach them everywhere.
        static public List<string> drinks = new List<string>();
        static public List<int> stock = new List<int>();
        static public List<double> prices = new List<double>();
        static public List<string> recommended = new List<string>();
        static public List<double> recommendedPrices = new List<double>();
        static public List<int> recommendedStock = new List<int>();
        private static double customerMoney = 0;

        static void Main(string[] args)
        {
            InitDrinkList(); 
            InitRecommendedDrinkList();
            InitMenu();
            int input;
            do
            {
                Console.Clear();
                PrintMenu();
                input = (int)UserInput("Please choose an option: "); // letting user to choose what they want to see from the menu

                switch (input)
                {
                    case 1: 
                        Console.Clear();
                        PrintDrinks();
                        input = (int)UserInput("Press 0 to go back to main menu: ");
                        break;
                    case 2:
                        Console.Clear();
                        double deposit = UserInput("Please deposit money: ");
                        Deposit(deposit);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine($"You current credit: {customerMoney} SEK");
                        PrintDrinks();
                        do
                        {
                            input = (int)UserInput("Choose a drink from the list above: ") - 1;
                        } while ((input + 1) < 1 || (input + 1) > drinks.Count);
                        int amount = 0;
                        do
                        {
                            amount = (int)UserInput($"How many {drinks[input]}s would you like to purchase?: ");
                        } while (amount < 1);
                        Purchase(input, amount);
                        input = (int)UserInput("Press 0 to go back to main menu: ");
                        break;
                    case 4:
                        Console.Clear();
                        PrintRecommendedDrinks();
                        input = (int)UserInput("Press 0 to go back to main menu: ");
                        break;
                    default:
                        return;
                }
            } while (input != 5);
        }
        static void InitDrinkList() // Adding items to my list of drinks and initializing this method above my switch case
        {
            drinks.Add($"Cola");
            stock.Add(5);
            prices.Add(12.99);

            drinks.Add($"Fanta");
            stock.Add(5);
            prices.Add(12.99);

            drinks.Add($"Nocco");
            stock.Add(5);
            prices.Add(22.99);

            drinks.Add($"Beer");
            stock.Add(5);
            prices.Add(17.99);

            drinks.Add($"Vodka");
            stock.Add(5);
            prices.Add(120.00);

        }
        static void InitRecommendedDrinkList() // Adding items to my list of recommended drinks and initializing this method above my switch case
        {
            recommended.Add($"Cola");
            recommendedStock.Add(5);
            recommendedPrices.Add(12.99);

            recommended.Add($"Fanta");
            recommendedStock.Add(5);
            recommendedPrices.Add(12.99);

            recommended.Add($"Nocco");
            recommendedStock.Add(5);
            recommendedPrices.Add(12.99);

            recommended.Add($"Redbull");
            recommendedStock.Add(5);
            recommendedPrices.Add(12.99);

            recommended.Add($"Vitamin Well");
            recommendedStock.Add(5);
            recommendedPrices.Add(12.99);
        }
        static void InitMenu() // Here i am adding options to my list of menu
        {
            menu.Add($"1. Display vending machine items");
            menu.Add($"2. Insert Money");
            menu.Add($"3. Purchase drinks");
            menu.Add($"4. Recommended drink");
            menu.Add($"5. Exit");
        }
        static void PrintMenu() /*Here i am looping through my list of menu and getting all items which i then pass into a do while loop,
                                outside of the switch case so you will be able to return to it in every case. */
        {
            Console.WriteLine($"You current credit: {customerMoney} SEK");
            foreach (var row in menu)
            {
                Console.WriteLine(row);
            }
        }
        static void PrintRecommendedDrinks() // printing every drink in my recommended drinks list and passing this method to my switch case                                                       
        {
            Console.WriteLine($"  Brand\t\t\tStock\tPrice");
            for (int i = 0; i < recommended.Count; i++) // doing a for loop here so if i is less then drinks it will +1 on drinks.
            {
                if (recommended[i].Length > 6)
                {
                    Console.WriteLine($"{i + 1} {recommended[i]}\t\t{recommendedStock[i]}\t{recommendedPrices[i]}");
                }
                else
                {
                    Console.WriteLine($"{i + 1} {recommended[i]}\t\t\t{recommendedStock[i]}\t{recommendedPrices[i]}");
                }
            }
        }
        static void PrintDrinks() // printing every drink in my drinks list which will be used in a switch case in 2 places, so you can see which items there is in the vending machine,
                                  // and also but drinks from the list.
        {
            Console.WriteLine($"  Brand\t\t\tStock\tPrice"); 
            for (int i = 0; i < drinks.Count; i++) // doing a for loop here so if i is less then drinks it will +1 on drinks.
            {
                if (drinks[i].Length > 6)
                {
                    Console.WriteLine($"{i + 1} {drinks[i]}\t\t{stock[i]}\t{prices[i]}");
                }
                else
                {
                    Console.WriteLine($"{i + 1} {drinks[i]}\t\t\t{stock[i]}\t{prices[i]}");
                }                
            }
        }
        static double UserInput(string msg) // this method is for userinput
        {
            while (true)
            {
                try
                {
                    Console.Write(msg);
                    string userInput = Console.ReadLine();
                    userInput = userInput.Replace(".", ","); // Replacing . with , when you give double values
                    return double.Parse(userInput);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
        static void Purchase(int itemId, int amount) // here we have the logic for the Purchase.
        {
            if (customerMoney < prices[itemId] * amount)
            {
                Console.WriteLine($"Insufficient funds, please deposit more to purchse {amount} {drinks[itemId]}"); /* Here the user needs to have right amount of money,
                                                                                                                        To be able to purchase the drink.   */

            }
            else if (stock[itemId] < amount)
            {
                Console.WriteLine($"{drinks[itemId]} is not in stock in the quantity you requested..."); // if user wants to take more drinks than we have in the stock it wont let him do it.
            }
            else
            {
                customerMoney -= amount * prices[itemId];
                stock[itemId] -= amount;
                Console.WriteLine($"Enjoy your {drinks[itemId]}, you now have {Math.Round(customerMoney, 2)} SEK left to purchase with."); /* After you purchased your drink successfully you will get a message 
                                                                                                                                            of which drink you purchased and how much money left you have to purchase with*/
            }
        }
        static void Deposit(double amount) /* This is the logic, you will be able to insert as much money as you want into the vending machine,
                                            the amount of money you insert to the machine will be stored in our field which is called customersmoney,from the field variable you will be able to see 
                                           your amount of money in the entire program. */
        {
            while (amount < prices.Min())
            {
                amount = UserInput($"Insufficient amount, please deposit at least {prices.Min()}: ");
            }
            customerMoney += amount;
        }
    }
}