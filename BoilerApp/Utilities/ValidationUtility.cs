
namespace BoilerApp.Utilities
{
    public class ValidationUtility
    {
        public static bool ValidateUserChoice(string? userChoice, int totalChoices)
        {
            if (string.IsNullOrEmpty(userChoice))
            {
                Console.WriteLine("Choose any option to continue");
                return false;
            }
            int parsedUserChoice;
            if (!int.TryParse(userChoice, out parsedUserChoice))
            {
                Console.WriteLine("Invalid input, expected input [\"Number\"]");
                return false;
            }
            if (parsedUserChoice > 0 && parsedUserChoice <= totalChoices)
            {
                return true;
            }
            Console.WriteLine("Choice out of bound, choose within the options");
            return false;

        }
    }
}
