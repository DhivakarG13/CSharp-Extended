using BoilerApp.Utilities;
using Constants;

namespace BoilerApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger("BoilerLog.csv");
            BoilerManager boilerManager = new(logger);
            Controller controller = new(boilerManager);
            bool closeAppFlag = false;
            while (!closeAppFlag)
            {
                try
                {
                    IOHandlerUtility.ActionTitleWriter("Boiler App");
                    IOHandlerUtility.DisplayDialog(new MainMenuOptions());
                    MainMenuOptions mainMenuChoice = (MainMenuOptions)IOHandlerUtility.GetUserChoice(Enum.GetNames(typeof(MainMenuOptions)).Length);
                    closeAppFlag = controller.Run(mainMenuChoice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred");
                    Console.WriteLine($"Error Message : {ex.Message}");
                    Console.WriteLine($"Resetting your Boiler");
                    controller.Run(MainMenuOptions.Reset_Lockout);
                }
            }
        }
    }
}
