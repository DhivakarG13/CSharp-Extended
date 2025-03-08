using System;

namespace BoilerApp.Utilities
{
    public static class IOHandlerUtility
    {
        public static void DisplayDialog(Enum dialogToPrint)
        {
            int optionIndex = 1;
            Console.WriteLine("---");
            foreach(string option in Enum.GetNames(dialogToPrint.GetType()))
            {
                Console.WriteLine($"[{optionIndex++}] {option}");
            }
            Console.WriteLine("---\n");
        }

        public static int GetUserChoice(int totalChoices)
        {
            string? userChoice = string.Empty;
            bool isValidUserChoice = false;
            int parsedUserChoice;
            while(!isValidUserChoice)
            {
                Console.Write("Enter your choice (Number): ");
                userChoice = Console.ReadLine();
                isValidUserChoice = ValidationUtility.ValidateUserChoice(userChoice, totalChoices);
            }
            int.TryParse(userChoice, out parsedUserChoice);
            return parsedUserChoice;
        }

        public static void PrintActionSuccess(string action)
        {
            Console.WriteLine("\n\n ---------");
            Console.WriteLine($" {action} SUCCESSFUL");
            Console.WriteLine(" Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        public static void PrintActionFailure(string action)
        {
            Console.WriteLine("\n\n ---------");
            Console.WriteLine($" {action} FAILED");
            Console.WriteLine(" Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        public static void StateChangeNotifier(string stateChangeMessage)
        {
            Console.WriteLine("\n\n"+stateChangeMessage+"\n");
        }

        public static void ActionTitleWriter(string actionTitle)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("---------------");
            Console.WriteLine($"- {actionTitle} -");
            Console.WriteLine("---------------");
            Console.ResetColor();
        }
    }
}
