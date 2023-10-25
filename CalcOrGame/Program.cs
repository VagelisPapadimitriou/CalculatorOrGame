using System;
using System.Linq;
using System.Threading;

namespace CalcOrGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string name;
            int age;
            string selection;
            string mathOperation;
            double mathResult;
            int randomNumber;
            int userGameNumber;
            string gameResult;

            //Greetings
            ShowGreetings();
            Thread.Sleep(250);

            //User Data
            name = GetUserName();
            age = GetUserAge();

            //Selection
            selection = GetCalcOrGameChoice(age);

            //Execute Selection
            if (selection == "1")
            {
                //Calculator
                mathOperation = GetΟperationChoice();
                mathResult = CalculateNumbers(mathOperation);
                //Final
                PrintFinalResultCalc(name, age, mathOperation, mathResult);
            }
            else if (selection == "2")
            {
                //Game
                randomNumber = CreateRandomNumber();
                userGameNumber = GetGameNumber();
                gameResult = PrintGameResult(userGameNumber, randomNumber);
                //Final
                PrintFinalResultGame(name, age, gameResult);
            };

        }

        //Greetings
        static void ShowGreetings()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Welcome user!!!");
            Console.ResetColor();
        }

        //User Data
        static void ShowMenuCreateUserName()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Give me your name: ");
            Console.ResetColor();
        }

        static string GetUserName()
        {
            ShowMenuCreateUserName();
            Thread.Sleep(250);

            string input;
            input = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(input))
            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("You forgot your name: ");
                    Console.ResetColor();
                    input = Console.ReadLine();
                } while (String.IsNullOrWhiteSpace(input));
            }



            return input;
        }

        static void ShowMenuCreateUserAge()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Give me your age: ");
            Console.ResetColor();
        }

        static int GetUserAge()
        {
            ShowMenuCreateUserAge();
            Thread.Sleep(250);

            string input;
            input = Console.ReadLine();
            int age;

            do
            {
                if (String.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("You forgot your age: ");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    continue;
                }
                if (!Int32.TryParse(input, out age))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Give me your age: ");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    continue;
                }
                if (Convert.ToInt32(input) < 0 || Convert.ToInt32(input) > 100)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Give me your age: ");
                    Console.ResetColor();
                    input = Console.ReadLine();
                    continue;
                }
            } while (!Int32.TryParse(input, out age) || String.IsNullOrWhiteSpace(input) || Convert.ToInt32(input) < 0 || Convert.ToInt32(input) > 100);

            age = Convert.ToInt32(input);

            return age;
        }

        //Selection
        static void ShowMenuCalcOrGameChoice()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Choose one of the following...");
            Console.WriteLine($"{"1 - Use my calculator",-25}" + "(Free for All...)");
            Console.WriteLine($"{"2 - Play a game",-25}" + "(You must be over 18 years old to enter...)");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("And your choice is: ");
            Console.ResetColor();
        }

        static string GetCalcOrGameChoice(int age)
        {
            ShowMenuCalcOrGameChoice();
            Thread.Sleep(250);

            string input;
            input = Console.ReadLine();

            if (input != "1" && input != "2")
            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Your choice was not one of the options. Choose again: ");
                    Console.ResetColor();
                    input = Console.ReadLine();
                } while (input != "1" && input != "2");
            }
            if (input == "2" && age < 18)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Sorry, you do not meet the requirements");
                Console.WriteLine("You can only use the Calculator... Here you go friend!");
                Console.ResetColor();
                return "1";
            }

            return input;
        }


        //Calculator
        static double CalculateNumbers(string choice)       //calculator manager
        {
            double result;

            if (!(choice == "/"))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Give me the numbers to execute your math operation.");
                Console.Write("I want you to give them to me seperated: ");
                Console.ResetColor();
                double[] p = GetNumbersForOtherOperations();
                result = CalculateOtherOperations(choice, p);
                PrintOtherOperationsResult(choice, result, p);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Give me two numbers for division. Remember, you cannot divide by zero.");
                Console.ResetColor();
                double[] divisionNumbers = GetNumbersForDivision();
                result = CalculateDivision(divisionNumbers);
                PrintDivisionResult(divisionNumbers, result);
            }
            return result;
        }

        static void ShowCalculatorGreetings()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is the best Calculator in the world!!!");
            Console.ResetColor();
        }

        static void ShowMenuΟperationChoice()
        {
            ShowCalculatorGreetings();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("-> Choose one of the operations <-");
            Console.WriteLine($"{"1 - Addition",-25}" + "-> + <-");
            Console.WriteLine($"{"2 - Subtraction",-25}" + "-> - <-");
            Console.WriteLine($"{"3 - Multiplication",-25}" + "-> * <-");
            Console.WriteLine($"{"4 - Division",-25}" + "-> / <-");
            Console.ResetColor();
        }

        static string GetΟperationChoice()
        {
            ShowMenuΟperationChoice();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Number of operation: ");
            Console.ResetColor();
            string choice;
            string input = Console.ReadLine();
            switch (input)
            {
                case "1": choice = "+"; break;
                case "2": choice = "-"; break;
                case "3": choice = "*"; break;
                case "4": choice = "/"; break;
                default: choice = "Wrong"; break;
            }

            if (choice == "Wrong")
            {
                do
                {
                    Console.WriteLine("Wrong choice, the operation does not exists.");
                    Console.Write("Choose again: ");
                    string input2 = Console.ReadLine();
                    switch (input2)
                    {
                        case "1": choice = "+"; break;
                        case "2": choice = "-"; break;
                        case "3": choice = "*"; break;
                        case "4": choice = "/"; break;
                        default: choice = "Wrong"; break;
                    }
                } while (choice == "Wrong");
            }
            return choice;
        }

        static double[] GetNumbersForDivision()
        {
            double[] division = new double[2];

            string dividendInput;
            do
            {
                Console.Write("Enter a Dividend: ");
                dividendInput = Console.ReadLine();

            } while (!dividendInput.All(x => char.IsNumber(x)) || String.IsNullOrEmpty(dividendInput));

            double dividend = Convert.ToDouble(dividendInput);
            division[0] = dividend;



            string divisorInput;
            double divisor;
            do
            {
                Console.Write("Enter a Divisor: ");
                divisorInput = Console.ReadLine();
                if (divisorInput == "0")
                {
                    Console.Write("Remember, you cannot divide by zero. Enter a non-zero Divisor: ");
                }
            } while (divisorInput == "0" || !divisorInput.All(x => char.IsNumber(x)) || String.IsNullOrEmpty(divisorInput));

            divisor = Convert.ToDouble(divisorInput);
            division[1] = divisor;


            return division;
        }

        static double CalculateDivision(double[] p)
        {
            return p[0] / p[1];
        }

        static void PrintDivisionResult(double[] div, double res)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"The result of your division {div[0]} / {div[1]} = {res}");
            Console.ResetColor();
        }

        static double[] GetNumbersForOtherOperations()
        {
            string input;

            do
            {
                input = Console.ReadLine();
                if (!AllowMoreThanTwoNumbers(input))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Try Again: ");
                    Console.ResetColor();
                }
            } while (!AllowMoreThanTwoNumbers(input));


            string[] stringNumbers = input.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            double[] numbers = new double[stringNumbers.Length];

            for (int i = 0; i < stringNumbers.Length; i++)
            {
                numbers[i] = Convert.ToDouble(stringNumbers[i]);
            }
            return numbers;
        }

        static bool AllowMoreThanTwoNumbers(string input)
        {
            if (!input.All(x => char.IsNumber(x) || x == ' '))
            {
                return false;
            }
            var arrayWithoutSpaces = input.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x));
            return arrayWithoutSpaces.Count() > 1;
        }

        static double CalculateOtherOperations(string mathOper, params double[] p)
        {
            double result = 0;

            if (mathOper == "+")
            {
                foreach (var item in p)
                {
                    result += item;
                }
            }
            if (mathOper == "-")
            {
                foreach (var item in p)
                {
                    result -= item;
                }
            }
            if (mathOper == "*")
            {
                result = 1;
                foreach (var item in p)
                {
                    result *= item;
                }
            }
            return result;
        }

        static void PrintOtherOperationsResult(string mathOper, double result, params double[] p)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("The result of your operation is: ");
            for (int i = 0; i < p.Length - 1; i++)
            {
                Console.Write(p[i] + $" {mathOper} ");
            }
            Console.WriteLine($"{p[p.Length - 1]}" + " = " + $"{result}");
            Console.ResetColor();
        }

        //Game
        static void ShowGameGreetings()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Let's see how lucky you are today!!");
            Console.ResetColor();
        }

        static void ShowGameMenuNumberChoice()
        {
            ShowGameGreetings();
            Thread.Sleep(500);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Try to guess the number your opponent will choose. ");
            Thread.Sleep(500);

            Console.ResetColor();
        }

        static int GetGameNumber()
        {

            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Select a number between 1 and 4: ");
            Console.ResetColor();
            string input;
            do
            {
                input = Console.ReadLine();
                if (!input.All(x => char.IsNumber(x)) || String.IsNullOrEmpty(input))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Try Again: ");
                    Console.ResetColor();
                }
            } while (!input.All(x => char.IsNumber(x)) || String.IsNullOrWhiteSpace(input));
            int number = Convert.ToInt32(input);

            return number;

        }

        static int CreateRandomNumber()
        {
            ShowGameMenuNumberChoice();

            Thread.Sleep(500);
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 5);
            Console.Write("Your opponent is thinking...");
            Thread.Sleep(1500);
            Console.WriteLine("and chooses the number: **");

            return randomNumber;
        }

        static string PrintGameResult(int userGameNumber, int randomNumber)
        {
            ShowChoiceOfGameNumbers(userGameNumber, randomNumber);
            Thread.Sleep(1250);


            if (userGameNumber == randomNumber)
            {
                Console.WriteLine("Congratulations!! You won...!!!");
                return "won";
            }
            else
            {
                Console.WriteLine("You were so close... Try again next time!!");
                return "lost";
            }

        }

        static void ShowChoiceOfGameNumbers(int userGameNumber, int randomNumber)
        {
            Thread.Sleep(250);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Your choice was: {userGameNumber}");
            Console.ResetColor();
            Thread.Sleep(250);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Your opponent chose: {randomNumber}");
            Console.ResetColor();
        }

        //Final
        static void PrintFinalResultSharesLogs(string name, int age)
        {
            Thread.Sleep(500);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < 6; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }

            Console.WriteLine("Printing Results:");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("---- User Data ----");
            Console.ResetColor();
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Name: {name} - Age: {age}");
            Console.ResetColor();
            Thread.Sleep(1000);
        }

        static void PrintFinalResultCalc(string name, int age, string mathOperation, double mathResult)
        {
            PrintFinalResultSharesLogs(name, age);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("");

            Console.WriteLine($"User used the calculator for the operation {mathOperation} and the result was: {mathResult}");

            Console.WriteLine();
            Console.ResetColor();
        }

        static void PrintFinalResultGame(string name, int age, string gameResult)
        {
            PrintFinalResultSharesLogs(name, age);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();

            Console.WriteLine($"User played the game and {gameResult}!!!");

            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
